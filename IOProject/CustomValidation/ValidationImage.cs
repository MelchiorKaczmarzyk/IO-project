using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;

namespace IOProject.CustomValidation
{
    public class ValidationImage : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var image = value as IFormFile;
            if (image.ContentType.StartsWith("image"))
            {
                return true;
            }
            else return false;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new();
            rule.ErrorMessage = base.ErrorMessage;
            rule.ValidationType = "fileImage";
            return new ModelClientValidationRule[] { rule };
        }
    }
}
