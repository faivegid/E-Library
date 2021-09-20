using System.Collections.Generic;

namespace GBLAC.Services.FluentValidation.ErrorModel
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
