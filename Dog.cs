namespace TestsLucas
{
    public class Dog
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        [CustomAttributes.HashIgnore()]
        public int Age { get; set; }
    }
}
