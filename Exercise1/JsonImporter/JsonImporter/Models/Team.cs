using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonImporter.Models
{
    [Serializable]
    public class Team
    {
        [JsonIgnore]
        [Key]
        public int TeamId { get; set; }

        public int TeamNumber { get; set; }
        public Detail Detail { get; set; }
        public List<Player> Players { get; set; }

        [ForeignKey("CoachId")]
        public Coach Coach { get; set; }

        [ForeignKey("AssistCoachId1")]
        public Coach AssistCoach1 { get; set; }

        [ForeignKey("AssistCoachId2")]
        public Coach AssistCoach2 { get; set; }

        [JsonIgnore]
        public int MessageId { get; set; }

        [JsonIgnore]
        public Message Message { get; set; }

        public override string ToString()
        {
            return $"{TeamId},{TeamNumber},{Detail.DetailId},{Coach.PersonId},{AssistCoach1.PersonId},{AssistCoach2.PersonId},{MessageId}";
        }
    }
}