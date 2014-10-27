using System.Linq;
using Raven.Client.Indexes;

namespace RavenIntro.Tests.Indexs
{
    public class CompaniesSearch : AbstractIndexCreationTask<Company, CompaniesSearch.ReduceResult>
    {
        public CompaniesSearch()
        {
            Map = companies => from company in companies
                select new
                {
                    Query = new object[]
                    {
                        company.Id,
                        company.ExternalId,
                        company.Name,
                        company.Contact.Name,
                        company.Address.Line1,
                        company.Address.Line2,
                        company.Address.City,
                        company.Address.Region,
                        company.Address.PostalCode,
                        company.Address.Country,
                    }
                };
        }

        public class ReduceResult
        {
            public string Query { get; set; }
        }
    }
}