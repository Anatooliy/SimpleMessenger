using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Messenger.Models
{
    public class Message
    {
        public int Id { get; set; }

        [DisplayName("Message")]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string MessagesText { get; set; }

        public DateTime Date { get; set; }

        [DisplayName("Author")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}