namespace ECommerce.Services
{
    public class AdminService
    {
        public async Task<string> AdLogin(string email, string password)
        {
            if (email == "admin@mail" && password == "admin")
            {
                return "Login Success";
            }
            else
            {
                throw new Exception("Invalid email or password");
            }
        }
    }
}