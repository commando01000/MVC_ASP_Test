﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Comany.Database.Access.Contexts;
using Comany.Database.Access.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace Comany.Database.Access.Contexts.Configurations
{
    public partial class CustomerCustomerDemoConfiguration : IEntityTypeConfiguration<CustomerCustomerDemo>
    {
        public void Configure(EntityTypeBuilder<CustomerCustomerDemo> entity)
        {
            entity.HasKey(e => new { e.CustomerId, e.CustomerTypeId }).IsClustered(false);

            entity.Property(e => e.CustomerId).IsFixedLength();
            entity.Property(e => e.CustomerTypeId).IsFixedLength();

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerCustomerDemos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerCustomerDemo_Customers");

            entity.HasOne(d => d.CustomerType).WithMany(p => p.CustomerCustomerDemos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerCustomerDemo");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CustomerCustomerDemo> entity);
    }
}
