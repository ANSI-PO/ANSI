namespace Domain.Models
{
    public class AnswerModel
    {
        public int AnswerId { get; set; }
        public string AnswerName { get; set; }
        public bool isPicked { get; set; } = false;
    }
}
