using System;
using System.Collections.Generic;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models
{
    public class EmployeeBaseData
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }    
        public string Email {  get; set; }


    }

    public class EmployeeReqUpdateData
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Guid> RoleGuids { get; set; }

    }
}
