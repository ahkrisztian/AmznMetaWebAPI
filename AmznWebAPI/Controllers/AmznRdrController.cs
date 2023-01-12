using AmznMetaLibrary.Models;
using AmznMetaLibrary.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;
using System.Reflection.Emit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmznWebAPI.Controllers
{
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
        [HttpGet]
        public async Task<IEnumerable<ReviewModel>> GetComments(string url)
        {
            return await repo.WhenAllReady(url);           
        }

    }
}
