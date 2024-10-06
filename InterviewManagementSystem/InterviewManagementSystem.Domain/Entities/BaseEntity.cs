namespace InterviewManagementSystem.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? CreateAt { get; set; } = DateTime.Now;

        public DateTime? UpdateAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
