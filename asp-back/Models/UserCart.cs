using System;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;

namespace asp_back.Models
{
    public class CartItem
    {
        public CartItem(string Name, string Description, int? Discount, byte[] ImgData, int UserId, bool IsBought)
        {
            this.UserId = UserId;
            this.ImgData = ImgData;
            this.Name = Name;
            this.Discount = Discount;
            this.Description = Description;
            this.IsBought = IsBought;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Discount { get; set; }
        public byte[] ImgData { get; set; }
        public int UserId { get; set; }  
        public bool IsBought { get; set; }

    }
    public interface ICartItemRepo
    {
        void Create(CartItem item);
        void Delete(int Id);
        List<dynamic> GetItems(int UserId);
        public void Purchase(int Id);
    }
    public class CartItemRepo : ICartItemRepo
    {
        string connectionString = null;
        public CartItemRepo(string conn)
        {
            connectionString = conn;
        }
        public List<dynamic> GetItems(int UserId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var info = db.Query<dynamic>("SELECT * FROM UsersItemsTB WHERE UserId = @UserId", new { UserId }).ToList();
                return info;
            }
        }
        public void Create(CartItem item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery1 = "INSERT INTO UsersItemsTB (UserId,ImgData,Name,Discount,Description,IsBought) VALUES(@UserId,@ImgData,@Name,@Discount,@Description,@IsBought)";
                var result = db.Execute(sqlQuery1, item);
            }
        }
        public void Delete(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM UsersItemsTB WHERE Id = @Id";
                db.Execute(sqlQuery, new { Id });
            }
        }
        public void Purchase(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE UsersItemsTB Set IsBought = 1 WHERE Id = @Id";
                db.Execute(sqlQuery, new { Id });
            }
        }

    }
}
