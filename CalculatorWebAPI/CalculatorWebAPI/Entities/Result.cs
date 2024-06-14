using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorWebAPI.Entities
{
    [Table("Results")]
    public class Result
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("FirstNumber")]
        public string FirstNumber { get; set; }

        [Column("SecondNumber")]
        public string SecondNumber { get; set; }

        [Column("FirstCurrency")]
        public string FirstCurrency { get; set; }

        [Column("SecondCurrency")]
        public string SecondCurrency { get; set; }

        [Column("MathOperation")]
        public string MathOperation { get; set; }

        [Column("Result")]
        public decimal Results { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

    }
}
