var builder = WebApplication.CreateBuilder(args);

// Add Service to the conainer.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the Http request pipeline.
// This line of code find all ther class that implement ICarter and write therr routs to here
app.MapCarter();

app.Run();
