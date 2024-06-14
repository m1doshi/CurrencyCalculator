﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculatorWebAPI.Models
{
    public class CurrencyModel
    {
        public int Id { get; set; }

        public int Cur_ID { get; set; }

        public DateTime Date { get; set; }

        public string Cur_Abbreviation { get; set; }

        public int Cur_Scale { get; set; }

        public string Cur_Name { get; set; }

        public decimal Cur_OfficialRate { get; set; }
    }
}
