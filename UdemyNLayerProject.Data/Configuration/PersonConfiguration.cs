using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(i => i.Id); // Id property'si Primary Key olsun.
            builder.Property(i => i.Id).UseIdentityColumn(); // Id değeri sürekli 1 olarak artsın. 
            builder.Property(i => i.Name).HasMaxLength(50); // Name değeri en fazla 50 karakter olsun ve gerekli olsun.
            builder.Property(i => i.SurName).HasMaxLength(50); // Name değeri en fazla 50 karakter olsun ve gerekli olsun.
            builder.ToTable("Persons");


        }
    }
}
