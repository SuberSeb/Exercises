using System;
using System.ComponentModel.DataAnnotations;

namespace JsonImporter.Models
{
    [Serializable]
    public class Detail
    {
        [Key]
        public int DetailId { get; set; }

        public string TeamName { get; set; }
        public string TeamNameInternational { get; set; }
        public string ExternalId { get; set; }
        public string InternationalReference { get; set; }
        public string TeamNickname { get; set; }
        public string TeamCode { get; set; }
        public string TeamCodeLong { get; set; }
        public string TeamCodeInternational { get; set; }
        public string TeamCodeLongInternational { get; set; }
        public string TeamNicknameInternational { get; set; }
        public string CountryCode { get; set; }
        public string CountryCodeIOC { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        public YesOrNoEnum IsHomeCompetitor { get; set; }

        public override string ToString()
        {
            return $"[{DetailId},{TeamName},{TeamNameInternational},{ExternalId}," +
                $"{InternationalReference},{TeamNickname},{TeamCode},{TeamCodeLong}," +
                $"{TeamCodeInternational},{TeamCodeLongInternational},{TeamNameInternational},{CountryCode}," +
                $"{CountryCodeIOC},{Country},{Website},{(int)IsHomeCompetitor}]";
        }
    }
}