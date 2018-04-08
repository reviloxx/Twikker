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
    [Migration("20180407190045_UniqueNicknames")]
    partial class UniqueNicknames
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

                    b.Property<int?>("UserTextId");

                    b.HasKey("ReactionId");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("UserTextId");

                    b.ToTable("Reactions");
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

            modelBuilder.Entity("Twikker.Data.Models.UserText", b =>
                {
                    b.Property<int>("UserTextId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("CreatorUserId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("UserTextId");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("UserTexts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserText");
                });

            modelBuilder.Entity("Twikker.Data.Models.Comment", b =>
                {
                    b.HasBaseType("Twikker.Data.Models.UserText");

                    b.Property<int?>("PostUserTextId");

                    b.HasIndex("PostUserTextId");

                    b.ToTable("Comment");

                    b.HasDiscriminator().HasValue("Comment");
                });

            modelBuilder.Entity("Twikker.Data.Models.Post", b =>
                {
                    b.HasBaseType("Twikker.Data.Models.UserText");


                    b.ToTable("Post");

                    b.HasDiscriminator().HasValue("Post");
                });

            modelBuilder.Entity("Twikker.Data.Models.Reaction", b =>
                {
                    b.HasOne("Twikker.Data.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Twikker.Data.Models.UserText")
                        .WithMany("Reactions")
                        .HasForeignKey("UserTextId");
                });

            modelBuilder.Entity("Twikker.Data.Models.UserText", b =>
                {
                    b.HasOne("Twikker.Data.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Twikker.Data.Models.Comment", b =>
                {
                    b.HasOne("Twikker.Data.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostUserTextId");
                });
#pragma warning restore 612, 618
        }
    }
}
