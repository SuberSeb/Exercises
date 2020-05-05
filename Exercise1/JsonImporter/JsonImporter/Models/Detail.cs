﻿namespace JsonImporter.Models
{
    internal enum YesOrNo : byte
    {
        No,
        Yes
    }

    internal class Detail
    {
        public string TeamName { get; set; }
        public string TeamNameInternational { get; set; }
        public int TeamId { get; set; }
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
        public YesOrNo IsHomeCompetitor { get; set; }
    }
}