﻿using System.ComponentModel.DataAnnotations;

namespace OnlineCoursePlatform.App.ViewModels.Answer
{
    public class AnswerViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Text required")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "Must be one correct answer required")]
        public bool IsCorrect { get; set; }
    }
}
