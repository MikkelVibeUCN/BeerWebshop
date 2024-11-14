using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using BeerWebshop.APIClientLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeerWebshop.DesktopClient.Controllers;

namespace BeerWebshop.DesktopClient
{
	public partial class ManageOrdersForm : Form
	{
		private readonly OrderController _orderController;
		public ManageOrdersForm(OrderController orderController)
		{
			InitializeComponent();
			LoadData();
			_orderController = orderController;
		}

		private void lstOrders_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}

		//TODO: Implement the LoadData method og fædiggør viewet
		private async Task LoadData()
		{
			try

			{
				IEnumerable<OrderDTO> products = await _orderController.GetAllOrders();
				lstOrders.Items.Clear();
				foreach (var product in products)
				{
					lstOrders.Items.Add(product);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error retrieving data from the server. Error is: '{ex.Message}'", "Communication error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			if (lstOrders.Items.Count > 0) { lstOrders.SelectedIndex = 0; }
		}
	}
}
