using BeerWebshop.APIClientLibrary.ApiClient.Client;
using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace BeerWebshop.DesktopClient
{
    public partial class LoginForm : Form
    {
        private readonly AccountAPIClient _accountAPIClient;
        public string? JwtToken { get; private set; }
        public LoginForm()
        {
            _accountAPIClient = new AccountAPIClient("https://localhost:7244/api/v1/");
            
            InitializeComponent();

            LoadPictures();
        }

        private void LoadPictures()
        {
            pictureBox.Image = null;
            var imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/Anders_Hejlsberg.jpg/330px-Anders_Hejlsberg.jpg";
            pictureBox.Load(imageUrl);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox1.Image = null;
            var imageUrl1 = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7b/Anders_Hejlsberg_at_PDC2008.jpg/300px-Anders_Hejlsberg_at_PDC2008.jpg";
            pictureBox1.Load(imageUrl1);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox2.Image = null;
            var imageUrl2 = "https://media.licdn.com/dms/image/C4E03AQGRfBRQcoI7Kw/profile-displayphoto-shrink_800_800/0/1624267235021?e=2147483647&v=beta&t=qY7bfscxxPll-_cXR4WMI5M4BtKB79kvNaV_C65yXY4";
            pictureBox2.Load(imageUrl2);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox3.Image = null;
            var imageUrl3 = "https://th.bing.com/th/id/OIP.Sh5-0947wcN175xUfsPzDwAAAA?rs=1&pid=ImgDetMain";
            pictureBox3.Load(imageUrl3);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;


        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Email og password skal udfyldes", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var viewModel = new LoginViewModel
            {
                Email = email,
                Password = password
            };

            try
            {
                JwtToken = await _accountAPIClient.GetLoginToken(viewModel);

                if (!string.IsNullOrEmpty(JwtToken) && IsAdmin(JwtToken))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                    
                }
                else
                {
                    MessageBox.Show("Forkert email eller password", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der skete en fejl: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsAdmin(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(jwtToken))
                throw new ArgumentException("Invalid JWT token");

            var token = handler.ReadJwtToken(jwtToken);

            var roleClaim = token.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            return roleClaim != null && roleClaim.Value == "Admin";
        }
    }
}
