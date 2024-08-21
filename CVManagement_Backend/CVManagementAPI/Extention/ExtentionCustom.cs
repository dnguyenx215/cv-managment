using CvManagement.Middleware;

namespace CvManagement.Extention
{
    public static class ExtentionCustom
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
