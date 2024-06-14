using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorWebAPI.Entities
{
    [Table("Currencies")]
    public class Currency
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Cur_ID")]
        public int Cur_ID { get; internal set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Cur_Abbreviation")]
        public string Cur_Abbreviation { get; set; }

        [Column("Cur_Scale")]
        public int Cur_Scale { get; set; }

        [Column("Cur_Name")]
        public string Cur_Name { get; set; }

        [Column("Cur_OfficialRate")]
        public decimal Cur_OfficialRate { get; set; }
    }
}
