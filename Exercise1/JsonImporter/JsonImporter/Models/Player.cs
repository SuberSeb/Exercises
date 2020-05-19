using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace JsonImporter.Models
{
    [Serializable]
    public class Player
    {
        [Key]
        public int PersonId { get; set; }

        public int Pno { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string InternationalFamilyName { get; set; }
        public string InternationalFirstName { get; set; }
        public string ScoreboardName { get; set; }
        public string TVName { get; set; }
        public string NickName { get; set; }
        public string Website { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Height { get; set; }
        public string ExternalId { get; set; }
        public string InternationalReference { get; set; }
        public string ShirtNumber { get; set; }
        public string PlayingPosition { get; set; }
        public YesOrNoEnum Starter { get; set; } = YesOrNoEnum.No;
        public YesOrNoEnum Captain { get; set; } = YesOrNoEnum.No;
        public YesOrNoEnum Active { get; set; } = YesOrNoEnum.No;
        public string NationalityCode { get; set; }
        public string NationalityCodeIOC { get; set; }
        public string Nationality { get; set; }

        [JsonIgnore]
        public int TeamId { get; set; }

        [JsonIgnore]
        public Team Team { get; set; }

        public override string ToString()
        {
            return $"{PersonId},{Pno},{FamilyName},{FirstName}," +
                $"{InternationalFamilyName},{InternationalFirstName},{ScoreboardName},{TVName}," +
                $"{NickName},{Website},{DateOfBirth},{Height}," +
                $"{ExternalId},{InternationalReference},{ShirtNumber},{PlayingPosition}," +
                $"{(int)Starter},{(int)Captain},{(int)Active},{NationalityCode}," +
                $"{NationalityCodeIOC},{Nationality},{TeamId}";
        }
    }
}