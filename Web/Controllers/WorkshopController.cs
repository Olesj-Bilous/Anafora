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

        [AllowAnonymous]
        [HttpGet]
        public List<ProductStringProperty> Properties()
        {
            return _context.Set<ProductStringProperty>().ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        public ProductStringProperty Property(Guid id)
        {
            return _context.Set<ProductStringProperty>().Find(id);
        }

        [AllowAnonymous]
        [HttpGet]
        public List<ProductStringValue> Values(Guid propertyId)
        {
            return _context.Set<ProductStringValue>().Where(value => value.Property.Id == propertyId).ToList();
        }
    }
}
