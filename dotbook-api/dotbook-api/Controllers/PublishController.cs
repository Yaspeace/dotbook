using dotbook_api.DataAccess.TableModels;
using dotbook_api.Models.Base;
using dotbook_api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace dotbook_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishController : Controller
    {
        private readonly PublishService _publishSrv;

        public PublishController(PublishService publishSrv)
        {
            _publishSrv = publishSrv;
        }

        [HttpGet("{id}")]
        public Publish GetById(int id)
        {
            return _publishSrv.GetById(id);
        }

        [HttpGet]
        public IEnumerable<Publish> Search([FromQuery] BaseQueryParams filter = null, [FromQuery] string search = "")
        {
            return _publishSrv.Search(filter, search);
        }

        [HttpPost]
        public string Aboba()
        {
            return "Aboba";
        }
    }
}
