﻿// <auto-generated />
using System;
using ImageWebResizer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImageWebResizer.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20181019173212_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("ImageWebResizer.Models.Picture", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateUpload");

                    b.Property<string>("FileName");

                    b.Property<int?>("Height");

                    b.Property<long?>("Length");

                    b.Property<long?>("Length300");

                    b.Property<string>("Name");

                    b.Property<string>("OriginalName");

                    b.Property<string>("Path300");

                    b.Property<string>("PathOriginal");

                    b.Property<int?>("Width");

                    b.HasKey("Id");

                    b.ToTable("Pictures");
                });
#pragma warning restore 612, 618
        }
    }
}