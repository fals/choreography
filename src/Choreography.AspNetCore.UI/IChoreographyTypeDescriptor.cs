using System;

namespace Choreography.AspNetCore.UI
{
    public interface IChoreographyTypeDescriptor
    {
        ChoreographyTypeInfo GetTypeInfo(Type type);
    }
}
