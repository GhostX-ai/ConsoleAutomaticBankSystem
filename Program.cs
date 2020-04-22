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
        }
        static void Welcoming()
        {
            Console.Write("1-для регистрации\n2-для входа\n3-для выхода");
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

        private static void SingIn()
        {
            UAccaount ut = new UAccaount();
            Console.Write("Login:");
            int lg = int.Parse(Console.ReadLine());
            string pass1 = Pass("Password");
            string pass2 = Pass("Repeat password");
            if (pass1 != pass2)
            {
                Console.WriteLine("Пароли не одинаковые!");
                SingIn();
            }
            else
            {
                ut.Login = lg;
                ut.Password = pass1;
                Console.Write("Введите Ф.И.О:");
                ut.FullName = Console.ReadLine();
                Console.Write("Введите Пол(М/Ж):");
                ut.Gender = Console.ReadLine();
                Console.Write("Введиет семейное положение(женать/замужем/нет никого):");
                ut.FStatus = Console.ReadLine();
                Console.Write("Введиет ваш возраст:");
                ut.Age = int.Parse(Console.ReadLine());
                Console.Write("Гражданство:");
                ut.CityZone = Console.ReadLine().ToLower();
                Console.Write("Номер паспорта:");
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

        private static void SingUp()
        {
            Console.Write("Login:");
            int log = int.Parse(Console.ReadLine());
            string pass = Pass("Pass");
            ua = ua.SignIn(log, pass);
            if (ua.id == 0 || ua == null)
            {
                Console.WriteLine("Login или Password был неправельно введён!");
                SingUp();
            }
        }
    }
}
