using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eCarColor
    {
        Red,
        Blue,
        Black,
        Grey
    }

    public enum eDoorsAmount
    {
        Two = 2,
        Three,
        Four,
        Five
    }

    public abstract class Car : Vehicle
    {
        readonly eCarColor r_ColorOfCar;
        readonly eDoorsAmount r_AmountOfDoors;
        const int k_NumberOfWheels = 4;
        const float k_MaxAirPressureForCar = 31;

        public Car(string i_ModelName, string i_LicenceID, eCarColor ColorOfCar, eDoorsAmount AmountOfDoors, string i_WheelsManfacturer) :
          base(i_ModelName, i_LicenceID)
        {
            r_ColorOfCar = ColorOfCar;
            r_AmountOfDoors = AmountOfDoors;
            createVehicleWheels(k_MaxAirPressureForCar, i_WheelsManfacturer, k_NumberOfWheels);
        }

        public eCarColor ColorOfCar
        {
            get
            {
                return r_ColorOfCar;
            }
        }

        public eDoorsAmount AmountOfDoors
        {
            get
            {
                return r_AmountOfDoors;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());
            string carToString = string.Format(
@"Car Color is {0}
Doors Amount is {1}
", Enum.GetName(typeof(eCarColor), (int)ColorOfCar), Enum.GetName(typeof(eDoorsAmount), (int)AmountOfDoors));
            builder.Append(carToString);
            return builder.ToString();
        }
    }

    public class FuelCar : Car
    {
        const eFuelType k_FuelType = eFuelType.Octan96;
        const float k_MaxAmountOfFuelInLitters = 55;

        public FuelCar(string i_ModelName, string i_LicenceID, eCarColor ColorOfCar, eDoorsAmount AmountOfDoors, string i_WheelsManfacturer, float i_AmountOfCurrentFuelInLitters) :
          base(i_ModelName, i_LicenceID, ColorOfCar, AmountOfDoors, i_WheelsManfacturer)
        {
            asssaignEnergySourceToFuel(ref EnergySourceByRef, k_FuelType, i_AmountOfCurrentFuelInLitters, k_MaxAmountOfFuelInLitters, EnergyType.Fuel);
        }
    }
    
     public class ElectricCar : Car
     {
        const float k_MaxBatteryTime = 1.8f;

        public ElectricCar(string i_ModelName, string i_LicenceID, eCarColor ColorOfCar, eDoorsAmount AmountOfDoors, string i_WheelsManfacturer, float i_AmountOfCurrentBattryLeft) :
            base(i_ModelName, i_LicenceID, ColorOfCar, AmountOfDoors, i_WheelsManfacturer)
        {
            asssaignEnergySourceToElectric(ref EnergySourceByRef, i_AmountOfCurrentBattryLeft, k_MaxBatteryTime, EnergyType.Electric);
        }
     }
}
