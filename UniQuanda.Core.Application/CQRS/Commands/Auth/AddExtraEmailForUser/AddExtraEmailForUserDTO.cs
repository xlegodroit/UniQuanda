﻿using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmailForUser;

public class AddExtraEmailForUserRequestDTO
{
    [Required]
    [MaxLength(320)]
    [EmailAddress]
    public string NewExtraEmail { get; set; }

    [Required]
    [PasswordValidator]
    public string Password { get; set; }
}