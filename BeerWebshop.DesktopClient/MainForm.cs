using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.DesktopClient.Controllers;


namespace BeerWebshop.DesktopClient
{
    public partial class MainForm : Form
    {
        private readonly ProductController _productController;
        private readonly OrderController _orderController;
        public string JwtToken;
        public MainForm(string jwtToken)
        {
            JwtToken = jwtToken;
            _orderController = new OrderController(new OrderApiClient("https://localhost:7244/api/v1/"), jwtToken);
            _productController = new ProductController(new ProductAPIClient("https://localhost:7244/api/v1/"), jwtToken);
            InitializeComponent();

        }

        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            new AddProductForm(_productController).ShowDialog();
        }

        private void btnEditProducts_Click(object sender, EventArgs e)
        {
            new ViewEditDeleteForm(_productController).ShowDialog();
        }

        private void btnViewProducts_Click(object sender, EventArgs e)
        {

            new ViewEditDeleteForm(_productController).ShowDialog();

        }

        private void btnOpenOrders_Click(object sender, EventArgs e)
        {
            new ManageOrdersForm(_orderController).ShowDialog();
        }
    }
}
