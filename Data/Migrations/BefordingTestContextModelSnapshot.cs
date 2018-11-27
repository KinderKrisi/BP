﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(BefordingTestContext))]
    partial class BefordingTestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Address", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName");

                    b.Property<string>("Floor");

                    b.Property<string>("HouseLetter");

                    b.Property<string>("HouseNumber");

                    b.Property<string>("MunicipalityCode");

                    b.Property<string>("PostCodeIdentifier");

                    b.Property<string>("SideOrDoor");

                    b.Property<string>("StreetName");

                    b.Property<string>("StreetNumber");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Data.HospitalProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("NameOfHospital");

                    b.Property<float>("Rate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Data.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message");

                    b.Property<int?>("PatientId");

                    b.Property<int?>("ProfileId");

                    b.Property<string>("Severity");

                    b.Property<DateTime>("TimeOfOccurrence");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Data.Patient", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<string>("CivilRegistrationNumber");

                    b.Property<string>("CivilStatusCode");

                    b.Property<string>("CountryIdentificationCode");

                    b.Property<string>("CountryIdentificationCodeSst");

                    b.Property<string>("CountryIdentificationText");

                    b.Property<string>("ParishDistrictCode");

                    b.Property<string>("ParishDistrictText");

                    b.Property<string>("PersonGenderCode");

                    b.Property<int?>("PersonNameid");

                    b.Property<string>("PopulationDistrictCode");

                    b.Property<string>("PopulationDistrictText");

                    b.Property<string>("PractitionerIdentificationCode");

                    b.Property<string>("RegionalCode");

                    b.Property<string>("RegionalName");

                    b.Property<string>("SocialDistrictCode");

                    b.Property<string>("SocialDistrictText");

                    b.Property<string>("StatusCode");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("PersonNameid");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Data.PersonName", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.HasKey("id");

                    b.ToTable("PersonName");
                });

            modelBuilder.Entity("Data.Patient", b =>
                {
                    b.HasOne("Data.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Data.PersonName", "PersonName")
                        .WithMany()
                        .HasForeignKey("PersonNameid");
                });
#pragma warning restore 612, 618
        }
    }
}
