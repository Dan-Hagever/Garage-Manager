using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        static readonly Dictionary<int, GarageVehicle> sr_CurrentGarageVehicles = new Dictionary<int, GarageVehicle>();

        public static Dictionary<int, GarageVehicle> CurrentGarageVehicles
        {
            get
            {
                return sr_CurrentGarageVehicles;
            }
        }

        public static void InsertNewVehicle(eVehicleType i_VehicleType, string i_ModelName, string i_LicenceID, object i_UniqueParameter1, object i_UniqueParameter2, string i_WheelsManfacturer, float i_AmountOfCurrentEnergyLeft, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            Vehicle newVehicle = CreateVehicle.createVehicleFromType(i_VehicleType, i_ModelName, i_LicenceID, i_UniqueParameter1, i_UniqueParameter2, i_WheelsManfacturer, i_AmountOfCurrentEnergyLeft);
            GarageVehicle newGarageVehicle = new GarageVehicle(newVehicle, i_OwnerName, i_OwnerPhoneNumber);
        }
        
        public static List<string> GetLicenseIDsWithFilter(eGarageVehicleStatus i_StatusToFilter)
        {
            List<string> licenseIDs = new List<string>();
            foreach (GarageVehicle garageVehicle in CurrentGarageVehicles.Values)
            {
                if(i_StatusToFilter == garageVehicle.CarStatus)
                {
                    licenseIDs.Add(garageVehicle.VehicleOfGarage.LicenceID);
                }
            }
            return licenseIDs;
        }

        public static List<string> GetLicenseIDs()
        {
            List<string> licenseIDs = new List<string>();
            foreach (GarageVehicle garageVehicle in CurrentGarageVehicles.Values)
            {
                licenseIDs.Add(garageVehicle.VehicleOfGarage.LicenceID);
            }
            return licenseIDs;
        }

        public static void ChangeVehicleStatus(string i_LicenseID, eGarageVehicleStatus i_NewStatus)
        {
            if (!CurrentGarageVehicles.TryGetValue(i_LicenseID.GetHashCode(), out GarageVehicle currentVehicle))
            {
                throw new ArgumentException();
            }
            else
            {
                currentVehicle.CarStatus = i_NewStatus;
            }
        }

        public static void InflateWheelsToMax(string i_LicenseID)
        {
            if (!CurrentGarageVehicles.TryGetValue(i_LicenseID.GetHashCode(), out GarageVehicle currentVehicle))
            {
                throw new ArgumentException();
            }
            else
            {
                foreach (Wheel oneWheel in currentVehicle.VehicleOfGarage.VehicleWheels)
                {
                    oneWheel.CurrentAirPressure = oneWheel.MaxAirPressure;
                }
            }
        }

        public static void Refueling(string i_LicenseID, float i_EnergyToAdd, eFuelType i_VehicleFuelType)
        {
            if (!CurrentGarageVehicles.TryGetValue(i_LicenseID.GetHashCode(), out GarageVehicle currentVehicle))
            {
                throw new ArgumentException();
            }
            (currentVehicle.VehicleOfGarage.EnergySource as FuelEnergy).Refueling(i_EnergyToAdd, i_VehicleFuelType);
        }

        public static void BatteryLoad(string i_LicenseID, float i_NumberToHoursToLoad)
        {
            if (!CurrentGarageVehicles.TryGetValue(i_LicenseID.GetHashCode(), out GarageVehicle currentVehicle))
            {
                throw new ArgumentException();
            }
            (currentVehicle.VehicleOfGarage.EnergySource as ElectricEnergy).LoadBattery(i_NumberToHoursToLoad);
        }

        public static string PrintVehicleDetails(string i_LicenseID)
        {
            if (!CurrentGarageVehicles.TryGetValue(i_LicenseID.GetHashCode(), out GarageVehicle currentVehicle))
            {
                throw new ArgumentException();
            }
            else
            {
                return currentVehicle.ToString();
            }
        }
    }

    public class GarageVehicle : Garage
    {
        string m_OwnerName;
        string m_OwnerPhoneNumber;
        eGarageVehicleStatus m_CarStatus;
        Vehicle m_Vehicle;

        public GarageVehicle(Vehicle i_NewVehicleToInsert, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (!CurrentGarageVehicles.ContainsKey(i_NewVehicleToInsert.GetHashCode()))
            {
                m_OwnerName = i_OwnerName;
                m_OwnerPhoneNumber = i_OwnerPhoneNumber;
                m_CarStatus = eGarageVehicleStatus.OnRepair;
                m_Vehicle = i_NewVehicleToInsert;
                CurrentGarageVehicles.Add(i_NewVehicleToInsert.GetHashCode(), this);
            }
            else
            {
                CurrentGarageVehicles.TryGetValue(i_NewVehicleToInsert.GetHashCode(), out GarageVehicle garageVehicle);
                garageVehicle.m_CarStatus = eGarageVehicleStatus.OnRepair;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            string detailsToString = string.Format(
@"Details regarding {0} with licenseID {1}:
{0} Owner Name is: {2}
{0} Owner Phone Number is: {3}
{0} repairing status is: {4}
", VehicleOfGarage.GetType().Name.ToString() , m_Vehicle.LicenceID.ToString() ,m_OwnerName.ToString(), m_OwnerPhoneNumber.ToString(), Enum.GetName(typeof(eGarageVehicleStatus), (int)CarStatus));
            builder.Append(detailsToString);
            builder.Append(VehicleOfGarage.ToString());
            return builder.ToString();
        }

        public eGarageVehicleStatus CarStatus
        {
            get
            {
                return m_CarStatus;
            }
            set
            {
                m_CarStatus = value;
            }
        }

        public Vehicle VehicleOfGarage
        {
            get
            {
                return m_Vehicle;
            }
        }

    }

    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue = 0;
        private float m_MaxValue = 0;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) :
            base(string.Format("The amount you enter is not valid because it is out of range! the rang is {0} to {1}", i_MinValue, i_MaxValue))
        {
            this.m_MinValue = i_MinValue;
            this.m_MaxValue = i_MaxValue;
        } 

        public override string Message
        {
            get
            {
                return string.Format("The amount you enter is not valid because it is out of range! the rang is {0} to {1}", MinValue, MaxValue);
            }
        }

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
            set
            {
                m_MinValue = value;
            }
        }

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
            set
            {
                m_MaxValue = value;
            }
        }
    }

    public enum eGarageVehicleStatus
    {
        OnRepair,
        Repaired,
        Paid
    }
}
