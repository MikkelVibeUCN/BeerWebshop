using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.DesktopClient.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeerWebshop.DesktopClient
{
    public partial class EditProductForm : Form
    {
        private readonly ProductController _productController;
        public EditProductForm()
        {
            InitializeComponent();
            _productController = new ProductController(new ProductAPIClient("https://localhost:7244/api/v1/"));
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
