using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Webapi.DatabaseContext;
using Webapi.Repositories.Comments;
using Webapi.Repositories.Events;
using Webapi.Repositories.Signups;
using Webapi.ServiceProviderExtensions;
using Webapi.Services.Signups;

var allowCorsLocalhost = "allowCorsLocalhost";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<JsonOptions>(options =>
  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddDbContext<WebapiContext>(options =>
{
  string temp = builder.Configuration.GetValue<string>("ENVIRONMENT");
  string connectionString = builder.Configuration.GetValue<string>("WEBAPI_CONNECTIONSTRING");
  options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ISignupRepository, SignupRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ISignupService, SignupService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configuration =>
{
  configuration.CustomOperationIds(apiDescription =>
    $"{apiDescription.ActionDescriptor.RouteValues["controller"]}_{((ControllerActionDescriptor)apiDescription.ActionDescriptor).ActionName}");
  configuration.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "EventMeUp Webapi",
    Version = "v1",
    Contact = new OpenApiContact
    {
      Name = "fabianmue"
    }
  });
  configuration.IncludeXmlComments(Path.Combine(
    AppContext.BaseDirectory,
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
  ));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: allowCorsLocalhost, builder =>
    builder
      .WithOrigins("http://localhost:4200")
      .AllowAnyMethod()
      .AllowAnyHeader());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  if (app.Environment.IsEnvironment("Local"))
  {
    scope.ServiceProvider.DeleteDatabase();
  }

  scope.ServiceProvider.MigrateDatabase();

  if (app.Environment.IsEnvironment("Local"))
  {
    scope.ServiceProvider.Seed();
  }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

if (app.Environment.IsEnvironment("Local"))
{
  app.UseCors(allowCorsLocalhost);
}

app.MapControllers();
app.Run();
