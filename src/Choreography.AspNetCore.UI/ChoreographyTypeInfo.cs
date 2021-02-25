﻿namespace Choreography.AspNetCore.UI
{
    /// <summary>
    /// Information about the described type
    /// </summary>
    public class ChoreographyTypeInfo
    {
        /// <summary>
        /// The short name of that type
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The event object
        /// </summary>
        public object Object { get; set; }
    }
}