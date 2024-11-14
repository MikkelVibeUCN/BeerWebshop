using BeerWebshop.APIClientLibrary;
using BeerWebshop.APIClientLibrary.ApiClient.Client.Interfaces;
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
    public partial class ViewProductsForm : Form
    {
        private readonly IProductAPIClient _productAPIClient;
        public ViewProductsForm()
        {
            InitializeComponent();
        }
    }

}