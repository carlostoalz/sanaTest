using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;

namespace SanaTest.API
{
    [ExcludeFromCodeCoverage]
    internal static class SwaggerExtension
    {
        internal static void AddSwaggerDocumentation(this IServiceCollection services) => services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("V1", GetOpenApiInfo());
            //options.AddSecurityDefinition("Bearer", GetOpenApiSecurityScheme());
            //options.AddSecurityRequirement(GetOpenApiSecurityRequirement());
        });

        internal static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("./V1/swagger.json", "AccDiv API"));
        }

        static OpenApiInfo GetOpenApiInfo() => new()
        {
            Title = "SanaTest API",
            Version = "V1",
            Description = "API Web shop Sana"
        };

        static OpenApiSecurityScheme GetOpenApiSecurityScheme() => new()
        {
            Description = "JWT Authorization header using the bearer scheme. \n\n" +
                          "Enter 'Bearer' [space] and then yout token in the text input below. \n" +
                          "Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        };

        static OpenApiSecurityRequirement GetOpenApiSecurityRequirement() => new()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new()
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        }, new List<string>()
    }
};
    }
}