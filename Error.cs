namespace ACO
{
    public class Error
    {
        public string Message { get; set; }
        public string Details { get; set; }

        public Error(string message, string details = "")
        {
            Message = message;
            Details = details;
        }
    }
}
