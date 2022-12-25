namespace Bad.Code.BadSmells._05MutableData
{
    public class Person
    {
        public int Id { get; set; }
        // name is required, at least 3 character
        // name is mutable
        public string Name { get; set; }
    }


}