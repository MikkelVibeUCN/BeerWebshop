namespace BeerWebshop.DAL.DATA.Entities;

public class Customer : Account
{
	public string? Name { get; set; }
	public string? Address { get; set; }
	public string? Phone { get; set; }
	public int? Age { get; set; }
}
