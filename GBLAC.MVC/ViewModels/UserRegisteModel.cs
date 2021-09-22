namespace GBLAC.Models.ViewModels
{
    public class UserRegisteModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }  
        public string Email { get; set; }
        public string UserName { get => Email; }      
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
