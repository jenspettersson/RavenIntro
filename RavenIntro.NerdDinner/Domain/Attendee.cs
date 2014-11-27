namespace RavenIntro.NerdDinner.Domain
{
    public class Attendee
    {
        public string Name { get; private set; }
        public Gender Gender { get; private set; }

        public Attendee(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
        }
    }
}