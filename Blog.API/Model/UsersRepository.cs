using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Blog.API.Model
{
    public class UsersRepository
    {
        private string connectionString;
        public UsersRepository()
        {
            connectionString = @"Persist Security Info=False; Data Source=DESKTOP-K3CNDLV\MSSQLSERVER2; Initial Catalog=BlogDB;integrated security=true;";
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public void Add(Users user)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO Users(Username,Password,Name,Surname,Text) VALUES(@Username,@Password,@Name,@Surname,@Text)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, user);
            }
        }

        public IEnumerable<Users> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT * FROM Users";
               

                dbConnection.Open();
                return dbConnection.Query<Users>(sQuery);
              

            }
        }
        public Users GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"SELECT * FROM Users Where UserId=@UserId";
                dbConnection.Open();
                return dbConnection.Query<Users>(sQuery, new { UserId = id }).FirstOrDefault();
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"DELETE FROM Users Where UserId=@UserId";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { UserId = id });
            }
        }
        public void Update(Users user)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE Users SET Username=@Username,Password=@Password,Name=@Name,Surname=@Surname,Text=@Text Where UserId=@UserId";
                dbConnection.Open();
                dbConnection.Execute(sQuery, user);
            }
        }
    }
}
