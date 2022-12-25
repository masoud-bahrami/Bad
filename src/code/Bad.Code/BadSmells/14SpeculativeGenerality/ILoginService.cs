namespace Bad.Code.BadSmells._14SpeculativeGenerality
{
    public interface ILoginService
    {
        void Login(string userName, string password);
    }

    public class LoginByUserAndPassword : ILoginService
    {
        public void Login(string userName, string password)
        {
            //implementation
        }
    }

    /// <summary>
    /// but we never need to login by Google
    /// </summary>
    public class LoginByGoogle : ILoginService
    {
        public void Login(string userName, string password)
        {
            //TODO
        }
    }

    /// <summary>
    /// but we never need to login by Facebook
    /// </summary>
    public class LoginByFaceBook : ILoginService
    {
        public void Login(string userName, string password)
        {
            //TODO
        }
    }
}