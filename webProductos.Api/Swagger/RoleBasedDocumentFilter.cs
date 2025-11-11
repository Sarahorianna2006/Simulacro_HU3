using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace webProductos.Api.Swagger;

// Implementa IDocumentFilter para que Swagger la reconozca
public class RoleBasedDocumentFilter : IDocumentFilter
{
    
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Marcamos qué endpoints requieren rol Admin (Swagger los usará para ocultarlos)
        foreach (var path in swaggerDoc.Paths)
        {
            foreach (var operation in path.Value.Operations)
            {
                if (operation.Value.Tags.Any(t => 
                        t.Name.Contains("Admin", StringComparison.OrdinalIgnoreCase)) ||
                    operation.Value.Summary?.Contains("Admin", StringComparison.OrdinalIgnoreCase) == true)
                {
                    operation.Value.Description += " [ADMIN_ONLY]";
                }
            }
        }
    }
}