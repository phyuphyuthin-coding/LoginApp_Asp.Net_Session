using LoginAppWithSessionInAsp.Net.Models.DataModels;
using LoginAppWithSessionInAsp.Net.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAppWithSessionInAsp.Net.Services
{
    public class UserService
    {
        private readonly AppDbContext db;

        public UserService(AppDbContext db)
        {
            this.db = db;
        }

        public List<UserDataModel> GetData()
        {
            List<UserDataModel> userLst = db.Users.ToList();
            return userLst;
        }

        public bool GetUserByEmail(string email)
        {
            bool isExist = db.Users.Select(x => x.email).Any(x => x == email);
            return isExist;
        }

        public UserDataModel ExistByEmailAndPassword(string email, string password)
        {
            UserDataModel user = db.Users
                .FirstOrDefault(x => x.email == email);
            
            bool verified = BCrypt.Net.BCrypt.Verify(password+user.email, user.password);
            
            return verified ? user : null;
        }

        public int CreateUser(UserViewModel userViewModel)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userViewModel.Password+userViewModel.Email);
            UserDataModel userDataModel = new UserDataModel
            {
                userName = userViewModel.UserName,
                email = userViewModel.Email,
                password = passwordHash
            };
            db.Users.Add(userDataModel);
            int result = db.SaveChanges();
            return result;
        }
    }
}
