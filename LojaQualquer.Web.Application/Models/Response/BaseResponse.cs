namespace LojaQualquer.Web.Application.Models.Response
{
    public class BaseResponse
    {
        public int? StatusCode { get; set; }
        public ResponseError ResponseError { get; set; }
    }

    public class ResponseError
    {
        public string Message { get; set; }
    }
}