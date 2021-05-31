using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Controllers
{
    public class DataForTransfer
    {
        public int fromstorageid { get; set; }
        public int tostorageid { get; set; }
        public List<ProductDataModel> products { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : Controller
    {
        StorageService _SS;
        public TransfersController(StorageService ss)
        {
            _SS = ss;
        }
        [HttpPost]
        public async Task<IActionResult> Create(DataForTransfer data)
        {
            try
            {
                return Json(_SS.Transfer(data.fromstorageid, data.tostorageid, data.products));
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Json(_SS.GetTransfersData());
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Json(_SS.DeleteTransfer(id));
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
        }
    }
}
