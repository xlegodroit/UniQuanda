﻿using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmail;
using UniQuanda.Core.Application.Repositories;
using UniQuanda.Core.Application.Services;
using UniQuanda.Core.Application.Services.Auth;
using UniQuanda.Core.Domain.Entities.Auth;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.ValueObjects;

namespace UniQuanda.Tests.CQRS.Commands.Auth.AddExtraEmail;

[TestFixture]
public class AddExtraEmailHandlerTests
{
    private const int IdUser = 1;
    private const string NewExtraEmail = "email@domain.com";
    private const string PlainPassword = "PlainPassword";
    private const string HashedPassword = "HashedPassword";
    private const string Nickname = "Nickname";
    private const string MainUserEmail = "mainEmail@domain.com";
    private const string ExtraUserEmail = "extraEmail@domain.com";
    private const int NewUserExpirationInHours = 24;
    private readonly string _emailConfirmationToken = Guid.NewGuid().ToString();

    private AddExtraEmailHandler addExtraEmailHandler;
    private AddExtraEmailCommand addExtraEmailCommand;
    private Mock<IAuthRepository> authRepository;
    private Mock<IPasswordsService> passwordsService;
    private Mock<IEmailService> emailService;
    private Mock<ITokensService> tokensService;
    private Mock<IExpirationService> expirationService;

    [SetUp]
    public void SetupTests()
    {
        this.authRepository = new Mock<IAuthRepository>();
        this.passwordsService = new Mock<IPasswordsService>();
        this.emailService = new Mock<IEmailService>();
        this.tokensService = new Mock<ITokensService>();
        this.expirationService = new Mock<IExpirationService>();

        this.SetupEmailConfirmationToken();
        this.SetupExpirationService();

        this.addExtraEmailHandler = new AddExtraEmailHandler(this.authRepository.Object, this.passwordsService.Object, this.emailService.Object, this.tokensService.Object, this.expirationService.Object);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumSuccessful_WhenCallIsValid()
    {
        this.SetupAddExtraEmailCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowOfAddExtraEmailWhenEmailIsAvailable(true);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.Successful);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumContentNotExist_WhenUserNotExists()
    {
        this.SetupAddExtraEmailCommand();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync((UserSecurityEntity)null);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumInvalidPassword_WhenGivenPasswordIsInvalid()
    {
        this.SetupAddExtraEmailCommand("InvalidPassword");
        var userSecurityEntity = GetUserSecurityEntity();
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.passwordsService
            .Setup(ps => ps.VerifyPassword(this.addExtraEmailCommand.PlainPassword, userSecurityEntity.HashedPassword))
            .Returns(false);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.InvalidPassword);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumEmailNotAvailable_WhenEmailIsNotAvailable()
    {
        this.SetupAddExtraEmailCommand(PlainPassword, ExtraUserEmail);
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.authRepository
            .Setup(ar => ar.IsEmailAvailableAsync(null, this.addExtraEmailCommand.NewExtraEmail, CancellationToken.None))
            .ReturnsAsync(false);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.EmailNotAvailable);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumOverLimitOfExtraEmails_WhenUserHasAlready3ExtraEmails()
    {
        this.SetupAddExtraEmailCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        userSecurityEntity.Emails.Add(new UserEmailSecurity { Id = 3, IsMain = false, Value = "thirdEmail@domain.com" });
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowToCheckPointOfIsUserAllowed(AddExtraEmailStatus.OverLimitOfExtraEmails);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.OverLimitOfExtraEmails);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumContentNotExists_WhenCheckOnIsUserAllowedIsUserNotExists()
    {
        this.SetupAddExtraEmailCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowToCheckPointOfIsUserAllowed(AddExtraEmailStatus.UserNotExist);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumUserHasActionToConfirm_WhenUserHasOtherActionToConfirm()
    {
        this.SetupAddExtraEmailCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowToCheckPointOfIsUserAllowed(AddExtraEmailStatus.UserHasActionToConfirm);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.UserHasActionToConfirm);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumDbConflict_WhenAddExtraEmailIsNotSuccessful()
    {
        this.SetupAddExtraEmailCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowOfAddExtraEmailWhenEmailIsAvailable(false);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.DbConflict);
    }

    [Test]
    public async Task AddExtraEmail_ShouldReturnResultEnumContentNotExist_WhenAddExtraEmailNotExists()
    {
        this.SetupAddExtraEmailCommand();
        var userSecurityEntity = GetUserSecurityEntity();
        this.SetupValidPasswordFlow(userSecurityEntity);
        this.SetupFlowOfAddExtraEmailWhenEmailIsAvailable(null);

        var result = await addExtraEmailHandler.Handle(this.addExtraEmailCommand, CancellationToken.None);

        result.Should().Be(UpdateSecurityResultEnum.ContentNotExist);
    }

    private void SetupEmailConfirmationToken()
    {
        this.tokensService
            .Setup(ts => ts.GenerateNewEmailConfirmationToken())
            .Returns(_emailConfirmationToken);
    }

    private void SetupExpirationService()
    {
        this.expirationService
            .Setup(es => es.GetNewUserExpirationInHours())
            .Returns(NewUserExpirationInHours);
    }

    private void SetupAddExtraEmailCommand(string plainPassword = PlainPassword, string newExtraEmail = NewExtraEmail)
    {
        var addExtraEmailRequestDTO = new AddExtraEmailRequestDTO
        {
            NewExtraEmail = newExtraEmail,
            Password = plainPassword
        };
        this.addExtraEmailCommand = new(addExtraEmailRequestDTO, IdUser);
    }

    private static UserSecurityEntity GetUserSecurityEntity()
    {
        return new UserSecurityEntity
        {
            Nickname = Nickname,
            HashedPassword = HashedPassword,
            Emails = new List<UserEmailSecurity>()
            {
                new UserEmailSecurity { Id = 1, IsMain = true, Value = MainUserEmail},
                new UserEmailSecurity { Id = 2, IsMain = false, Value = ExtraUserEmail}
            }
        };
    }

    private void SetupValidPasswordFlow(UserSecurityEntity userSecurityEntity)
    {
        this.authRepository
            .Setup(ar => ar.GetUserWithEmailsByIdAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(userSecurityEntity);
        this.passwordsService
            .Setup(ps => ps.VerifyPassword(this.addExtraEmailCommand.PlainPassword, userSecurityEntity.HashedPassword))
            .Returns(true);
    }

    private void SetupFlowOfAddExtraEmailWhenEmailIsAvailable(bool? addResult)
    {
        this.authRepository
            .Setup(ar => ar.IsEmailAvailableAsync(null, this.addExtraEmailCommand.NewExtraEmail, CancellationToken.None))
            .ReturnsAsync(true);
        this.authRepository
            .Setup(ar => ar.IsUserAllowedToAddExtraEmailAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(AddExtraEmailStatus.AllowedToAdd);
        this.authRepository
            .Setup(ar => ar.AddExtraEmailAsync(It.IsAny<UserEmailToConfirm>(), CancellationToken.None))
            .ReturnsAsync(addResult);
    }

    private void SetupFlowToCheckPointOfIsUserAllowed(AddExtraEmailStatus resultOfIsUserAllowed)
    {
        this.authRepository
            .Setup(ar => ar.IsEmailAvailableAsync(null, this.addExtraEmailCommand.NewExtraEmail, CancellationToken.None))
            .ReturnsAsync(true);
        this.authRepository
            .Setup(ar => ar.IsUserAllowedToAddExtraEmailAsync(IdUser, CancellationToken.None))
            .ReturnsAsync(resultOfIsUserAllowed);
    }
}
