namespace Common.DTO.Authentication
{
    public record LoginResponse(bool Flag, string Message = null!, string Token = null!);
}
