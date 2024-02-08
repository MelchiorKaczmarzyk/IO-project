
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Web.Mvc;

namespace IOProject.CustomValidation
{
    public class ValidationAttachments : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            
            var attachments = value as List<IFormFile>;
            foreach (var attachment in attachments) 
            {
                //Nie można dodać exe tylko
                if(attachment.Length > 100000000 /*&& plik nie jest wykonywalny*/) { return false; }
            }
            return true;
            //if (image.ContentType.StartsWith("image")) {}
            
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new();
            rule.ErrorMessage = base.ErrorMessage;
            rule.ValidationType = "fileType";
            return new ModelClientValidationRule[] { rule };
        }
    }
}