namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class OrderLine
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public float SubTotal
        {
            get
            {
                return Quantity * Product.Price;
            }
        }

        public OrderLine(int quantity, Product product)
        {
            Quantity = quantity;
            Product = product;
        }
    }
}
