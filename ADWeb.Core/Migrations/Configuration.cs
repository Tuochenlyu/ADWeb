namespace ADWeb.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ADWeb.Core.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<ADWeb.Core.DAL.ADWebDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ADWeb.Core.DAL.ADWebDB context)
        {
            context.DomainOU.AddOrUpdate(
                o => o.DistinguishedName,
                new DomainOU { Name = "Employees", DistinguishedName = "OU=Employees,OU=Accounts,DC=test,DC=local", Enabled = true, Notes = "All full time employees are stored in this OU." },
                new DomainOU { Name = "Contractors", DistinguishedName = "OU=Contractors,OU=Accounts,DC=test,DC=local", Enabled = true, Notes = "All Contractor accounts are stored in this OU" },
                new DomainOU { Name = "Vendors", DistinguishedName = "OU=Vendors,OU=Accounts,DC=test,DC=local", Enabled = true, Notes = "All Vendor accounts are stored in this OU" },
                new DomainOU { Name = "Group", DistinguishedName = "OU=Groups,OU=Account - Resources,DC=test,DC=local", Enabled = true, Notes = "All security groups are stored here" },
                new DomainOU { Name = "Disabled Users", DistinguishedName = "OU=Disabled,OU=Account - Resources,DC=test,DC=local", Enabled = true, Notes = "Stores disabled user accounts" },
                new DomainOU { Name = "Generics", DistinguishedName = "OU=Generics,OU=Account - Resources,DC=test,DC=local", Enabled = true, Notes = "Generic Accounts stored in this OU." },
                new DomainOU { Name = "Services", DistinguishedName = "OU=Services,OU=Account - Resources,DC=test,DC=local", Enabled = true, Notes = "Service Accounts stored in this OU." },
                new DomainOU { Name = "Temp-Users", DistinguishedName = "OU=Temp-Users,OU=Account - Resources,DC=test,DC=local", Enabled = true, Notes = "Temporary Accounts stored in this OU." }
            );
        }
    }
}
