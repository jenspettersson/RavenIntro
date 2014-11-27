using System;
using NUnit.Framework;
using Raven.Client.Document;
using RavenIntro.NerdDinner.Domain;

namespace RavenIntro.NerdDinner
{
    [TestFixture]
    public class Runner
    {
        private DocumentStore _documentStore;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "NerdDinner"
            };

            _documentStore.Initialize();
        }

        [Test]
        public void CreateDinner()
        {
            using (var session = _documentStore.OpenSession())
            {
                var dinner = new Dinner("AO-möte", new DateTime(2014, 11, 27));

                session.Store(dinner);
                session.SaveChanges();
            }
        }

        [Test]
        public void LoadDinner()
        {
            using (var session = _documentStore.OpenSession())
            {
                var dinner = session.Load<Dinner>("dinners/1");
                
                dinner.Register("Jens Pettersson", Gender.Male);

                session.SaveChanges();
            }
        }

        [Test]
        public void CreateManyDinners()
        {
            var random = new Random();
            using (var session = _documentStore.OpenSession())
            {
                var dateTime = new DateTime(2014, 11, 01);
                for (int i = 0; i < 1500; i++)
                {
                    var dinner = new Dinner("Dinner " + i + 1, dateTime);
                    session.Store(dinner);

                    dateTime = dateTime.AddDays(random.Next(1, 15));
                }

                session.SaveChanges();
            }
        }
    }
}