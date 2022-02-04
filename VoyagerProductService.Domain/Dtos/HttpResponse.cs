using System.Net;

namespace VoyagerProductService.Domain.Dtos
{
    public class HttpResponse<T> : HttpResponse
    {
        public HttpResponse() : base()
        {

        }

        public HttpResponse(HttpStatusCode statusCode) : base()
        {
            StatusCode = statusCode;
        }

        public HttpResponse(T responseValue) : this()
        {
            ResponseValue = responseValue;
        }

        public T ResponseValue { get; protected set; }
    }

    public class HttpResponse
    {
        public HttpResponse()
        {
        }
        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }

}
