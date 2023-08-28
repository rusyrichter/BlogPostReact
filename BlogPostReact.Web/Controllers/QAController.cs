using BlogPostReact.Data;
using BlogPostReact.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace BlogPostReact.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QAController : ControllerBase
    {
        private string _connectionString;

        public QAController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("askaquestion")]
        public void AskAQuestion(QuestionSubmission questionSubmission)
        {
            var currentUser = User.Identity.Name;
            var repo = new UserRepository(_connectionString);
            questionSubmission.DatePosted = DateTime.Now;
            questionSubmission.UserId = repo.GetUserIdPerEmail(currentUser);

            var repoTwo = new QuestionAnswerRepository(_connectionString);
            repoTwo.AddQuestion(questionSubmission, questionSubmission.Tags);
        }

        [HttpPost]
        [Route("answeraquestion")]
        public void Answer(Answer answer)
        {
            var userRepo = new UserRepository(_connectionString);
            var currentUser = User.Identity.Name;
            var repo = new QuestionAnswerRepository(_connectionString);
            var newAnswer = new Answer
            {
                Text = answer.Text,
                Date = DateTime.Now,
                QuestionId = answer.QuestionId,
                UserId = userRepo.GetUserIdPerEmail(currentUser),
            };
            repo.Answer(newAnswer);
        }

        [HttpGet]
        [Route("getquestions")]
        public List<Question> GetQuestions()
        {
            var repo = new QuestionAnswerRepository(_connectionString);
            var questions = repo.GetQuestions();
            return questions;
        }
        [HttpGet]
        [Route("getquestionbyId")]
        public Question GetQuestionById(int id)
        {
            var repo = new QuestionAnswerRepository(_connectionString);
            return repo.GetQuestionPerId(id);
        }
    }
}
