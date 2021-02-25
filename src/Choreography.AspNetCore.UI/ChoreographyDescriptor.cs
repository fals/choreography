using System;
using System.Collections.Generic;
using System.Linq;

namespace Choreography.AspNetCore.UI
{
    public class ChoreographyDescriptor : IChoreographyDescriptor
    {
        private readonly Type _referenceType;

        public ChoreographyDescriptor(Type referenceType)
        {
            _referenceType = referenceType;
        }

        public IEnumerable<ChoreographyTypeInfo> GetTypeInfos()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => _referenceType.IsAssignableFrom(p) && p.IsAbstract == false && p.IsInterface == false)
                .Select((t) =>
                {
                    var @object = Activator.CreateInstance(t);

                    var propertyInfo = @object.GetType().GetProperties();
                    foreach (var property in propertyInfo)
                    {
                        try
                        {
                            if (property.PropertyType.IsAssignableFrom(typeof(string)))
                                property.SetValue(@object, "string");
                            else
                                property.SetValue(@object, Activator.CreateInstance(property.PropertyType));
                        }
                        catch (Exception)
                        {
                        }
                    }

                    return new ChoreographyTypeInfo()
                    {
                        Name = t.Name,
                        Object = @object
                    };
                });

            return types;
        }
    }
}
