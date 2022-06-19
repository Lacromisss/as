using System.ComponentModel.DataAnnotations;

namespace AspPage.Vm
{
    public class RegisterVm
    {
        [Required ,MaxLength(20)]
        public string FirstName { get; set; }
        [Required, MaxLength(20)]

        public string LastName { get; set; }
        [Required,DataType(DataType.EmailAddress) ]


        public string Email { get; set; }
        [Required, DataType(DataType.Password)]

        public string Password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }
        [Required,MaxLength(20)]
        public string UserName { get; set; }
    }
}
