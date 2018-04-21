﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Twikker.Data;
using Twikker.Data.Models;

namespace Twikker.Data.Migrations
{
    [DbContext(typeof(TwikkerContext))]
    partial class TwikkerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Twikker.Data.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("CreatorId");

                    b.Property<int>("PostId");

                    b.Property<int>("UserTextId");

                    b.HasKey("CommentId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserTextId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Twikker.Data.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("CreatorId");

                    b.Property<int>("UserTextId");

                    b.HasKey("PostId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("UserTextId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Twikker.Data.Models.Reaction", b =>
                {
                    b.Property<int>("ReactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("CreatorId");

                    b.Property<int>("ReactionType");

                    b.Property<int>("UserTextId");

                    b.HasKey("ReactionId");

                    b.HasIndex("CreatorId");

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

                    b.HasKey("UserTextId");

                    b.ToTable("UserText");
                });

            modelBuilder.Entity("Twikker.Data.Models.Comment", b =>
                {
                    b.HasOne("Twikker.Data.Models.User", "Creator")
                        .WithMany("Comments")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Twikker.Data.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Twikker.Data.Models.UserText", "UserText")
                        .WithMany()
                        .HasForeignKey("UserTextId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Twikker.Data.Models.Post", b =>
                {
                    b.HasOne("Twikker.Data.Models.User", "Creator")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Twikker.Data.Models.UserText", "UserText")
                        .WithMany()
                        .HasForeignKey("UserTextId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Twikker.Data.Models.Reaction", b =>
                {
                    b.HasOne("Twikker.Data.Models.User", "Creator")
                        .WithMany("Reactions")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Twikker.Data.Models.UserText", "UserText")
                        .WithMany("Reactions")
                        .HasForeignKey("UserTextId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
