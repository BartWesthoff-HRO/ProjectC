﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProjectC.Models;

namespace ProjectC.DataContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base(nameOrConnectionString: "ProjectCadmin")
        {
            // dit is een test//
        }

        public virtual DbSet<Medewerker> Medewerkers { get; set; }
        public virtual DbSet<ContactPerson> ContactPersons { get; set; }

        public virtual DbSet<ProjectC.Models.label> labels { get; set; }

        public virtual DbSet<ProjectC.Models.klant> klants { get; set; }
    }
}