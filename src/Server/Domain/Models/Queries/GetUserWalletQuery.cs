﻿using FinanceLab.Shared.Domain.Models.Outputs;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Queries;

public sealed record GetUserWalletQuery(string UserName) : IRequest<UserWalletOutput>;