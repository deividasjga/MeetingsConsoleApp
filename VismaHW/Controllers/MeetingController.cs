using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaHW.DataEnum;
using VismaHW.Models;

namespace VismaHW.Controllers
{
    public static class MeetingController
    {
        public static void Create()
        {
            bool neraName = false;
            string name = "";
            while(neraName!=true)
            {
                Console.WriteLine("Name: ");
                string pavadinimas = Console.ReadLine();
                Meeting meetingCR = DB.meetings.Find(meetingCR => meetingCR.Name == pavadinimas);
                if (meetingCR == null)
                {
                    name = pavadinimas; neraName = true;
                }
                else { Console.WriteLine("This name already exists!"); }
            }

            Console.WriteLine("Responsible person: ");
            string responsibleperson = Console.ReadLine();
            Console.WriteLine("Description: ");
            string description = Console.ReadLine();
            bool cat = false;
            string category="";
            while (cat!=true)
            {
                Console.WriteLine("Category (select one): 1. CodeMonkey, 2. Hub, 3. Short, 4. TeamBuilding");
                category = Console.ReadLine();
                if (category == "1") { category = CategoryCategory.CodeMonkey.ToString(); cat = true; }
                else if (category == "2") { category = CategoryCategory.Hub.ToString(); cat = true; }
                else if (category == "3") { category = CategoryCategory.Short.ToString(); cat = true; }
                else if (category == "4") { category = CategoryCategory.TeamBuilding.ToString(); cat = true; }
                else { Console.WriteLine("Wrong input!"); }
            }
            bool ty = false;
            string type = "";
            while (ty != true)
            {
                Console.WriteLine("Type (select one): 1. Live, 2. InPerson");
                type = Console.ReadLine();
                if (type == "1") { type = TypeCategory.Live.ToString(); ty = true; }
                else if (type == "2") { type = TypeCategory.InPerson.ToString(); ty = true; }
                else { Console.WriteLine("Wrong input!"); }
            }

            Console.WriteLine("Start date (dd/MM/yyyy): ");
            string startdate = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParseExact(startdate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Invalid date! Try again.");
                startdate = Console.ReadLine();
            }

            Console.WriteLine("End date (dd/MM/yyyy): ");
            string enddate = Console.ReadLine();
            while (!DateTime.TryParseExact(enddate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Invalid date! Try again.");
                enddate = Console.ReadLine();
            }

            Meeting meeting = new Meeting(name, responsibleperson, description, category, type, startdate, enddate);
            DB.meetings.Add(meeting);
            DB.SaveMeetings();
            Console.WriteLine($"Created a new meeting {name} !");
            Console.WriteLine("");
        }



        public static void Delete()
        {
            bool grizti = false;
            while (grizti != true)
            {
                List();
                Console.WriteLine("");
                Console.WriteLine("Enter meetings name you want to delete:         (Enter B to go back)");
                string pavadinimas = Console.ReadLine();
                if(pavadinimas=="b" || pavadinimas == "B")
                { 
                    grizti = true;
                    break; 
                }
                Meeting meeting = DB.meetings.Find(meeting => meeting.Name == pavadinimas);
                if (meeting != null)
                {
                    try {
                        string RespPerName = "";
                        Console.WriteLine("Enter your name: ");
                        RespPerName = Console.ReadLine();
                        if (DB.meetings.Find(meeting => meeting.ResponsiblePerson == RespPerName) == null)
                        {
                            Console.WriteLine("You cannot delete this meeting!");
                            return;
                        }
                        else
                        {

                            DB.meetings.Remove(meeting);
                            Console.WriteLine($"You just deleted a meeting named {pavadinimas}!");
                            Console.WriteLine("");
                            DB.SaveMeetings();
                            grizti = true;
                        }
                        }catch (Exception ex) { Console.WriteLine("Error! Wrong input!");  return; }
                } 
                else { Console.WriteLine("Meeting not found!"); }
            }
        }


        public static void AddPerson()
        {
            bool pridetas = false;
            var countMeetings = DB.meetings.Count();
            if (countMeetings == 0)
            {
                Console.WriteLine("There are no meetings!");
                Console.WriteLine();
                return; 
            }

            while (pridetas != true)
            {
                List();
                Console.WriteLine("");
                Console.WriteLine("Meeting's name:                                  (Enter B to go back)");
                string meetingName = Console.ReadLine();
                if (meetingName == "b" || meetingName == "B") { pridetas = true; Console.WriteLine(""); return; }
                Meeting meeting = DB.meetings.Find(meeting => meeting.Name == meetingName);
                if (meeting != null)
                {
                    Console.WriteLine("Person's name to add: ");
                    string person = Console.ReadLine();
                    if (person == "b" || person == "B") { pridetas = true; Console.WriteLine(""); return; }
                    People people = new People(person);
                    if (meeting.People.Find(people => people.PersonName == person) != null)
                    {
                        Console.WriteLine("This person is already in the list!");
                        return;
                    }
                    List<Meeting> peopleMeetings = new List<Meeting>();

                    foreach (Meeting meetingtwo in DB.meetings)
                    {
                        if (meetingtwo.People.Exists(x => x.PersonName == people.PersonName) != null)
                        {
                            peopleMeetings.Add(meetingtwo);
                        }
                    }

                    Meeting updatedMeeting = meeting;
                    updatedMeeting.People.Add(people);
                    DB.meetings.Remove(meeting);
                    DB.meetings.Add(updatedMeeting);
                    DB.SaveMeetings();
                    Console.WriteLine($"Successfully added a new person to {meetingName}!");
                    pridetas = true;

                }
                else { Console.WriteLine("Meeting not found!"); }
            }
        }


        public static void RemovePerson()
        {
            var countMeetings = DB.meetings.Count();
            if (countMeetings == 0)
            {
                Console.WriteLine("There are no meetings!");
                Console.WriteLine();
                return;
            }
            bool istrintas = false;
            while (istrintas != true)
            {
                List();
                Console.WriteLine("");
                Console.WriteLine("Meeting's name:                                  (Enter B to go back)");
                string meetingName = Console.ReadLine();
                if (meetingName == "b" || meetingName == "B") { istrintas = true; Console.WriteLine(""); return; }
                Meeting meeting = DB.meetings.Find(meeting => meeting.Name == meetingName);
                if (meeting != null)
                {
                    foreach(var person in meeting.People)
                    {
                        Console.WriteLine($"People list from {meetingName}:");
                        Console.WriteLine($"(-)  {person.PersonName} ");
                    }
                    Console.WriteLine("");
                    bool istrintasDu = false;
                    while(istrintasDu!= true)
                    {
                        Console.WriteLine("Person to delete: ");
                        string whoDelete = Console.ReadLine();
                        if (whoDelete == "b" || whoDelete == "B") { istrintasDu = true; Console.WriteLine(""); return; }
                        foreach (Meeting meetingtwo in DB.meetings)
                        {
                            if (meetingtwo.People.Find(person => person.PersonName == whoDelete) != null)
                            {
                                if(meetingtwo.ResponsiblePerson == whoDelete)
                                {
                                    Console.WriteLine("You have no permission to remove this person!");
                                    break;  //
                                }
                                else if(meetingtwo.ResponsiblePerson != whoDelete)
                                {
                                    People willDelete = meeting.People.FirstOrDefault(person => person.PersonName == whoDelete);

                                    Meeting oldMeeting = meeting;
                                    meeting.People.Remove(willDelete);
                                    DB.meetings.Remove(oldMeeting);
                                    DB.meetings.Add(meeting);
                                    DB.SaveMeetings();
                                    Console.WriteLine($"Removed {meetingName} from the meeting!");

                                    istrintasDu = true;
                                    istrintas = true;
                                    return;

                                }
                            }
                            else { Console.WriteLine("Person not found!"); }
                        }
                    }
                }
                else { Console.WriteLine("Meeting not found!"); }
            }
        }


        public static void List()
        {
            Console.WriteLine("All meetings: ");
            foreach (Meeting meeting in DB.meetings)
            {
                Console.WriteLine("(*) Name: " + meeting.Name);
            }
        }

        public static void ListAllMeetings()
        {
            bool grizti = false;
            while (grizti != true)
            {
                List();
                string parameter = "";
                //string ats = "";
                string search = "";
                bool filtruota = false;
                string p = "";
                IEnumerable<Meeting> ats = null;
                while (filtruota != true)
                {
                    Console.WriteLine("Choose how to filter the data:                           (Enter B to go back)");
                    Console.WriteLine("-----------------------------------------------------------------------------");
                    Console.WriteLine(" 1. Filter by description                4. Filter by type ");
                    Console.WriteLine(" 2. Filter by responsible person         5. Filter by dates");
                    Console.WriteLine(" 3. Filter by category                   6. Filter by the number of attendees");
                    Console.WriteLine("-----------------------------------------------------------------------------");
                    Console.WriteLine("");
                    parameter = Console.ReadLine();
                    if(parameter == "b" || parameter == "B") { grizti = true; Console.WriteLine(""); return; }
                    if (parameter == "1")
                    {
                        Console.WriteLine("search: ");
                        search = Console.ReadLine();
                        ats = DB.meetings.Where(meeting => meeting.Description.Contains(search)).ToList();
                        filtruota = true;
                    }
                    else if (parameter == "2")
                    {
                        Console.WriteLine("search: ");
                        search = Console.ReadLine();
                        ats = DB.meetings.Where(meeting => meeting.ResponsiblePerson.Contains(search)).ToList();
                        filtruota = true;
                    }
                    else if (parameter == "3")
                    {
                        Console.WriteLine("search: ");
                        search = Console.ReadLine();
                        ats = DB.meetings.Where(meeting => meeting.Category.Contains(search)).ToList();
                        filtruota = true;
                    }
                    else if (parameter == "4")
                    {
                        Console.WriteLine("search: ");
                        search = Console.ReadLine();
                        ats = DB.meetings.Where(meeting => meeting.Type.Contains(search)).ToList();
                        filtruota = true;
                    }
                    else if (parameter == "5")
                    {
                        ats = DB.meetings.Where(meeting => meeting.StartDate.Contains(search)).ToList();   
                        filtruota = true;
                    }
                    else if (parameter == "6")
                    {
                        string howMany = "";
                        Console.WriteLine("Minimum amount of people attending a meeting: ");
                        try
                        {
                            howMany = Console.ReadLine();
                            ats = DB.meetings.Where(meeting => meeting.People.Count >= Int32.Parse(howMany)).ToList();
                            filtruota = true;
                        }
                        catch (Exception ex) { Console.WriteLine("Error! Wrong input!"); return; }
                    }
                    else { Console.WriteLine("Wrong input!"); }
                }

                Console.WriteLine("Filtered: ");
                foreach (Meeting meeting in ats)
                {
                    Console.WriteLine("[after filter] Name: " + meeting.Name);

                }
                Console.WriteLine("");
                Console.WriteLine(".........................................");
                Console.WriteLine("");
            }
        }
}
}
