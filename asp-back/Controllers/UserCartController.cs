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
    public class UserCartController : ControllerBase
    {
        ICartItemRepo carts;
        public UserCartController(ICartItemRepo carts)
        {
            this.carts = carts;
        } 
    }
}