using Newtonsoft.Json;

namespace GBLAC.Services.FluentValidation.ErrorModel
{
    public class ErrorModel
    {
        public string FieldName { get; set; }
        public string  Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}