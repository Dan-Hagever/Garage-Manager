using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        string m_NameOfManufacturer;
        float m_CurrentAirPressure;
        float m_MaxAirPressure;

        public Wheel(float i_MaxAirPressure, string i_NameOfManufacturer)
        {
            if (i_MaxAirPressure > 0)
            {
                m_MaxAirPressure = i_MaxAirPressure;
            }
            else
            {
                throw new ValueOutOfRangeException(0, int.MaxValue);
            }
            if (i_NameOfManufacturer != null)
            {
                m_NameOfManufacturer = i_NameOfManufacturer;
            }
            else
            {
                throw new FormatException();
            }
        }

        public void Inflating (float i_AirToAdd)
        {
            if ((m_CurrentAirPressure + i_AirToAdd > m_MaxAirPressure) && (m_CurrentAirPressure + i_AirToAdd < 0))
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure - m_CurrentAirPressure);
            }
            else
            {
                m_CurrentAirPressure += i_AirToAdd;
            }
        }

        public string NameOfManufacturer
        {
            get
            {
                return m_NameOfManufacturer;
            }
        }

        public override string ToString()
        {
            string wheelToString = string.Format(
@"Manufacturer: {0}
    Current Air Pressure: {1}
    Max Air Pressure: {2}"
    , m_NameOfManufacturer, m_CurrentAirPressure, m_MaxAirPressure);
            return wheelToString;
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }
    }
}
