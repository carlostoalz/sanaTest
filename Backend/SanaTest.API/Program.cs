using SanaTest.API;
using SanaTest.BE;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddUserGraphQL();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("App"));
builder.Services.AddCorsDocumentation(builder.Configuration["AllowedOrigins"]);
builder.Services.AddDependencys(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();
app.UseCorsDocumentation();
app.UseUserMiddlewares();
app.UseUserRoutes();
app.UseGraphQL();
app.Run();
