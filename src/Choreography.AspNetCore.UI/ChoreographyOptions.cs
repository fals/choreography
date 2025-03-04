﻿using System;
using System.IO;
using System.Reflection;

namespace Choreography.AspNetCore.UI
{
    public class ChoreographyOptions
    {
        /// <summary>
        /// Gets or sets a title for the choreography-ui page
        /// </summary>
        public string DocumentTitle { get; set; }

        /// <summary>
        /// Gets or sets a route prefix for accessing the choreography-ui
        /// </summary>
        public string RoutePrefix { get; set; } = "choreography";

        /// <summary>
        /// Gets or sets a Stream function for retrieving the choreography-ui page
        /// </summary>
        internal Func<Stream> IndexStream { get; set; } = () => typeof(ChoreographyOptions).GetTypeInfo().Assembly
            .GetManifestResourceStream("Choreography.AspNetCore.UI.index.html");

        /// <summary>
        /// Gets or sets a Stream function for retrieving the choreography-ui page
        /// </summary>
        internal Func<Stream> CssStream { get; set; } = () => typeof(ChoreographyOptions).GetTypeInfo().Assembly
            .GetManifestResourceStream("Choreography.AspNetCore.UI.choreography.css");

        /// <summary>
        /// Gets or sets a Stream function for retrieving the choreography-ui javascript lib
        /// </summary>
        internal Func<Stream> JsStream { get; set; } = () => typeof(ChoreographyOptions).GetTypeInfo().Assembly
            .GetManifestResourceStream("Choreography.AspNetCore.UI.choreography-ui.js");

        /// <summary>
        /// Gets or sets additional content to place in the head of the choreography-ui page
        /// </summary>
        public string HeadContent { get; set; } = "";
        /// <summary>
        /// Gets or sets additional content about the message broker where messages are produced, to place in of the choreography-ui page
        /// </summary>
        public string MessageBroker { get; set; }
        /// <summary>
        /// Gets or sets additional content about the topic where messages are produced, to place in of the choreography-ui page
        /// </summary>
        public string TopicName { get; set; }
    }
}
