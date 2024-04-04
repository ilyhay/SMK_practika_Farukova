using SMK_practika.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMK_practika
{
    internal class Helper
    {
        private static Entities s_firstDBEntities;

        public static Entities GetContext()
        {
            if (s_firstDBEntities == null)
            {
                s_firstDBEntities = new Entities();
            }
            return s_firstDBEntities;
        }
        public static void CreateEmployee(Company_Employee employee)
        {
            using (var context = GetContext())
            {
                context.Company_Employee.Add(employee);
                context.SaveChanges();
            }
        }

        public static void UpdateEmployee(Company_Employee employee)
        {
            using (var context = GetContext())
            {
                context.Entry(employee).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void DeleteEmployee(Company_Employee employee)
        {
            using (var context = GetContext())
            {
                context.Company_Employee.Remove(employee);
                context.SaveChanges();
            }
        }
    }
}
