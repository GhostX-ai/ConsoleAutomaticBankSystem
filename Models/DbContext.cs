using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace CABS.Models
{
    class SCS
    {
        public static string CnSt = @"Data Source = localhost;Initial Catalog = CADB; Integrated Security=True;";
    }
    class UAccaount : SCS, IUAcc
    {
        public int id { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string FStatus { get; set; }
        public int Age { get; set; }
        public string CityZone { get; set; }
        public int Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ICNum { get; set; }

        private SqlConnection cn = new SqlConnection(CnSt);
        public void Add(UAccaount ua)
        {
            cn.Open();
            string cm = $"insert into U_Accaunt(RoleId,Fullname,Gender,FStatus,Age,CityZone,Login,Password,ICard) Values({ua.RoleId},'{ua.FullName}','{ua.Gender}','{ua.FStatus}',{ua.Age},'{ua.CityZone}',{ua.Login},'{ua.Password}','{ua.ICNum}');";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }

        public void Delete(int? id)
        {
            cn.Open();
            string cm = $"delete from U_Accaunt where id={id}";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }

        public List<UAccaount> SelectAll()
        {
            cn.Open();
            string cm = $"select * from U_Accaunt a join U_Roles p on a.RoleId = p.id";
            SqlCommand cd = new SqlCommand(cm, cn);
            List<UAccaount> li = new List<UAccaount>();
            SqlDataReader r = cd.ExecuteReader();
            while (r.Read())
            {
                li.Add(new UAccaount()
                {
                    id = int.Parse(r.GetValue("id").ToString()),
                    RoleId = int.Parse(r.GetValue("RoleId").ToString()),
                    Gender = r.GetValue("Gender").ToString(),
                    Age = int.Parse(r.GetValue("Age").ToString()),
                    CityZone = r.GetValue("CityZone").ToString(),
                    FStatus = r.GetValue("FStatus").ToString(),
                    FullName = r.GetValue("FullName").ToString(),
                    Login = int.Parse(r.GetValue("Login").ToString()),
                    Password = r.GetValue("Password").ToString(),
                    Role = r.GetValue("Role").ToString(),
                    ICNum = r.GetValue("ICard").ToString()
                });
            }
            cn.Close();
            return li;
        }

        public UAccaount SingleById(int? id)
        {
            cn.Open();
            string cm = $"select * from U_Accaunt a join U_Roles p on a.RoleId = p.id where a.id = {id}";
            SqlCommand cd = new SqlCommand(cm, cn);
            SqlDataReader r = cd.ExecuteReader();
            UAccaount uas = new UAccaount();
            while (r.Read())
            {
                uas = new UAccaount()
                {
                    id = int.Parse(r.GetValue("id").ToString()),
                    RoleId = int.Parse(r.GetValue("RoleId").ToString()),
                    Gender = r.GetValue("Gender").ToString(),
                    Age = int.Parse(r.GetValue("Age").ToString()),
                    CityZone = r.GetValue("CityZone").ToString(),
                    FStatus = r.GetValue("FStatus").ToString(),
                    FullName = r.GetValue("FullName").ToString(),
                    Login = int.Parse(r.GetValue("Login").ToString()),
                    Password = r.GetValue("Password").ToString(),
                    Role = r.GetValue("Role").ToString(),
                    ICNum = r.GetValue("ICard").ToString()
                };
            }
            cn.Close();
            return uas;
        }

        public UAccaount SignIn(int Login, string Password)
        {
            cn.Open();
            string cm = $"select * from U_Accaunt a join U_Roles p on a.RoleId = p.id where Login = {Login} and Password='{Password}'";
            SqlCommand cd = new SqlCommand(cm, cn);
            SqlDataReader r = cd.ExecuteReader();
            UAccaount uas = new UAccaount();
            while (r.Read())
            {
                uas = new UAccaount()
                {
                    id = int.Parse(r.GetValue("id").ToString()),
                    RoleId = int.Parse(r.GetValue("RoleId").ToString()),
                    Gender = r.GetValue("Gender").ToString(),
                    Age = int.Parse(r.GetValue("Age").ToString()),
                    CityZone = r.GetValue("CityZone").ToString(),
                    FStatus = r.GetValue("FStatus").ToString(),
                    FullName = r.GetValue("FullName").ToString(),
                    Login = int.Parse(r.GetValue("Login").ToString()),
                    Password = r.GetValue("Password").ToString(),
                    Role = r.GetValue("Role").ToString()
                };
            }
            cn.Close();
            return uas;
        }
        public void Update(UAccaount ua, int? id)
        {
            cn.Open();
            string cm = $"update U_Accaunt set RoleId = {ua.RoleId},FullName='{ua.FullName}',Gender='{ua.Gender}',FStatus'{ua.FStatus}',Age={ua.Age},CityZone='{ua.CityZone}',Login={ua.Login},Password='{ua.Password}' where id =";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }
    }
    interface IUAcc
    {
        int id { get; set; }
        int RoleId { get; set; }
        string ICNum { get; set; }
        string FullName { get; set; }
        string Gender { get; set; }
        string FStatus { get; set; }
        int Age { get; set; }
        string CityZone { get; set; }
        int Login { get; set; }
        string Password { get; set; }
        string Role { get; set; }
        void Add(UAccaount ua);
        List<UAccaount> SelectAll();
        UAccaount SingleById(int? id);
        void Delete(int? id);
        void Update(UAccaount ua, int? id);
    }
    class UApp : SCS, IUApp
    {
        public int id { get; set; }
        public int UId { get; set; }
        public double Pay { get; set; }
        public double CreditSum { get; set; }
        public bool Status { get; set; }
        public string CreditGoal { get; set; }
        public string CreditDeadLine { get; set; }

        private SqlConnection cn = new SqlConnection(CnSt);
        public void Add(UApp ua)
        {
            cn.Open();
            string cm = $"insert into U_App(UId,CreditSum,CreditGoal,CreditDeadLine,Status,Pay) Values({ua.UId},{ua.CreditSum},'{ua.CreditGoal}','{ua.CreditDeadLine}',{ua.Status},{ua.Pay});";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }

        public void Delete(int? id)
        {
            cn.Open();
            string cm = $"delete from U_App where id = {id}";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }

        public List<UApp> SelectAll()
        {
            cn.Open();
            string cm = $"select * from U_App";
            SqlCommand cd = new SqlCommand(cm, cn);
            SqlDataReader r = cd.ExecuteReader();
            List<UApp> li = new List<UApp>();
            while (r.Read())
            {
                li.Add(new UApp()
                {
                    id = int.Parse(r.GetValue("id").ToString()),
                    UId = int.Parse(r.GetValue("UId").ToString()),
                    CreditSum = double.Parse(r.GetValue("CreditSum").ToString()),
                    CreditGoal = r.GetValue("CreditGoal").ToString(),
                    CreditDeadLine = r.GetValue("CreditDeadLine").ToString(),
                    Status = bool.Parse(r.GetValue("Status").ToString()),
                    Pay = double.Parse(r.GetValue("Pay").ToString())
                });
            }
            cn.Close();
            return li;
        }

        public UApp SingleById(int? id)
        {
            cn.Open();
            string cm = $"select * from U_App where UId = {id}";
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
                    CreditDeadLine = r.GetValue("CreditDeadLine").ToString(),
                    Status = bool.Parse(r.GetValue("Status").ToString()),
                    Pay = double.Parse(r.GetValue("Pay").ToString())
                };
            }
            cn.Close();
            return ml;
        }
        public List<UApp> SingleAllById(int? id)
        {
            cn.Open();
            string cm = $"select * from U_App where UId = {id}";
            SqlCommand cd = new SqlCommand(cm, cn);
            SqlDataReader r = cd.ExecuteReader();
            List<UApp> ml = new List<UApp>();
            while (r.Read())
            {
                ml.Add(new UApp()
                {
                    id = int.Parse(r.GetValue("id").ToString()),
                    UId = int.Parse(r.GetValue("UId").ToString()),
                    CreditSum = double.Parse(r.GetValue("CreditSum").ToString()),
                    CreditGoal = r.GetValue("CreditGoal").ToString(),
                    CreditDeadLine = r.GetValue("CreditDeadLine").ToString(),
                    Status = bool.Parse(r.GetValue("Status").ToString()),
                    Pay = double.Parse(r.GetValue("Pay").ToString())
                });
            }
            cn.Close();
            return ml;
        }
        public void Update(UApp ua, int? id)
        {
            cn.Open();
            string cm = $"update U_App set CreditSum={ua.CreditSum}, CreditDeadLine = '{ua.CreditDeadLine}',CreditGoal = '{ua.CreditGoal}'";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }
    }
    interface IUApp
    {
        int id { get; set; }
        int UId { get; set; }
        double CreditSum { get; set; }
        bool Status { get; set; }
        string CreditGoal { get; set; }
        DateTime CreditDeadLine { get; set; }
        void Add(UApp ua);
        List<UApp> SelectAll();
        UApp SingleById(int? id);
        void Delete(int? id);
        void Update(UApp ua, int? id);
    }
    class UGraph : SCS, IUGraph
    {
        public int id { get; set; }
        public int U_AppId { get; set; }
        public double PMonth { get; set; }
        public double Months { get; set; }
        private SqlConnection cn = new SqlConnection(CnSt);
        public void Add(UGraph ug)
        {
            cn.Open();
            string cm = $"insert into U_Graph(PMonth,U_AppId,Months) Values({ug.PMonth},{ug.U_AppId},{ug.Months});";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }

        public void Delete(int? id)
        {
            cn.Open();
            string cm = $"delete from U_Graph where id = {id}";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }

        public List<UGraph> SelectAll()
        {
            cn.Open();
            string cm = $"select * from U_Graph";
            SqlCommand cd = new SqlCommand(cm, cn);
            SqlDataReader r = cd.ExecuteReader();
            List<UGraph> li = new List<UGraph>();
            while (r.Read())
            {
                li.Add(new UGraph()
                {
                    id = int.Parse(r.GetValue("id").ToString()),
                    Months = double.Parse(r.GetValue("Months").ToString()),
                    PMonth = double.Parse(r.GetValue("PMonth").ToString()),
                    U_AppId = int.Parse(r.GetValue("U_AppId").ToString())
                });
            }
            cn.Close();
            return li;
        }

        public UGraph SingleById(int? id)
        {
            cn.Open();
            string cm = $"select * from U_Graph where id = {id}";
            SqlCommand cd = new SqlCommand(cm, cn);
            SqlDataReader r = cd.ExecuteReader();
            UGraph ml = new UGraph();
            while (r.Read())
            {
                ml = new UGraph()
                {
                    id = int.Parse(r.GetValue("id").ToString()),
                    Months = double.Parse(r.GetValue("Months").ToString()),
                    PMonth = double.Parse(r.GetValue("PMonth").ToString()),
                    U_AppId = int.Parse(r.GetValue("U_AppId").ToString())
                };
            }
            cn.Close();
            return ml;
        }

        public void Update(UGraph ua, int? id)
        {
            cn.Open();
            string cm = $"update U_Graph set PMonth={ua.PMonth},Months={ua.Months}";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }
    }
    interface IUGraph
    {
        int id { get; set; }
        int U_AppId { get; set; }
        double PMonth { get; set; }
        double Months { get; set; }
        void Add(UGraph ug);
        List<UGraph> SelectAll();
        UGraph SingleById(int? id);
        void Delete(int? id);
        void Update(UGraph ua, int? id);
    }
    // class UAppP : SCS, IUAppP
    // {
    //     public int id { get; set; }
    //     public string Condition { get; set; }
    //     public int Point { get; set; }
    //     public string PName { get; set; }
    //     private SqlConnection cn = new SqlConnection(CnSt);
    //     public List<UAppP> SelectAll()
    //     {
    //         cn.Open();
    //         string cm = $"select * from U_App_P";
    //         SqlCommand cd = new SqlCommand(cm, cn);
    //         SqlDataReader r = cd.ExecuteReader();
    //         List<UAppP> li = new List<UAppP>();
    //         while (r.Read())
    //         {
    //             li.Add(new UAppP()
    //             {
    //                 id = int.Parse(r.GetValue("id").ToString()),
    //                 Condition = r.GetValue("Condition").ToString(),
    //                 Point = int.Parse(r.GetValue("Point").ToString()),
    //                 PName = r.GetValue("PName").ToString()
    //             });
    //         }
    //         cn.Close();
    //         return li;
    //     }
    //     public UAppP SelectOne(int? uid)
    //     {
    //         cn.Open();
    //         string cm = $"select * from U_App_P where ";
    //         SqlCommand cd = new SqlCommand(cm, cn);
    //         SqlDataReader r = cd.ExecuteReader();
    //         UAppP ml = new UAppP();
    //         while (r.Read())
    //         {
    //             ml = new UAppP()
    //             {
    //                 id = int.Parse(r.GetValue("id").ToString()),
    //                 Condition = r.GetValue("Condition").ToString(),
    //                 Point = int.Parse(r.GetValue("Point").ToString()),
    //                 PName = r.GetValue("PName").ToString()
    //             };
    //         }
    //         cn.Close();
    //         return ml;
    //     }
    // }
    // interface IUAppP
    // {
    //     int id { get; set; }
    //     string Condition { get; set; }
    //     int Point { get; set; }
    // }
}