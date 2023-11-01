namespace Client.ViewModel
{
    public class RegisterVM
    {
        public string? Fullname { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string? Email { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
