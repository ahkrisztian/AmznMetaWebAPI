using AmznMetaLibrary.Calculator;
using AmznMetaLibrary.CreateLinks;
using AmznMetaLibrary.Models;
using AmznMetaLibrary.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Reflection.Emit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmznWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AmznRdrController : ControllerBase
{
    private readonly IAmznMetaRepo repo;

    public AmznRdrController(IAmznMetaRepo repo)
    {
        this.repo = repo;
    }

    // GET: api/AmznRdrController/url
    /// <summary>
    /// Alle Bewertungen abrufen.
    /// </summary>
    /// <remarks>
    /// GET /AmznRdrController/url
    /// Beispiel link:
    /// </remarks>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewModel>>> GetComments(string url)
    {
        if(string.IsNullOrEmpty(url))
        {
            Log.Logger.Information("api/GetComments was called but with an bad Url. BadRequest400");
            return BadRequest();
        }

        var output = await repo.WhenAllReady(url);

        if(output.Count > 0) 
        {
            Log.Logger.Information("api/GetComments was called and returned OK200");
            return Ok(output);
        }

        Log.Logger.Information("api/GetComments was called and returned NotFound404");
        return NotFound();
    }

    /// <summary>
    /// Sie können mit dieser Anfrage sehen, was die Kunden über das Produkt denken, ob es gut oder schlecht ist.
    /// </summary>
    /// <remarks>
    /// GET /AmznRdrController/Treatment/url
    /// Beispiel link:
    /// </remarks>
    /// <returns></returns>
    [HttpGet]
    [Route("Treatment")]
    public async Task<ActionResult<TreatmentModel>> Treatment(string url)
    {
        if (string.IsNullOrEmpty(url) || !url.ToLower().Contains("www.amazon.de"))
        {
            Log.Logger.Information("api/Treatment was called but with an bad Url. BadRequest400");
            return BadRequest();
        }

        var output = await repo.WhenAllReady(url);

        if (output.Count > 0)
        {

            //Add to Database (Redis?)
            string[] good = new string[] { "sehr gut", "toll", "tolles puzzle", "schön", "super schön", "gut", "gutes puzzle",
                                "super", "macht spaß", "top", "top artikel", "wunderschön", "wunderschönes motiv", "perfekt", "spaß gemacht"};

            string[] bad = new string[] { "nicht gut", "nicht schön", "kein spaß gemacht", "Fehlendes Puzzelteil", "Teile Fehlen", "Fehlende Puzzleteile",
                                "schlechte Druckqualität", "fehlte ein Teil" };

            var calc = new QualityCalc(output, good, bad);

            Log.Logger.Information("api/Treatment was called and returned OK200");
            return Ok(calc.Judgement());
        }


        Log.Logger.Information("api/GetComments was called and returned NotFound404");
        return NotFound();
    }
}
