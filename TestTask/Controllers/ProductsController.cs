using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        StorageService _SS;
        public ProductsController(StorageService ss)
        {
            _SS = ss;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Json(_SS.GetProductsData());
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }
    }
}
