using System.Text.Json;
using ahead.PeopleProfileImportSample;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/import", async (HttpRequest req, ILogger<LogMe> logger) =>
{
    if (!req.HasFormContentType) return Results.BadRequest();
    var form = await req.ReadFormAsync();

    string? expectingFile = null; 
    
    foreach (var file in form.Files)
    {
        switch (file.ContentType)
        {
            case "application/json":
            {
                if (expectingFile != null)
                {
                    logger.LogWarning("Expected file with fileName {fileName}", expectingFile);
                    continue;
                }
                await using var stream = file.OpenReadStream();
                var doc = await JsonDocument.ParseAsync(stream);
                if (doc.RootElement.TryGetProperty("employeeId", out var idProp))
                {
                    logger.LogInformation("Received json file with employee id {id}", idProp.GetString());
                    if (doc.RootElement.TryGetProperty("file", out var fileProp))
                    {
                        logger.LogInformation("Expecting file associated with {employeeId} names {fileName}",
                            idProp.GetString(), fileProp.GetString());
                        expectingFile = $"{idProp.GetString()}__{fileProp.GetString()}";
                    }
                }
                else
                    logger.LogWarning("Json document did not contain an employeeId");
                break;
            }
            default:
            {
                break;
            }
                
        }
    }

    return Results.Ok();
});

app.Run();

namespace ahead.PeopleProfileImportSample
{
    internal class LogMe
    {
    }
}