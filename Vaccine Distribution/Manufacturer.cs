using System;
using System.Collections.Generic;
using System.Linq;

namespace VaccineDistribution
{
    internal class Manufacturer
    {
       static int TotalDose = 50000;
       private readonly static int PricePerDose = 20;
       private readonly static int ManufacturingCostPerDose = 15;
       static int TotalManufacturerReceivableAmount;
       public  static List<Hospital> ListOfHospitals = new List<Hospital>();

        public static void RegisterHospital()
          {
            try
            {
                Console.WriteLine("Enter the below details to get your hospital registered!!");
                Console.WriteLine("Enter your name:");
                string name = Console.ReadLine();
                while (name.Any(char.IsDigit) || name == "" || name.Length < 4)
                {
                    Console.WriteLine("Enter your name again & check name doesn't have a numbers\n");
                    Console.WriteLine("Enter your name:");
                    name = Console.ReadLine();
                }

                Console.WriteLine("Enter your Vaccine capacity:");
                string vaccCap = Console.ReadLine();
                while (!vaccCap.All(char.IsDigit) || vaccCap == "" || Convert.ToInt32(vaccCap) <= 0)
                {
                    Console.WriteLine("Enter your capacity again & check capacity doesn't have letters or special Characters!!\n");
                    Console.WriteLine("Enter your vaccine requirement:");
                    vaccCap = Console.ReadLine();
                }
                int capacity = int.Parse(vaccCap);
                string UniqueId = HelperClass.GenarateUniqueID(name);
                Console.WriteLine($"\nThe Hospital id is:{UniqueId}");
                var HospitalObj = new Hospital() { Id = UniqueId, Name = name, Capacity = capacity, VaccineBudget = 200000 };
                ListOfHospitals.Add(HospitalObj);
                HelperClass.RegistrationSuccess();
            }
            catch (Exception e)
            {
                HelperClass.ErrorMsgHospitalCapacity();
                Console.WriteLine(e.Message);
            }
          }
        public static void SupplyVaccines()
        {
            if (ListOfHospitals.Count != 0)
            {
                Hospital hospital = null;
                bool r = true;
                while (r)
                {
                    ShowListOfHospitals();
                    Console.WriteLine("Enter Hospital ID");
                    string userInputID = Console.ReadLine();
                    hospital = ListOfHospitals.Find(h => h.Id == userInputID);
                   if(hospital == null)
                    {
                        Console.WriteLine("Please Enter Valid Hospital Id");
                    }
                    else
                    {
                        r = false;
                    }
                }
                bool x = true;
                while (x)
                {
                    Console.WriteLine("Please Enter the Doses to be Supplied");
                    string supplyUserRequirmenttext = Console.ReadLine();
                    if (supplyUserRequirmenttext != "")
                    {
                        if (int.TryParse(supplyUserRequirmenttext, out int number))
                        {
                            int supplyUserRequirment = Convert.ToInt32(supplyUserRequirmenttext);
                            if (PricePerDose * supplyUserRequirment <= hospital.VaccineBudget)
                            {
                                if (supplyUserRequirment > 0 && supplyUserRequirment <= hospital.Capacity && supplyUserRequirment <= TotalDose && hospital.AvailableDose < hospital.Capacity)
                                {
                                    hospital.TotalDose += supplyUserRequirment;
                                    hospital.AvailableDose += supplyUserRequirment;
                                    hospital.HospitalTotalPayableAmount += PricePerDose * supplyUserRequirment;
                                    hospital.VaccineBudget -= hospital.HospitalTotalPayableAmount;
                                    TotalManufacturerReceivableAmount += hospital.HospitalTotalPayableAmount;
                                    TotalDose -= supplyUserRequirment;
                                    Console.WriteLine("Dose Supplied is: " + supplyUserRequirment);
                                    Console.WriteLine(hospital.VaccineBudget);
                                    Console.WriteLine(TotalDose);
                                    Console.WriteLine("--------------**** The Supply is Successful ****----------");
                                    x = false;
                                }
                                else
                                {
                                    Console.WriteLine("Requested Doses are exceeding limit");
                                }
                            }
                            else
                            {
                                Console.WriteLine("The Hospital Vaccine Budget reached its Max Limit");
                                x = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Input Should not have letters or special characters");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Empty Input Is Not accepted Please Try again!!");
                    }
                }
            }
            else
            {
                HelperClass.ErrorDisplayHospital();
            }
        }
        public static void RemoveHospital()
        {
            if (ListOfHospitals.Count != 0)
            {
                bool a = true;
                while (a)
                {
                    ShowListOfHospitals();
                    Console.WriteLine("Enter the Hospital Id to be Deleted");
                    string deleteHospitalId = Console.ReadLine();
                    Hospital hospitalObj = ListOfHospitals.Find(hospital => hospital.Id == deleteHospitalId);
                    if (hospitalObj == null)
                    {
                        HelperClass.ErrorMsgHospitalId();
                    }
                    else
                    {
                        ListOfHospitals.Remove(hospitalObj);
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine($"*** Hospital with Id {hospitalObj.Id} Removed Successfully ***");
                        Console.WriteLine("---------------------------------------------------------------------------");
                        a = false;
                    }
                }

            }
            else
            {
                Console.WriteLine("----------------------------------------------\n*** No Hospital Entity to Remove From Data ****\n----------------------------------------------");
            }

        }
        public static void ShowListOfHospitals()
        {
            if (ListOfHospitals.Count != 0)
            {

                ListOfHospitals.ForEach(hospital =>
                {
                    Console.WriteLine($"Name: {hospital.Name} || UniqueId: {hospital.Id} || Capacity :{hospital.Capacity} || Doses Available: {hospital.AvailableDose}");
                });
            }
            else
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("*** No Hospital to Display ***");
                Console.WriteLine("---------------------------------------------------------------");
            }
        }
        public static void NumberOfVaccinesSupplied()
        {
            if (ListOfHospitals.Count != 0)
            {
                ListOfHospitals.ForEach(hospital => {
                    Console.WriteLine($"{ListOfHospitals.IndexOf(hospital) + 1}. {hospital.Name}");
                });
                bool a = true;
                while (a)
                {
                    try
                    {
                        int HospitalIndex;
                        Console.WriteLine("Please Select the Hospital by Entering its No.");
                        HospitalIndex = int.Parse(Console.ReadLine());
                        ListOfHospitals.ForEach(hospital => {
                            if (HospitalIndex <= 0 || ListOfHospitals.Count < HospitalIndex)
                            {
                                throw new Exception("Please Enter Valid serial No.");
                            }
                            else
                            {
                                if (ListOfHospitals[HospitalIndex - 1] == hospital)
                                {
                                    Console.WriteLine("---------------------------------------------------------------------------");
                                    Console.WriteLine("The Total Number of Doses Supplied by Manufacturer: " + hospital.TotalDose);
                                    Console.WriteLine("\n");
                                    Console.WriteLine("The Total Number of Doses Available in Hospital: " + hospital.AvailableDose);
                                    Console.WriteLine("---------------------------------------------------------------------------");
                                    a = false;
                                }
                            }
                        });
                    }
                    catch(Exception x)
                    {
                        Console.WriteLine(x.Message);
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("Letter & Special Characters are not allowed as Input");
                        Console.WriteLine("---------------------------------------------------------------------------");
                    }
                }
            }
            else
            {
                HelperClass.ErrorDisplayHospital();
            }
        }
        public static void TotalReceivableAmount()
        {
            Console.WriteLine($"Total amount to be received by the Manufacturer is {TotalManufacturerReceivableAmount}");
        }
        public static void ExpectedRetunsPerHospital()
        {
            if (ListOfHospitals.Count != 0)
            {
                ListOfHospitals.ForEach(hospital =>
                {
                    Console.WriteLine($"{ListOfHospitals.IndexOf(hospital) + 1}. {hospital.Name}");
                });
                bool b = true;
                while (b)
                {
                    try
                    {
                        int HospitalIndex;
                        Console.WriteLine("Please Select the Hospital by Entering its No.");
                        HospitalIndex = int.Parse(Console.ReadLine());
                        ListOfHospitals.ForEach(hospital =>
                        {
                            if (HospitalIndex <= 0 || ListOfHospitals.Count < HospitalIndex)
                            {
                                throw new Exception("Hospital Serial No. starts from 1");

                            }
                            else
                            {
                                if (ListOfHospitals[HospitalIndex - 1] == hospital)
                                {
                                    Console.WriteLine("---------------------------------------------------------------------------");
                                    Console.WriteLine($"Total amount to be paid by {hospital.Name} is : {hospital.HospitalTotalPayableAmount}");
                                    Console.WriteLine("---------------------------------------------------------------------------");
                                    b = false;
                                }
                            }
                        });

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Please Enter the serial number among the appeared Hospital List");
                    }
                }
            }
            else
            {
                HelperClass.ErrorDisplayHospital();
            }
        } 
        public static void IncomeStatment()
        {
            int vaccineSuppliedByManufacturer=0;
            ListOfHospitals.ForEach((hospital) =>
            {
                vaccineSuppliedByManufacturer += hospital.TotalDose;
            });
            if (ManufacturingCostPerDose < PricePerDose) {
                int profitPerDose = PricePerDose - ManufacturingCostPerDose;
                Console.WriteLine($"Total Profit gained by Manufacturer is: {profitPerDose * vaccineSuppliedByManufacturer}");
            }
            else
            {
                int losePerDose = ManufacturingCostPerDose - PricePerDose;
                Console.WriteLine($"Total Profit gained by Manufacturer is: {losePerDose * vaccineSuppliedByManufacturer}");
            }
        }
    }
}
