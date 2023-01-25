using System;
using System.Linq;

namespace VaccineDistribution
{
    public class ManufacturerService:Global
    {
        public void RegisterHospital()
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
                HospitalList.Add(HospitalObj);
                HelperClass.RegistrationSuccess();
            }
            catch (Exception e)
            {
                HelperClass.ErrorMsgHospitalCapacity();
                Console.WriteLine(e.Message);
            }
        }
        public void SupplyVaccines()
        {
            Manufacturer m1 = new Manufacturer() { VaccineStock=50000,ManufacturingCostPerDose=15,PricePerDose=20};
            if (HospitalList.Count != 0)
            {
                Hospital hospital = null;
                bool r = true;
                while (r)
                {
                    ShowListOfHospitals();
                    Console.WriteLine("Enter Hospital ID");
                    string userInputID = Console.ReadLine();
                    hospital = HospitalList.Find(h => h.Id == userInputID);
                    if (hospital == null)
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
                            if ( m1.PricePerDose* supplyUserRequirment <= hospital.VaccineBudget)
                            {
                                if (supplyUserRequirment > 0 && supplyUserRequirment <= m1.VaccineStock && supplyUserRequirment+hospital.AvailableDose <= hospital.Capacity)
                                {
                                    hospital.TotalDose += supplyUserRequirment;
                                    hospital.AvailableDose += supplyUserRequirment;
                                    hospital.HospitalTotalPayableAmount += m1.PricePerDose * supplyUserRequirment;
                                    hospital.VaccineBudget -= hospital.HospitalTotalPayableAmount;
                                    totalManufacturerReceivableAmount += hospital.HospitalTotalPayableAmount;
                                    m1.VaccineStock -= supplyUserRequirment;
                                    Console.WriteLine("Dose Supplied is: " + supplyUserRequirment);
                                    Console.WriteLine(hospital.VaccineBudget);
                                    Console.WriteLine(m1.VaccineStock);
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
        public void RemoveHospital()
        {
            if (HospitalList.Count != 0)
            {
                bool a = true;
                while (a)
                {
                    ShowListOfHospitals();
                    Console.WriteLine("Enter the Hospital Id to be Deleted");
                    string deleteHospitalId = Console.ReadLine();
                    Hospital hospitalObj = HospitalList.Find(hospital => hospital.Id == deleteHospitalId);
                    if (hospitalObj == null)
                    {
                        HelperClass.ErrorMsgHospitalId();
                    }
                    else
                    {
                        HospitalList.Remove(hospitalObj);
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
        public void ShowListOfHospitals()
        {
            if (HospitalList.Count != 0)
            {

                HospitalList.ForEach(hospital =>
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
        public void NumberOfVaccinesSupplied()
        {
            if (HospitalList.Count != 0)
            {
                HospitalList.ForEach(hospital => {
                    Console.WriteLine($"{HospitalList.IndexOf(hospital) + 1}. {hospital.Name}");
                });
                bool a = true;
                while (a)
                {
                    try
                    {
                        int HospitalIndex;
                        Console.WriteLine("Please Select the Hospital by Entering its No.");
                        HospitalIndex = int.Parse(Console.ReadLine());
                        HospitalList.ForEach(hospital => {
                            if (HospitalIndex <= 0 || HospitalList.Count < HospitalIndex)
                            {
                                throw new Exception("Please Enter Valid serial No.");
                            }
                            else
                            {
                                if (HospitalList[HospitalIndex - 1] == hospital)
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
                    catch (Exception x)
                    {
                        Console.WriteLine(x.Message);
                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("Input should not be null or any letters & Special Characters");
                        Console.WriteLine("---------------------------------------------------------------------------");
                    }
                }
            }
            else
            {
                HelperClass.ErrorDisplayHospital();
            }
        }
        public void TotalReceivableAmount()
        {
            Console.WriteLine($"Total amount to be received by the Manufacturer is {totalManufacturerReceivableAmount}");
        }
        public void ExpectedRetunsPerHospital()
        {
            if (HospitalList.Count != 0)
            {
                HospitalList.ForEach(hospital =>
                {
                    Console.WriteLine($"{HospitalList.IndexOf(hospital) + 1}. {hospital.Name}");
                });
                bool b = true;
                while (b)
                {
                    try
                    {
                        int HospitalIndex;
                        Console.WriteLine("Please Select the Hospital by Entering its No.");
                        HospitalIndex = int.Parse(Console.ReadLine());
                        HospitalList.ForEach(hospital =>
                        {
                            if (HospitalIndex <= 0 || HospitalList.Count < HospitalIndex)
                            {
                                throw new Exception("Hospital Serial No. starts from 1");

                            }
                            else
                            {
                                if (HospitalList[HospitalIndex - 1] == hospital)
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
        public void IncomeStatment()
        {
            Manufacturer m1 = new Manufacturer() {PricePerDose = 20,ManufacturingCostPerDose=15 };   
            int vaccineSuppliedByManufacturer = 0;
            HospitalList.ForEach((hospital) =>
            {
                vaccineSuppliedByManufacturer += hospital.TotalDose;
            });
            if (m1.ManufacturingCostPerDose < m1.PricePerDose)
            {
                int profitPerDose = m1.PricePerDose - m1.ManufacturingCostPerDose;
                Console.WriteLine($"Total Profit gained by Manufacturer is: {profitPerDose * vaccineSuppliedByManufacturer}");
            }
            else
            {
                int losePerDose = m1.ManufacturingCostPerDose - m1.PricePerDose;
                Console.WriteLine($"Total Profit gained by Manufacturer is: {losePerDose * vaccineSuppliedByManufacturer}");
            }
        }
        
        public void RenameHospital()
        {
            ShowListOfHospitals();
            Console.WriteLine("Enter the Id of Hospital:");
            string Id = Console.ReadLine();
            Console.WriteLine("Enter New name of Hospital: ");
            string newHspName = Console.ReadLine(); 
            HospitalList.ForEach((hospital) => {
                if(hospital.Id == Id) {
                    hospital.Name = newHspName;
                }
                else
                {
                    Console.WriteLine("Please Enter valid Hospital Id");
                }
            });



        }




    }
}
