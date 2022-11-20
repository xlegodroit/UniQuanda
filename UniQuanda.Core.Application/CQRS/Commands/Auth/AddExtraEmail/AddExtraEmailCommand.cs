﻿using MediatR;
using UniQuanda.Core.Domain.Enums;
using UniQuanda.Core.Domain.Utils;

namespace UniQuanda.Core.Application.CQRS.Commands.Auth.AddExtraEmail;

public class AddExtraEmailCommand : IRequest<UpdateSecurityResultEnum>
{
    public AddExtraEmailCommand(AddExtraEmailRequestDTO request, int idUser, UserAgentInfo userAgentInfo)
    {
        NewExtraEmail = request.NewExtraEmail;
        PlainPassword = request.Password;
        IdUser = idUser;
        UserAgentInfo = userAgentInfo;
    }

    public string NewExtraEmail { get; set; }
    public string PlainPassword { get; set; }
    public int IdUser { get; set; }
    public UserAgentInfo UserAgentInfo { get; set; }
}