using System.Linq;
using System;


namespace VaccineDistribution
{

    public class Hospital
    {
        public string Name;
        public int Capacity;
        public string Id;
        public int AvailableDose;
        public int TotalDose;
        public int SuppliedDose;
        public int HospitalTotalPayableAmount;
        public int VaccineBudget;
      
        public static void TransferVaccines()
        {
            if (Manufacturer.ListOfHospitals.Count > 1)
            {
               
                // Supplier Hospital
                Hospital supplyHospital = null;
                bool x = true;
                while (x)
                {
                    Manufacturer.ShowListOfHospitals();
                    Console.WriteLine("Enter the Id of Hospital Supplyig Vaccines");
                    string supplyHospitalId = Console.ReadLine();
                    supplyHospital = Manufacturer.ListOfHospitals.Find(hospital => hospital.Id == supplyHospitalId);
                    if(supplyHospital == null)
                    {
                        Console.WriteLine("***Please Enter the Valid Supplier Hospital Id*** \n" +
                            "check if the supply hospital has enough vaccines to supply");
                    }
                    else
                    {
                        x= false;
                    }
                }

                //Receiver hospital
                Hospital recieverHospital = null;
                bool y = true;
                while (y)
                {
                    Manufacturer.ShowListOfHospitals();
                    Console.WriteLine("Enter the Id of Hospital Recieving Vaccines");
                    string recieverHospitalId = Console.ReadLine();
                    recieverHospital = Manufacturer.ListOfHospitals.Find(hospital => hospital.Id == recieverHospitalId);
                    if(recieverHospital == null)
                    {
                        Console.WriteLine("***Please Enter the Valid Receiver Hospital Id***");
                    }
                    else
                    {
                        y= false;
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
                                Manufacturer.ListOfHospitals.ForEach(hospital =>
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
