﻿// <auto-generated />
using System;
using ContactManager.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactManager.API.Migrations
{
    [DbContext(typeof(ContactInfoContext))]
    partial class ContactInfoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.20");

            modelBuilder.Entity("ContactManager.API.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressDetails")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("ContactId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Address");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressDetails = "This is my address 1",
                            ContactId = 1,
                            Type = "Home"
                        },
                        new
                        {
                            Id = 2,
                            AddressDetails = "This is Fullscale Address",
                            ContactId = 1,
                            Type = "Work"
                        },
                        new
                        {
                            Id = 3,
                            AddressDetails = "This is my address",
                            ContactId = 2,
                            Type = "Home"
                        },
                        new
                        {
                            Id = 4,
                            AddressDetails = "This is Fullscale Address",
                            ContactId = 3,
                            Type = "Work"
                        });
                });

            modelBuilder.Entity("ContactManager.API.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Emergency")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Favorite")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Emergency = false,
                            Favorite = false,
                            FirstName = "Jeremy",
                            LastName = "Flores",
                            Note = "This is a Note"
                        },
                        new
                        {
                            Id = 2,
                            Emergency = false,
                            Favorite = false,
                            FirstName = "Lirae",
                            LastName = "Data",
                            Note = "This is a Note 2"
                        },
                        new
                        {
                            Id = 3,
                            Emergency = false,
                            Favorite = false,
                            FirstName = "Charis Arlie",
                            LastName = "Baclayon"
                        });
                });

            modelBuilder.Entity("ContactManager.API.Entities.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContactId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Email");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactId = 3,
                            EmailAddress = "jeremygwapo@gmail.com",
                            Type = "Home"
                        });
                });

            modelBuilder.Entity("ContactManager.API.Entities.Number", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContactId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Number");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContactId = 1,
                            ContactNumber = "099292929",
                            Type = "Work"
                        });
                });

            modelBuilder.Entity("ContactManager.API.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ContactManager.API.Entities.Address", b =>
                {
                    b.HasOne("ContactManager.API.Entities.Contact", "Contact")
                        .WithMany("Addresses")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("ContactManager.API.Entities.Email", b =>
                {
                    b.HasOne("ContactManager.API.Entities.Contact", "Contact")
                        .WithMany("Emails")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("ContactManager.API.Entities.Number", b =>
                {
                    b.HasOne("ContactManager.API.Entities.Contact", "Contact")
                        .WithMany("Numbers")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("ContactManager.API.Entities.Contact", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Emails");

                    b.Navigation("Numbers");
                });
#pragma warning restore 612, 618
        }
    }
}
