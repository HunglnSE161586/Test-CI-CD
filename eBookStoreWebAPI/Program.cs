using eBookStoreLib.DataAccess;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddControllers().AddOData(option=>option.Select().Filter()
//        .Count().OrderBy().Expand().SetMaxTop(100));

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Author>("Authors");
    builder.EntitySet<Book>("Books");
    builder.EntitySet<Publisher>("Publishers");
    builder.EntitySet<User>("Users");

    return builder.GetEdmModel();
}
// Add services to the container.

builder.Services.AddControllers().AddOData(options =>
{
    options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100);

    options.EnableQueryFeatures();
    var routeOptions = options.AddRouteComponents("odata", GetEdmModel()).RouteOptions;

    routeOptions.EnableQualifiedOperationCall = true;
    routeOptions.EnableKeyInParenthesis = false;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();