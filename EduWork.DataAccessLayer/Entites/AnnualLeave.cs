using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class AnnualLeave : BaseEntity
    {
        public required int Year { get; set; }
        public required int TotalLeaveDays { get; set; }
        public required int LeftLeaveDays { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
