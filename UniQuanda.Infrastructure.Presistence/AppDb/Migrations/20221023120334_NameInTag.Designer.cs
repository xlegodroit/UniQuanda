﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Infrastructure.Presistence.AppDb;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AppDb.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221023120334_NameInTag")]
    partial class NameInTag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "academic_title_enum", new[] { "engineer", "bachelor", "academic" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AcademicTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<AcademicTitleEnum>("AcademicTitleType")
                        .HasColumnType("academic_title_enum");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AcademicTitleType", "Name")
                        .IsUnique();

                    b.ToTable("AcademicTitles");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int?>("ParentAnswerId")
                        .HasColumnType("integer");

                    b.Property<int>("ParentQuestionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentAnswerId");

                    b.HasIndex("ParentQuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AboutText")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)");

                    b.Property<string>("Avatar")
                        .HasColumnType("text");

                    b.Property<string>("Banner")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("City")
                        .HasMaxLength(57)
                        .HasColumnType("character varying(57)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.Property<string>("LastName")
                        .HasMaxLength(51)
                        .HasColumnType("character varying(51)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(22)
                        .HasColumnType("character varying(22)");

                    b.Property<string>("SemanticScholarProfile")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Roman",
                            Nickname = "Programista"
                        });
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUserAnswerInteraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerId")
                        .HasColumnType("integer");

                    b.Property<int>("AppUserId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCreator")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("AppUserId", "AnswerId")
                        .IsUnique();

                    b.ToTable("AppUsersAnswersInteractions");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUserInUniversity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int>("UniversityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("Order", "AppUserId")
                        .IsUnique();

                    b.HasIndex("UniversityId", "AppUserId")
                        .IsUnique();

                    b.ToTable("AppUsersInUniversities");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUserQuestionInteraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCreator")
                        .HasColumnType("boolean");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("AppUserId", "QuestionId")
                        .IsUnique();

                    b.ToTable("AppUsersQuestionsInteractions");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUserTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AcademicTitleId")
                        .HasColumnType("integer");

                    b.Property<int>("AppUserId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AcademicTitleId");

                    b.HasIndex("AppUserId", "AcademicTitleId")
                        .IsUnique();

                    b.HasIndex("AppUserId", "Order")
                        .IsUnique();

                    b.ToTable("AppUsersTitles");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("ParentTagId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentTagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.TagInQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("QuestionId", "Order")
                        .IsUnique();

                    b.HasIndex("QuestionId", "TagId")
                        .IsUnique();

                    b.ToTable("TagsInQuestions");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.UserPointsInTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("integer");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("AppUserId", "TagId")
                        .IsUnique();

                    b.ToTable("UsersPointsInTags");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.Answer", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.Answer", "ParentAnswerIdNavigation")
                        .WithMany("Comments")
                        .HasForeignKey("ParentAnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.Question", "ParentQuestionIdNavigation")
                        .WithMany("Answers")
                        .HasForeignKey("ParentQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentAnswerIdNavigation");

                    b.Navigation("ParentQuestionIdNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUserAnswerInteraction", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.Answer", "AnswerIdNavigation")
                        .WithMany("AppUsersAnswerInteractions")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUser", "AppUserIdNavigation")
                        .WithMany("AppUserAnswersInteractions")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnswerIdNavigation");

                    b.Navigation("AppUserIdNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUserInUniversity", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUser", "AppUserIdNavigation")
                        .WithMany("AppUserInUniversities")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.University", "UniversityIdNavigation")
                        .WithMany("AppUsersInUniversity")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUserIdNavigation");

                    b.Navigation("UniversityIdNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUserQuestionInteraction", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUser", "AppUserIdNavigation")
                        .WithMany("AppUserQuestionsInteractions")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.Question", "QuestionIdNavigation")
                        .WithMany("AppUsersQuestionInteractions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUserIdNavigation");

                    b.Navigation("QuestionIdNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUserTitle", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.AcademicTitle", "AcademicTitleIdNavigation")
                        .WithMany("UsersTitle")
                        .HasForeignKey("AcademicTitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUser", "AppUserIdNavigation")
                        .WithMany("AppUserTitles")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcademicTitleIdNavigation");

                    b.Navigation("AppUserIdNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.Tag", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.Tag", "ParentTagIdNavigation")
                        .WithMany("ChildTags")
                        .HasForeignKey("ParentTagId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ParentTagIdNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.TagInQuestion", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.Question", "QuestionIdNavigation")
                        .WithMany("TagsInQuestion")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.Tag", "TagIdNavigation")
                        .WithMany("TagInQuestions")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionIdNavigation");

                    b.Navigation("TagIdNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.UserPointsInTag", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUser", "AppUserIdNavigation")
                        .WithMany("UserPointsInTags")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniQuanda.Infrastructure.Presistence.AppDb.Models.Tag", "TagIdNavigation")
                        .WithMany("UsersPointsInTag")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUserIdNavigation");

                    b.Navigation("TagIdNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AcademicTitle", b =>
                {
                    b.Navigation("UsersTitle");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.Answer", b =>
                {
                    b.Navigation("AppUsersAnswerInteractions");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.AppUser", b =>
                {
                    b.Navigation("AppUserAnswersInteractions");

                    b.Navigation("AppUserInUniversities");

                    b.Navigation("AppUserQuestionsInteractions");

                    b.Navigation("AppUserTitles");

                    b.Navigation("UserPointsInTags");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("AppUsersQuestionInteractions");

                    b.Navigation("TagsInQuestion");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.Tag", b =>
                {
                    b.Navigation("ChildTags");

                    b.Navigation("TagInQuestions");

                    b.Navigation("UsersPointsInTag");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AppDb.Models.University", b =>
                {
                    b.Navigation("AppUsersInUniversity");
                });
#pragma warning restore 612, 618
        }
    }
}
