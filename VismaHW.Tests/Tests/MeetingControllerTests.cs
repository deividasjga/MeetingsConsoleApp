using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaHW.Models;
using Xunit;

namespace VismaHW.Tests.Tests
{
    public class MeetingControllerTests
    {
        [Fact]
        public void MeetingController_CreateMeet()
        {
            Meeting test = new Meeting("testUnitam", "John", "My description!", "Hub", "Live", "02/03/2022", "10/03/2022");
            test.Should().NotBeNull();
            DB.meetings.Add(test);
            DB.SaveMeetings();
            Assert.Contains(test, DB.meetings);
        }

        [Fact]
        public void MeetingController_AddPerson()
        {
            Meeting test = new Meeting("testUnitam", "John", "My description!", "Hub", "Live", "02/03/2022", "10/03/2022");
            string person = "John";
            People people = new People(person);
            test.People.Add(people);
            Assert.NotNull(people);
        }
    }
}
