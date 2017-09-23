using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SondageApi.Model;

namespace SondageApi.Controllers
{
    [Route("api/[controller]")]
    public class SondagesController : Controller
    {
        private readonly SondageContext _context;
        public SondagesController(SondageContext context)
        {
            _context = context;

            if (_context.SimpleSondageDAOs.Count() == 0)
            {
                _context.SimpleSondageDAOs.Add(new SimpleSondageDAO());
                _context.SaveChanges();
            }
        }

        // GET api/sondages
        [HttpGet]
        public IActionResult<string> Get()
        {
            return new string[] { "Quel sondage choisissez vous ?", "1", "2" };
        }

        [HttpGet("{id}", Name = "GetSondage")]
        public IActionResult GetSondageById(int id)
        {
            var dbSondage = _context.SimpleSondageDAOs.FirstOrDefault(t => t.Id == 1);
            if (dbSondage == null)
            {
                return NotFound();
            }
            return new ObjectResult(dbSondage);
        }

       

        // GET api/sondages/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/sondages
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/sondages/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/sondages/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
