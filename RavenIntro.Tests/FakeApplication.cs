using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using RavenIntro.Tests.Indexs;

namespace RavenIntro.Tests
{
    public class FakeApplication
    {
        private readonly DocumentStore _documentStore;

        public FakeApplication(bool buildIndexes = true)
        {
            _documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "IntroToRaven"
            };

            _documentStore.Initialize();

            if(buildIndexes)
            {
                IndexCreation.CreateIndexes(typeof(OrdersByCompany).Assembly, _documentStore);
            }
        }

        public IDocumentSession NewSession()
        {
            return _documentStore.OpenSession();
        }
    }
}