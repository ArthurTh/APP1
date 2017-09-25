using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SondageApi.Model;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace SondageApi.Controllers
{
    [Route("api/[controller]")]
    public class SondagesController : Controller
    {
                                                                                                /*private readonly SondageContext _context;
                                                                                                public SondagesController(SondageContext context)
                                                                                                {
                                                                                                    _context = context;

                                                                                                    if (_context.SimpleSondageDAOs.Count() == 0)
                                                                                                    {
                                                                                                        _context.SimpleSondageDAOs.Add(new SimpleSondageDAO());
                                                                                                        _context.SaveChanges();
                                                                                                    }
                                                                                                }*/
        public readonly ISondageDAO sondage = new SimpleSondageDAO();

        // GET api/sondages
        [Authorize]
        [HttpGet]
        public IActionResult GetSondageAvailable()
        {
                                                                                                // Génération d'un objet sondage
                                                                                                //ISondageDAO sondage = new SimpleSondageDAO();
            // Récupération des descriptions
            IList<Poll> question = sondage.GetAvailablePolls();
            // Formatage
            string description = JsonConvert.SerializeObject(question);
            // Retour de GET
            if (!description.Equals(null)) return Ok(description);
            else return NotFound();
                                                                                                // Formatage de la réponse
                                                                                                /*String result = null;
                                                                                                foreach (Poll sondage in dbSondage.GetAvailablePolls())
                                                                                                {
                                                                                                    result = result + "Sondage numéro : " + sondage.Id 
                                                                                                        + Environment.NewLine + "Description : " + sondage.Description 
                                                                                                        + Environment.NewLine;
                                                                                                }
                                                                                                return new ObjectResult(result);*/
        }

        [Authorize]
        [HttpGet("{PollId}/{QId}", Name = "GetSondage")]
        public IActionResult GetSondageById(int PollId, int QId)
        {
            // Récupération des descriptions
            PollQuestion question = sondage.GetNextQuestion(PollId, QId-1);
            // Formatage
            string contenu = JsonConvert.SerializeObject(question);
            // Retour de GET
            if (!contenu.Equals("null"))
                return Ok(contenu);
            else return NotFound();
                                                                                                // Formatage de la réponse
                                                                                                //foreach(PollQuestion question in dbSondage.GetNextQuestion(PollId, QId))
                                                                                                /*answer = "Sondage numéro : " + question.PollId 
                                                                                                    + Environment.NewLine + "Question numéro : " + question.QuestionId 
                                                                                                    + Environment.NewLine + question.Text 
                                                                                                    + Environment.NewLine;
                                                                                                return new ObjectResult(answer);*/
        }

        [Authorize]
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
