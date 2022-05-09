using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaHW.Models;

namespace VismaHW
{
    public class DB
    {
        public static List<Meeting> meetings = new List<Meeting>();
        public static void SaveMeetings()
        {
            var save = JsonConvert.SerializeObject(meetings);
            File.WriteAllText("meetings.json", save);
        }

        public static void Load()
        {
            var load = File.ReadAllText("meetings.json");
            var duomenys = JsonConvert.DeserializeObject<List<Meeting>>(load);
            meetings = duomenys;
        }
    }
}
