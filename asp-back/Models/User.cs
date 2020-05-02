using System;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace asp_back.Models
{
    public class User
    {
        public User(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
        }
        public User(int Id, string Login)
        {
            this.Login = Login;
            this.Id = Id;
        }
        public User(int Id, string Login, string Password)
        {
            this.Id = Id;
            this.Login = Login;
            this.Password = Password;
        }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
    public interface IUserRepository
    {
        int Create(User user);
        int? ifExistsGetId(User user);
    }
    public class UserRepo : IUserRepository
    {
        string connectionString = null;
        public UserRepo(string conn)
        {
            connectionString = conn;
        }
        public int Create(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery1 = "INSERT INTO UserTB (Login,Password) VALUES(@Login,@Password)";
                var result = db.Execute(sqlQuery1, user);

                var sqlQuery = "INSERT INTO UserTB (Login,Password) VALUES(@Login,@Password); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                return userId.Value;
            }
        }
        public int? ifExistsGetId(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var Login = user.Login;
                var Password = user.Password;
                return db.Query<int?>(@"SELECT Id
                FROM UserTB
                WHERE Login = @Login and Password = @Password", new { Login, Password }).FirstOrDefault();
            }
        }
    }
}
