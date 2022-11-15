using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Recebando valores via body
app.MapPost("/saveproduct", (Produto product) =>
{
    return product.Code + " - " + product.Name;
});

//Recebando valores via QueryParams 
//api.com/v1/product?dataStart={date}anddataEnd={date}
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;
});

//Recebando valores via rota
//api.com/v1/product/1
app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
    return code;
});


//Recebando valores via Headersggit
app.MapGet("/getproductbyhearder", (HttpRequest request) =>
{
    return request.Headers["product-code"].ToString();
});

app.Run();



public class Produto
{
    public string? Code { get; set; }
    public string? Name { get; set; }


}