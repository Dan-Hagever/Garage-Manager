using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        bool m_CarryingToxic;
        float m_LoadVolume;
        const float k_MaxAirPressureForCar = 26;
        const int k_NumberOfWheels = 12;
        const eFuelType k_FuelType = eFuelType.Soler;
        const float k_MaxAmountOfFuelInLitters = 110;

        public Truck(string i_ModelName, string i_LicenceID, bool i_CarryingToxic, float i_LoadVolume, string i_WheelsManfacturer, float i_AmountOfCurrentFuelInLitters) :
         base(i_ModelName, i_LicenceID)
        {
            m_CarryingToxic = i_CarryingToxic;
            m_LoadVolume = i_LoadVolume;
            createVehicleWheels(k_MaxAirPressureForCar, i_WheelsManfacturer, k_NumberOfWheels);
            asssaignEnergySourceToFuel(ref EnergySourceByRef, k_FuelType, i_AmountOfCurrentFuelInLitters, k_MaxAmountOfFuelInLitters, EnergyType.Fuel);
        }

        public bool CarryingToxic
        {
            get
            {
                return m_CarryingToxic;
            }
        }

        public float LoadVolume
        {
            get
            {
                return m_LoadVolume;
            }
        }

        public override string ToString()
        {
            string not;
            if (CarryingToxic)
            {
                not = " ";
            }
            else
            {
                not = " not ";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());
            string truckToString = string.Format(
@"Truck is{0}Carrying Toxic Materials
Maximun Truck Load Volume is {1}
", not, LoadVolume.ToString());
            builder.Append(truckToString);
            return builder.ToString();
        }
    }
}
