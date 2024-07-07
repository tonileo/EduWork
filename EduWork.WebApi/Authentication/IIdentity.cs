namespace EduWork.WebApi.Authentication
{
    public interface IIdentity
    {
        string? DisplayName { get; }
        string? Email { get; }
        Guid? ObjectId { get; }
    }
}
