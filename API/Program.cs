using API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("file:///C:/Users/hp/Desktop/TodoApp/UI/index.html",
                                "http://localhost:8080");
        });

    options.AddPolicy("AnotherPolicy",
        policy =>
        {
            policy.WithOrigins("file:///C:/Users/hp/Desktop/TodoApp/UI/index.html")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});
// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddOpenApi();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "TodoApp");
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
