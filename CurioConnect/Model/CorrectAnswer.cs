using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurioConnect.Model
{
    internal class CorrectAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public QuizQuestion Question { get; set; }
        public int OptionId { get; set; }
        public AnswerOptions AnswerOptions { get; set; }
    }
}
