namespace web2projekat.Izuzeci
{
    public class ActionExceptioncs : Exception
    {
        public ActionExceptioncs()
        {
        }

        public ActionExceptioncs(string message) : base(message)
        {
        }

        public ActionExceptioncs(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

