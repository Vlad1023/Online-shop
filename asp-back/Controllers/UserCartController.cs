using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using asp_back.Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json.Linq;
namespace asp_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserCartController : ControllerBase
    {
        ICartItemRepo carts;
        public UserCartController(ICartItemRepo carts)
        {
            this.carts = carts;
        }
        [HttpPost("Create")]
        public void Create(JsonElement json)
        {
            var Discountvalue = Convert.ToInt32(json.GetProperty("Discount").ToString());
            int? Discount = Discountvalue;
            if (Discountvalue == 0) Discount = null;
            var item = new CartItem(
                json.GetProperty("Name").ToString(),
                json.GetProperty("Description").ToString(),
                Discount,
                Convert.FromBase64String(json.GetProperty("ImgData").ToString()),
                Convert.ToInt32(JObject.Parse(HttpContext.Session.GetString("user")).GetValue("Id")),          
                false
                );
            carts.Create(item);
        }
        [HttpGet("GetUserItems")]
        public string getCurrentUserItems()
        {
            var sessionString = HttpContext.Session.GetString("user");
            var Id = Convert.ToInt32(JObject.Parse(sessionString).GetValue("Id"));
            var referList = carts.GetItems(Id);
            var resultList = new List<Object>();
            referList.ForEach(el =>
            {
                resultList.Add
                    (new { Id = el.Id, Name = el.Name, Description = el.Description, Discount = el.Discount, ImgData = Convert.ToBase64String(el.ImgData), IsBought = el.IsBought });
            });
            var json = System.Text.Json.JsonSerializer.Serialize(resultList);
            return json;
        }
        [HttpPut("Purchase")]
        public void Delete(JsonElement Id)
        {
            var ItemId = Convert.ToInt32(Id.ToString());
            carts.Purchase(ItemId);
        }
    }
}