using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class CreateVehicle
    {
        public static Vehicle createVehicleFromType(eVehicleType i_VehicletoCreate, string i_ModelName, string i_LicenceID, object i_UniqueParameter1, object i_UniqueParameter2, string i_WheelsManfacturer, float i_AmountOfCurrentEnergyLeft)
        {
            Vehicle newVehicle = new Vehicle(i_ModelName, i_LicenceID);
            if ((i_VehicletoCreate == eVehicleType.FuelMotorBike) || (i_VehicletoCreate == eVehicleType.ElectricMotorBike))
            {
                eLicenceType licenceID = (eLicenceType)i_UniqueParameter1;
                int engineCapacity = (int)i_UniqueParameter2;
                if (i_VehicletoCreate == eVehicleType.FuelMotorBike)
                {
                    FuelMotorBike newFuelMotorBike = new FuelMotorBike(i_ModelName, i_LicenceID, licenceID, engineCapacity, i_WheelsManfacturer, i_AmountOfCurrentEnergyLeft);
                    newVehicle = newFuelMotorBike;
                    newVehicle = newVehicle as FuelMotorBike;
                }
                else
                {
                    ElectricMotorBike newElectricMotorBike = new ElectricMotorBike(i_ModelName, i_LicenceID, licenceID, engineCapacity, i_WheelsManfacturer, i_AmountOfCurrentEnergyLeft);
                    newVehicle = newElectricMotorBike;
                    newVehicle = newVehicle as ElectricMotorBike;
                }
            }
            else if ((i_VehicletoCreate == eVehicleType.FuelCar) || (i_VehicletoCreate == eVehicleType.ElectricCar))
            {
                eCarColor carColor = (eCarColor)i_UniqueParameter1;
                eDoorsAmount doorsAmount = (eDoorsAmount)i_UniqueParameter2;
                if (i_VehicletoCreate == eVehicleType.FuelCar)
                {
                    FuelCar newFuelCar = new FuelCar(i_ModelName, i_LicenceID, carColor, doorsAmount, i_WheelsManfacturer, i_AmountOfCurrentEnergyLeft);
                    newVehicle = newFuelCar;
                    newVehicle = newVehicle as FuelCar;
                }
                else
                {
                    ElectricCar newElectricCar = new ElectricCar(i_ModelName, i_LicenceID, carColor, doorsAmount, i_WheelsManfacturer, i_AmountOfCurrentEnergyLeft);
                    newVehicle = newElectricCar;
                    newVehicle = newVehicle as ElectricCar;
                }
            }
            else if (i_VehicletoCreate == eVehicleType.Truck)
            {
                bool carryingToxic = (bool)i_UniqueParameter1;
                float loadVolume = (float)i_UniqueParameter2;
                Truck newTruck = new Truck(i_ModelName, i_LicenceID, carryingToxic, loadVolume, i_WheelsManfacturer, i_AmountOfCurrentEnergyLeft);
                newVehicle = newTruck;
                newVehicle = newVehicle as Truck;
            }
            else
            {
                throw new ArgumentException();
            }
            if(newVehicle == null)
            {
                throw new FormatException();
            }
            return newVehicle;
        }
    }
    
    public enum eVehicleType
    {
        FuelMotorBike,
        ElectricMotorBike,
        FuelCar,
        ElectricCar,
        Truck,
    }
}
