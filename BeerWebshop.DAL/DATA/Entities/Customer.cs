namespace BeerWebshop.DAL.DATA.Entities;

public class Customer
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Address { get; set; }
	public string? ZipCode { get; set; }
	public string? City { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public string? Phone { get; set; }
	public int Age { get; set; }
}
