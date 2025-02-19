namespace APIMeteo.CustomException
{
    public class NoResultException : Exception
    {
        public NoResultException(string message) : base(message)
        {
        }
    }
}