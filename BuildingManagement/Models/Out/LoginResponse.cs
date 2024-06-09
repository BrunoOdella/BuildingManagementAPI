namespace Models.Out
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string UserType { get; set; }

        public LoginResponse(string token, string userType)
        {
            Token = token;
            UserType = userType;
        }
    }
}
