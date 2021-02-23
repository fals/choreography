using System.Collections.Generic;

namespace Choreography.AspNetCore.UI
{
    public interface IChoreographyDescriptor
    {
        IEnumerable<ChoreographyTypeInfo> GetTypeInfos();
    }
}
