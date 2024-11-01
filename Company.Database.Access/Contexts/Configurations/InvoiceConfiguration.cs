﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Company.Database.Access.Contexts;
using Company.Database.Access.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Database.Access.Contexts.Configurations
{
    public partial class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> entity)
        {
            entity.ToView("Invoices");

            entity.Property(e => e.CustomerId).IsFixedLength();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Invoice> entity);
    }
}
