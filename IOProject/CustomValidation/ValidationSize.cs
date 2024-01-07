using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IOProject.CustomValidation
{
    public class ValidationSize : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var image = value as IFormFile;
            if (image.Length < 100000000)
            {
                return true;
            }
            else return false;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new();
            rule.ErrorMessage = base.ErrorMessage;
            rule.ValidationType = "fileSize";
            return new ModelClientValidationRule[] { rule };
        }
    }
}
