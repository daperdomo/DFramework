namespace DFramework.Application.Common.Interfaces.Authentication
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
    }
}
