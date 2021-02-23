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
                .Where(p => _referenceType.IsAssignableFrom(p))
                .Select(t => new ChoreographyTypeInfo()
                {
                    Name = t.Name
                });

            return types;
        }
    }
}
