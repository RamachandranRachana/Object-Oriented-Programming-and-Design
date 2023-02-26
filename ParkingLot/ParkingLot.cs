using ParkingLotAppClassEnums;
using ParkingLotPanel;
using System.Text;

namespace ParkingLot
{
    internal class ParkingSpot
    {
        public ParkingSpot(int number, ParkingSpotType parkingSpotType)
        {
            Number = number;
            VehicleNumber = null;
            ParkingSpotType = parkingSpotType;
            IsFree = true;
        }

        public int Number { get; set; }
        public int? VehicleNumber { get; set; }

        public ParkingSpotType ParkingSpotType { get; set; }

        public bool IsFree { get; set; }

        public void AssignVehicle(int vehiclenumber)
        {
            VehicleNumber = vehiclenumber;
            IsFree = false;
        }

        public void RemoveVehicle()
        {
            VehicleNumber = null;
            IsFree = true;
        }
    }

    internal class HandicappedSpot : ParkingSpot
    {
        public HandicappedSpot(int number, ParkingSpotType parkingSpotType) : base(number, parkingSpotType)
        {
        }
    }

    internal class CompactSpot : ParkingSpot
    {
        public CompactSpot(int number, ParkingSpotType parkingSpotType) : base(number, parkingSpotType)
        {
        }
    }

    internal class LargeSpot : ParkingSpot
    {
        public LargeSpot(int number, ParkingSpotType parkingSpotType) : base(number, parkingSpotType)
        {
        }
    }

    internal class MotorbikeSpot : ParkingSpot
    {
        public MotorbikeSpot(int number, ParkingSpotType parkingSpotType) : base(number, parkingSpotType)
        {
        }
    }

    internal class ElectricSpot : ParkingSpot
    {
        public ElectricSpot(int number, ParkingSpotType parkingSpotType) : base(number, parkingSpotType)
        {
        }
    }

    internal class ParkingFloor
    {
        private List<HandicappedSpot> _handicappedSpots;
        private List<CompactSpot> _compactSpots;
        private List<LargeSpot> _largeSpots;
        private List<MotorbikeSpot> _motorbikeSpots;
        private List<ElectricSpot> _electricSpots;
        private IPanel _floorpanel;

        private Dictionary<ParkingSpotType,int> _parkingAvailableSpotsMap;

        public string FloorName { get; set; }
        public ParkingFloor(string floorName)
        {
            FloorName = floorName;

            _handicappedSpots = new List<HandicappedSpot>();
            _compactSpots = new List<CompactSpot>();
            _largeSpots = new List<LargeSpot>();
            _motorbikeSpots = new List<MotorbikeSpot>();
            _electricSpots = new List<ElectricSpot>();

            _parkingAvailableSpotsMap = new Dictionary<ParkingSpotType, int>
            {
                { ParkingSpotType.HANDICAPPED,0 },
                { ParkingSpotType.COMPACT,0 },
                { ParkingSpotType.LARGE,0 },
                { ParkingSpotType.MOTORBIKE,0 },
                { ParkingSpotType.ELECTRIC,0 },
            };
            _floorpanel = new FloorPanel();
        }

        public void AddParkingSpot(ParkingSpot spot)
        {
            switch (spot.ParkingSpotType)
            {
                case ParkingSpotType.HANDICAPPED:
                    _handicappedSpots.Add(new HandicappedSpot(spot.Number, spot.ParkingSpotType));
                    _parkingAvailableSpotsMap[spot.ParkingSpotType] += 1;
                    break;
                case ParkingSpotType.COMPACT:
                    _compactSpots.Add(new CompactSpot(spot.Number, spot.ParkingSpotType));
                    _parkingAvailableSpotsMap[spot.ParkingSpotType] += 1;
                    break;
                case ParkingSpotType.LARGE:
                    _largeSpots.Add(new LargeSpot(spot.Number, spot.ParkingSpotType));
                    _parkingAvailableSpotsMap[spot.ParkingSpotType] += 1;
                    break;
                case ParkingSpotType.MOTORBIKE:
                    _motorbikeSpots.Add(new MotorbikeSpot(spot.Number, spot.ParkingSpotType));
                    _parkingAvailableSpotsMap[spot.ParkingSpotType] += 1;
                    break;
                case ParkingSpotType.ELECTRIC:
                    _electricSpots.Add(new ElectricSpot(spot.Number, spot.ParkingSpotType));
                    _parkingAvailableSpotsMap[spot.ParkingSpotType] += 1;
                    break;
            }
        }

