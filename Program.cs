using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");



//Recebando valores via QueryParams 
//api.com/v1/product?dataStart={date}anddataEnd={date}
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;
});



//Recebando valores via Headersggit
app.MapGet("/getproductbyhearder", (HttpRequest request) =>
{
    return request.Headers["product-code"].ToString();
});

//Recebando valores via body
//Rota de inserir
app.MapPost("/saveproduct", (Produto product) =>
{
    ProductRepository.Add(product);
});


//Recebando valores via rota
//api.com/v1/product/1
//Rota de consulta
app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.Getby(code);
    return product;
});



//Rota de edição
app.MapPut("/editProduct", (Produto product) =>
{
    var productSave = ProductRepository.Getby(product.Code);

    productSave.Name = product.Name;
});


//Rota de delete
app.MapDelete("/deleteProduct/{code}", ([FromRoute] string code) =>
{
    var productSave = ProductRepository.Getby(code);
    ProductRepository.Remove(productSave);

});
app.Run();

public static class ProductRepository
{
    public static List<Produto> Products { get; set; }

    public static void Add(Produto product)
    {
        if (Products == null)
            Products = new List<Produto>();

        Products.Add(product);
    }



    public static void Remove(Produto product)
    {
        Products.Remove(product);
    }

    public static Produto Getby(string code)
    {
        return Products.FirstOrDefault(p => p.Code == code);
    }

}

public class Produto
{
    public string? Code { get; set; }
    public string? Name { get; set; }


}