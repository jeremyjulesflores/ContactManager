namespace ContactManager.API.Models
{
    public class ContactWithoutDetailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool Favorite { get; set; } = false;
        public bool Emergency { get; set; } = false;
    }
}
