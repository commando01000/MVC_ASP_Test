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
    public partial class OrderDetailsExtendedConfiguration : IEntityTypeConfiguration<OrderDetailsExtended>
    {
        public void Configure(EntityTypeBuilder<OrderDetailsExtended> entity)
        {
            entity.ToView("Order Details Extended");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<OrderDetailsExtended> entity);
    }
}