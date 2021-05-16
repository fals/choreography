using System;
using System.Text.Json.Serialization;

namespace Choreography.AspNetCore.UI
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
        /// The event object Code
        /// </summary>
        public string Object { get; set; }

        /// <summary>
        /// The event object Code, line by line for Json Serialization of multiline
        /// </summary>
        [JsonIgnore]
        public string[] ObjectLines
        {
            get
            {
                return Object?.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            }
        }
    }
}
