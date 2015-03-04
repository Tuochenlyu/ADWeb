﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADWeb.Core.Entities
{
    public class DomainUser
    {
        public int DomainUserID { get; set; }

        [Key]
        public string Username { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<UserUpdateHistory> UpdateHistory { get; set; }
    }
}
