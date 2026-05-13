using System.ComponentModel.DataAnnotations;

namespace to_do_list_with_asp.net_.Models
{
    public class LoginViewModel
    {
        
        
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

        }
    }

