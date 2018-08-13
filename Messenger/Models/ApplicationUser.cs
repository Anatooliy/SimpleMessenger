using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messenger.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Message> Messages { get; set; }

        public ApplicationUser()
        {
            Messages = new List<Message>();
        }
    }
}