﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.OnlineId;

namespace CurioConnect.Model
{
    internal class AnswerOptions
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public QuizQuestion Question { get; set; }

    }
}
