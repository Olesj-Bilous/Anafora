using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaData.Model.Global.Product;
using AnaforaWeb.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnaforaWeb.Controllers
{
    public class TypeController : ModelController<ProductType>
    {
        public TypeController(DataContext context) : base(context)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public List<ProductStringProperty> Properties(Guid id)
        {
            return _context.Set<ProductStringPropertyType>()
                .Where(propType => propType.Type.Id == id)
                .Select(propType => propType.Property)
                .ToList();
        }
    }

    public class PropertyController : ModelController<ProductStringProperty>
    {
        public PropertyController(DataContext context) : base(context)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public List<ProductStringValue> Values(Guid id)
        {
            return _context.Set<ProductStringValue>().Where(value => value.Property.Id == id).ToList();
        }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class ModelController<T> : ControllerBase where T : class, IDataModel<Guid>
    {
        public ModelController(DataContext context)
        {
            _context = context;
        }

        protected readonly DataContext _context;

        private static string _type = typeof(T).ToString();

        [AllowAnonymous]
        [HttpGet]
        public List<T> All()
        {
            return _context.Set<T>().ToList();
        }

        //[ContentAuthorize(_type, AnaforaData.Utils.Enums.Permissions.Read)]
        [AllowAnonymous]
        [HttpGet]
        public T Get(Guid guid)
        {
            return _context.Set<T>().Find(guid);
        }
    }
}
