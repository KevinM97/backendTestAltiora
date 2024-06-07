namespace BackTest.Api.Response
{
    public class ErrorResponse
    {
        public bool ok => false;
        public string Message { get; set; }
    }
}
