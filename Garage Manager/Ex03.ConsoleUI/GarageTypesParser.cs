using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageTypesParser
    {
        internal static readonly Dictionary<int, eVehicleType> sr_VehicleTypesDic = new Dictionary<int, eVehicleType>();
        internal static readonly Dictionary<int, eGarageVehicleStatus> sr_GarageVehicleStatusDic = new Dictionary<int, eGarageVehicleStatus>();
        internal static readonly Dictionary<int, eFuelType> sr_FuelTypeDic = new Dictionary<int, eFuelType>();
        internal static readonly Dictionary<int, eLicenceType> sr_LicenceTypeDic = new Dictionary<int, eLicenceType>();
        internal static readonly Dictionary<int, eCarColor> sr_CarColorDic = new Dictionary<int, eCarColor>();
        internal static readonly Dictionary<int, eDoorsAmount> sr_DoorsAmountDic = new Dictionary<int, eDoorsAmount>();

        public GarageTypesParser()
        {
            int i = 1;
            foreach(eVehicleType typeOfVehicle in (eVehicleType[])Enum.GetValues(typeof(eVehicleType)))
            {
                sr_VehicleTypesDic.Add(i, typeOfVehicle);
                i++;
            }
            i = 1;
            foreach (eGarageVehicleStatus vehicleStatus in (eGarageVehicleStatus[])Enum.GetValues(typeof(eGarageVehicleStatus)))
            {
                sr_GarageVehicleStatusDic.Add(i, vehicleStatus);
                i++;
            }
            i = 1;
            foreach (eFuelType fuelType in (eFuelType[])Enum.GetValues(typeof(eFuelType)))
            {
                sr_FuelTypeDic.Add(i, fuelType);
                i++;
            }
            i = 1;
            foreach (eLicenceType licenceType in (eLicenceType[])Enum.GetValues(typeof(eLicenceType)))
            {
                sr_LicenceTypeDic.Add(i, licenceType);
                i++;
            }
            i = 1;
            foreach (eCarColor carColor in (eCarColor[])Enum.GetValues(typeof(eCarColor)))
            {
                sr_CarColorDic.Add(i, carColor);
                i++;
            }
            i = 2;
            foreach (eDoorsAmount doorsAmount in (eDoorsAmount[])Enum.GetValues(typeof(eDoorsAmount)))
            {
                sr_DoorsAmountDic.Add(i, doorsAmount);
                i++;
            }
        }

        internal eVehicleType parseVehicleType(int i_IntToParse)
        {
            if (sr_VehicleTypesDic.TryGetValue(i_IntToParse, out eVehicleType o_TypeOfVehicle))
            {
                return o_TypeOfVehicle;
            }
            else
            {
                throw new ValueOutOfRangeException(1f, 5f);
            }
        }

        internal string parseString(string i_Name)
        {
            return i_Name.Trim();
        }

        internal float parseFloat(string i_stringToParse)
        {
            if (float.TryParse(i_stringToParse, out float o_FloatParsed))
            {
                return o_FloatParsed;
            }
            else
            {
                throw new FormatException();
            }
        }

        internal int parseInt(string i_stringToParse)
        {
            if (int.TryParse(i_stringToParse, out int o_IntParsed))
            {
                return o_IntParsed;
            }
            else
            {
                throw new FormatException();
            }
        }

        internal bool parseBool(string i_stringToParse)
        {
            if (i_stringToParse.Trim() == "1")
            {
                return true;
            }
            else if (i_stringToParse.Trim() == "2")
            {
                return false;
            }
            else
            {
                throw new FormatException();
            }
        }

        internal eGarageVehicleStatus parseGarageVehicleStatus(int i_IntToParse)
        {
           
            if (sr_GarageVehicleStatusDic.TryGetValue(i_IntToParse, out eGarageVehicleStatus o_Status))
            {
                return o_Status;
            }
            else
            {
                throw new ValueOutOfRangeException(1f, 3f);
            }
        }

        internal eFuelType parseFuelType(int i_IntToParse)
        { 
            if (sr_FuelTypeDic.TryGetValue(i_IntToParse, out eFuelType o_FuelType))
            {
                return o_FuelType;
            }
            else
            {
                throw new ValueOutOfRangeException(1f, 4f);
            }
        }

        internal eLicenceType parseLicenceType(int i_IntToParse)
        {
            if (sr_LicenceTypeDic.TryGetValue(i_IntToParse, out eLicenceType o_LicenceType))
            {
                return o_LicenceType;
            }
            else
            {
                throw new ValueOutOfRangeException(1f, 4f);
            }
        }

        internal eCarColor parseCarColor(int i_IntToParse)
        {
            if (sr_CarColorDic.TryGetValue(i_IntToParse, out eCarColor o_CarColor))
            {
                return o_CarColor;
            }
            else
            {
                throw new ValueOutOfRangeException(1f, 4f); ;
            }
        }

        internal eDoorsAmount parseDoorsAmount(int i_IntToParse)
        {
            if (sr_DoorsAmountDic.TryGetValue(i_IntToParse, out eDoorsAmount o_DoorsAmount))
            {
                return o_DoorsAmount;
            }
            else
            {
                throw new ValueOutOfRangeException(2f,5f);
            }
        }
    }
}
