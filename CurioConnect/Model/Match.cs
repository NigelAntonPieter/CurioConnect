using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurioConnect.Model
{
    internal class Match
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [InverseProperty("Matches")]
        public User user { get; set; }

        public int MatchedUserId { get; set; }
        [InverseProperty("MatchedUsers")]
        public User MatchedUser { get; set; }
    }
}
