using dotbook_api.DataAccess.TableModels;
using dotbook_api.Models.Base;
using dotbook_api.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace dotbook_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemeController : Controller
    {
        private readonly BaseReadService<Theme> _themeService;

        public ThemeController(BaseReadService<Theme> themeService)
        {
            _themeService = themeService;
        }

        [HttpGet("{id}")]
        public Theme GetById(int id)
        {
            return _themeService.GetById(id);
        }

        [HttpGet]
        public IEnumerable<Theme> Get([FromQuery] BaseQueryParams filter = null)
        {
            return _themeService.Get(filter).ToArray();
        }
    }
}
