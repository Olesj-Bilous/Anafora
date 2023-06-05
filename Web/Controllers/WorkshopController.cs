using AnaforaData.Context;
using AnaforaData.Model.Global.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnaforaWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        public WorkshopController(DataContext context)
        {
            _context = context;
        }

        private readonly DataContext _context;

        [AllowAnonymous]
        [HttpGet]
        public List<ProductType> Types()
        {
            return _context.Set<ProductType>().ToList();
        }
    }
}
