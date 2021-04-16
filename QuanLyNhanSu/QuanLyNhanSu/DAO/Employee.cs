using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSu.DAO
{
    public class Employee
    {
        private static Employee instance;
        public static Employee Instance
        {
            get
            {
                if (instance == null)
                    instance = new Employee();
                return Employee.instance;
            }
            private set { Employee.instance = value; }
        }
        private Employee() { }
        public List<Employee> GetEmployeesByCategoryID(int id)
        {
            List<Employee> list = new List<Employee>();
            string query = "select * from Employee where id=" + id;
            DataTable date = dataprovider.Instance.Excutequery(query);
            foreach(DataRow item in dataprovider.Rows)
            {
                Employee employee = new Employee();
                list.Add(employee);
            }
            return list;
        }
        public List<Employee> GetEmployeeList()
        {
            
            List<Employee> list = new List<Employee>();
            string query = "select * from Employee";
            DataTable date = dataprovider.Instance.Excutequery(query);
            foreach (DataRow item in dataprovider.Rows)
            {
                Employee employee = new Employee();
                list.Add(employee);
            }
            return list;
        }
    }
}
