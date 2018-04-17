using System;

namespace SocialNetwork.DataContext
{
    public class Like
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid CommentId { get; set; }
       
        public Post Post { get; set; }

        public Comment Comment{ get; set; }
    }
}
