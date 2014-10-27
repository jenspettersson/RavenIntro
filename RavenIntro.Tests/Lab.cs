using System.Linq;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Linq;
using RavenIntro.Tests.Indexs;

namespace RavenIntro.Tests
{
    [TestFixture]
    public class Lab : FakeApplication
    {
        [Test]
        public void GetByCompany()
        {
            using (var session = NewSession())
            {
                var query = session.Query<CompaniesSearch.ReduceResult, CompaniesSearch>()
                                   .Customize(c => c.ShowTimings())
                                   .Where(x => x.Query == "usa")
                                   .As<Company>();

                var results = query.ToList();                      
            }
        }

        [Test]
        public void ProductSales()
        {
            using (var session = NewSession())
            {
                var query = session.Query<ProductSales.ReduceResult, ProductSales>()
                                   .Customize(c => c.ShowTimings())
                                   .TransformWith<ProductSalesTransformer, ProductSalesViewModel>();

                var results = query.ToList();
            }
        }
    }
}
