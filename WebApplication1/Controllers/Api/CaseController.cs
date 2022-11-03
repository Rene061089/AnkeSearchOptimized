using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessLogic;
using static WebApplication1.BusinessLogic.AzureSearch;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers.API
    {
    [Route("api/[controller]")]
    [ApiController]
    public class CaseController : ControllerBase
        {
        [HttpPost("search")]
        public Task<CaseContainer> Search(PostSearch data)
            {
            var result = new AzureSearch().Search(data).Result;
            return Task.FromResult(result);
            }     
        
        [HttpPost("rulingsbyid")]
        public Task<CaseContainer> GetRulingsbyId(int[] ids)
        {
            var result = new AzureSearch().GetRulingsbyId(ids).Result;
            return Task.FromResult(result);
        }

        [HttpGet("autocomplete")]
        public Task<List<string>> AutocompleteAsync(string term)
            {
            var result = new AzureSearch().Autocomplete(term);

            return result;
            }


        [HttpPost("suggest")]
        public Task<List<string>> SuggestAsync(bool fuzzy, string term)
            {
            var result = new AzureSearch().Suggest(fuzzy, term);

            return result;
            }

        }

    }
