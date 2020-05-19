using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JsonImporter.Models
{
    [Serializable]
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public string Type { get; set; }
        public List<Team> Teams { get; set; }

        public override string ToString()
        {
            return $"{MessageId},{Type}";
        }
    }
}