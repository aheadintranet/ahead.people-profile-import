var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapPost("/api/import", async (HttpRequest req) =>
{
    if(req.HasFormContentType)
    {
        var form = await req.ReadFormAsync();

        foreach(var file in form.Files)
        {
            // You may save the file or process it in some way.
            var filename = file.FileName;
            using (var stream = file.OpenReadStream())
            {
                // Do something with stream...
            }
        }
    }

    return Results.Ok();
});

app.Run();