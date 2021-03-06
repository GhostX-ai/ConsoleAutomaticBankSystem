﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using CABS.Models;
namespace CABS
{
    class Program
    {
        static UAccaount ua = new UAccaount();
        static void Main()
        {
        x1:
            CheckApp();
            Welcoming();
            if (ua != null)
            {
                if (ua.Role == "Client")
                {
                    ClientPart();
                }
                else if (ua.Role == "Admin")
                {
                    AdminPart();
                }
            }
            else
            {
                goto x1;
            }
        }
        static void CheckApp()
        {
            SqlConnection cn = new SqlConnection(@"Data Source = localhost;Initial Catalog = CADB; Integrated Security=True;");
            cn.Open();
            string cm = $"select * from U_Graph";
            SqlCommand cd = new SqlCommand(cm, cn);
            SqlDataReader r = cd.ExecuteReader();
            string cmd = "";
            while (r.Read())
            {
                if (r.GetValue("Months").ToString() == "0")
                {
                    UAppS(int.Parse(r.GetValue("U_AppId").ToString()));
                }
            }
            r.Close();
        }
        static void UAppS(int? id)
        {
            SqlConnection cn = new SqlConnection(@"Data Source = localhost;Initial Catalog = CADB; Integrated Security=True;");
            cn.Open();
            string cmd = $"update U_App set Done = 'True' where id = {id}";
            SqlCommand cd = new SqlCommand(cmd, cn);
            cd.ExecuteNonQuery();
        }
        private static void AdminPart()
        {
            while (true)
            {
            x1:
                Console.WriteLine($"Welcome to Admin panel\n1-for see all clients\n2-for see all clients and their apps\n3-for exit");
                int chs = 0;
                try
                {
                    chs = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("You have to write number!!!");
                    goto x1;
                }
                switch (chs)
                {
                    case 1:
                        {
                            var li = ua.SelectAll();
                            Console.WriteLine("id\tFullName\tFamily Status\tAge\tNationality\tGender\tIdentity Card's numbers");
                            foreach (var x in li)
                            {
                                Console.WriteLine($"{x.id}\t{x.FullName}\t{x.FStatus}\t{x.Age}\t{x.CityZone}\t{x.Gender}\t{x.ICNum}");
                                Console.WriteLine("=======================================================================================");
                            }
                        }
                        break;
                    case 2:
                        {
                            var li = ua.SelectAll();
                            UApp up = new UApp();
                            Console.WriteLine("id\tFullName\tFamily Status\tAge\tNationality\tGender\tIdentity Card's numbers");
                            foreach (var x in li)
                            {
                                Console.WriteLine($"{x.id}\t\t{x.FullName}\t\t{x.FStatus}\t\t{x.Age}\t\t{x.CityZone}\t\t{x.Gender}\t\t{x.ICNum}");
                                Console.WriteLine("=======================================================================================");
                                Console.WriteLine("\tid\tCreditSum\tCreditGoal\tCreditDeadLine\tPayment\tPayed");
                                Console.WriteLine("=======================================================================================");
                                foreach (var z in up.SelectAll())
                                {
                                    Console.WriteLine($"\t{z.id}\t\t{z.CreditSum}\t\t{z.CreditGoal}\t\t{z.CreditDeadLine}\t\t{z.Pay}\t\t{z.Done}");
                                    Console.WriteLine("=======================================================================================");
                                }
                            }
                        }
                        break;
                }
                if (chs == 3)
                {
                    break;
                }
            }
        }

