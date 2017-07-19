using BallyTech.Infrastructure.Security;


namespace BallyTech.UI.Web.Platform.WebService
{
    public static class SessionContext
    {
        private static ISession session;

        public static void SetSession(ISession session)
        {
            SessionContext.session = session;
        }

        public static ISession Current
        {
            get { return session; }
        }

        public static void Clear()
        {
            session = null;
        }
    }
}
