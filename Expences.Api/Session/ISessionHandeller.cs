namespace Expences.Api.Session
{
    public interface ISessionHandeller
    {
        void SetSession();
        string GetSession();
    }
}