        public void AddVehicleToSpot(ParkingSpot spot,int vehicleNumber,VehicleType vehicleType)
        {
            bool isAvailable = false;
            switch(vehicleType)
            {
                case VehicleType.CAR:
                    isAvailable=_parkingAvailableSpotsMap[ParkingSpotType.COMPACT] > 0 ? true : false;
                    break;
                case VehicleType.MOTORBIKE:
                    isAvailable = _parkingAvailableSpotsMap[ParkingSpotType.MOTORBIKE] > 0 ? true : false;
                    break;
                case VehicleType.TRUCK:
                    isAvailable = _parkingAvailableSpotsMap[ParkingSpotType.LARGE] > 0 ? true : false;
                    break;
                case VehicleType.VAN:
                    isAvailable = _parkingAvailableSpotsMap[ParkingSpotType.HANDICAPPED] > 0 ? true : false;
                    break;
                case VehicleType.ELECTRIC:
                    isAvailable = _parkingAvailableSpotsMap[ParkingSpotType.ELECTRIC] > 0 ? true : false;
                    break;
            }
            if (isAvailable)
            {
                spot.AssignVehicle(vehicleNumber);
                _parkingAvailableSpotsMap[spot.ParkingSpotType] -= 1;
                updateFloorDisplayPanel();
            }
            else
            {
                Console.WriteLine("No Spot Availble for this Vehicle Type");
            }
        }

        public void RemoveVehicleFromSpot(ParkingSpot spot)
        {
            spot.RemoveVehicle();
            _parkingAvailableSpotsMap[spot.ParkingSpotType] += 1;
            updateFloorDisplayPanel();
        }

        public void updateFloorDisplayPanel()
        {
            _floorpanel.Display(this.ToString());
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in _parkingAvailableSpotsMap)
            {
                stringBuilder.AppendLine(item.ToString());
            }
            return stringBuilder.ToString();
        }

        public bool IsFull()
        {
            foreach (var item in _parkingAvailableSpotsMap)
            {
                if (item.Value != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsAvailable(ParkingSpotType spotType)
        {
            if (_parkingAvailableSpotsMap[spotType] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    internal class ParkingLot
    {
        private List<ParkingFloor> _parkingFloors;

        public string ParkingLotName { get; set; }

        private IPanel _parkingLotPanel;

        private static ParkingLot? _parkingLotInstance = null;

        private ParkingLot(string parkingLotName)
        {
            _parkingFloors = new List<ParkingFloor>();
            ParkingLotName = parkingLotName;
            _parkingLotPanel = new LotPanel();
        }

        public void AddParkingFloor(string floorName)
        {
            _parkingFloors.Add(new ParkingFloor(floorName));
        }

        public static ParkingLot GetInstance()
        {
            if(_parkingLotInstance==null)
            {
                _parkingLotInstance = new ParkingLot("Arora Parking Lot");
            }
            return _parkingLotInstance;
        }

        public bool IsFull()
        {
            foreach(var floor in _parkingFloors)
            {
                if (!floor.IsFull())
                {
                    return false;
                }
            }
            return true;
        }

        public string FirstAvailableFloor(ParkingSpotType spotType)
        {
            foreach (var floor in _parkingFloors)
            {
                if (floor.IsAvailable(spotType))
                {
                    return floor.FloorName;
                }
            }
            return "No availabliity";
        }


        public void DisplayAvailabilityStatus()
        {
            if(IsFull())
            {
                _parkingLotPanel.Display($"{ParkingLotName} has no available parking spots");
            }
           else
            {
                _parkingLotPanel.Display(this.ToString());
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var floor in _parkingFloors)
            {
                sb.AppendLine(floor.ToString());
            }
            return sb.ToString();
        }
    }

 
}