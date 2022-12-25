namespace Bad.Code.BadSmells._10Primitive_Obsession
{
    /// <summary>
    ///  https://hackernoon.com/what-is-primitive-obsession-and-how-can-we-fix-it-wh2f33ki
    /// </summary>
    public class Person
    {
        public Person(string id,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string city,
            string country)
        {
        }

        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public void ChangeAddress(string address, string postcode, string city, string country)
        {
            // change address logic
        }
    }
}