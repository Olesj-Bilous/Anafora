using AnaforaData.Context;
using AnaforaData.Model;
using AnaforaData.Model.Global.Product;
using AnaforaWeb.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult<List<T>>> All()
        {
            var all = await _context.Set<T>().ToListAsync();
            return Ok(all);
        }

        //[ContentAuthorize(_type, AnaforaData.Utils.Enums.Permissions.Read)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<T>> Get(Guid guid)
        {
            var model = await _context.Set<T>().FindAsync(guid);
            if (model == null) return NotFound();
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpDelete]
        public async Task<ActionResult> Remove(Guid guid)
        {
            var model = await _context.Set<T>().FindAsync(guid);
            if (model == null) return NotFound();
            _context.Set<T>().Remove(model);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] T model)
        {
            await _context.AddRangeAsync(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] T model)
        {
            var oldModel = await _context.Set<T>().FindAsync(model.Id);
            if (model == null) return NotFound();
            foreach (var prop in typeof(T).GetProperties())
            {
                var attributes = prop.GetCustomAttributes(typeof(ValidationAttribute), true);
                var newValue = prop.GetValue(model);
                if (attributes.Any(a => a.GetType() == typeof(RequiredAttribute)) && newValue == null) return BadRequest();
                prop.SetValue(oldModel, newValue);
            }
            
            return Ok();
        }
    }
}
