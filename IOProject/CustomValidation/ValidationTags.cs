using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IOProject.CustomValidation
{
    public class ValidationTags : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            var tags = value as List<string>;
            if (tags.IsNullOrEmpty())
            {
                return false;
            }
            else return true;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new();
            rule.ErrorMessage = base.ErrorMessage;
            rule.ValidationType = "tagsNotEmpty";
            return new ModelClientValidationRule[] { rule };
        }
    }
}
