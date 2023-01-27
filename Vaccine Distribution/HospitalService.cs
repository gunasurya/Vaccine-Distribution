using System;
using System.Linq;

namespace VaccineDistribution
{
    class HospitalService
    {
        public void CreateAndAddManufacturer()
        {
            Console.WriteLine("Enter name of Manufacturer");
            string name = Console.ReadLine();
            while(name.Any(char.IsDigit) || name == "") {
                Console.WriteLine("Enter your name again & check name doesn't have a numbers\n");
                Console.WriteLine("Enter your name:");
                name = Console.ReadLine();
            }
            Console.WriteLine("Enter name of Vaccine");
           string vaccineName =  Console.ReadLine();
            while (vaccineName.Any(char.IsDigit) || vaccineName == "")
            {
                Console.WriteLine("Enter your Vaccine name again & check name doesn't have a numbers\n");
                Console.WriteLine("Enter your Vaccine name:");
                vaccineName = Console.ReadLine();
            }
            Console.WriteLine("Enter cost per Dose");
            string cost = Console.ReadLine();
            while (!cost.All(char.IsDigit) || cost == "" || Convert.ToInt32(cost) <= 0)
            {
                Console.WriteLine("Enter your cost again & check cost doesn't have letters or special Characters!!\n");
                Console.WriteLine("Enter your cost:");
                cost = Console.ReadLine();
            }
            int costPerDose = int.Parse(cost);
            var ManufacturerObj = new Manufacturer{Name= name , VaccineName = vaccineName , PricePerDose = costPerDose};
            GlobalStorage.manufacturerList.Add(ManufacturerObj);
            GlobalStorage.manufacturerList.ForEach(manufacturer =>
            {
                Console.WriteLine($"{manufacturer.Name} || {manufacturer.VaccineName} || {manufacturer.PricePerDose} ");
            });
        }
        public void TransferVaccines()
        {
            if (GlobalStorage.HospitalList.Count > 1)
            {
                // Supplier Hospital
                Hospital supplyHospital = null;
                bool x = true;
                while (x)
                {
                    GlobalStorage.HospitalList.ForEach(h =>
                    {
                    Console.WriteLine($"{ h.Name}||{h.Id}||{h.AvailableDose}");
                    });
                    Console.WriteLine("Enter the Id of Hospital Supplyig Vaccines");
                    string supplyHospitalId = Console.ReadLine();
                    supplyHospital = GlobalStorage.HospitalList.Find(hospital => hospital.Id == supplyHospitalId);
                    if (supplyHospital == null)
                    {
                        Console.WriteLine("***Please Enter the Valid Supplier Hospital Id*** \n" +
                            "check if the supply hospital has enough vaccines to supply");
                    }
                    else
                    {
                        x = false;
                    }
                }

                //Receiver hospital
                Hospital recieverHospital = null;
                bool y = true;
                while (y)
                {
                    GlobalStorage.HospitalList.ForEach(h =>
                    {
                        Console.WriteLine($"{h.Name}||{h.Id}||{h.AvailableDose}");
                    });
                    Console.WriteLine("Enter the Id of Hospital Recieving Vaccines");
                    string recieverHospitalId = Console.ReadLine();
                    recieverHospital = GlobalStorage.HospitalList.Find(hospital => hospital.Id == recieverHospitalId);
                    if (recieverHospital == null)
                    {
                        Console.WriteLine("***Please Enter the Valid Receiver Hospital Id***");
                    }
                    else
                    {
                        y = false;
                    }
                }
                bool z = true;
                while (z)
                {
                    Console.WriteLine("Enter No. of Doses to be Transfered");
                    string dosesCount = Console.ReadLine();
                    if (dosesCount.All(char.IsDigit) && dosesCount != "")
                    {
                        int transferedDosesCount = Convert.ToInt32(dosesCount);
                        if (transferedDosesCount > 0)
                        {
                            if (transferedDosesCount <= supplyHospital.AvailableDose && transferedDosesCount + recieverHospital.AvailableDose <= recieverHospital.Capacity)
                            {
                                recieverHospital.AvailableDose += transferedDosesCount;
                                supplyHospital.AvailableDose -= transferedDosesCount;
                                supplyHospital.SuppliedDose += transferedDosesCount;
                                GlobalStorage.HospitalList.ForEach(hospital =>
                                {
                                    Console.WriteLine($"{hospital.Name}:{hospital.AvailableDose}");
                                    z = false;
                                });
                            }
                            else
                            {
                                Console.WriteLine("  *****Transfer Failed ****  ");
                            }
                        }
                        else
                        {
                            HelperClass.ErrorMsgDoseCount();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input should not be null or any letters & Special Characters");
                    }
                }
            }

            else
            {
                Console.WriteLine("There must be Minimum Two Hospitals to run Transfer Operation");
            }
        }
    }
}
