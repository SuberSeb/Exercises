using System.Collections.Generic;

namespace JsonImporter.Models
{
    public class Team
    {
        public int TeamNumber { get; set; }
        public Detail Detail { get; set; }
        public List<Player> Players { get; set; }
        public Coach Coach { get; set; }
        public Coach AssistCoach1 { get; set; }
        public Coach AssistCoach2 { get; set; }
    }
}