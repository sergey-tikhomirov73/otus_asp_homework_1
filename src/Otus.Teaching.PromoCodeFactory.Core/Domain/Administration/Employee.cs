﻿using System;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Employee
        : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

      //  public List<Role> Roles { get; set; }
        public List<Guid> RoleGuids { get; set; } // список идентификаторов ролей
        public int AppliedPromocodesCount { get; set; }
    }
}