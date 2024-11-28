using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;

namespace BeerWebshop.DesktopClient.Controllers
{
    public partial class AddProductForm : Form
    {
        private readonly ProductController _productController;
        private readonly CategoryController _categoryController;
        private readonly BreweryController _breweryController;
        private List<CategoryDTO> _categories = new List<CategoryDTO>();
        private List<BreweryDTO> _breweries = new List<BreweryDTO>();
        private readonly string BaseUri = "https://localhost:7244/api/v1/";

        public AddProductForm(ProductController productController)
        {
            InitializeComponent();
            _productController = productController;
            _categoryController = new CategoryController(new CategoryAPIClient(BaseUri));
            _breweryController = new BreweryController(new BreweryAPIClient(BaseUri));
            LoadCategories().ConfigureAwait(false);
            LoadBreweries().ConfigureAwait(false);
        }

        private async Task LoadBreweries()
        {
            try
            {
                var breweries = await _breweryController.GetAllBreweries();
                _breweries = breweries.ToList();
                cmbBreweries.DataSource = _breweries;
                cmbBreweries.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl med at loade bryggerier: {ex.Message}");
            }
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
                MessageBox.Show($"Fejl med at loade kategorier: {ex.Message}");
            }
        }

        private async void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                // Gather data from input fields
                string name = txtName.Text;
                var selectedBrewery = cmbBreweries.SelectedItem as BreweryDTO;
                string breweryName = selectedBrewery.Name;
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
                    MessageBox.Show("Produkt tilføjet!", "Success");
                }
                else
                {
                    MessageBox.Show("Der skete en fejl med at tilføje produktet", "Error");
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
