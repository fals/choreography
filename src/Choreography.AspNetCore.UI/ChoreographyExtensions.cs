﻿using Choreography.AspNetCore.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.AspNetCore.Builder
{
    public static class ChoreographyExtensions
    {
        /// <summary>
        /// Register the ChoreographyUI middleware with provided options
        /// </summary>
        public static IApplicationBuilder UseChoreographyUI(this IApplicationBuilder app, ChoreographyOptions options)
        {
            return app.UseMiddleware<ChoreographyMiddleware>(options);
        }

        /// <summary>
        /// Register the ChoreographyUI middleware with optional setup action for DI-injected options
        /// </summary>
        public static IApplicationBuilder UseChoreographyUI(
            this IApplicationBuilder app,
            Action<ChoreographyOptions> setupAction = null)
        {
            ChoreographyOptions options;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<ChoreographyOptions>>().Value;
                setupAction?.Invoke(options);
            }

            return app.UseChoreographyUI(options);
        }
    }
}