using System;

namespace LoadLink.LoadMatching.Application.Common
{
    public static class CommonLM
    {
        private const string VTYPE_CHAR_CODES = "VRKFSDTCUHLONPIE";
        private const string VSIZE_CHAR_CODES = "TLB";
        private const string POSTATTRIB_CHAR_CODES = "ABWCZITVXMGFHEN";

        public enum SearchType
        {
            All = 0, // Only Included 
            Included = 1, // All the result (Included + Excluded)
            Excluded = 2  // Only Excluded
        }


        #region Local Conversion_Method
        public static string VTypeNumToString(int lVType)
        {
            //-----------------------------------------------------------------------------------
            //Purpose    This function will return a string representation of the
            //           Vehicle Equipment type number
            //Inputs     Vehicle number code
            //
            //Supported Vehicle Types
            //           V - Van / Dry Box
            //           R - Reefer
            //           K - Straight Truck
            //           F - Flat Bed/Deck
            //
            //           S - Step Deck
            //           D - Double Drop
            //           T - Rack and Tarp
            //           C - Container
            //
            //           U - Super B
            //           H - Floats
            //           L - LowBoy / RGN
            //           O - Other
            //           N - Curtainside
            //           P - Power Only
            //           I - Roll Tite Trailer
            //           E - Dump Trailer
            //
            //Returns    Vehicle type string
            //Effects    None
            //-----------------------------------------------------------------------------------
            // Change Log
            // Date      Developer    Descn
            // -------   -----------  --------------------------------------------------------------
            // 2005May17 Hiro         Initial Build
            // 2013Jan24 Hiro         New Vehicle type N (Curtainside) and P (Power Only)
            // 2016Feb23 Hiro         New Vehicle type I (Roll Tite Trailer) and E (Dump Trailer)
            //-----------------------------------------------------------------------------------

            string VTypes = VTYPE_CHAR_CODES;
            string sVTypeString;

            if (lVType == 0)
            {
                return "--";
            }
            else
            {
                //V Type - calculate Van, Reefer, Flat, etc...
                sVTypeString = "";

                //0 to 11
                for (int i = 0; i <= ((VTypes.Length) - 1); i++)
                {
                    if (((long)(Math.Pow(2, i)) & lVType) > 0)
                    {
                        sVTypeString = sVTypeString + VTypes.Substring(i, 1);
                    }
                }
                return sVTypeString;
            }
        }
        public static int VTypeStringToNum(string sVType)
        {
            //-----------------------------------------------------------------------------------
            //Purpose    This function will return a numeric representation of the
            //           Vehicle Equipment type string
            //Inputs     Vehicle number string
            //
            //Supported Vehicle Types
            //           V - Van / Dry Box
            //           R - Reefer
            //           K - Straight Truck
            //           F - Flat Bed/Deck
            //
            //           S - Step Deck
            //           D - Double Drop
            //           T - Rack and Tarp
            //           C - Container
            //
            //           U - Super B
            //           H - Floats
            //           L - LowBoy / RGN
            //           O - Other
            //           N - Curtainside
            //           P - Power Only
            //           I - Roll Tite Trailer
            //           E - Dump Trailer
            //
            //Returns    Vehicle type Num
            //Effects    None
            //-----------------------------------------------------------------------------------
            // Change Log
            // Date       Developer    Description
            // -------    -----------  --------------------------------------------------------------
            // 2017.03.20 Hiro         Initial Release
            //-----------------------------------------------------------------------------------
            int RetVType = 0;

            for (int i = 0; i <= ((sVType.Length) - 1); i++)
            {
                RetVType += (int)(Math.Pow(2, VTYPE_CHAR_CODES.IndexOf((sVType.Substring(i, 1)))));
            }

            return RetVType;
        }

        public static string VSizeNumToString(int VSize, string PostType)
        {
            string tempConvertVsize = "";
            //*******************************************************************************************************
            //* Name	 : VSizeNumToString(VSize)
            //* Purpose  : Function to convert the vsize number to the proper designation
            //*			
            //* Inputs   : VSize: Numeric value for Vehicle Size
            //*            PostType: E:Equipment, L:Load
            //*
            //* Outputs: : String
            //*
            //*******************************************************************************************************		
            // Change Log
            // Date       Developer    Description
            // -------    -----------  --------------------------------------------------------------
            // 2018.01.19 Hiro         Initial Release
            //*******************************************************************************************************		
            switch (VSize)
            {
                case 1:
                    if (PostType == "E")
                    {
                        tempConvertVsize = "R";
                    }
                    else
                    {
                        tempConvertVsize = "Q";
                    }
                    break;
                case 2:
                    tempConvertVsize = "H";
                    break;
                case 3:
                    tempConvertVsize = "A";
                    break;
                case 4:
                    tempConvertVsize = "L";
                    break;
                case 7:
                    tempConvertVsize = "I";
                    break;
                case 8:
                    tempConvertVsize = "T";
                    break;
                case 15:
                    tempConvertVsize = "U";
                    break;
                default:
                    tempConvertVsize = "-";
                    break;
            }
            return tempConvertVsize;
        }

