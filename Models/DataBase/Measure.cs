namespace APIMeteo.Models
{ 
    public class Measure
    {
        public int MeasureId { get; set; }
        public required Room Room { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double Altitude { get; set; }
    }
}