using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JsonImporter.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public string Type { get; set; }
        public List<Team> Teams { get; set; }
    }
}