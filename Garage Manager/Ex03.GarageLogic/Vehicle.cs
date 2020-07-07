using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        readonly string r_ModelName = null;
        readonly string r_LicenceID = null;
        float m_PercentOfEnergyLeft;
        readonly List<Wheel> r_VehicleWheels = new List<Wheel>();
        VehicleEnergy m_EnergySource;

        public Vehicle(string i_ModelName, string i_LicenceID)
        {
            r_ModelName = i_ModelName;
            r_LicenceID = i_LicenceID;
        }

        protected void createVehicleWheels(float i_MaxAirPressure, string i_NameOfManufacturer, int i_NumberOfWheels)
        {
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel WheelToAdd = new Wheel(i_MaxAirPressure, i_NameOfManufacturer);
                r_VehicleWheels.Add(WheelToAdd);
            }
        }

        public string ModelName
        {
            get
            {
                return r_ModelName;
            }
            set
            {
                ModelName = value;
            }
        }

        public string LicenceID
        {
            get
            {
                return r_LicenceID;
            }
        }

        public float PercentOfEnergyLeft
        {
            get
            {
                return m_PercentOfEnergyLeft;
            }

            set
            {
                m_PercentOfEnergyLeft = value;
            }
        }

        public VehicleEnergy EnergySource
        {
            get
            {
                return m_EnergySource;
            }
            set
            {
                EnergySource = value;
                if(EnergySource != null)
                {
                    PercentOfEnergyLeft = percentOfEnergyLeftCalculation(m_EnergySource.CurrentAmount, m_EnergySource.MaxAmount);
                }
            }
        }

        public ref VehicleEnergy EnergySourceByRef
        {
            get
            {
                return ref m_EnergySource;
            }
        }

        public override int GetHashCode()
        {
            return this.r_LicenceID.GetHashCode();
        }

        public List<Wheel> VehicleWheels
        {
            get
            {
                return r_VehicleWheels;
            }
        }

        protected float percentOfEnergyLeftCalculation(float i_AmountOfCurrentEnergy, float i_MaxEnergy)
        {
            return (i_AmountOfCurrentEnergy / i_MaxEnergy) * 100;
        }

        protected void asssaignEnergySourceToFuel(ref VehicleEnergy io_Energy, eFuelType i_FuelType, float i_CurrentAmount, float i_MaxAmount, EnergyType i_Fuel)
        {
            FuelEnergy.ValidateFuelType(i_Fuel);
            io_Energy = new FuelEnergy(i_FuelType, i_CurrentAmount, i_MaxAmount);
            PercentOfEnergyLeft = percentOfEnergyLeftCalculation(i_CurrentAmount, i_MaxAmount);
        }

        protected void asssaignEnergySourceToElectric(ref VehicleEnergy io_Energy, float i_CurrentAmount, float i_MaxAmount, EnergyType i_Electric)
        {
            ElectricEnergy.ValidateElectriclType(i_Electric);
            io_Energy = new ElectricEnergy(i_CurrentAmount, i_MaxAmount);
            PercentOfEnergyLeft = percentOfEnergyLeftCalculation(i_CurrentAmount, i_MaxAmount);
        }

        public override string ToString()
        {
            string vehicleToString = string.Format(
@"The {0} Data is: 
{0} Model: {1}
License ID: {2}
Percentage Of Energy Left: {3}
There are {4} Car Wheels, each data is:
    {5}
{6}", this.GetType().Name.ToString(), r_ModelName, r_LicenceID, m_PercentOfEnergyLeft, VehicleWheels.Count, VehicleWheels[0], m_EnergySource);
            return vehicleToString;
        }
    }
}
