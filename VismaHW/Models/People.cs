using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaHW.Models
{
    public class People
    {
        //private string? responsibleperson;
        //private DataTime? startdate;

        public People(string? personame) //, DateTime? timestart)
        {
            PersonName = personame;
            TimeStart = DateTime.Now;
        }

        public string? PersonName { get; set; }
        public DateTime? TimeStart { get; set; }

    }
}
