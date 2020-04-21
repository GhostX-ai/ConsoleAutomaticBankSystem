using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private SqlConnection cn = new SqlConnection(CnSt);
        public void Add(UAccaount ua)
        {
            cn.Open();
            string cm = $"insert into U_Accaunt(RoleId,Fullname,Gender,FStatus,Age,CityZone,Login,Password) Values({ua.RoleId},'{ua.FullName}','{ua.Gender}','{ua.FStatus}',{ua.Age},'{ua.CityZone}',{ua.Login},'{ua.Password}')";
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
                    Role = r.GetValue("Role").ToString()
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
                    Role = r.GetValue("Role").ToString()
                };
            }
            cn.Close();
            return uas;
        }

        public void Update(UAccaount ua, int? id)
        {
            cn.Open();
            string cm = $"update U_Accaunt set RoleId = {ua.RoleId},FullName='{ua.FullName}',Gender='{ua.Gender}',FStatus'{ua.FStatus}',Age={ua.Age},CityZone='{ua.CityZone}',Login={ua.Login},Password='{ua.Password}' where id = {id}";
            SqlCommand cd = new SqlCommand(cm, cn);
            cd.ExecuteNonQuery();
            cn.Close();
        }
    }
    interface IUAcc
    {
        int id { get; set; }
        int RoleId { get; set; }
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
        void Update(UAccaount ua);
    }
}