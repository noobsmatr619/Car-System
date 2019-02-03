using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string name, input, useranswer, username, oldUsername = "", caranswer, findThisName="", newusername, usercaranswer, choicewriter;
            string[] carnames = new string[] { "FIESTA", "MONDEO", "ASTRA", "CORSA", "COROLLA", "IA", "CRUZE", "PRIUS", "MIATA", "OPTIMA", "IMPALA", "FORESTER", "HIGHLANDER", "QSEVEN", "RIDGELINE", "IMPREZA", "CAMRY", "SORNETO", "SIENNA", "BOXTER" };

             double password, newpassword, oldPassword = 1;
            int userdaychoice;

            List<string> registeredUserList = new List<string>();
            List<string> registeredUserPassList = new List<string>();
            List<string> registeredUserAndPassList = new List<string>();

            List<string> packagesSelected = new List<string>();
            List<string> carList = new List<string>();
            List<double> dayList = new List<double>();
            List<double> amountList = new List<double>();
            List<DateTime> pickList = new List<DateTime>();

        appStart:
            Console.WriteLine("Welcome to PUIC Car Rentals, Would you like to Rent a car Today ?");
            Console.WriteLine("If Yes Press 'Y' if No Press 'N' ");
            input = Console.ReadLine();
            Array.Sort(carnames);

            string cType = "New";

            if (input != null && input.ToUpper() == "Y")
            {
            //Console.Write("Please Enter Your name to continue\n");
            //name = Console.ReadLine();
            //Console.WriteLine("For new user you need to sign up first. \n If you have a user name and password then use the login portal,for new user input 'new' for login insert 'login' ");

            userType:
                if (cType == "Old")
                {
                    useranswer = "L";
                }
                else
                {
                    Console.WriteLine("New User ? Press 'R' to Register. \n Old User ? Press 'L' to Log In. \n Press Any Other Key To Go Back.");

                    useranswer = Console.ReadLine();
                    if (useranswer.ToUpper() != "L" && useranswer.ToUpper() != "R")
                    {
                        Console.Clear();
                        goto appStart;
                    }
                }

                if (useranswer.ToUpper() == "R")
                {
                signupProcess:
                    Console.WriteLine("Insert your Desired Username only in letter [8 Letters]");
                    oldUsername = Console.ReadLine().ToString();


                    Console.WriteLine("Enter your Desired Password only in numbers [6 Numbers]");
                    string tX = Console.ReadLine();
                    bool ok = Double.TryParse(tX, out oldPassword);
                    if (ok)
                    {

                        if ((oldUsername.Length == 8 && oldPassword.ToString().Length == 6))
                        {
                            registered:
                            Console.Clear();
                            registeredUserList.Add(oldUsername);
                            registeredUserPassList.Add(oldPassword.ToString());
                            registeredUserAndPassList.Add(oldUsername + "&" + oldPassword);

                            Console.WriteLine("Sign up complete please login now");
                            cType = "Old";
                            goto userType;
                        }

                        else
                        {
                            regLengthError:
                            Console.Clear();
                            Console.WriteLine("Username And Password Length is wrong.\n Want to Try Again ? Press 'Y'. Or Press Any Key to Go Back.");
                            string readLine = Console.ReadLine();
                            if (readLine != null && readLine.ToUpper() == "Y")
                            {
                                Console.Clear();
                                goto signupProcess;
                            }
                            else
                            {
                                Console.Clear();
                                goto userType;
                            }
                        }

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Password Format Wasn't Correct. Try Again.");
                        goto userType;
                    }
                }
                if (useranswer.ToUpper() == "L")
                {
                login:
                    try
                    {
                        Console.WriteLine("Enter your Username [8 Letters]");

                        username = Console.ReadLine();

                        Console.WriteLine("Enter your Password [6 Numbers]");
                        string tX = Console.ReadLine();
                        bool ok = Double.TryParse(tX, out password);

                        if (ok)
                        {

                            if (username.Length == 8 && password.ToString().Length == 6)
                            {
                                if (registeredUserList.Contains(username))
                                {
                                    if (registeredUserAndPassList.Contains(username + "&" + password))
                                    {
                                        Console.WriteLine("Successfully Logged In.");
                                        loggedIn:
                                        Console.WriteLine("To Search for a Car, Press 'S'. \n To See All Cars, Press 'A' \n Press Any Other Key to Log Out.");

                                        caranswer = Console.ReadLine();

                                        search:
                                        if (caranswer.ToUpper() == "S")
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Which car do you want to search for? Write a Name - ");
                                            findThisName = Console.ReadLine();
                                            findThisName = findThisName.Trim();

                                        }
                                        else if (caranswer.ToUpper() == "A")
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Select Car number on left of Car name to Rent That Car.");
                                            for (int i = 0; i < carnames.Length; i++)
                                            {
                                                Console.WriteLine((i+1)+". "+carnames[i]+"\t");
                                                if (i + 1 < carnames.Length)
                                                {
                                                    i++;
                                                    Console.WriteLine((i + 1) + ". " + carnames[i] + "\t");
                                                }
                                                if (i + 1 < carnames.Length)
                                                {
                                                    i++;
                                                    Console.WriteLine((i + 1) + ". " + carnames[i] + "\t");
                                                }
                                            }

                                            string tI = Console.ReadLine();

                                            string selectedCar = carnames[Int32.Parse(tI)-1];

                                            if (String.IsNullOrEmpty(selectedCar))
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Please, Enter Valid Input. Like - 1, 2, 3 etc.");
                                                goto loggedIn;
                                            }
                                            else
                                            {
                                                findThisName = selectedCar;
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            goto appStart;
                                        }

                                        if (carnames.Contains(findThisName.ToUpper()))
                                        {
                                            Console.Clear();
                                            Console.WriteLine("We've Found Your Car - " + findThisName + " !!!");
                                            Console.WriteLine("Press 'R' to Rent this Car. Press 'N' to Get another Car. \n Press Any Other Key to Go Back.");
                                            string tI = Console.ReadLine();
                                            double amount = 0;

                                            if (tI.ToUpper() == "R")
                                            {
                                                Console.Clear();
                                                packageChoose:
                                                Console.WriteLine("Press 'P' if You Want To Get a Car Today with Package. \n" +
                                                                  "Press 'M' to Give a Date Range Manually. Press Any Other Key to Go Back.");


                                                tI = Console.ReadLine();


                                                double days = 0;
                                                double perDayAmount = 0, totalAmount = 0;

                                                DateTime pickUpDate = new DateTime();

                                                if (tI.ToUpper() == "P")
                                                {

                                                    Console.WriteLine("We've got Three Offers for Your Car - " + findThisName);
                                                    Console.WriteLine("Press '1' To - Rent For 1 Day at 100 Euro.\n " +
                                                                      "Press '2' To - Rent For 3 Days at 250 Euro.\n " +
                                                                      "Press '3' To - Rent For 7 Days at 500 Euro.");


                                                    tI = Console.ReadLine();

                                                    if (tI == "1")
                                                    {
                                                        days = 1;
                                                        amount = 100;
                                                    }
                                                    else if (tI == "2")
                                                    {
                                                        days = 3;
                                                        amount = 250;
                                                    }
                                                    else if (tI == "3")
                                                    {
                                                        days = 7;
                                                        amount = 500;
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        goto packageChoose;
                                                    }

                                                    totalAmount = amount;
                                                    pickUpDate = DateTime.Today.Date;
                                                }
                                                else if (tI.ToUpper() == "M")
                                                {

                                                    Console.WriteLine("When You Want to Pick Up ? Enter Date Like - 25/09/2017");
                                                    tI = Console.ReadLine();

                                                    Console.WriteLine("When You Want to Give it Back ? Enter Date Like - 28/09/2017");
                                                    string tI2 = Console.ReadLine();
                                                    DateTime startDate = new DateTime();
                                                    DateTime endDate = new DateTime();
                                                    string[] formats = { "dd/MM/yyyy", "d/M/yyyy" };
                                                    bool oks = DateTime.TryParseExact(tI, formats, new CultureInfo("en-US"),
                                                        DateTimeStyles.None, out startDate);
                                                    bool oks2 = DateTime.TryParseExact(tI2, formats, new CultureInfo("en-US"),
                                                        DateTimeStyles.None, out endDate);

                                                    if (!oks || !oks2)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Invalid Date Format.");
                                                        goto packageChoose;
                                                    }
                                                    else
                                                    {
                                                        days = (endDate - startDate).TotalDays;
                                                        if (days >= 7)
                                                        {
                                                            perDayAmount = Math.Truncate(Convert.ToDouble(500 / 7));
                                                        }
                                                        else if (days >= 3)
                                                        {
                                                            perDayAmount = Math.Truncate(Convert.ToDouble(250 / 3));
                                                        }
                                                        else if (days >= 1)
                                                        {
                                                            perDayAmount = 100;
                                                        }
                                                        totalAmount = days * perDayAmount;
                                                        if (totalAmount <= 0 || days <= 0)
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Give Minimum 1 Day.");
                                                            goto packageChoose;
                                                        }
                                                        else if (startDate < DateTime.Today)
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Can't Give Rent for Previous Date.");
                                                            goto packageChoose;
                                                        }
                                                        else
                                                        {
                                                            pickUpDate = startDate.Date;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    goto loggedIn;
                                                }


                                                List<string> tempCar = new List<string>();
                                                tempCar = carnames.ToList();
                                                tempCar.Remove(findThisName.ToUpper());
                                                carnames = tempCar.ToArray();
                                                carList.Add(findThisName);
                                                dayList.Add(days);
                                                amountList.Add(totalAmount);
                                                pickList.Add(pickUpDate);
                                                bought:
                                                Console.Clear();
                                                string tMsg =
                                                    findThisName + " Added to your Cart for " + days +
                                                    "Days. Total Payable = " + totalAmount + " Euro.";
                                                Console.WriteLine(tMsg);
                                                packagesSelected.Add(tMsg);

                                                Console.WriteLine("Press 'Y' To Rent Another Car. \n" +
                                                                  "Press Any Other Key to Finish Rent.");

                                                tI = Console.ReadLine();
                                                if (tI.ToUpper() == "Y")
                                                {
                                                    Console.Clear();
                                                    goto loggedIn;
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Your Total Rent Details - ");
                                                    Console.WriteLine("Rented Cars = " + carList.Count + " " + String.Join(", ", carList));
                                                    Console.WriteLine("Rented Days = " + dayList.Sum());
                                                    Console.WriteLine("Total Amount = " + amountList.Sum() + " Euro");

                                                    Console.WriteLine("Thank You for staying with us. Please Pick Up Your Rented Car - \n");
                                                    for (int i = 0; i < pickList.Count; i++)
                                                    {
                                                        Console.WriteLine(carList[i] + " - " + pickList[i].ToString("yyyy MMMM dd") + " for " + dayList[i] + " days. Get Back on " + pickList[i].AddDays(dayList[i]).ToString("yyyy MMMM dd"));
                                                    }

                                                    cType = "New";

                                                    Console.WriteLine("Press Any Key to Go to Home Page.");

                                                    Console.ReadLine();

                                                    goto appStart;
                                                }
                                            }
                                            else if (tI == "N")
                                            {
                                                Console.Clear();
                                                goto search;
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                goto loggedIn;
                                            }

                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Sorry, We Couldn't Find Your Car - " + findThisName);
                                            Console.WriteLine("Press 'S' to Search Again OR Any key to Go Back");
                                            string readLine = Console.ReadLine();
                                            if (readLine != null && readLine.ToUpper() == "S")
                                            {
                                                Console.Clear();
                                                goto search;
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                goto loggedIn;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        loginPassError:
                                        Console.Clear();
                                        Console.WriteLine("Password disn't matched.\n Want to Try Again ? Press 'Y'. Or Press Any Key to Go Back.");
                                        string readLine = Console.ReadLine();
                                        if (readLine != null && readLine.ToUpper() == "Y")
                                        {
                                            Console.Clear();
                                            goto login;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            cType = "New";
                                            goto userType;
                                        }
                                    }

                                }
                                else
                                {
                                    loginUserNameError:
                                    Console.Clear();
                                    Console.WriteLine("Username not found.\n Want to Try Again ? Press 'Y'. Or Press Any Key to Go Back.");
                                    string readLine = Console.ReadLine();
                                    if (readLine != null && readLine.ToUpper() == "Y")
                                    {
                                        Console.Clear();
                                        goto login;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        cType = "New";
                                        goto userType;
                                    }
                                }
                            }
                            else
                            {
                                loginLengthError:
                                Console.Clear();
                                Console.WriteLine("Username And Password Length is wrong.\n Want to Try Again ? Press 'Y'. Or Press Any Key to Go Back.");
                                string readLine = Console.ReadLine();
                                if (readLine != null && readLine.ToUpper() == "Y")
                                {
                                    Console.Clear();
                                    goto login;
                                }
                                else
                                {
                                    Console.Clear();
                                    cType = "New";
                                    goto userType;
                                }
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Password Format Wasn't Correct.\n Want to Try Again ? Press 'Y'. Or Press Any Key to Go Back.");
                            string readLine = Console.ReadLine();
                            if (readLine != null && readLine.ToUpper() == "Y")
                            {
                                Console.Clear();
                                goto login;
                            }
                            else
                            {
                                Console.Clear();
                                cType = "New";
                                goto userType;
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                        goto login;
                    }
                }
                if (useranswer.ToUpper() != "R" && useranswer.ToUpper() != "L")
                {
                    Console.Clear();
                    cType = "New";
                    goto userType;
                }
            }
            else if (input != null && input.ToUpper() == "N")
            {
                Console.WriteLine("Thanks for Using our services, have a nice day");
                Console.WriteLine("Press any Key to exit the portal");
                cType = "New";
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                cType = "New";
                goto appStart;
            }

        }
    }
}