using System.Linq;
using Raven.Client.Indexes;

namespace RavenIntro.Tests.Indexs
{
    public class ProductSales : AbstractIndexCreationTask<Order, ProductSales.ReduceResult>
    {
        public override string IndexName { get { return "Product/Sales"; } }

        public ProductSales()
        {
            Map = orders => from order in orders
                from line in order.Lines
                select new
                {
                    Product = line.Product,
                    Count = 1,
                    Total = (line.Quantity * line.PricePerUnit) * (1 - line.Discount)
                };

            Reduce = results => from result in results
                group result by result.Product
                into g
                select new
                {
                    Product = g.Key,
                    Count = g.Sum(x => x.Count),
                    Total = g.Sum(x => x.Total)
                };
        }

        public class ReduceResult
        {
            public string Product { get; set; }
            public int Count { get; set; }
            public decimal Total { get; set; }
        }
    }

    public class ProductSalesTransformer : AbstractTransformerCreationTask<ProductSales.ReduceResult>
    {
        public ProductSalesTransformer()
        {
            TransformResults = results => from result in results
                                          let product = LoadDocument<Product>(result.Product)
                                          select new ProductSalesViewModel
                                          {
                                              Product = product.Id,
                                              ProductName = product.Name,
                                              Count = result.Count,
                                              Total = result.Total
                                          };
        }
    }

    public class ProductSalesViewModel
    {
        public string Product { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} total: {2}", ProductName, Count, Total);
        }
    }
}