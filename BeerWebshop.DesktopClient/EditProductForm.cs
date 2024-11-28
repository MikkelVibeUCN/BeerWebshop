using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
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
using System.Xml.Linq;

namespace BeerWebshop.DesktopClient
{
    public partial class EditProductForm : Form
    {
        private readonly ProductController _productController;
        private readonly ProductDTO _product;
        public EditProductForm(ProductController productController, ProductDTO product)
        {
            InitializeComponent();
            _productController = productController;
            _product = product;
            EditProductForm_Load();
        }

        public void EditProductForm_Load()
        {
            LoadProductData();

        }
        private void LoadProductData()
        {
            if (_product == null) return;

            txtIdEdit.Text = _product.Id.ToString();
            txtNameEdit.Text = _product.Name;
            txtBreweryEdit.Text = _product.BreweryName;
            txtPriceEdit.Text = _product.Price.ToString("F2");
            txtStockEdit.Text = _product.Stock.ToString();
            txtABVEdit.Text = _product.ABV.ToString();
            txtCategoryEdit.Text = _product.CategoryName;
            txtDescriptionEdit.Text = _product.Description;
            txtImageUrlEdit.Text = _product.ImageUrl;
            lblRowVersionEdit.Text = _product.RowVersion;

            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            try
            {
                if (!string.IsNullOrEmpty(_product.ImageUrl))
                {
                    pictureBox.Load(_product.ImageUrl);
                }
                else
                {
                    pictureBox.Image = null;
                    var imageUrl = "https://storage.googleapis.com/pod_public/1300/163656.jpg";
                    pictureBox.Load(imageUrl);
                }
            }
            catch
            {
                var imageUrl = "https://storage.googleapis.com/pod_public/1300/163656.jpg";
                pictureBox.Load(imageUrl);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnEditProduct_Click(object sender, EventArgs e)
        {
            try
            {
                var product = new ProductDTO
                {
                    Id = int.TryParse(txtIdEdit.Text, out var parsedId) ? parsedId : 0,
                    Name = txtNameEdit.Text,
                    BreweryName = txtBreweryEdit.Text,
                    Price = float.TryParse(txtPriceEdit.Text, out var parsedPrice) ? parsedPrice : 0,
                    Description = txtDescriptionEdit.Text,
                    Stock = int.TryParse(txtStockEdit.Text, out var parsedStock) ? parsedStock : 0,
                    ABV = float.TryParse(txtABVEdit.Text, out var parsedABV) ? parsedABV : 0,
                    CategoryName = txtCategoryEdit.Text,
                    ImageUrl = txtImageUrlEdit.Text,
                    RowVersion = lblRowVersionEdit.Text
                };

                await _productController.EditProduct(product);

                MessageBox.Show("Produktet blev ændret!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl med at ændre produktet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }
}
