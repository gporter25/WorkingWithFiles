namespace Operations.SacramentoClasses
{
    public class DataItemInvalid
    {
        public int Row { get; set; }
        public string Line { get; set; }
        public override string ToString()
        {
            return $"[{Row}] '{Line}'";
        }
    }
}
