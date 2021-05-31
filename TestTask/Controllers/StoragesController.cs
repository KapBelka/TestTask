using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StoragesController : Controller
    {
        StorageService _SS;
        DataContext _Db;
        public StoragesController(StorageService ss, DataContext db)
        {
            _SS = ss;
            _Db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetStorages()
        {
            try
            {
                return Json(_SS.GetStoragesData());
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorage(int id, string datetime)
        {
            try
            {
                return Json(_SS.GetStorageData(id, DateTime.Parse(datetime)));
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }
    }
}
