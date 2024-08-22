namespace DataAccessLayer.Models
{
    public abstract class EntityBase
    {
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
