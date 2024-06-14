using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class Result
    {
        public string FirstNumber { get; set; }
        public string SecondNumber { get; set; }
        public string FirstCurrency { get; set; }
        public string SecondCurrency { get; set; }
        public string MathOperation { get; set; }
        public string Results {  get; set; }
        public DateTime Date {  get; set; }

    }
}
