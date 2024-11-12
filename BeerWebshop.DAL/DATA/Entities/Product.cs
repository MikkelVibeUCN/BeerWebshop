namespace BeerWebshop.DAL.DATA.Entities;

public class Product
{
	public int? Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public float Price { get; set; }
	public int Stock { get; set; }
	public string? ImageUrl { get; set; }
	public float Abv { get; set; }
	public byte[] RowVersion { get; set; }
	public bool IsDeleted { get; set; }


	public Category Category { get; set; }
	public Brewery Brewery { get; set; }

	public Product() { }


	public Product(int? id, string name, Category category, Brewery brewery, float price, string description, int stock, float abv, string? imageUrl, bool isDeleted)
	{
		Id = id;
		Name = name;
		Category = category;
		Brewery = brewery;
		Price = price;
		Description = description;
		Stock = stock;
		Abv = abv;
		ImageUrl = imageUrl;
		IsDeleted = isDeleted;

	}



}



