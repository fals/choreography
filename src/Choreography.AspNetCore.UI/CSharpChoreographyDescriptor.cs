using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Choreography.AspNetCore.UI
{
    public class CSharpChoreographyDescriptor : IChoreographyTypeDescriptor
    {
        private const string NullableSymbol = "?";
        private static ConcurrentDictionary<Type, ChoreographyTypeInfo> cachedTypes = new ConcurrentDictionary<Type, ChoreographyTypeInfo>();

        public ChoreographyTypeInfo GetTypeInfo(Type type)
        {
            StringBuilder typeDescription = new StringBuilder($"public class {type.Name}");
            ConcurrentDictionary<Type, ChoreographyTypeInfo> nestedTypes = new ConcurrentDictionary<Type, ChoreographyTypeInfo>();
            typeDescription.AppendLine("\n{");
            var properties = GetPropertiesDescriptions(type, nestedTypes);
            typeDescription.AppendLine(properties);
            typeDescription.AppendLine("}\n");

            foreach (var nestedType in nestedTypes.Values)
            {
                typeDescription.Append(nestedType.Object);
            }

            if (cachedTypes.TryGetValue(type, out var typeInfo))
            {
                return typeInfo;
            }

            typeInfo = new ChoreographyTypeInfo()
            {
                Name = type.Name,
                Object = typeDescription.ToString()
            };

            cachedTypes.TryAdd(type, typeInfo);

            return typeInfo;
        }

        private string GetPropertiesDescriptions(Type type, ConcurrentDictionary<Type, ChoreographyTypeInfo> nestedTypes)
        {
            var propertiesDescription = new StringBuilder();

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                bool isNullable = property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
                var propertyDescription = GetPropertyDescription(property.Name, property.PropertyType, isNullable, nestedTypes);
                propertiesDescription.AppendLine(propertyDescription);
            }

            return propertiesDescription.ToString();
        }

        private string GetPropertyDescription(string name, Type propertyType, bool isNullable, ConcurrentDictionary<Type, ChoreographyTypeInfo> nestedTypes)
        {
            string typeName = string.Empty;
            bool isCollection = propertyType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (!propertyType.IsEnum)
            {
                System.TypeCode typeCode;
                try
                {
                    typeCode = System.Type.GetTypeCode(propertyType);
                }
                catch
                {
                    typeCode = TypeCode.Object;
                }

                switch (typeCode)
                {
                    case TypeCode.String:
                        typeName = "string";
                        break;
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                        typeName = "int";
                        break;
                    case TypeCode.Boolean:
                        typeName = "bool";
                        break;
                    case TypeCode.DateTime:
                        typeName = "DateTime";
                        break;
                    case TypeCode.Object:
                        {
                            Type nullableType = null;

                            try
                            {
                                nullableType = Nullable.GetUnderlyingType(propertyType);
                            }
                            catch
                            {
                                nullableType = propertyType;
                            }

                            if (isNullable && nullableType != null)
                            {
                                return GetPropertyDescription(name, nullableType, isNullable, nestedTypes);
                            }
                            else if (propertyType.IsArray)
                            {
                                typeName = propertyType.Name;
                            }
                            else if (isCollection)
                            {
                                var elementType = propertyType.GenericTypeArguments.First();

                                typeName = $"IEnumerable<{elementType.Name}>";
                            }
                            else
                            {
                                typeName = propertyType.Name;

                                if (propertyType.IsClass && !propertyType.IsPrimitive && !nestedTypes.ContainsKey(propertyType))
                                    nestedTypes.TryAdd(propertyType, GetTypeInfo(propertyType));
                            }

                            break;
                        }
                    default:
                        typeName = propertyType.FullName;
                        break;
                }
            }
            else
            {
                typeName = propertyType.Name;

                AddEnumDescription(propertyType, nestedTypes);
            }

            typeName = isNullable ? typeName + NullableSymbol : typeName;

            return $"\tpublic {typeName} {name}" + " { get; set; }";
        }

        private void AddEnumDescription(Type propertyType, ConcurrentDictionary<Type, ChoreographyTypeInfo> nestedTypes)
        {
            if (propertyType.IsEnum && !propertyType.IsPrimitive && !nestedTypes.ContainsKey(propertyType))
            {
                StringBuilder typeDescription = new StringBuilder($"public enum {propertyType.Name}");
                typeDescription.AppendLine("{");

                foreach (var value in Enum.GetValues(propertyType))
                {
                    typeDescription.AppendLine($"\t{value} = {(int)value},");
                }

                typeDescription.AppendLine("}");

                var typeInfo = new ChoreographyTypeInfo()
                {
                    Name = propertyType.Name,
                    Object = typeDescription.ToString()
                };

                nestedTypes.TryAdd(propertyType, typeInfo);
            }
        }
    }
}