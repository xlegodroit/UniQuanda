﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using UniQuanda.Core.Application.Validators;

namespace UniQuanda.Core.Application.CQRS.Commands.AppUser.Profile.UpdateAppUserProfile;

public class UpdateAppUserProfileRequestDTO
{
    [MaxLength(35)]
    public string? FirstName { get; set; }

    [MaxLength(51)]
    public string? LastName { get; set; }

    [MaxLength(22)]
    public string? PhoneNumber { get; set; }

    [MaxLength(300)]
    public string? City { get; set; }

    [DateTimeEarlierThanCurrentValidator]
    public DateTime? Birthdate { get; set; }

    [RegularExpression("^https://www.semanticscholar.org/author/.*/.*$")]
    public string? SemanticScholarProfile { get; set; }

    public string? AboutText { get; set; }

    [ImageUploadValidator("Avatar")]
    public IFormFile? Avatar { get; set; }

    [ImageUploadValidator("Banner")]
    public IFormFile? Banner { get; set; }
}

public class UpdateAppUserProfileResponseDTO
{
    public bool? IsSuccessful { get; set; }
    public string? AvatarUrl { get; set; }
}
