using System;

namespace VaccineDistribution
{
    public class HelperClass
    {
        
        public static string GenarateUniqueID(string hospitalName)
        {

            return hospitalName.Substring(0, 4) + DateTime.Now.ToString("dd-MM-yyyy HH:m:s");
            
        }
        public static void RegistrationSuccess()
        {
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("------- *** The Registration is Successful **** -------");
            Console.WriteLine("-------------------------------------------------------");

        }

        public static void ErrorMsgHospitalName()
        {
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("*** Please Enter Valid Hospital Name & check if name has minimum 4 letters***");
            Console.WriteLine("-------------------------------------------------------");
        }


        public static void ErrorMsgHospitalCapacity()
        {
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("*** Please Enter valid Vaccine Capacity ****");
            Console.WriteLine("-------------------------------------------------------");
        }

        public static void ErrorMsgHospitalId()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("****Please Enter the Valid  Hospital Id*****");
            Console.WriteLine("---------------------------------------------------------------------------");
        }

        public static void ErrorMsgExccedingStorgeCapacity()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("*** Please Enter Doses within your Storage Capacity ****");
            Console.WriteLine("---------------------------------------------------------------------------");
        }



        public static void ErrorMsgDoseCount()
        {
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("**** Please Enter Valid Dose Count ****");
            Console.WriteLine("---------------------------------------------------------------------------");
        }

      

        public static void ErrorDisplayHospital()
        {

            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("*** There is no Data Of Hospitals To Display ***");
            Console.WriteLine("---------------------------------------------------------------------------");

        }


        






    }
}
