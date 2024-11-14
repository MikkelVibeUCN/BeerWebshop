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
			InitializeOrderLinesGrid();
			_ = LoadData();
		}

		//TODO: Få det til at se bedre ud
		private void InitializeOrderLinesGrid()
		{
			dgvOrderlines.Columns.Clear();

			dgvOrderlines.Columns.Add("ProductName", "Product Name");
			dgvOrderlines.Columns.Add("Quantity", "Quantity");
			dgvOrderlines.Columns.Add("Price", "Price");
			dgvOrderlines.Columns.Add("TotalPrice", "Total Price");

			dgvOrderlines.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvOrderlines.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dgvOrderlines.Columns["TotalPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
