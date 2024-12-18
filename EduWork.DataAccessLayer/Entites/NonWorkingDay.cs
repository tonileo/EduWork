using EduWork.DataAccessLayer.Entites.Abstractions;

namespace EduWork.DataAccessLayer.Entites
{
    public class NonWorkingDay : BaseEntity
    {
        public required DateOnly NonWorkingDate { get; set; }

        public required string Title { get; set; } = string.Empty;
    }
}
