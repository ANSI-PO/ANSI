using System.Collections.Generic;

namespace FrontEnd.Models;

public class QuestionModel
{
    public int QuestionId { get; set; }
    public string QuestionName { get; set; }
    public List<AnswerModel> Answers { get; set; }
}
