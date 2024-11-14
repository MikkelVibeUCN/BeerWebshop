using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.DesktopClient.Controllers;

namespace BeerWebshop.DesktopClient
{
	public partial class ManageOrdersForm : Form
	{
		private readonly OrderController _orderController;
		public ManageOrdersForm(OrderController orderController)
		{
			_orderController = orderController ?? throw new ArgumentNullException(nameof(orderController));
			InitializeComponent();
			ConfigureLayout();
			InitializeOrderLinesGrid();
			_ = LoadData();
		}

		private void ConfigureLayout()
		{
			this.Size = new Size(1400, 800);
			this.BackColor = Color.LightGray;
			this.Font = new Font("Segoe UI", 10);
		}

		//TODO: Få det til at se bedre ud
		private void InitializeOrderLinesGrid()
		{
			dgvOrderlines.EnableHeadersVisualStyles = false;
			dgvOrderlines.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
			dgvOrderlines.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			dgvOrderlines.DefaultCellStyle.Font = new Font("Segoe UI", 10);
			dgvOrderlines.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
			dgvOrderlines.RowHeadersVisible = false;
			dgvOrderlines.BorderStyle = BorderStyle.None;

			dgvOrderlines.Columns.Clear();

			// Set flexible and fixed column widths
			dgvOrderlines.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "ProductName",
				HeaderText = "Product Name",
				AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Fills available space
			});
			dgvOrderlines.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Quantity",
				HeaderText = "Quantity",
				AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				MinimumWidth = 60 // Minimum width to avoid overflow
			});
			dgvOrderlines.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Price",
				HeaderText = "Price",
				AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				MinimumWidth = 60
			});
			dgvOrderlines.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Subtotal",
				HeaderText = "Subtotal",
				AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
				MinimumWidth = 80
			});

			// Align numeric columns to the right
			dgvOrderlines.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvOrderlines.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvOrderlines.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

			// Set the DataGridView to fill the form width with constraints
			dgvOrderlines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvOrderlines.MaximumSize = new Size(800, dgvOrderlines.Height); // Set max width to 800px (adjust as needed)
			dgvOrderlines.AutoSize = true; // Resize to fit content within constraints
		}



		private void lstOrders_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstOrders.SelectedItem is OrderDTO selectedOrder)
			{
				DisplayOrderDetails(selectedOrder);
			}
		}

		private void DisplayOrderDetails(OrderDTO selectedOrder)
		{
			textBox1.Text = selectedOrder.Id.ToString();
			textBox2.Text = selectedOrder.Date.ToString("dd-MM-yyyy");
			textBox3.Text = selectedOrder.IsDelivered.ToString();
			txtBox_Total.Text = selectedOrder.TotalPrice.ToString("C");

			dgvOrderlines.Rows.Clear();
			foreach (var orderLine in selectedOrder.OrderLines)
			{
				dgvOrderlines.Rows.Add(
					orderLine.Product.Name,
					orderLine.Quantity,
					orderLine.Product.Price,
					orderLine.Quantity * orderLine.Product.Price
				);
			}
		}

		//TODO: Implement the LoadData method og fædiggør viewet
		private async Task LoadData()
		{
			try
			{
				IEnumerable<OrderDTO> orders = await _orderController.GetAllOrders();

				lstOrders.Items.Clear();

				foreach (var order in orders)
				{
					lstOrders.Items.Add(order);
				}

				if (lstOrders.Items.Count > 0)
				{
					lstOrders.SelectedIndex = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error retrieving data from the server. Error: '{ex.Message}'",
								"Communication error",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
			}
		}


	}
}
