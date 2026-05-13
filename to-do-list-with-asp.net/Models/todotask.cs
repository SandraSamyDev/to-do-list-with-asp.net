using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace to_do_list_with_asp.net_.Models
{
    public class todotask
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsImportant { get; set; }

        public DateTime CreatedDate { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation Property
        public User User { get; set; }
    }
}