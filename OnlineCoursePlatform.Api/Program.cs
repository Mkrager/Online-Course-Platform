using OnlineCoursePlatform.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureService()
    .ConfigurePipeline();

app.Run();

public partial class Program { }