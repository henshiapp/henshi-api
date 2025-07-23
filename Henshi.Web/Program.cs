using Henshi.Flashcards.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddOpenApi();

    builder.Services.AddControllers()
        .AddApplicationPart(typeof(Henshi.Flashcards.FlashcardCollectionsController).Assembly);

    builder.Services.AddFlashcardServices(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.MapControllers();
}

app.Run();