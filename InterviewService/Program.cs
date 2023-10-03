using InterviewService.DAL;
using InterviewService.MongoDbSettings;
using InterviewService.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<InterviewDBSettings>(
            builder.Configuration.GetSection(nameof(InterviewDBSettings)));

builder.Services.AddSingleton<IInterviewDBSettings>(sp =>
    sp.GetRequiredService<IOptions<InterviewDBSettings>>().Value);


// Add services to the container.
builder.Services.AddScoped<ITimeslotDAL, TimeslotDAL>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();
builder.Services.AddScoped<IBookingSlotDAL, BookingslotDAL>();
builder.Services.AddScoped<IBookingSlotService, BookingSlotService>();
builder.Services.AddScoped<IFeedbackDAL, FeedbackDAL>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddCors();

//Connection To MongoDb
//builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetConnectionString("MongoDB")));


builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(options =>
{
    options.AllowAnyOrigin(); 
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.MapControllers();

app.Run();
