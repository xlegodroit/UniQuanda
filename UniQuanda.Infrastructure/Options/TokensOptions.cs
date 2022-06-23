﻿using Microsoft.Extensions.Configuration;

namespace UniQuanda.Infrastructure.Options;

public class TokensOptions
{
    public TokensOptions(IConfiguration configuration)
    {
        var tokensSection = configuration.GetSection("Tokens");
        RefreshToken = new RefreshTokenOptions(tokensSection.GetSection("RefreshToken"));
        AccessToken = new AccessTokenOptions(tokensSection.GetSection("AccessToken"));
    }

    public RefreshTokenOptions RefreshToken { get; set; }
    public AccessTokenOptions AccessToken { get; set; }
}

public class RefreshTokenOptions
{
    public RefreshTokenOptions(IConfigurationSection section)
    {
        ValidityInMinutes = int.Parse(section["ValidityInMinutes"]);
    }

    public int ValidityInMinutes { get; set; }
}

public class AccessTokenOptions
{
    public AccessTokenOptions(IConfigurationSection section)
    {
        SecretKey = section["SecretKey"];
        ValidIssuer = section["ValidIssuer"];
        ValidAudience = section["ValidAudience"];
        ValidateIssuer = bool.Parse(section["ValidateIssuer"]);
        ValidateAudience = bool.Parse(section["ValidateAudience"]);
        ValidityInMinutes = int.Parse(section["ValidityInMinutes"]);
    }

    public string SecretKey { get; set; }
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public int ValidityInMinutes { get; set; }
}