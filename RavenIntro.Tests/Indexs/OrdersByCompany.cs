using System;
using System.Linq;
using Raven.Client.Indexes;

namespace RavenIntro.Tests.Indexs
{
    public class OrdersByCompany : AbstractIndexCreationTask<Order, OrdersByCompany.ReduceResult>
    {
        public override string IndexName { get { return "Orders/ByCompany"; } }

        public OrdersByCompany()
        {
            Map = orders => from order in orders
                            select new
                            {
                                order.Company,
                                Count = 1,
                                Total = order.Lines.Sum(l => (l.Quantity*l.PricePerUnit)*(1 - l.Discount))
                            };

            Reduce = results => from result in results
                                group result by result.Company
                                into g
                                select new
                                {
                                    Company = g.Key,
                                    Count = g.Sum(x => x.Count),
                                    Total = g.Sum(x => x.Total)
                                };
        }

        public class ReduceResult
        {
            public string Company { get; set; }
            public int Count { get; set; }
            public Decimal Total { get; set; }
        }
    }
}
