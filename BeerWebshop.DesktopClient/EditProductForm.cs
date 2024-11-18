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
        public EditProductForm(ProductController productController)
        {
            InitializeComponent();
            _productController = productController;
            EditProductForm_Load();
        }

        public async void EditProductForm_Load()
        {
            await LoadData();
            lstProduct.DisplayMember = "Name";
            UpdateUi();

        }
        private async Task LoadData()
        {
            try

            {
                ProductQueryParameters queryParameters = new ProductQueryParameters
                {
                    PageSize = 200
                };
                IEnumerable<ProductDTO> products = await _productController.getProducts(queryParameters);
                lstProduct.Items.Clear();
                foreach (var product in products)
                {
                    lstProduct.Items.Add(product);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl med at hente data fra serveren. Fejlen er: '{ex.Message}'", "Communication error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (lstProduct.Items.Count > 0) { lstProduct.SelectedIndex = 0; }
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

        private void UpdateUi()
        {
            if (lstProduct.SelectedIndex != -1)
            {
                UpdateSelectedProductOnUI();
                btnCancel.Enabled = true;
                btnEditProduct.Enabled = true;
            }
            else
            {
                btnCancel.Enabled = false;
                btnEditProduct.Enabled = false;
            }

        }

        private async void lstProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUi();
        }
        private void UpdateSelectedProductOnUI()
        {
            if (lstProduct.SelectedIndex == -1) { return; }
            ProductDTO product = (ProductDTO)lstProduct.SelectedItem;
            txtIdEdit.Text = product.Id.ToString();
            txtNameEdit.Text = product.Name;
            txtBreweryEdit.Text = product.BreweryName;
            txtPriceEdit.Text = product.Price.ToString("F2");
            txtStockEdit.Text = product.Stock.ToString();
            txtABVEdit.Text = product.ABV.ToString();
            txtCategoryEdit.Text = product.CategoryName;
            txtDescriptionEdit.Text = product.Description;
            txtImageUrlEdit.Text = product.ImageUrl;
            lblRowVersionEdit.Text = product.RowVersion;
        }
    }
    }
