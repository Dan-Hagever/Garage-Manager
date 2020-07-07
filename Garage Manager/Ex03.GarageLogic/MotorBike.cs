using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eLicenceType
    {
        A,
        A1,
        A2,
        B
    }

    public abstract class MotorBike : Vehicle
    {
        readonly eLicenceType r_LicenceType;
        readonly int r_EngineCapacity;
        const float k_MaxAirPressureForBike = 33;
        const int k_NumberOfWheels = 2;

        public MotorBike (string i_ModelName, string i_LicenceID, eLicenceType i_LicenceType, int i_EngineCapacity, string i_WheelsManfacturer) :
            base(i_ModelName, i_LicenceID)
        {
            r_LicenceType = i_LicenceType;
            r_EngineCapacity = i_EngineCapacity;
            createVehicleWheels(k_MaxAirPressureForBike, i_WheelsManfacturer, k_NumberOfWheels);
        }

        public eLicenceType MotorBikeLicenceType
        {
            get
            {
                return r_LicenceType;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return r_EngineCapacity;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());
            string motorBikeToString = string.Format(
@"Licesne Type is {0}
Engine Capacity is {1}
", Enum.GetName(typeof(eLicenceType), (int)r_LicenceType), EngineCapacity.ToString());
            builder.Append(motorBikeToString);
            return builder.ToString();
        }
    }

    public class FuelMotorBike : MotorBike
    {
        const eFuelType k_TypefOfFuel = eFuelType.Octan95;
        const float k_MaxFuelInLitters = 8;

        public FuelMotorBike(string i_ModelName, string i_LicenceID, eLicenceType i_LicenceType, int i_EngineCapacity, string i_WheelsManfacturer, float i_AmountOfCurrentFuelInLitters) :
            base(i_ModelName, i_LicenceID, i_LicenceType, i_EngineCapacity, i_WheelsManfacturer)
        {
            asssaignEnergySourceToFuel(ref EnergySourceByRef, k_TypefOfFuel, i_AmountOfCurrentFuelInLitters, k_MaxFuelInLitters, EnergyType.Fuel);
        }
    }

    public class ElectricMotorBike : MotorBike
    {
        const float k_MaxBatteryTime = 1.4f;

        public ElectricMotorBike(string i_ModelName, string i_LicenceID, eLicenceType i_LicenceType, int i_EngineCapacity, string i_WheelsManfacturer, float i_AmountOfCurrentBattryLeft) :
            base(i_ModelName, i_LicenceID, i_LicenceType, i_EngineCapacity, i_WheelsManfacturer)
        {
            asssaignEnergySourceToElectric(ref EnergySourceByRef, i_AmountOfCurrentBattryLeft, k_MaxBatteryTime, EnergyType.Electric);
        }
    }
}
