using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using RavenIntro.Tests.Indexs;

namespace RavenIntro.Tests
{
    public class FakeApplication
    {
        private readonly DocumentStore _documentStore;

        public FakeApplication()
        {
            _documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "RavenIntro"
            };

            _documentStore.Initialize();

            IndexCreation.CreateIndexes(typeof(OrdersByCompany).Assembly, _documentStore);
        }

        public IDocumentSession NewSession()
        {
            return _documentStore.OpenSession();
        }
    }
}