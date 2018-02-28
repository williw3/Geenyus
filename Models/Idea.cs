using System.Collections.Generic;


namespace belt2.Models
{
    public class Idea : BaseEntity
    {
        public int IdeaId {get; set;}
        public string Description {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public List<Like> IdeaLikes {get; set;}
        public Idea()
        {
            IdeaLikes = new List<Like>();
        }
    }
}