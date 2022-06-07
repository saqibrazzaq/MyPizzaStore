namespace auth.Entities.Exceptions
{
    public class UnAuthorizedUserException : Exception
    {
        public UnAuthorizedUserException(string message) : base(message)
        {

        }
    }
}
