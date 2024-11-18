namespace RetrieveJWTToken.Services
{
    public interface ICentralizedLoggerService
    {
        Task<int> WritingLogAsync(string strMessage, string strType);
    }
}
