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
        //private ICaseRepository _data;
        //public CaseController(ICaseRepository data)
        //{
        //    _data = data;
        //}


        // GET: api/<ValuesController>

        [HttpPost("search")]
        //[ActionName("CaseApi")]
        //[Route("api/case/search")]
        //public Task<CaseContainer> Get(string words, bool? principal, bool? complaintsUpheld, bool? companyComplaintsUpheld, bool? complaintsPartlyUpheld, bool? paragraph4)
        //[AcceptVerbs("POST", "GET")]
        public Task<CaseContainer> Search(PostSearch data)
            {
            //var result = new AzureSearch().Search(words, principal, complaintsUpheld, companyComplaintsUpheld, complaintsPartlyUpheld, paragraph4).Result;
            var result = new AzureSearch().Search(data).Result;
            return Task.FromResult(result);
            }
        
        
        
        [HttpPost("rulingsbyid")]
        public Task<CaseContainer> GetRulingsbyId(int[] ids)
        {
            //var result = new AzureSearch().Search(words, principal, complaintsUpheld, companyComplaintsUpheld, complaintsPartlyUpheld, paragraph4).Result;
            var result = new AzureSearch().GetRulingsbyId(ids).Result;
            return Task.FromResult(result);
        }


        // GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ValuesController>/5

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



        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        }



    }
