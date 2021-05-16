using System;
using System.Collections.Generic;
using System.Linq;

namespace Choreography.AspNetCore.UI
{
    public class ChoreographyDescriptor : IChoreographyDescriptor
    {
        private readonly Type _referenceType;
        private readonly CSharpChoreographyDescriptor _descriptor;

        public ChoreographyDescriptor(Type referenceType)
        {
            _referenceType = referenceType;
            _descriptor = new CSharpChoreographyDescriptor();
        }

        public IEnumerable<ChoreographyTypeInfo> GetTypeInfos()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => _referenceType.IsAssignableFrom(p) && p.IsAbstract == false && p.IsInterface == false)
                .Select((t) =>
                {
                    return _descriptor.GetTypeInfo(t);
                });

            return types;
        }
    }
}
