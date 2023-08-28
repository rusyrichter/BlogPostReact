using BlogPostReact.Data;

namespace BlogPostReact.Web.Models
{
    public class QuestionSubmission : Question
    {
        public List<string> Tags { get; set; }
    }
}
