
namespace LoadLink.LoadMatching.Api.Infrastructure.Http
{
    public class BusinessHttpCode
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

        public BusinessHttpCode(int code, string message)
        {
            ResponseCode = code;
            ResponseMessage = message;
        }
    }

    public static class ResponseCode
    {
        public static BusinessHttpCode NotSubscribe => new BusinessHttpCode(700, "You are not subscribed to the service.");

        // Add more business http code here
    }


}
