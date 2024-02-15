
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
                //Check size and file extension (don't allow .exe)
                string fileExtension = Path.GetExtension(attachment.FileName);
                if(attachment.Length > 100000000 || fileExtension==".exe") { return false; }
            }
            return true;
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