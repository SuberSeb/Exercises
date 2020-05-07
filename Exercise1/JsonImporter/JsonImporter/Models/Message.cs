namespace JsonImporter.Models
{
    public class Message
    {
        public string Type { get; set; }
        public int MessageId { get; set; }
        public Team[] Teams { get; set; }
    }
}