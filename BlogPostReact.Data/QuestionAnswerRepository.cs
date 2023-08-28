using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPostReact.Data
{
    public class QuestionAnswerRepository
    {
        public string _connectionString;
        public QuestionAnswerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddQuestion(Question question, List<String>tags)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            context.Questions.Add(question);
            context.SaveChanges();

            int tagId;

            foreach(string tag in tags)
            {
                Tag t = GetTag(tag);

                if(t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }

                context.QuestionsTags.Add(new QuestionsTags
                {
                    QuestionId = question.Id,
                    TagId = tagId

                });
            }
            context.SaveChanges();
        }

        public List<Question> GetQuestions()
        {
            using var context = new QuestionAnswerDBContext(_connectionString);

            var questions = context.Questions
                .Include(q => q.Answers)
                .Select(q => new Question
                {
                    Id = q.Id,
                    Title = q.Title,
                    Text = q.Text,
                    DatePosted = q.DatePosted,
                    UserId = q.UserId,
                    User = q.User,
                    Answers = q.Answers,
                    QuestionsTags = q.QuestionsTags.Select(qt => new QuestionsTags
                    {
                        QuestionId = qt.QuestionId,
                        TagId = qt.TagId,
                        Tag = qt.Tag
                    }).ToList()
                })
                .ToList();

            return questions;
        }
        public Question GetQuestionPerId(int id)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            return context.Questions.Include(q => q.Answers)
                                    .ThenInclude(a => a.User)
                                    .Include(q => q.User)
                                    .Select(q => new Question
                                    {
                                        Id = q.Id,
                                        Title = q.Title,
                                        Text = q.Text,
                                        DatePosted = q.DatePosted,
                                        UserId = q.UserId,
                                        User = q.User,
                                        Answers = q.Answers,
                                        QuestionsTags = q.QuestionsTags.Select(qt => new QuestionsTags
                                        {
                                            QuestionId = qt.QuestionId,
                                            TagId = qt.TagId,
                                            Tag = qt.Tag
                                        }).ToList()
                                    })
                                    .ToList().FirstOrDefault(q => q.Id == id);
        }

        private int AddTag(string name)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            var tag = new Tag { Name = name };            
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.Id;
        }
        private Tag GetTag(string name)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == name);
        }
       
        public void Answer(Answer answer)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
        }
    }
}
