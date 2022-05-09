using VismaHW;
using VismaHW.Controllers;

DB.Load();
Menu();

void Menu()
{
    while (true)
    {
        Console.WriteLine(" 1. Create a new meeting");
        Console.WriteLine(" 2. Delete a meeting");
        Console.WriteLine(" 3. Add a person to the meeting");
        Console.WriteLine(" 4. Remove a person from the meeting");
        Console.WriteLine(" 5. List all the meetings");
        Console.WriteLine(" 6. Exit");
        var input = Console.ReadLine();
        switch (input)
        {
            case "1":
                Console.WriteLine("-- Create --");
                MeetingController.Create();
                break;

            case "2":
                Console.WriteLine("-- Delete --");
                MeetingController.Delete();
                break;

            case "3":
                Console.WriteLine("-- Add a person to the meeting --");
                MeetingController.AddPerson();
                break;

            case "4":
                Console.WriteLine("-- Remove a person --");
                MeetingController.RemovePerson();
                break;

            case "5":
                Console.WriteLine("-- List --");
                MeetingController.ListAllMeetings();
                break;

            case "6":
                Console.WriteLine("Exiting...");
                return;

            default:
                Console.WriteLine("    [X]    ");
                Console.WriteLine("wrong input!");
                break;
        }
    }
}