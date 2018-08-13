using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Messenger.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() : base("MessengerContext") { }

        public DbSet<Message> Messages { get; set; }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}