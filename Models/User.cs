using System.Collections.Generic;

namespace belt2.Models
{
    public class User: BaseEntity
    {
        public int UserId {get; set;}
        public string Name {get; set;}
        public string Alias {get; set;}
        public string EmailAddress {get; set;}
        public string Password {get; set;}
        public List<Like> UserLikes {get; set;}
        public List<Idea> UserIdeas {get; set;} 
        public User()
        {
            UserLikes = new List<Like>();
            UserIdeas = new List<Idea>();
        }
    }
}