using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;

namespace CurioConnect.Model
{
    internal class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
    }
}
