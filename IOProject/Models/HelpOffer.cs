using Microsoft.AspNetCore.Identity;
namespace IOProject.Models
{
    public class HelpOffer
    {
        //offer id
        public int Id { get; set; }

        //benefactor id
        public string BenefactorId { get; set; }

        //help project id
        public int ProjectId { get; set; }

        //help offer amount
        public double Amount { get; set; }

        //description what is included in the amount
        public string Description { get; set; }

        //time when created
        public DateTime WhenCreated { get; set; }
    }
}
