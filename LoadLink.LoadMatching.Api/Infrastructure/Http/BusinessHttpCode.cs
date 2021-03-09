
namespace LoadLink.LoadMatching.Api.Infrastructure.Http
{
    public class BusinessHttpCode
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public BusinessHttpCode(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    public static class ResponseCode
    {
        public static BusinessHttpCode NotSubscribe => new BusinessHttpCode(700, "You are not subscribed to the service.");

        // Add more business http code here
    }

    public class ResponceNotSubscribed
    {
        public int ResponseCode { get; } = 700;
        public string ResponseMessage { get; } = "You are not subscribed to the service.";
    }

}
