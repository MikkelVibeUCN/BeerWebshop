namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class OrderLine
    {
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }

        public float SubTotal
        {
            get
            {
                return Quantity * Product.Price;
            }
        }

        public OrderLine(int quantity, ProductDTO product)
        {
            Quantity = quantity;
            Product = product;
        }
    }
}
