using GraphQL_API_X_clone.middle_ware;
using GraphQL_API_X_clone.Models;
using GraphQL_API_X_clone.Models.Types;
using GraphQL_API_X_clone.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<UserService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddGraphQLServer()
    .AddQueryType<QueryType>()
    .AddMutationType<MutationType>()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseMiddleware<CustomMiddleware>();

app.MapGraphQL();

app.UseCors("MyPolicy");

app.Run();
