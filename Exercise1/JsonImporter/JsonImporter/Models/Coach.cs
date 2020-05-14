using System.ComponentModel.DataAnnotations;

namespace JsonImporter.Models
{
    public class Coach
    {
        [Key]
        public int PersonId { get; set; }

        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string InternationalFamilyName { get; set; }
        public string InternationalFirstName { get; set; }
        public string ScoreboardName { get; set; }
        public string TVName { get; set; }
        public string NickName { get; set; }
        public string ExternalId { get; set; }
        public string NationalityCode { get; set; }
        public string NationalityCodeIOC { get; set; }
        public string Nationality { get; set; }
    }
}