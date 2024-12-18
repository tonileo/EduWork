using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class SickLeaveRecord : BaseEntity
    {
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public string? Comment { get; set; }


        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
