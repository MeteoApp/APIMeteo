namespace APIMeteo.Models
{
    public class Alert
    {
        public int AlertId { get; set; }
        public required Measure Measure { get; set; }
        public string AlertMessage { get; set; } = string.Empty;
    }
}