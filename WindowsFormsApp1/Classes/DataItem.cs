using System;

namespace WindowsFormsApp1.Classes
{
    public class DataItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public int District { get; set; }
        public string Beat { get; set; }
        public int Grid { get; set; }
        public string Description { get; set; }
        public int NcicCode { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool Inspect { get; set; }
        public string Line => $"{Id},{Date},{Address},{District},{Beat}," + 
                              $"{Grid},{Description},{NcicCode},{Latitude},{Longitude}";
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
