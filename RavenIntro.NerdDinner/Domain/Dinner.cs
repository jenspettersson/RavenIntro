using System;
using System.Collections.Generic;

namespace RavenIntro.NerdDinner.Domain
{
    public class Dinner
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime When { get; set; }
        public List<Attendee> Attendees { get; set; }

        public Dinner(string name, DateTime when)
        {
            Name = name;
            When = when;
            Attendees = new List<Attendee>();
        }

        public void Register(string name, Gender gender)
        {
            Attendees.Add(new Attendee(name, gender));
        }
    }
}
