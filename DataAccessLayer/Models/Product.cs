namespace DataAccessLayer.Models
{
    public class Product : EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
