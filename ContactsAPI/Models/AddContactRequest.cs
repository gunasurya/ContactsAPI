namespace ContactsAPI.Models
{
    public class AddContactRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public long PhoneNumber { get; set; }

        public string Address { get; set; } = string.Empty;    
    }
}
