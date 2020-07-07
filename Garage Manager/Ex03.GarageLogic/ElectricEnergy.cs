using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : VehicleEnergy
    {
        public ElectricEnergy(float i_CurrentAmount, float i_MaxAmount) :
           base(i_CurrentAmount, i_MaxAmount)
        {
        }

        public void LoadBattery(float i_EnergyToAdd)
        {
            base.addEnergy(i_EnergyToAdd);
        }

        public static void ValidateElectriclType(EnergyType i_Energy)
        {
            if (i_Energy == EnergyType.Electric)
            {
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
