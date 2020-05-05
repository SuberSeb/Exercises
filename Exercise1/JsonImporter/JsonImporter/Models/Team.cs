using System.Collections.Generic;

namespace JsonImporter.Models
{
    internal class Team
    {
        public int TeamNumber { get; set; }
        public Detail Detail { get; set; }
        public List<Player> Players { get; set; }
        public Person Coach { get; set; }
        public Person AssistCoach1 { get; set; }
        public Person AssistCoach2 { get; set; }
    }
}