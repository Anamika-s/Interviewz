using System.Configuration;
using System.Security.Authentication;
using System.Text;
using AuthService;
using AuthService.MongoDBSettings;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.Configure<UserDatabaseSettings>(
//            builder.Configuration.GetSection(nameof(UserDatabaseSettings)));

//builder.Services.AddSingleton<IUserDatabaseSettings>(sp => 
//    sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value);

builder.Services.AddSingleton<IRecruiterService, RecruiterService>();
builder.Services.AddSingleton<ICandidateService,CandidateService>();
//builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
//builder.Services.AddSingleton((provider) =>
//{
//    var endpint = builder.Configuration["CosmosDb:Account"];
//    var primaryKey = builder.Configuration["CosmosDb:Key"];
//    var databaseName = builder.Configuration["CosmosDb:Account:DatabaseName"];
//    var cosmosClientOptions = new CosmosClientOptions
//    {
//        ApplicationName = databaseName
//    };
//    var cosomosClient = new CosmosClient(endpint, primaryKey, cosmosClientOptions);
//    return cosomosClient;
//});

//builder.Services.AddSingleton<IDocumentClient>(x => new DocumentClient(new Uri(builder.Configuration["CosmosDb:Account"]), builder.Configuration["CosmosDb:Key"]));
builder.Services.AddControllers().AddJsonOptions(
options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


// Add services to the container.

    var key = Encoding.ASCII.GetBytes("thisisasecretkeyisforCandidate&Recruiter"); 
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.WithOrigins("http://localhost:4200") 
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });


builder.Services.AddControllers().AddNewtonsoftJson();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

