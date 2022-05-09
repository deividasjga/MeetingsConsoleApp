using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaHW.DataEnum;

namespace VismaHW.Models
{

    public class Meeting
    {
        public Meeting(string? name, string? responsibleperson, string? description, string? category, string? type, string? startdate, string? enddate)
        {
            Name = name;
            ResponsiblePerson = responsibleperson;
            Description = description;
            Category = category;
            Type = type;
            StartDate = startdate;
            EndDate = enddate;
            People = new List<People>
            {
                new People(responsibleperson)
            };
        }

        public string? Name { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }           
        public string? Type { get; set; }               
        public string? StartDate { get; set; }           
        public string? EndDate { get; set; }            
        public List<People> People { get; set; }

    }
}
