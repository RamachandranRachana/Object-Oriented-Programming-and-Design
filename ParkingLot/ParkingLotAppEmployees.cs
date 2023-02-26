using ParkingLotAppClassEnums;
using System.ComponentModel;

namespace ParkingLotAppEmployee
{
    internal class Account
    {
        public Account(string userName, string password, AccountStatus accountstatus, Person person)
        {
            UserName = userName;
            _password = password;
            _accountstatus = accountstatus;
            _person = person;
        }

        protected string UserName { get; set; }
        private string _password { get; set; }

        private AccountStatus _accountstatus { get; set; }

        private Person _person { get; set; }
    }

    internal class Admin : Account
    {
        public Admin(string userName, string password, AccountStatus accountstatus, Person person) : base(userName, password, accountstatus, person)
        {
        }

        public void AddParkingFloor()
        {
            Console.WriteLine($"{UserName}: Added Parking floor");
        }

        public void AddParkingSpot()
        {
            Console.WriteLine($"{UserName}: Added Parking spot");
        }

        public void AddEntrancePanelInfo(string info)
        {
            Console.WriteLine($"{UserName}: Added Entrance Panel info --> {info}");
        }

        public void AddFloorPanelInfo(string info)
        {
            Console.WriteLine($"{UserName}: Added Floor Panel info -->{info}");
        }
    }

    internal class Attendent : Account
    {
        public Attendent(string userName, string password, AccountStatus accountstatus, Person person) : base(userName, password, accountstatus, person)
        {
        }

        public void ProcessTicket(int ticketNumber)
        {
            Console.WriteLine($"{UserName}: Processind the  token:{ticketNumber}");
        }
    }
}