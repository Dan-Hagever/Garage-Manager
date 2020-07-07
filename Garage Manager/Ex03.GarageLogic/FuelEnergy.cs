using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : VehicleEnergy
    {
        eFuelType m_VehicleFuelType;

        public FuelEnergy(eFuelType i_VehicleFuelType, float i_CurrentAmount, float i_MaxAmount) :
            base(i_CurrentAmount, i_MaxAmount)
        {
            m_VehicleFuelType = i_VehicleFuelType;
        }

        private void validationOfFuelType(eFuelType i_VehicleFuelType)
        {
            if (i_VehicleFuelType != m_VehicleFuelType)
            {
                throw new ArgumentException();
            }
        }

        public void Refueling(float i_EnergyToAdd, eFuelType i_VehicleFuelType)
        {
            validationOfFuelType(i_VehicleFuelType);
            base.addEnergy(i_EnergyToAdd);
        }

        public static void ValidateFuelType(EnergyType i_Energy)
        {
            if (!(i_Energy == EnergyType.Fuel))
            {
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            string energyToString;
            energyToString = string.Format(
@"Engine Data is: 
    Current Energy Left: {0}
    Maximum Capacity: {1}
    Fuel Type: {2}
", CurrentAmount, MaxAmount, m_VehicleFuelType);
        return energyToString;
        }
    }
}
