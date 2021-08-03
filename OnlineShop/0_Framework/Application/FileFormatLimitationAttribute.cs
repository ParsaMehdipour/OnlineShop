using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
    public class FileFormatLimitationAttribute : ValidationAttribute , IClientModelValidator
    {
        private readonly string[] _validFormats;

        public FileFormatLimitationAttribute(string[] validFormats)
        {
            _validFormats = validFormats;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file is null) return true;
            var fileFormat = Path.GetExtension(file.FileName);

            return _validFormats.Contains(fileFormat);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-validFileFormats", ErrorMessage);
        }
    }
}
