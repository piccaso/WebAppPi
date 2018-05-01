﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using WebAppPi.Db;

namespace WebAppPi.Migrations
{
    [DbContext(typeof(Db.Db))]
    [Migration("20180428163439_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("WebAppPi.Db.AnswerNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<string>("Conclusion");

                    b.Property<int?>("QuestionNodeId");

                    b.Property<decimal>("Score");

                    b.HasKey("Id");

                    b.HasIndex("QuestionNodeId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("WebAppPi.Db.QuestionNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Header");

                    b.Property<byte[]>("Image");

                    b.Property<int>("LastUsed");

                    b.Property<string>("Question");

                    b.HasKey("Id");

                    b.HasIndex("Question");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("WebAppPi.Db.AnswerNode", b =>
                {
                    b.HasOne("WebAppPi.Db.QuestionNode")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionNodeId");
                });
#pragma warning restore 612, 618
        }
    }
}