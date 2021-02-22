using Choreography.AspNetCore.UI;

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
    }
}
