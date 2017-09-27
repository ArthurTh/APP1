using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SondageApi.Model;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace SondageApi.Controllers
{
    [Route("api/[controller]")]
    public class SondagesController : Controller
    {

        private readonly ISondageDAO sondage = new SimpleSondageDAO();

        // GET api/sondages
        //[Authorize]
        [HttpGet]
        public IActionResult GetSondageAvailable()
        {                                                                                    
            // Récupération des descriptions
            IList<Poll> question = sondage.GetAvailablePolls();
            // Formatage
            string description = JsonConvert.SerializeObject(question);
            // Retour de GET
            if (!description.Equals(null)) return Ok(description);
            else return NotFound();                                                                                 
        }

        // GET api/sondages/PollId/QId
        //[Authorize]
        [HttpGet("{PollId}/{QId}", Name = "GetSondage")]
        public IActionResult GetSondageById(int PollId, int QId)
        {
            // Récupération des descriptions
            
            PollQuestion question = sondage.GetNextQuestion(PollId, QId);
            // Formatage
            string contenu = JsonConvert.SerializeObject(question);
            // Retour de GET
            if (!contenu.Equals("null"))
                return Ok(contenu);
            else return NotFound();
        }

        // POST api/sondages/PollId/QId
        //[Authorize]
        [HttpPost]
        public IActionResult PostAnswer([FromBody]PollQuestion answer)
        {
            if (answer != null)
            {
                sondage.SaveAnswer(1, answer);
                return Ok();
            }
            else return BadRequest();
        }
    }
}
