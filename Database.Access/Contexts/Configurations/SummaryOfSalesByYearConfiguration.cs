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
    public partial class SummaryOfSalesByYearConfiguration : IEntityTypeConfiguration<SummaryOfSalesByYear>
    {
        public void Configure(EntityTypeBuilder<SummaryOfSalesByYear> entity)
        {
            entity.ToView("Summary of Sales by Year");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SummaryOfSalesByYear> entity);
    }
}