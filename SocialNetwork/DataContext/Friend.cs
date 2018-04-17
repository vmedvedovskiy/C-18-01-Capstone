using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.DataContext
{
    public class Friend
    {
        public Guid UserId { get; set; }        
        public Guid FriendId { get; set; }
    }
}
