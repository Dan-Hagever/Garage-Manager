using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Messages
    {
        internal static void printMainMenu()
        {
            Console.WriteLine(
 @"Hey, welcome to the Garage Managment system!
To continue please choose the operation you want to committ:
    1   Insert new vehicle to the garage
    2   Display all licenceId of cars in the garage(optional to filter with car state)
    3   Modify car state
    4   Refuel gas tank
    5   Recharge car battery
    6   Inflate wheels to maximum
    7   Show all details about car
    8   Exit

Please enter your choise:");
        }

        internal static void askForVehicleType()
        {
            Console.WriteLine(
@"Please choose a vehicle type:
    1   FuelMotorBike
    2   ElectricMotorBike
    3   FuelCar
    4   ElectricCar
    5   Truck
    6   Return to main menu");
        }

        internal static void printShortMessage(string i_StringToPrint)
        {
            Console.WriteLine(
@"{0}", i_StringToPrint);
        }

        internal static void askForLicenseType()
        {
            Console.WriteLine(
@"Please insert motor bike license Type:
    1   A
    2   A1
    3   A2
    4   B");
        }

        internal static void askForVehcleStatus()
        {
            Console.WriteLine(
@"Please pick vehcle status:
    1   On Repair
    2   Repaired
    3   Paid");
        }

        internal static void askForCarColor()
        {
            Console.WriteLine(
@"Please choose car color:
    1   Red
    2   Blue
    3   Black
    4   Grey");
        }

        internal static void askForDoorsAmount()
        {
            Console.WriteLine(
@"Please choose car doors amount:
    2   two
    3   three
    4   four
    5   five");
        }

        internal static void askForFuelType()
        {
            Console.WriteLine(
@"Please choose car doors amount:
    1   Octan 95
    2   Octan 96
    3   Octan 98
    4   Soler");
        }

        internal static void askForBool(string i_BoolParam)
        {
            Console.WriteLine(
@"If the {0} press 1, press 2 otherwise:", i_BoolParam);
        }

        internal static void vehicleAllreadyExists()
        {
            Console.WriteLine(
@"Vehicle allready exists in the system, and was set to On Repair status");
        }

        internal static void vehicleInsertedSuccessfully()
        {
            Console.WriteLine(
@"Vehicle was inserted successfully");
        }

        internal static string insertAValidInput()
        {
            return string.Format(
@"Input is not valid, you need to enter a valid value");
        }

        internal static string insertLogicValue()
        {
            return string.Format(
@"Input is not valid Logically, you need to enter a valid input");
        }

        internal static void pressAnyKeyToContinue()
        {
            Console.WriteLine(
@"Press any key to continue");
        }

        internal static void insertANumberInValidRange(float i_Min, float i_Max)
        {
            ValueOutOfRangeException rangeException = new ValueOutOfRangeException(i_Min, i_Max);
            Console.WriteLine(
@rangeException.Message);
        }
    }
}
