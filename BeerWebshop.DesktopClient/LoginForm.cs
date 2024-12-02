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
