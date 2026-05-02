namespace to_do_list_with_asp.net_.Models
{
    public class todotask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public bool IsImportant { get; set; } = false; 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
