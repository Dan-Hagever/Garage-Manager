using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageManagerUI
    {
        readonly static GarageTypesParser r_UIGarageTypesParser = new GarageTypesParser();
        static bool m_LastWasValidLogically = true;
        static bool m_LastWasInValidRange = true;
        static bool m_Exit = false;
        static bool m_StayInMethod = true;

        private enum eMenuState
        {
            InsertNewVehicle = 1,
            GetLicenseIDs,
            ChangeVehicleStatus,
            InflateWheelsToMax,
            Refueling,
            BattaryLoad,
            PrintVehicleDetails,
            Exit,
        }

        public static void RunGarageManager()
        {
            while (!m_Exit)
            {
                runConsole();
            }
            Environment.Exit(0);
        }

        private static void runConsole()
        {
            m_StayInMethod = true;
            Console.Clear();
            Messages.printMainMenu();
            printInvalidRangeOrParseMessages(m_LastWasValidLogically, m_LastWasInValidRange, 1f, 8f);
            try
            {
                updateNoExceptionBooleansToTrue();
                int i_input = ReciveInputAsInt();
                chooseOperation(i_input);
            }
            catch (Exception ex)
            {
                checkExceptionsAndUpdateBooleans(ex);
                RunGarageManager();
            }
        }

        private static void InsertNewVehicleParser()
        {
            Messages.askForVehicleType();
            int i_input = GarageManagerUI.ReciveInputAsInt();
            if (i_input == GarageTypesParser.sr_VehicleTypesDic.Count() + 1)
            {
                m_StayInMethod = false;
            }
            else
            {
                Console.Clear();
                eVehicleType o_TypeOfVehicle = r_UIGarageTypesParser.parseVehicleType(i_input);
                GarageManagerUI.receiveInputsAndCreateNewVehicle(o_TypeOfVehicle);
            }
        }

        private static void GetLicenseIDsParser()
        {
            List<string> licenseIDs;
            Messages.askForBool("you want to filter vehicle license IDs by status");
            if (r_UIGarageTypesParser.parseBool(ReciveInput()))
            {
                Messages.askForVehcleStatus();
                int i_input = ReciveInputAsInt();
                eGarageVehicleStatus statusOfVehicle = r_UIGarageTypesParser.parseGarageVehicleStatus(i_input);
                licenseIDs = Garage.GetLicenseIDsWithFilter(statusOfVehicle);
            }
            else
            {
                licenseIDs = Garage.GetLicenseIDs();
            }
            Console.Clear();
            Messages.printShortMessage(@"Garage Vehicles are:");
            foreach (string licenseID in licenseIDs)
            {
                Console.WriteLine(licenseID);
            }
        }

        private static void ChangeVehicleStatusParser()
        {
            Messages.printShortMessage("Please insert a License ID to change:");
            string o_LicenseID = r_UIGarageTypesParser.parseString(ReciveInput());
            if (!checkifLicenseIDExists(o_LicenseID))
            {
                sayAndThrowExceptIfLicenseIDNotExists();
            }
            Messages.askForVehcleStatus();
            eGarageVehicleStatus o_VehicleStatus = r_UIGarageTypesParser.parseGarageVehicleStatus(ReciveInputAsInt());
            Garage.ChangeVehicleStatus(o_LicenseID, o_VehicleStatus);
        }

        private static void InflateWheelsToMaxParser()
        {
            Messages.printShortMessage("Please insert a License ID Inflate its wheels to maximum:");
            string o_LicenseID = r_UIGarageTypesParser.parseString(ReciveInput());
            if (!checkifLicenseIDExists(o_LicenseID))
            {
                sayAndThrowExceptIfLicenseIDNotExists();
            }
            Garage.InflateWheelsToMax(o_LicenseID);
        }

        private static void RefuelingParser()
        {
            Messages.printShortMessage("Please insert a License ID of a vehicle that runs by fuel:");
            string o_LicenseID = r_UIGarageTypesParser.parseString(ReciveInput());
            if (!checkifLicenseIDExists(o_LicenseID))
            {
                sayAndThrowExceptIfLicenseIDNotExists();
            }
            Messages.printShortMessage("Please insert amount of fuel (in litters) to add:");
            float o_iAmountToAdd = r_UIGarageTypesParser.parseFloat(ReciveInput());
            Messages.askForFuelType();
            eFuelType o_TypeOfFuel = r_UIGarageTypesParser.parseFuelType(ReciveInputAsInt());
            Garage.Refueling(o_LicenseID, o_iAmountToAdd, o_TypeOfFuel);
        }

        private static void BatteryLoadParser()
        {
            Messages.printShortMessage("Please insert a License ID of a vehicle that runs by Electric:");
            string o_LicenseID = r_UIGarageTypesParser.parseString(ReciveInput());
            if (!checkifLicenseIDExists(o_LicenseID))
            {
                sayAndThrowExceptIfLicenseIDNotExists();
            }
            Messages.printShortMessage("Please insert amount of battery hours add:");
            float o_iAmountToAdd = r_UIGarageTypesParser.parseFloat(ReciveInput());
            Garage.BatteryLoad(o_LicenseID, o_iAmountToAdd);
        }

        private static void PrintVehicleDetailsParser()
        {
            Messages.printShortMessage("Please insert a License ID of a vehicle to view its details:");
            string o_LicenseID = r_UIGarageTypesParser.parseString(ReciveInput());
            if (!checkifLicenseIDExists(o_LicenseID))
            {
                sayAndThrowExceptIfLicenseIDNotExists();
            }
            Console.Write(Garage.PrintVehicleDetails(o_LicenseID));
        }

        private static void GarageMethodsWrapper(Action i_GarageAction)
        {
            bool validity = true;
            string errorAnouncment = "";
            while (m_StayInMethod)
            {
                try
                {
                    Console.Clear();
                    if (!validity)
                    {
                        Console.WriteLine(errorAnouncment);
                    }
                    i_GarageAction.Invoke();
                    if (m_StayInMethod == true)
                    {
                        Messages.printShortMessage(@"Action was commited successfully");
                        Messages.pressAnyKeyToContinue();
                        Console.ReadKey();
                    }
                    m_StayInMethod = false;
                }
                catch (Exception genericException)
                {
                    validity = false;
                    if (genericException is ArgumentException)
                    {
                        errorAnouncment = Messages.insertLogicValue();
                    }
                    else if (genericException is ValueOutOfRangeException)
                    {
                        errorAnouncment = string.Format(genericException.Message.ToString());
                    }
                    else if (genericException is NullReferenceException)
                    {
                        errorAnouncment = string.Format("vehicle is of wronge type");
                    }
                    else
                    {
                        errorAnouncment = Messages.insertAValidInput();
                    }
                    m_StayInMethod = true;
                }
            }
            validity = true;
        }

        private static void receiveInputsAndCreateNewVehicle(GarageLogic.eVehicleType io_TypeOfVehicle)
        {
            try
            {
                Messages.printShortMessage(@"Please insert vehicle model name:");
                string o_ModelName = r_UIGarageTypesParser.parseString(ReciveInput());
                Messages.printShortMessage(@"Please insert vehicle license ID:");
                string o_LicenseID = r_UIGarageTypesParser.parseString(ReciveInput());
                object o_FirstUnique = getfirstUniqueObject(io_TypeOfVehicle);
                object o_SecondUnique = getSecondtUniqueObject(io_TypeOfVehicle);
                Messages.printShortMessage(@"Please insert wheels manufacturer name:");
                string o_WheelsManufacturer = r_UIGarageTypesParser.parseString(ReciveInput());
                Messages.printShortMessage(@"Please insert amount of energy left in vehicle:");
                float o_AmountOfEnergyLeft = r_UIGarageTypesParser.parseFloat(ReciveInput());
                Messages.printShortMessage(@"Please insert the owner's of vehicle name:");
                string o_OwnerName = r_UIGarageTypesParser.parseString(ReciveInput());
                Messages.printShortMessage(@"Please insert owner's of vehicle phone number:");
                string o_OwnerPhoneNumber = r_UIGarageTypesParser.parseString(ReciveInput());

                if (Garage.CurrentGarageVehicles.TryGetValue(o_LicenseID.GetHashCode(), out GarageVehicle garageVehicle))
                {
                    Garage.InsertNewVehicle(io_TypeOfVehicle, o_ModelName, o_LicenseID, o_FirstUnique, o_SecondUnique, o_WheelsManufacturer, o_AmountOfEnergyLeft, o_OwnerName, o_OwnerPhoneNumber);
                    Messages.vehicleAllreadyExists();
                }
                else
                {
                    Garage.InsertNewVehicle(io_TypeOfVehicle, o_ModelName, o_LicenseID, o_FirstUnique, o_SecondUnique, o_WheelsManufacturer, o_AmountOfEnergyLeft, o_OwnerName, o_OwnerPhoneNumber);
                    Messages.vehicleInsertedSuccessfully();
                }
            }
            catch (Exception Exception)
            {
                if (Exception is ArgumentException)
                {
                    Console.WriteLine(Messages.insertLogicValue());
                }
                else if (Exception is ValueOutOfRangeException)
                {
                    Console.WriteLine(Exception.Message.ToString());
                }
                else
                {
                    Console.WriteLine(Messages.insertAValidInput());
                }
                m_StayInMethod = false;
                Messages.printShortMessage("press any key to return to main menu");
                Console.ReadKey();
            }
        }

        private static void printInvalidRangeOrParseMessages(bool i_LastWasValidLogically, bool i_LastWasInValidRange, float i_Min, float i_Max)
        {
            if ((!i_LastWasValidLogically) && (i_LastWasInValidRange))
            {
                Console.WriteLine(Messages.insertLogicValue());
            }
            else if ((i_LastWasValidLogically) && (!i_LastWasInValidRange))
            {
                Messages.insertANumberInValidRange(i_Min, i_Max);
            }
            else if((!i_LastWasInValidRange) && (!i_LastWasValidLogically))
            {
                Console.WriteLine(Messages.insertAValidInput());
            }

        }

        private static void updateNoExceptionBooleansToTrue()
        {
            m_LastWasValidLogically = true;
            m_LastWasInValidRange = true;
        }

        private static void checkExceptionsAndUpdateBooleans(Exception i_Exception)
        {
            updateNoExceptionBooleansToTrue();
            if (i_Exception is ArgumentException)
            {
                m_LastWasValidLogically = false;
                m_LastWasInValidRange = true;
            }
            else if (i_Exception is ValueOutOfRangeException)
            {
                m_LastWasValidLogically = true;
                m_LastWasInValidRange = false;
            }
            else
            {
                m_LastWasValidLogically = false;
                m_LastWasInValidRange = false;
            }
        }

        private static object getfirstUniqueObject(GarageLogic.eVehicleType i_TypeOfVehicle)
        {
            object o_FirstUniqueParam;
            if ((i_TypeOfVehicle == eVehicleType.FuelMotorBike) || (i_TypeOfVehicle == eVehicleType.ElectricMotorBike))
            {
                Messages.askForLicenseType();
                o_FirstUniqueParam = r_UIGarageTypesParser.parseLicenceType(ReciveInputAsInt());

            }
            else if ((i_TypeOfVehicle == eVehicleType.FuelCar) || (i_TypeOfVehicle == eVehicleType.ElectricCar))
            {
                Messages.askForCarColor();
                o_FirstUniqueParam = r_UIGarageTypesParser.parseCarColor(ReciveInputAsInt());
            }
            else if (i_TypeOfVehicle == eVehicleType.Truck)
            {
                Messages.askForBool("truck is carrying toxic materials");
                o_FirstUniqueParam = r_UIGarageTypesParser.parseBool(ReciveInput());
            }
            else
            {
                o_FirstUniqueParam = "";
            }
            return o_FirstUniqueParam;
        }

        private static object getSecondtUniqueObject(GarageLogic.eVehicleType i_TypeOfVehicle)
        {
            object o_SecondUniqueParam;
            if ((i_TypeOfVehicle == eVehicleType.FuelMotorBike) || (i_TypeOfVehicle == eVehicleType.ElectricMotorBike))
            {
                Messages.printShortMessage(@"Please insert motor bike Engine Capacity (as a number only):");
                o_SecondUniqueParam = ReciveInputAsInt();
            }
            else if ((i_TypeOfVehicle == eVehicleType.FuelCar) || (i_TypeOfVehicle == eVehicleType.ElectricCar))
            {
                Messages.askForDoorsAmount();
                o_SecondUniqueParam = r_UIGarageTypesParser.parseDoorsAmount(ReciveInputAsInt());
            }
            else if (i_TypeOfVehicle == eVehicleType.Truck)
            {
                Messages.printShortMessage(@"Please insert truck is load volume (as a number only):");
                o_SecondUniqueParam = r_UIGarageTypesParser.parseFloat(ReciveInput());
            }
            else
            {
                o_SecondUniqueParam = "";
            }
            return o_SecondUniqueParam;
        }

        private static void chooseOperation(int i_UserChoose)
        {
            Action o_garageMethod = null;
            if (i_UserChoose == (int)eMenuState.InsertNewVehicle)
            {
                o_garageMethod = InsertNewVehicleParser;
            }
            else if (i_UserChoose == (int)eMenuState.GetLicenseIDs)
            {
                o_garageMethod = GetLicenseIDsParser;
            }
            else if (i_UserChoose == (int)eMenuState.ChangeVehicleStatus)
            {
                o_garageMethod = ChangeVehicleStatusParser;
            }
            else if (i_UserChoose == (int)eMenuState.InflateWheelsToMax)
            {
                o_garageMethod = RefuelingParser;
            }
            else if (i_UserChoose == (int)eMenuState.Refueling)
            {
                o_garageMethod = BatteryLoadParser;
            }
            else if (i_UserChoose == (int)eMenuState.BattaryLoad)
            {
                o_garageMethod = InflateWheelsToMaxParser;
            }
            else if (i_UserChoose == (int)eMenuState.PrintVehicleDetails)
            {
                o_garageMethod = PrintVehicleDetailsParser;
            }
            else if (i_UserChoose == (int)eMenuState.Exit)
            {
                m_Exit = true;
            }
            else
            {
                throw new ValueOutOfRangeException(1f, 8f);
            }
            if (o_garageMethod != null)
            {
                GarageMethodsWrapper(o_garageMethod);
            }
        }

        public static int ReciveInputAsInt()
        {
            string newInput = ReciveInput();
            return r_UIGarageTypesParser.parseInt(newInput);
        }

        public static string ReciveInput()
        {
            string newInput = Console.ReadLine();
            return newInput.Trim();
        }

        private static bool checkifLicenseIDExists(string i_LicenseID)
        {
            bool o_Exists = false;
            if(Garage.CurrentGarageVehicles.TryGetValue(i_LicenseID.GetHashCode(), out GarageVehicle garageVehicle))
            {
                o_Exists = true;
            }
            return o_Exists;
        }

        private static void sayAndThrowExceptIfLicenseIDNotExists()
        {
            Messages.printShortMessage("Vehicle does not exists in the system");
            Messages.pressAnyKeyToContinue();
            Console.ReadKey();
            throw new ArgumentException();
        }
    }
}
