namespace webProductos.Api.Extensions;

public static class SwaggerAppExtensions
{
    public static IApplicationBuilder UseSwaggerWithRoleUI(this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/admin/swagger.json", "API - Admin");
                c.SwaggerEndpoint("/swagger/user/swagger.json", "API - User");
                c.InjectJavascript("/swagger-role.js");
            });
        }

        return app;
    }
}