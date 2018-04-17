using System;

namespace C_18_01_Capstone.Main.DataContext
{
    public class Like
    {
        public  Guid PostId { get; set; }
        public  Guid UserId { get; set; }
        public  Guid CommentId { get; set; }
               
        public virtual Post Post { get; set; }

        public virtual Comment Comment{ get; set; }
    }
}
