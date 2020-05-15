using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JsonImporter.Models
{
    [Serializable]
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public int TeamNumber { get; set; }
        public Detail Detail { get; set; }
        public List<Player> Players { get; set; }
        public Coach Coach { get; set; }
        public Coach AssistCoach1 { get; set; }
        public Coach AssistCoach2 { get; set; }
    }
}