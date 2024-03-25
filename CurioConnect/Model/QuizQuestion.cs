using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurioConnect.Model
{
    internal class QuizQuestion
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string QuizText { get; set; }
        public Quiz Quiz { get; set; }
    }
}
