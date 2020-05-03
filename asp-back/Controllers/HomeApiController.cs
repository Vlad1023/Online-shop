using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using asp_back.Models;
using Newtonsoft.Json;


namespace asp_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        IMarket1Repository repo1;
        IMarket2Repository repo2;
        public HomeApiController(IMarket1Repository market1TB, IMarket2Repository market2TB)
        {
            repo1 = market1TB;
            repo2 = market2TB;
        }
        // GET: HomeApi/market1
        [HttpGet("market1")]
        public string Get1(int id)
        {
            var referList = repo1.GetItems();
            var resultList = new List<Object>();
            referList.ForEach(el => {
                resultList.Add
                        (new { Id = el.Id, Name = el.Name, Description = el.Description, ImgData = Convert.ToBase64String(el.ImgData) });
            });
            var json = System.Text.Json.JsonSerializer.Serialize(resultList);
            return json;
        }
        // GET: HomeApi/market2
        [HttpGet("market2")]
        public string Get2(int id)
        {
            var referList = repo2.GetItems();
            var resultList = new List<Object>();
            referList.ForEach(el => {
                resultList.Add
                    (new { Id = el.Id, Name = el.Name, Description = el.Description, Discount = el.Discount, ImgData = Convert.ToBase64String(el.ImgData) });
            });
            var json = System.Text.Json.JsonSerializer.Serialize(resultList);
            return json;
        }
    }
}