using System.ComponentModel.DataAnnotations;

namespace belt2.Models
{
    public class RegisterIdeaModel : BaseEntity
    {
        [Required]
        [MinLength(15)]
        public string Description {get; set;}
    }
}