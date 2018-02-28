using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace belt2
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt {get; set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        
        public DateTime UpdatedAt {get; set;}
    }
}