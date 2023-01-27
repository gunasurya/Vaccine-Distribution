using System;

namespace VaccineDistribution
{
     class UserChoice
    {
         
         static void PrintMenu(String[] options)
        {
            int i = 0;
            foreach (String option in options)
            {
                Console.WriteLine($"{++i}.{option}");
            }
            Console.Write("Choose your option : ");
        }

        public static void UserChoiceInput()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("**** VACCINE DISTRIBUTION MAIN MENU ****");
            Console.WriteLine("===============================================");
            string[] options = {
                                "Register hospital",
                                "Supply vaccines",
                                "Transfer vaccines",
                                "Remove hospitals",
                                "Show hospitals",
                                "Number of vaccines supplied",
                                "Total amount to be paid by Hospital",
                                "Total Amount to be received by Manufacturer",
                                "IncomeStatment Of Manufacturer",
                                "Add Manufacturer",
                                "Rename Hospital",
                                "Exit"
            };
            ManufacturerService manufacturerService = new ManufacturerService();
            HospitalService hospitalService = new HospitalService();
            int option;
            while (true)
            {

                PrintMenu(options);
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());

                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please enter an integer value between 1 and " + options.Length);
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine("An unexpected error happened. Please try again");
                    //Console.WriteLine(ex);
                    continue;
                }
                switch (option)
                {
                    case 1:
                        manufacturerService.RegisterHospital();
                        break;
                    case 2:
                        manufacturerService.SupplyVaccines();
                        break;
                    case 3:
                        hospitalService.TransferVaccines();
                        break;
                    case 4:
                        manufacturerService.RemoveHospital();
                        break;
                    case 5:
                        manufacturerService.ShowListOfHospitals();
                        break;
                    case 6:
                        manufacturerService.NumberOfVaccinesSupplied();
                        break;
                    case 7:
                        manufacturerService.ExpectedRetunsPerHospital();
                        break;
                    case 8:
                        manufacturerService.TotalReceivableAmount();
                        break;
                    case 9:
                        manufacturerService.IncomeStatment();
                        break;
                    case 10:
                        hospitalService.CreateAndAddManufacturer();
                        break;
                    case 11:
                        manufacturerService.RenameHospital();
                        break; 
                    case 12:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter an integer value between 1 and " + options.Length);
                        break;
                }

            }
        }
    }
}
