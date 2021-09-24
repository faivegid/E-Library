using Newtonsoft.Json;

namespace GBLAC.Services.GlobalErrorHandler
{
    internal class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
