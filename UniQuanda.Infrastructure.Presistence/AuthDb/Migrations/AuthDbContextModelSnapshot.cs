﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniQuanda.Infrastructure.Presistence.AuthDb;

#nullable disable

namespace UniQuanda.Infrastructure.Presistence.AuthDb.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    partial class AuthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "o_auth_provider_enum", new[] { "google" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "premium_payment_status_enum", new[] { "new", "pending", "canceled", "completed" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_action_to_confirm_enum", new[] { "recover_password", "new_main_email", "new_extra_email" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.OAuthUser", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnType("integer");

                    b.Property<string>("OAuthId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OAuthProvider")
                        .HasColumnType("integer");

                    b.Property<string>("OAuthRegisterConfirmationCode")
                        .HasColumnType("text");

                    b.HasKey("IdUser");

                    b.HasIndex("OAuthId", "OAuthProvider")
                        .IsUnique();

                    b.ToTable("OAuthUsers");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.TempUser", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("City")
                        .HasMaxLength(57)
                        .HasColumnType("character varying(57)");

                    b.Property<string>("Contact")
                        .HasMaxLength(22)
                        .HasColumnType("character varying(22)");

                    b.Property<string>("EmailConfirmationCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<DateTime>("ExistsUntil")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.Property<string>("LastName")
                        .HasMaxLength(51)
                        .HasColumnType("character varying(51)");

                    b.HasKey("IdUser");

                    b.ToTable("TempUsers");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HashedPassword = "$2a$12$bIkUNGSkHjgVl80kICadyezV4AgRo6oMwuIEC3X9ian.d7a6xJRIe",
                            Nickname = "Programista"
                        });
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.UserActionToConfirm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionType")
                        .HasColumnType("integer");

                    b.Property<string>("ConfirmationToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExistsUntil")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdUser")
                        .HasColumnType("integer");

                    b.Property<int?>("IdUserEmail")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.HasIndex("IdUserEmail")
                        .IsUnique();

                    b.HasIndex("ActionType", "IdUser")
                        .IsUnique();

                    b.ToTable("UsersActionsToConfirm");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.UserEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IdUser")
                        .HasColumnType("integer");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("UsersEmails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IdUser = 1,
                            IsMain = true,
                            Value = "user@uniquanda.pl"
                        });
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.OAuthUser", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AuthDb.Models.User", "IdUserNavigation")
                        .WithOne("IdOAuthUserNavigation")
                        .HasForeignKey("UniQuanda.Infrastructure.Presistence.AuthDb.Models.OAuthUser", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.TempUser", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AuthDb.Models.User", "IdUserNavigation")
                        .WithOne("IdTempUserNavigation")
                        .HasForeignKey("UniQuanda.Infrastructure.Presistence.AuthDb.Models.TempUser", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.UserActionToConfirm", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AuthDb.Models.User", "IdUserNavigation")
                        .WithMany("ActionsToConfirm")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniQuanda.Infrastructure.Presistence.AuthDb.Models.UserEmail", "IdUserEmailNavigation")
                        .WithOne("IdUserActionToConfirmNavigation")
                        .HasForeignKey("UniQuanda.Infrastructure.Presistence.AuthDb.Models.UserActionToConfirm", "IdUserEmail")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("IdUserEmailNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.UserEmail", b =>
                {
                    b.HasOne("UniQuanda.Infrastructure.Presistence.AuthDb.Models.User", "IdUserNavigation")
                        .WithMany("Emails")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.User", b =>
                {
                    b.Navigation("ActionsToConfirm");

                    b.Navigation("Emails");

                    b.Navigation("IdOAuthUserNavigation");

                    b.Navigation("IdTempUserNavigation")
                        .IsRequired();
                });

            modelBuilder.Entity("UniQuanda.Infrastructure.Presistence.AuthDb.Models.UserEmail", b =>
                {
                    b.Navigation("IdUserActionToConfirmNavigation")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
