using System;
using System.Collections.Generic;
using System.Linq;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static List<Employee> Employees=null;
        public static List<Role> Roles = null;
        public static void Build() 
        { 
            BuildRoleRepo();
            BuildEmployeeRepo();
        }
        //   public static IEnumerable<Employee> Employees => new List<Employee>();
        public static void BuildEmployeeRepo()
        {
            Employees = new List<Employee>();

            Employee item = new Employee()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                Email = "owner@somemail.ru",
                FirstName = "Иван",
                LastName = "Сергеев",
                RoleGuids= new List<Guid>()
                {
                    Roles.FirstOrDefault(x => x.Name == "Admin").Id,
                    Roles.FirstOrDefault(x => x.Name == "PartnerManager").Id
                },
                AppliedPromocodesCount = 5
            };

            Employees.Add(item);    

            item = new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",
                RoleGuids = new List<Guid>()
                {
                    Roles.FirstOrDefault(x => x.Name == "PartnerManager").Id
                },
                AppliedPromocodesCount = 10
            };
            Employees.Add(item);

        }
       

        public static void BuildRoleRepo()
         {
            Roles = new List<Role>();

            Role role = new Role()
            {
                Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Name = "Admin",
                Description = "Администратор",
            };

            Roles.Add(role);

            role = new Role()
            {
                Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Name = "PartnerManager",
                Description = "Партнерский менеджер"
            };

            Roles.Add(role);
        }
    }
}