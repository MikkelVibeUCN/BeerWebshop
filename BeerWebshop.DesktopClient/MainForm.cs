using BeerWebshop.DesktopClient.Controllers;

namespace BeerWebshop.DesktopClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void grpFrontpage_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            new AddProductForm().ShowDialog();
        }
    }
}
