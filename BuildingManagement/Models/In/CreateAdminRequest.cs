namespace Models.In
{
    public class CreateAdminRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Admin ToEntity()
        {
            return new Admin
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };
        }
    }
}
