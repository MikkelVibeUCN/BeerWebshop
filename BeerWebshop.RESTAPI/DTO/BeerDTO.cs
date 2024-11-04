namespace BeerWebshop.RESTAPI.DTO;

public class BeerDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public double ABV { get; set; }

    public string Category { get; set; }
}
