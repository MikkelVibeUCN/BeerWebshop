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
using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary;
using System.Xml.Linq;


namespace BeerWebshop.DesktopClient
{

    public partial class ViewEditDeleteForm : Form
    {
        private readonly ProductController _productController;

        public ViewEditDeleteForm(ProductController productController)
        {
            InitializeComponent();
            _productController = productController;
            ViewEditDeleteForm_Load();
        }
        public async void ViewEditDeleteForm_Load()
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
                MessageBox.Show($"Error retrieving data from the server. Error is: '{ex.Message}'", "Communication error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (lstProduct.Items.Count > 0) { lstProduct.SelectedIndex = 0; }
        }
        private void UpdateUi()
        {
            if (lstProduct.SelectedIndex != -1)
            {
                UpdateSelectedProductOnUI();
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
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
            lblName.Text = product.Name;
            lblBrewery.Text = product.BreweryName;
            lblPrice.Text = product.Price.ToString("F2");
            lblStock.Text = product.Stock.ToString();
            lblABV.Text = product.ABV.ToString();
            lblCategory.Text = product.CategoryName;
            lblDescription.Text = product.Description;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //TODO: Fix så UI opdaterer med det samme når man editer
            new EditProductForm(_productController).ShowDialog();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedProduct();
        }

        private async Task DeleteSelectedProduct()
        {
            if (lstProduct.SelectedIndex == -1) { return; }
            if (MessageBox.Show("Er du sikker på at du vil slette dette produkt?", "Delete?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                try
                {
                    ProductDTO productToDelete = (ProductDTO)lstProduct.SelectedItem;
                    await _productController.DeleteProduct(productToDelete);
                    lstProduct.Items.Remove(productToDelete);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Der skete en fejl i at slette produktet fra serveren. Fejlen er: '{ex.Message}'", "Communication error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblStock_Click(object sender, EventArgs e)
        {

        }
    }
}
