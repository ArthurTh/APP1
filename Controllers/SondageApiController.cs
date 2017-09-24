using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SondageApi.Model;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult GetSondageAvailable()
        {
            String result = null;
            var dbSondage = _context.SimpleSondageDAOs.FirstOrDefault(t => t.Id == 1);
            if (dbSondage == null)
            {
                return NotFound();
            }
            
            foreach(Poll sondage in dbSondage.GetAvailablePolls())
            {
                result = result + "Sondage numéro : " + sondage.Id 
                    + Environment.NewLine + "Description : " + sondage.Description 
                    + Environment.NewLine;
            }
            return new ObjectResult(result);
        }

        
        [HttpGet("{PollId}/{QId}", Name = "GetSondage")]
        public IActionResult GetSondageById(int PollId, int QId)
        {
            String answer = null;
            var dbSondage = _context.SimpleSondageDAOs.FirstOrDefault(t => t.Id == 1);
            if (dbSondage == null)
            {
                return NotFound();
            }

            PollQuestion question = dbSondage.GetNextQuestion(PollId, QId);
            //foreach(PollQuestion question in dbSondage.GetNextQuestion(PollId, QId))
            answer = "Sondage numéro : " + question.PollId 
                + Environment.NewLine + "Question numéro : " + question.QuestionId 
                + Environment.NewLine + question.Text 
                + Environment.NewLine;
            return new ObjectResult(answer);
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
