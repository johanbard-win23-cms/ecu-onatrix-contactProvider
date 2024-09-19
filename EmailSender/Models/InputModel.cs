namespace EmailSender.Models
{
    public class InputModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Category { get; set; } = null!;
    }
}
