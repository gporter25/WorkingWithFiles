namespace Operations.NorthWindClasses
{
    public class CustomerItem : Customers
    {
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public override string ToString() => CompanyName;

    }
}