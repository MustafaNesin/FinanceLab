﻿using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Dtos;

[PublicAPI]
public record WalletDto(List<AssetDto> Assets);