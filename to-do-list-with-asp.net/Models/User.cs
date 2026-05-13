using System.ComponentModel.DataAnnotations;

namespace to_do_list_with_asp.net_.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public ICollection<todotask> TodoTasks { get; set; }
            = new List<todotask>();
    }
}