using System;
using System.Collections.Generic;
using Choreography.AspNetCore.UI;
using Xunit;

namespace Choreography.UnitTests
{
    public class CSharpChoreographyDescriptorTests
    {
        internal class CSharpClass
        {
            public decimal Decimal { get; set; }
            public Guid Guid { get; set; }
            public Guid? NullableGuid { get; set; }
            public string String { get; set; }
            public Int16 Integer16 { get; set; }
            public Int32 Integer32 { get; set; }
            public int Integer { get; set; }
            public bool Bool { get; set; }
            public Boolean Boolean { get; set; }
            public DateTime DateTime { get; set; }
            public DateTime? Nullable { get; set; }
            public Nullable<int> NullableGeneric { get; set; }
            public CshapEnum CshapEnum { get; set; }
            public CshapEnum? NullableCshapEnum { get; set; }
            public DateTimeOffset? DateTimeOffset { get; set; }
            public NestedCSharpClass NestedCSharpClass { get; set; }
            public string[] PrimitiveArray { get; set; }
            public NestedCSharpClass[] ObjectArray { get; set; }
            public List<NestedCSharpClass> ObjectGenericCollection { get; set; }
        }

        internal enum CshapEnum
        {
            Value1 = 1,
            Value2 = 199,
            ValueNoInt
        }

        internal class NestedCSharpClass
        {
            public string NestedClassString { get; set; }
            public int NestedClassInt { get; set; }
        }

        internal class GenericClassWithMembers<T>
        {
            public T Data { get; set; }
        }

        internal class CSharpClassFromGeneric : GenericClassWithMembers<CSharpClass>
        {

        }

        private readonly CSharpChoreographyDescriptor cSharpChoreographyDescriptor;

        public CSharpChoreographyDescriptorTests()
        {
            cSharpChoreographyDescriptor = new CSharpChoreographyDescriptor();
        }

        [Fact]
        public void Should_Return_Description_With_Class_Name()
        {
            // Arrange, Act
            var description = cSharpChoreographyDescriptor.GetTypeInfo(typeof(CSharpClass));

            // Assert
            Assert.Contains(nameof(CSharpClass), description.Name);
        }

        [Fact]
        public void Should_Describe_Class_Name()
        {
            // Arrange, Act
            var description = cSharpChoreographyDescriptor.GetTypeInfo(typeof(CSharpClass));

            // Assert
            Assert.Contains($"public class {nameof(CSharpClass)}", description.Object);
        }

        [Theory]
        [InlineData("public System.Decimal Decimal { get; set; }")]
        [InlineData("public Guid Guid { get; set; }")]
        [InlineData("public Guid? NullableGuid { get; set; }")]
        [InlineData("public string String { get; set; }")]
        [InlineData("public int Integer16 { get; set; }")]
        [InlineData("public int Integer32 { get; set; }")]
        [InlineData("public int Integer { get; set; }")]
        [InlineData("public bool Bool { get; set; }")]
        [InlineData("public bool Boolean { get; set; }")]
        [InlineData("public DateTime DateTime { get; set; }")]
        [InlineData("public DateTime? Nullable { get; set; }")]
        [InlineData("public int? NullableGeneric { get; set; }")]
        [InlineData("public CshapEnum CshapEnum { get; set; }")]
        [InlineData("public CshapEnum? NullableCshapEnum { get; set; }")]
        [InlineData("public DateTimeOffset? DateTimeOffset { get; set; }")]
        [InlineData("public NestedCSharpClass NestedCSharpClass { get; set; }")]
        [InlineData("public String[] PrimitiveArray { get; set; }")]
        [InlineData("public NestedCSharpClass[] ObjectArray { get; set; }")]
        [InlineData("public IEnumerable<NestedCSharpClass> ObjectGenericCollection { get; set; }")]
        public void Should_Describe_Primitive_Property(string expectedPropertyDescription)
        {
            // Arrange, Act
            var description = cSharpChoreographyDescriptor.GetTypeInfo(typeof(CSharpClass));

            // Assert
            Assert.Contains(expectedPropertyDescription, description.Object);
        }

        [Theory]
        [InlineData("public class NestedCSharpClass")]
        [InlineData("public string NestedClassString { get; set; }")]
        [InlineData("public int NestedClassInt { get; set; }")]
        public void Should_Describe_NestedClass(string expectedDescription)
        {
            // Arrange, Act
            var description = cSharpChoreographyDescriptor.GetTypeInfo(typeof(CSharpClass));

            // Assert
            Assert.Contains(expectedDescription, description.Object);
        }

        [Theory]
        [InlineData("public enum CshapEnum")]
        [InlineData("Value1 = 1")]
        [InlineData("Value2 = 199")]
        [InlineData("ValueNoInt")]
        public void Should_Describe_Enum(string expectedDescription)
        {
            // Arrange, Act
            var description = cSharpChoreographyDescriptor.GetTypeInfo(typeof(CSharpClass));

            // Assert
            Assert.Contains(expectedDescription, description.Object);
        }

        [Theory]
        [InlineData("public class CSharpClassFromGeneric")]
        [InlineData("public CSharpClass Data { get; set; }")]
        public void Should_Describe_GenericClass_With_NestedClasses(string expectedDescription)
        {
            // Arrange, Act
            var description = cSharpChoreographyDescriptor.GetTypeInfo(typeof(CSharpClassFromGeneric));

            // Assert
            Assert.Contains(expectedDescription, description.Object);
        }
    }
}