        public static int EquipmentVSizeStringToNum(string sVSize)
        {
            int tempConvertVsize = 0;
            //*******************************************************************************************************
            //* Name	 : EquipmentVSizeStringToNum(sVSize)
            //* Purpose  : Function to convert the Equipment post vehicle designation to proper vehicle number
            //*			
            //* Inputs   : sVSize as String
            //*
            //* Outputs: : String
            //*
            //*******************************************************************************************************		
            // Change Log
            // Date       Developer    Description
            // -------    -----------  --------------------------------------------------------------
            // 2017.11.22 Kruti         Initial Release
            //*******************************************************************************************************		
            switch (sVSize)
            {
                //Full Truckload
                case "U":
                    tempConvertVsize = 15;
                    break;
                // 3/4 Truckload
                case "I":
                    tempConvertVsize = 7;
                    break;
                // Half or less Truckload
                case "A":
                    tempConvertVsize = 3;
                    break;
                // 1/4 Truckload
                case "R":
                    tempConvertVsize = 1;
                    break;
                default:
                    tempConvertVsize = 0;
                    break;
            }
            return tempConvertVsize;
        }
        public static int LoadVSizeStringToNum(string sVSize)
        {
            int tempConvertVsize = 0;
            //*******************************************************************************************************
            //* Name	 : VSizeStringToNum(sVSize)
            //* Purpose  : Function to convert the Load Post vehicle designation to proper vehicle number
            //*			
            //* Inputs   : sVSize as String
            //*
            //* Outputs: : String
            //*
            //*******************************************************************************************************		
            // Change Log
            // Date       Developer    Description
            // -------    -----------  --------------------------------------------------------------
            // 2017.11.22 Kruti         Initial Release
            //*******************************************************************************************************		
            switch (sVSize)
            {
                //Full Truckload
                case "T":
                    tempConvertVsize = 8;
                    break;
                // 3/4 Truckload
                case "L":
                    tempConvertVsize = 4;
                    break;
                // Half or less Truckload
                case "H":
                    tempConvertVsize = 2;
                    break;
                // 1/4 Truckload
                case "Q":
                    tempConvertVsize = 1;
                    break;
                default:
                    tempConvertVsize = 0;
                    break;
            }
            return tempConvertVsize;
        }

        public static int VSizeStringToNum(string sVSize)
        {
            int tempConvertVsize = 0;
            //*******************************************************************************************************
            //* Name	 : VSizeStringToNum(sVSize)
            //* Purpose  : Function to convert the vehicle designation to proper vehicle number
            //*			
            //* Inputs   : sVSize as String
            //*
            //* Outputs: : String
            //*
            //*******************************************************************************************************		
            // Change Log
            // Date       Developer    Description
            // -------    -----------  --------------------------------------------------------------
            // 2017.03.20 Hiro         Initial Release
            //*******************************************************************************************************		
            switch (sVSize)
            {
                case "T":
                    tempConvertVsize = 1;
                    break;
                case "L":
                    tempConvertVsize = 2;
                    break;
                case "B":
                    tempConvertVsize = 3;
                    break;
                default:
                    tempConvertVsize = 0;
                    break;
            }
            return tempConvertVsize;
        }
        public static int PostingAttributeStringToNum(string sPostAttrib)
        {
            //-----------------------------------------------------------------------------------
            //Purpose    This function will return a numeric representation of the
            //           Posting Attribute string
            //Inputs     Posting Attribute string
            //
            //Supported Posting Attribute
            //
            //	A	-	Air Ride 	
            //	B	-	B-train 
            //	W	-	Blanket Wrap
            //	C	-	Chains
            //	Z	-	Haz-Mat
            //	I	-	Insulated
            //	T	-	Tarps
            //	V	-	Vented
            //	X	-	Triaxle
            //	M	-	Team
            //	G	-	Tailgate
            //	F	-	Frozen 
            //	H	-	Heat
            //	E	-	Expedite
            //  N   -   Inbound
            //
            //Returns    Posting Attribute Number
            //Effects    None
            //-----------------------------------------------------------------------------------
            // Change Log
            // Date       Developer    Description
            // -------    -----------  --------------------------------------------------------------
            // 2017.03.20 Hiro         Initial Release
            //-----------------------------------------------------------------------------------
            int RetVType = 0;

            sPostAttrib = sPostAttrib.ToUpper();

            for (int i = 0; i <= ((sPostAttrib.Length) - 1); i++)
            {
                RetVType += (int)(Math.Pow(2, POSTATTRIB_CHAR_CODES.IndexOf((sPostAttrib.Substring(i, 1)))));
            }

            return RetVType;
        }


        #endregion
    }
}
