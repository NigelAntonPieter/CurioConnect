using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurioConnect.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Course { get; set; }
        public string Photo {  get; set; }
        public string Gender { get; set; }

        public string ImagePathWithFallBack => Photo ?? "ms-appx:///Assets/placeholder-small.jpg";
        public static User CurrentUser { get; set; }

        public int? QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public List<Match> Matches { get; set; }
        public List<Match> MatchedUsers { get; set; }
        public List<Message> Messages { get; set; } = null;
    }
}
