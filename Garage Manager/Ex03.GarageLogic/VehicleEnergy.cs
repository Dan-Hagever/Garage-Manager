using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{

    public enum EnergyType
    {
        Fuel,
        Electric
    }

    public enum eFuelType
    {
        Octan95,
        Octan96,
        Octan98,
        Soler
    }

    public class VehicleEnergy
    {
        float m_CurrentAmount;
        float m_MaxAmount;

        public VehicleEnergy(float i_CurrentAmount, float i_MaxAmount)
        {
            validationOfCtorEnergyParams(i_CurrentAmount, i_MaxAmount);
            m_CurrentAmount = i_CurrentAmount;
            m_MaxAmount = i_MaxAmount;
        }

        public float MaxAmount
        {
            get
            {
                return m_MaxAmount;
            }
        }

        public float CurrentAmount
        {
            get
            {
                return m_CurrentAmount;
            }
            set
            {
                m_CurrentAmount = value;
            }
        }

        protected virtual void validationOfCtorEnergyParams(float i_CurrentAmount, float i_MaxAmount)
        {
            if(m_CurrentAmount < 0 || i_MaxAmount < 0)
            {
                throw new ArgumentException();
            }
            if(i_CurrentAmount > i_MaxAmount)
            {
                throw new ValueOutOfRangeException(0, i_MaxAmount);
            }
        }

        protected virtual void addEnergy(float i_EnergyToAdd)
        {
            if ((m_CurrentAmount + i_EnergyToAdd > m_MaxAmount) && (m_CurrentAmount + i_EnergyToAdd > 0))
            {
                throw new ValueOutOfRangeException(0, m_MaxAmount - m_CurrentAmount);
            }
            else
            {
                m_CurrentAmount += i_EnergyToAdd;
            }
        }

        public override string ToString()
        {
            string energyToString;
            if (this as FuelEnergy != null)
            {
                energyToString = this.ToString();
            }
            else
            {
                energyToString = string.Format(
@"Engine Data is: 
    Current Energy Left: {0}
    Maximum Capacity: {1}
", CurrentAmount, MaxAmount);
            }
            return energyToString;
        }
    }
}