        private static bool CountP(UApp uap)
        {
            int points = 0;
            points += ua.Gender.ToLower() == "m" ? 1 : 2;
            points += ua.FStatus.ToLower() == "married" ? 2 : ua.FStatus == "not" ? 1 : ua.FStatus == "break up" ? 1 : 2;
            points += ua.Age <= 25 ? 0 : ua.Age <= 35 ? 1 : ua.Age <= 62 ? 2 : 1;
            points += ua.CityZone.ToLower() == "tajikistan" ? 1 : 0;
            if (uap.Pay * 0.8 > uap.CreditSum) { points += 4; }
            if (uap.Pay * 0.8 <= uap.CreditSum && uap.Pay * 1.5 > uap.CreditSum) { points += 3; }
            if (uap.Pay * 1.5 <= uap.CreditSum && uap.Pay * 2.5 >= uap.CreditSum) { points += 2; }
            if (uap.Pay * 2.5 < uap.CreditSum) { points += 1; }
            points += uap.CreditGoal.ToLower() == "for phone" ? 0 : uap.CreditGoal.ToLower() == "equipment" ? 1 : uap.CreditGoal.ToLower() == "repairs" ? 1 : 0;
            points += uap.CreditDeadLine < 12 ? 1 : 1;
            var li = uap.SingleAllById(ua.id);
            int s = 0;
            foreach (var x in li)
            {
                if (x.Status == true && x.Done == true)
                {
                    s++;
                }
            }
            points += s > 3 ? 2 : s >= 1 && s <= 2 ? -1 : 0;
            return points >= 12;
        }
        private static void ClientPart()
        {
            while (true)
            {
            x2:
                Console.Write($"Welcome {ua.FullName}\n1 for add an application\n2 for show your history of applications\n3-for show your aceped apps with graphic\n4 for Exit\n");
                int chs = 0;
                try
                {
                    chs = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Write a number!");
                    goto x2;
                }
                if (chs == 1)
                {
                    UApp uapp = new UApp();
                c1:
                    Console.Write("Credit sum:");
                    try
                    {
                        uapp.CreditSum = (double.Parse(Console.ReadLine()));
                    }
                    catch
                    {
                        Console.WriteLine("Write a number");
                        goto c1;
                    }
                    Console.Write("Credit goal:");
                    uapp.CreditGoal = Console.ReadLine();
                    Console.Write("How many month do you need?");
                    uapp.CreditDeadLine = double.Parse(Console.ReadLine());
                c2:
                    Console.Write("You payment:");
                    try
                    {
                        uapp.Pay = double.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Write a number");
                        goto c2;
                    }
                    bool st = CountP(uapp);
                    uapp.Status = st;
                    uapp.UId = ua.id;
                    uapp.Add(uapp);
                    if (uapp.Status)
                    {
                        double pmonth = uapp.CreditSum / uapp.CreditDeadLine;
                        SqlConnection cn = new SqlConnection(@"Data Source = localhost;Initial Catalog = CADB; Integrated Security=True;");
                        cn.Open();
                        string cm = $"select * from U_App where UId = { ua.id } and CreditGoal = '{uapp.CreditGoal}' and CreditDeadLine = {uapp.CreditDeadLine}";
                        SqlCommand cd = new SqlCommand(cm, cn);
                        SqlDataReader r = cd.ExecuteReader();
                        UApp ml = new UApp();
                        while (r.Read())
                        {
                            ml = new UApp()
                            {
                                id = int.Parse(r.GetValue("id").ToString()),
                                UId = int.Parse(r.GetValue("UId").ToString()),
                                CreditSum = double.Parse(r.GetValue("CreditSum").ToString()),
                                CreditGoal = r.GetValue("CreditGoal").ToString(),
                                CreditDeadLine = double.Parse(r.GetValue("CreditDeadLine").ToString()),
                                Status = bool.Parse(r.GetValue("Status").ToString()),
                                Pay = double.Parse(r.GetValue("Pay").ToString())
                            };
                        }
                        cn.Close();
                        UGraph ug = new UGraph()
                        {
                            PMonth = pmonth,
                            Months = uapp.CreditDeadLine,
                            U_AppId = ml.id
                        };
                        ug.Add(ug);
                    }
                    string acceped = uapp.Status ? "aceped" : "refused";
                    Console.ForegroundColor = uapp.Status ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine($"You credit status {acceped}");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else if (chs == 2)
                {
                    UApp uapp = new UApp();
                    var li = uapp.SingleAllById(ua.id);
                    Console.WriteLine($"ID\tCreditSum\tCreditDeadline\tCreditStatus");
                    foreach (var x in li)
                    {
                        System.Console.WriteLine("====================================");
                        string acpd = x.Status ? "aceped" : "canceled";
                        Console.WriteLine($"{x.id}\t\t{x.CreditSum}\t\t{x.CreditDeadLine}\t\t{acpd}");
                        System.Console.WriteLine("====================================");
                    }
                }
                else if (chs == 3)
                {
                    UApp uapp = new UApp();
                    var li = uapp.SingleAllById(ua.id);
                    Console.WriteLine($"ID\tCreditSum\tCreditDeadline\tCreditStatus\tPayed");
                    foreach (var x in li)
                    {
                        System.Console.WriteLine("====================================");
                        Console.WriteLine($"{x.id}\t\t{x.CreditSum}\t\t{x.CreditDeadLine}\t{x.Status}\t\t{x.Done}");
                        System.Console.WriteLine("====================================");
                        UGraph ug = new UGraph();
                        ug = ug.SingleById(x.id);
                        Console.WriteLine("\tId\tPer Month\tMonths");
                        Console.WriteLine($"\t{ug.id}\t{ug.PMonth}\t\t{ug.Months}");
                        System.Console.WriteLine("====================================");
                    }
                    Console.WriteLine("Do you have money for paying this month?Y/N");
                    char ches = char.Parse(Console.ReadKey().KeyChar.ToString().ToLower());
                    Console.WriteLine();
                    if (ches == 'y')
                    {
                    d1:
                        Console.WriteLine("Enter ID of app from sight graph");
                        int id = 0;
                        try
                        {
                            id = int.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Please choose number");
                            goto d1;
                        }
                        SqlConnection cn = new SqlConnection(@"Data Source = localhost;Initial Catalog = CADB; Integrated Security=True;");
                        cn.Open();
                        string cm = $"select * from U_Graph where id={id}";
                        SqlCommand cd = new SqlCommand(cm, cn);
                        SqlDataReader r = cd.ExecuteReader();
                        string cmd = "";
                        while (r.Read())
                        {
                            cmd = $"update U_Graph set Months = {int.Parse(r.GetValue("Months").ToString()) - 1} where id = {id}";
                        }
                        r.Close();
                        cd.CommandText = cmd;
                        cd.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                else
                {
                    break;
                }
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
                ut.Role = ut.RoleId == 1 ? "Admin" : "Client";
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
