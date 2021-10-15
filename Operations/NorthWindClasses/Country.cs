namespace Operations.NorthWindClasses
{
    public class Country
    {
        public int CountryIdentifier { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;

    }
}
