namespace ParkingLotAppClassEnums
{
    public enum VehicleType
    {
        CAR,
        TRUCK,
        ELECTRIC,
        VAN,
        MOTORBIKE
    }

    public enum ParkingSpotType
    {
        HANDICAPPED,
        COMPACT,
        LARGE,
        MOTORBIKE,
        ELECTRIC
    }

    public enum AccountStatus
    {
        ACTIVE,
        BLOCKED,
        BANNED,
        COMPROMISED,
        ARCHIVED,
        UNKNOWN
    }

    public enum TicketStatus
    {
        ACTIVE,
        PAID,
        LOST
    }

    internal class Address
    {
        public Address(string streetAdress, string city, string zipCode)
        {
            _streetaddress = streetAdress;
            _city = city;
            _zipCode = zipCode;
        }

        private string _streetaddress { get; set; }
        private string _city { get; set; }
        private string _zipCode { get; set; }
    }

    internal class Person
    {
        public Person(string phoneNumber, string name, string email, Address address)
        {
            _phonenumber = phoneNumber;
            _name = name;
            _email = email;
            _address = address;
        }

        private string _phonenumber { get; set; }
        private string _name { get; set; }
        private string _email { get; set; }

        private Address _address { get; set; }
    }
}