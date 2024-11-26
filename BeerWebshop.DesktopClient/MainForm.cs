using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.DesktopClient.Controllers;


namespace BeerWebshop.DesktopClient
{
    public partial class MainForm : Form
    {
        private readonly ProductController _productController;
        private readonly OrderController _orderController;
        private readonly string _jwtToken;
        public MainForm(string jwtToken)
        {
            _jwtToken = jwtToken;
            _orderController = new OrderController(new OrderApiClient("https://localhost:7244/api/v1/"));
            _productController = new ProductController(new ProductAPIClient("https://localhost:7244/api/v1/"));
            InitializeComponent();


        }

        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            new AddProductForm(_jwtToken).ShowDialog();
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
