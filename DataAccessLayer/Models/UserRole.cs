namespace DataAccessLayer.Models
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
