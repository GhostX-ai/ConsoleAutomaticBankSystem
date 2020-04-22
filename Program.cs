using System;
using System.Collections.Generic;
using CABS.Models;
namespace CABS
{
    class Program
    {
        static UAccaount ua = new UAccaount();
        static void Main()
        {
            Welcoming();
            if (ua != null)
            {
                if (ua.Role == "Client")
                {
                    ClientPart();
                }
            }
        }

        private static void ClientPart()
        {
            Console.Write($"Welcome {ua.FullName}\n1 for add an application\n2 for show your history of applications\n3 for Exit\n");
            int chs = int.Parse(Console.ReadLine());
            if (chs == 1)
            {
                UApp uapp = new UApp();
                Console.Write("Credit sum:");
                uapp.CreditSum = double.Parse(Console.ReadLine());
                Console.Write("Credit goal:");
                uapp.CreditGoal = Console.ReadLine();
                Console.Write("Credit deadline(yyyy-MM-dd)");
                uapp.CreditDeadLine = DateTime.Parse(Console.ReadLine());
                uapp.UId = ua.id;
                uapp.Add(uapp);
            }
        }

        static void Welcoming()
        {
            Console.Write("1-for SignUp\n2-for SignIn\n3-for Exit\n");
            int chs = 0;
            try
            {
                chs = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Welcoming();
            }
            switch (chs)
            {
                case 1:
                    {
                        SingUp();
                    }
                    break;
                case 2:
                    {
                        SingIn();
                    }
                    break;
            }
        }

        private static void SingUp()
        {
            UAccaount ut = new UAccaount();
            Console.Write("Login(your numbers):");
            int lg = int.Parse(Console.ReadLine());
            string pass1 = Pass("Password");
            string pass2 = Pass("Repeat password");
            if (pass1 != pass2)
            {
                Console.WriteLine("Passwords are not identity!");
                SingIn();
            }
            else
            {
                ut.Login = lg;
                ut.Password = pass1;
                Console.Clear();
                Console.Write("FullName:");
                ut.FullName = Console.ReadLine();
                Console.Write("Gender(M/F):");
                ut.Gender = Console.ReadLine();
                Console.Write("Family status(married or not):");
                ut.FStatus = Console.ReadLine();
                Console.Write("Age:");
                ut.Age = int.Parse(Console.ReadLine());
                Console.Write("Do you wont to be Admin?Y/N");
                string chs = Console.ReadLine();
                ut.RoleId = chs == "Y" ? 1 : 2;
                Console.Write("CityZone:");
                ut.CityZone = Console.ReadLine().ToLower();
                Console.Write("Identity Card Number:");
                ut.ICNum = Console.ReadLine();
                ua.Add(ut);
                ua = ut;
            }
        }
        static string Pass(string txt)
        {
            // \b-backspace
            // \r-enter
            string pass = "";
            while (true)
            {
                Console.Clear();
                Console.Write(txt);
                foreach (var x in pass)
                {
                    Console.Write("*");
                }
                char pc = Console.ReadKey().KeyChar;
                if (pc == '\b')
                {
                    EndLess(ref pass);
                }
                else if (pc == '\r')
                {
                    break;
                }
                else
                {
                    pass += pc.ToString();
                }
            }
            return pass;
        }
        static void EndLess(ref string x)
        {
            char[] z = x.ToCharArray();
            x = "";
            for (int i = 0; i < z.Length - 1; i++)
            {
                x += z[i];
            }
        }

        private static void SingIn()
        {
            Console.Write("Login:");
            int log = int.Parse(Console.ReadLine());
            string pass = Pass("Pass");
            ua = ua.SignIn(log, pass);
            if (ua.id == 0 || ua == null)
            {
                Console.WriteLine("Login or Password was not correct!");
                SingIn();
            }
        }
    }
}
