﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using Twikker.Data;
using Twikker.Data.Models;

namespace Twikker.Data.Migrations
{
    [DbContext(typeof(TwikkerContext))]
    [Migration("20180408091435_update3")]
    partial class update3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Twikker.Data.Models.Reaction", b =>
                {
                    b.Property<int>("ReactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("CreatorUserId");

                    b.Property<int>("ReactionType");

                    b.Property<int?>("TextId");

                    b.HasKey("ReactionId");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("TextId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("Twikker.Data.Models.Text", b =>
                {
                    b.Property<int>("TextId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("TextId");

                    b.ToTable("UserTexts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Text");
                });

            modelBuilder.Entity("Twikker.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("NickName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.HasIndex("NickName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Twikker.Data.Models.Comment", b =>
                {
                    b.HasBaseType("Twikker.Data.Models.Text");

                    b.Property<int?>("PostTextId");

                    b.HasIndex("PostTextId");

                    b.ToTable("Comment");

                    b.HasDiscriminator().HasValue("Comment");
                });

            modelBuilder.Entity("Twikker.Data.Models.Post", b =>
                {
                    b.HasBaseType("Twikker.Data.Models.Text");


                    b.ToTable("Post");

                    b.HasDiscriminator().HasValue("Post");
                });

            modelBuilder.Entity("Twikker.Data.Models.Reaction", b =>
                {
                    b.HasOne("Twikker.Data.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Twikker.Data.Models.Text")
                        .WithMany("Reactions")
                        .HasForeignKey("TextId");
                });

            modelBuilder.Entity("Twikker.Data.Models.Comment", b =>
                {
                    b.HasOne("Twikker.Data.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostTextId");
                });
#pragma warning restore 612, 618
        }
    }
}
