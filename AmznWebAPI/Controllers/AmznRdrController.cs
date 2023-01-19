using AmznMetaLibrary.Calculator;
using AmznMetaLibrary.CreateLinks;
using AmznMetaLibrary.Models;
using AmznMetaLibrary.Models.DTOs;
using AmznMetaLibrary.Models.OpinionModels;
using AmznMetaLibrary.Repo;
using AmznMetaLibrary.Repo.Data;
using AutoMapper;
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
    private readonly IDataRepo data;
    private readonly IMapper mapper;

    public AmznRdrController(IAmznMetaRepo repo, IDataRepo data, IMapper mapper)
    {
        this.repo = repo;
        this.data = data;
        this.mapper = mapper;
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

            string[] good = data.GetallGoodOps().Result.Select(x => x.Name).ToArray();

            string[] bad = data.GetallBadOps().Result.Select(x => x.Name).ToArray();

            var calc = new QualityCalc(output, good, bad);

            Log.Logger.Information("api/Treatment was called and returned OK200");
            return Ok(calc.Judgement());
        }


        Log.Logger.Information("api/GetComments was called and returned NotFound404");
        return NotFound();
    }

    [HttpPost]
    [Route("CreateBadOp")]
    public ActionResult<Badop> CreateBadOp(CreateBadOpDTO badopdto)
    {
        if(badopdto == null)
        {
            Log.Logger.Information("api/CreateBadOp was called but with a bad BadOpDTO Model");
            return BadRequest();
        }

        var badopmodel = mapper.Map<Badop>(badopdto);

        var result = data.CreateBadOp(badopmodel);

        Log.Logger.Information("api/CreateBadOp was called and returned OK200. Result Id:{id}", result.Result.Id);
        return Ok(result.Result);

    }

    [HttpPost]
    [Route("CreateGoodOp")]
    public ActionResult<GoodOp> CreateGoodOp(CreateGoodOpDTO goodopdto)
    {
        if(goodopdto == null)
        {
            Log.Logger.Information("api/CreateGoodOp was called but with a bad GoodOpDTO Model");
            return BadRequest();
        }

        var goodmodel = mapper.Map<GoodOp>(goodopdto);

        var result = data.CreateGoodOp(goodmodel);

        Log.Logger.Information("api/CreateGoodOp was called and returned OK200. Result Id:{id}", result.Result.Id);
        return Ok(result.Result);
    }

    [HttpGet]
    [Route("GetGoods")]
    public ActionResult<IEnumerable<GoodOp>> GetGoods()
    {
        var output = data.GetallGoodOps();

        if(output == null)
        {
            Log.Logger.Information("api/GetGoods was called and returned NoContent204");
            return NoContent();
        }

        Log.Logger.Information("api/GetGoods was called and returned OK200");
        return Ok(output);
    }

    [HttpGet]
    [Route("GetBads")]
    public ActionResult<IEnumerable<Badop>> GetBads()
    {
        var output = data.GetallBadOps();

        if (output == null)
        {
            Log.Logger.Information("api/GetBads was called and returned NoContent204");
            return NoContent();
        }

        Log.Logger.Information("api/GetBads was called and returned OK200");
        return Ok(output);
    }

    [HttpGet]
    [Route("GetBadOpById/{id}")]

    public async Task<ActionResult<Badop>> GetBadOpById(int id)
    {
        if (id <= 0)
        {
            Log.Logger.Information("api/GetBadOpById/id was called but with an BadRequest400 Id: {id}", id);
            return BadRequest();
        }

        var result = await data.GetBadOpById(id);

        if (result == null)
        {
            Log.Logger.Information("/api/GetBadOpById/{id} was called and returned NoContent201", id);
            return NoContent();
        }

        Log.Logger.Information("/api/GetBadOpById/{id} was called and returned OK200", id);
        return await data.GetBadOpById(id);
    }

    [HttpGet]
    [Route("GetGoodOpById/{id}")]
    public async Task<ActionResult<GoodOp>> GetGoodOpById(int id)
    {
        if (id <= 0)
        {
            Log.Logger.Information("api/GetGoodOpById/id was called but with an BadRequest400 Id: {id}", id);
            return BadRequest();
        }

        var result = await data.GetGoodOpById(id);

        if (result == null)
        {
            Log.Logger.Information("/api/GetGoodOpById/{id} was called and returned NoContent201", id);
            return NoContent();
        }

        Log.Logger.Information("/api/GetGoodOpById/{id} was called and returned OK200", id);
        return await data.GetGoodOpById(id);
    }
}
