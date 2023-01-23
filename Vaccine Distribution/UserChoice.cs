using System;

namespace VaccineDistribution
{
    public class UserChoice
    {

        public static void PrintMenu(String[] options)
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
                                "Exit"
                };
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
                        Manufacturer.RegisterHospital();
                        break;
                    case 2:
                        Manufacturer.SupplyVaccines();
                        break;
                    case 3:
                        Hospital.TransferVaccines();
                        break;
                    case 4:
                        Manufacturer.RemoveHospital();
                        break;
                    case 5:
                        Manufacturer.ShowListOfHospitals();
                        break;
                    case 6:
                        Manufacturer.NumberOfVaccinesSupplied();
                        break;
                    case 7:
                        Manufacturer.ExpectedRetunsPerHospital();
                        break;
                    case 8:
                        Manufacturer.TotalReceivableAmount();
                        break;
                    case 9:
                        Manufacturer.IncomeStatment();
                        break;
                    case 10:
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
