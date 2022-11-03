
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace WebApplication1.BusinessLogic
{

    public class AzureSearch
    {

        public async Task<List<string>> Autocomplete(string term)
        {

            SearchClient searchClient = CreateSearchClientForQueries("azuresql-index4");
            var ap = new AutocompleteOptions()
            {
                Mode = AutocompleteMode.OneTermWithContext,
                Size = 10,
                UseFuzzyMatching = true

            };
            var autocompleteResult = await searchClient.AutocompleteAsync(term, "sg", ap).ConfigureAwait(false);

            // Convert the autocompleteResult results to a list that can be displayed in the client.
            List<string> autocomplete = autocompleteResult.Value.Results.Select(x => x.Text).ToList();

            return autocomplete;
        }


        public async Task<List<string>> Suggest(bool fuzzy, string term)
        {
            SearchClient searchClient = CreateSearchClientForQueries("azuresql-index4");
            SuggestOptions options = new SuggestOptions()
            {
                UseFuzzyMatching = fuzzy,
                Size = 10,

            };
            var suggestResult = await searchClient.SuggestAsync<Cases>(term, "sg", options).ConfigureAwait(false);
            // Convert the suggestions results to a list that can be displayed in the client
            List<string> suggestions = suggestResult.Value.Results.Select(x => x.Text).ToList();
            return suggestions;


        }

        public async Task<CaseContainer> GetRulingsbyId(int[] ids)
        {
            SearchClient searchClient = CreateSearchClientForQueries("azuresql-index4");

            SearchOptions options = new SearchOptions()
            {
                IncludeTotalCount = true,
                Size = 100

            };

            List<string> filter = new List<string>();
            foreach (int item in ids)
            {
                filter.Add("(CASE_NUMBER eq '"+item+"')");

            }
            var sfilter = String.Join(" or ", filter);


            options.Filter = sfilter;

            var results = await searchClient.SearchAsync<Cases>("*", options);
            List<Cases> documents = new List<Cases>();
            List<FInsuranceType> fInsuranceType = new List<FInsuranceType>();
            List<FCompanyName> fCompanyName = new List<FCompanyName>();

            foreach (var s in results.Value.GetResults())
            {

                documents.Add(new Cases
                {
                    CASE_NUMBER = s.Document.CASE_NUMBER,
                    DOCUMENT_ID = s.Document.DOCUMENT_ID,
                    PRINCIPLE = s.Document.PRINCIPLE,
                    SUMMARY = s.Document.SUMMARY,
                    CLOSED_DATE = s.Document.CLOSED_DATE,
                    CompanyID = s.Document.CompanyID,
                    CompanyName = s.Document.CompanyName,
                    CompanyNameAndID = s.Document.CompanyNameAndID,
                    CREATION_DATE = s.Document.CREATION_DATE,
                    InsuranceType = s.Document.InsuranceType,
                    RESULT = s.Document.RESULT,
                    SearchWords = s.Document.SearchWords,
                    UPDATE_DATE = s.Document.UPDATE_DATE,


                });
            }
            CaseContainer caseContainer = new CaseContainer();
            caseContainer.cases = documents;
            caseContainer.total = results.Value.TotalCount;
            caseContainer.fInsuranceType = fInsuranceType;
            caseContainer.fCompanyName = fCompanyName;
            return caseContainer;

        }

        public int pagesToLoad(int arg)
        {
            
            if (arg > 10)
            {
                arg = 10;
            }
            return arg;
        }

        public string dataOrderBy(string sBy, string oBy)
        {
            var orderBy = "";
            if (sBy == null)
            {
                orderBy = "CLOSED_DATE";
            }
            else
            {
                orderBy = sBy;
            }
            if (oBy == null)
            {
                orderBy += " desc";
            }
            else
            {
                orderBy += " " + oBy;
            }

            return orderBy;
        }

        public async Task<CaseContainer> Search(PostSearch data)
        {
            SearchClient searchClient = CreateSearchClientForQueries("azuresql-index4");

            var orderBy = dataOrderBy(data.searchby, data.orderby);
            var loadpages = pagesToLoad(data.pages);

            data.words = data.words == "" ? "*" : data.words;
            var words = String.Join("+", data.words.Split(' '));

            List<string> filters = new List<string>();

            List<string> companies = new List<string>();
            if (data.company.Length > 0)
            {
                for (int i = 0; i < data.company.Length; i++)
                {
                    companies.Add("CompanyName eq '" + data.company[i] + "'");
                }

                filters.Add("(" + String.Join(" or ", companies) + ")");
            }

            List<string> insuranceTypes = new List<string>();
            if (data.insuranceType.Length > 0)
            {
                for (int i = 0; i < data.insuranceType.Length; i++)
                {
                    insuranceTypes.Add("InsuranceType eq '" + data.insuranceType[i] + "'");
                }

                filters.Add("(" + String.Join(" or ", insuranceTypes) + ")");

            }

            List<string> datefrom = new List<string>();
            if (data.from != "")
            {

                datefrom.Add("CLOSED_DATE ge " + data.from + "T00:00:00z" );

                filters.Add("(" + String.Join(" and ", datefrom) + ")");

            }
            List<string> dateto = new List<string>();
            if (data.to != "")
            {

                dateto.Add("CLOSED_DATE le " + data.to + "T00:00:00z");

                filters.Add("(" + String.Join(" and ", dateto) + ")");

            }

            if (data.principal == true)
            {
                filters.Add("PRINCIPLE eq true");
            }
     
            if (data.complaintsUpheld == true || data.companyComplaintsUpheld == true || data.complaintsPartlyUpheld == true || data.paragraph4 == true)
            {
                List<string> result = new List<string>();
                if (data.complaintsUpheld == true)
                {
                    result.Add("RESULT eq 'KLAGER MEDHOLD'");
                }
                if (data.companyComplaintsUpheld == true)
                {
                    result.Add("RESULT eq 'SELSKAB MEDHOLD'");
                }
                if (data.complaintsPartlyUpheld == true)
                {
                    result.Add("RESULT eq 'DELVIS MEDHOLD'");
                }
                if (data.paragraph4 == true)
                {
                    result.Add("RESULT eq 'PARAGRAF 4'");
                }

                if (result.Count > 0 && data.principal == true)
                {
                    filters[0] = filters[0];
                }

                filters.Add("(" + String.Join(" or ", result) + ")");

            }
            var filter = String.Join(" and ", filters);

            SearchOptions options = new SearchOptions()
            {
                IncludeTotalCount = true,
                Size = loadpages * 10,

            };

            options.OrderBy.Add(orderBy);

            if (filter.Length > 0)
            {
                options.Filter = filter;
            }
            options.Facets.Add("CompanyName,count:50");
            options.Facets.Add("InsuranceType,count:50");

            var results = await searchClient.SearchAsync<Cases>(words, options);
            List<Cases> documents = new List<Cases>();
            List<FInsuranceType> fInsuranceType = new List<FInsuranceType>();
            List<FCompanyName> fCompanyName = new List<FCompanyName>();

            foreach (var s in results.Value.GetResults())
            {

                documents.Add(new Cases
                {
                    CASE_NUMBER = s.Document.CASE_NUMBER,
                    DOCUMENT_ID = s.Document.DOCUMENT_ID,
                    PRINCIPLE = s.Document.PRINCIPLE,
                    SUMMARY = s.Document.SUMMARY,
                    CLOSED_DATE = s.Document.CLOSED_DATE,
                    CompanyID = s.Document.CompanyID,
                    CompanyName = s.Document.CompanyName,
                    CompanyNameAndID = s.Document.CompanyNameAndID,
                    CREATION_DATE = s.Document.CREATION_DATE,
                    InsuranceType = s.Document.InsuranceType,
                    RESULT = s.Document.RESULT,
                    SearchWords = s.Document.SearchWords,
                    UPDATE_DATE = s.Document.UPDATE_DATE,

                });
            }
            if (results.Value.Facets != null)
            {
                foreach (var f in results.Value.Facets)
                {
                    if (f.Key == "InsuranceType")
                    {
                        foreach (var insurance in f.Value)
                        {
                            fInsuranceType.Add(new FInsuranceType() { name = insurance.Value.ToString(), count = insurance.Count });
                        }

                    }
                    else if (f.Key == "CompanyName")
                    {
                        foreach (var company in f.Value)
                        {
                            fCompanyName.Add(new FCompanyName() { name = company.Value.ToString(), count = company.Count });
                        }
                    }
                }
            }

            CaseContainer caseContainer = new CaseContainer();
            caseContainer.cases = documents;
            caseContainer.total = results.Value.TotalCount;
            caseContainer.fInsuranceType = fInsuranceType;
            caseContainer.fCompanyName = fCompanyName;
            return caseContainer;
        }

        private static SearchClient CreateSearchClientForQueries(string indexName)
        {
            string searchServiceEndPoint = "https://ankesearchdev.search.windows.net";
            string queryApiKey = "qCeqDEc6mvgEmAE6RdhCD9zUqxqy0Bw1RkAxLHRZvZAzSeD371oz";

            SearchClient searchClient = new SearchClient(new Uri(searchServiceEndPoint), indexName, new AzureKeyCredential(queryApiKey));
            return searchClient;
        }

        public class Cases
        {
            public string? CASE_NUMBER { get; set; }
            public string? SUMMARY { get; set; }
            public bool PRINCIPLE { get; set; }
            public int DOCUMENT_ID { get; set; }
            public DateTime? CREATION_DATE { get; set; }
            public DateTime? UPDATE_DATE { get; set; }
            public DateTime? CLOSED_DATE { get; set; }
            public string RESULT { get; set; }
            public string InsuranceType { get; set; }
            public string CompanyName { get; set; }
            public int CompanyID { get; set; }
            public string CompanyNameAndID { get; set; }
            public string SearchWords { get; set; }

        }
        public class CaseContainer
        {
            public List<Cases> cases { get; set; }
            public long? total { get; set; }         
            public List<FInsuranceType> fInsuranceType { get; set; }
            public List<FCompanyName> fCompanyName { get; set; }
        }

        public class FInsuranceType
        {
            public string name { get; set; }
            public long? count { get; set; }

        }
        public class FCompanyName
        {
            public string name { get; set; }
            public long? count { get; set; }

        }
    }
}
