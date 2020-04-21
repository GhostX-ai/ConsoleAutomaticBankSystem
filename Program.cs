using System;
using System.Collections.Generic;
using CABS.Models;
namespace CABS
{
    class Program
    {
        UAccaount ua = new UAccaount();
        static void Main()
        {

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
            Console.Write("Login:");
            int lg = int.Parse(Console.ReadLine());
            Console.Write("Password:");
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

        }
    }
}
