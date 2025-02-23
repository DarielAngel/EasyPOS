using Domain.Customers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;


public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Tabla
        builder.ToTable("Customers");

        // Configuración de ID para PostgreSQL
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .HasColumnType("uuid")  // Tipo de dato UUID en PostgreSQL
            .HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value)
            );

        // Configuraciones de propiedades
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("character varying(50)");  // Tipo de dato específico de PostgreSQL

        builder.Property(c => c.Lastname)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("character varying(50)");
        
        // Ignorar propiedades calculadas
        builder.Ignore(c => c.FullName);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("character varying(255)");

        // Índice único para email
        builder.HasIndex(c => c.Email)
            .IsUnique();

        // Configuración de número de teléfono
        builder.Property(c => c.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value)!
            )
            .HasMaxLength(9)
            .HasColumnType("character varying(9)");

        // Configuración de dirección como owned type
        builder.OwnsOne(c => c.Address, addressBuilder => {
            addressBuilder.Property(a => a.Country)
                .HasMaxLength(3)
                .HasColumnType("character varying(3)");
            
            addressBuilder.Property(a => a.Line1)
                .HasMaxLength(20)
                .HasColumnType("character varying(20)");
            
            addressBuilder.Property(a => a.Line2)
                .HasMaxLength(20)
                .IsRequired(false)
                .HasColumnType("character varying(20)");
            
            addressBuilder.Property(a => a.City)
                .HasMaxLength(40)
                .HasColumnType("character varying(40)");
            
            addressBuilder.Property(a => a.State)
                .HasMaxLength(40)
                .HasColumnType("character varying(40)");
            
            addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(20)
                .IsRequired(false)
                .HasColumnType("character varying(20)");
        });

        // Configuración de propiedad booleana
        builder.Property(c => c.Active)
            .HasColumnType("boolean");
    }
}