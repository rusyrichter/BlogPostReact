﻿namespace BlogPostReact.Data
{
    public class UserRepository
    {
        public string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var context = new QuestionAnswerDBContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                return null;
            }
            return user;
        }
        public User GetByEmail(string email)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
        public int GetUserIdPerEmail(string email)
        {
            using var context = new QuestionAnswerDBContext(_connectionString);
            return context.Users.FirstOrDefault(q => q.Email == email).Id;

        }
    }
}
    
