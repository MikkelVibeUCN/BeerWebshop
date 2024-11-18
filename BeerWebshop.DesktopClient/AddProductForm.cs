using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeerWebshop.DesktopClient.Controllers
{
    public partial class AddProductForm : Form
    {
        private readonly ProductController _productController;
        private readonly CategoryController _categoryController;
        private List<CategoryDTO> _categories = new List<CategoryDTO>();

        public AddProductForm()
        {
            InitializeComponent();
            _productController = new ProductController(new ProductAPIClient("https://localhost:7244/api/v1/"));
            _categoryController = new CategoryController(new CategoryAPIClient("https://localhost:7244/api/v1/"));
            LoadCategories().ConfigureAwait(false);
        }


            private async Task LoadCategories()
        {
            try
            {
                var categories = await _categoryController.GetCategoriesAsync();
                _categories = categories.ToList();
                cmbCategory.DataSource = _categories;
                cmbCategory.DisplayMember = "Name";
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }

        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                // Gather data from input fields
                string name = txtName.Text;
                string breweryName = txtBrewery.Text;
                string description = txtDescription.Text;
                var selectedCategory = cmbCategory.SelectedItem as CategoryDTO;
                string categoryName = selectedCategory.Name;    

                float price = float.TryParse(txtPrice.Text, out float parsedPrice) ? parsedPrice : 0;
                int stock = int.TryParse(txtStock.Text, out int parsedStock) ? parsedStock : 0;
                float abv = float.TryParse(txtABV.Text, out float parsedABV) ? parsedABV : 0;


                // Create a ProductDTO object
                ProductDTO newProduct = new ProductDTO()
                {
                    Id = 0,
                    Name = name,
                    BreweryName = breweryName,
                    Price = price,
                    Description = description,
                    Stock = stock,
                    ABV = abv,
                    CategoryName = categoryName

                };


                // Call the controller to add the product
                int result = await _productController.AddProductAsync(newProduct);

                // Check if product was added successfully
                if (result > 0)
                {
                    MessageBox.Show("Product added successfully!", "Success");
                }
                else
                {
                    MessageBox.Show("Failed to add product.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            btnAddProduct_Click(sender, e);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
