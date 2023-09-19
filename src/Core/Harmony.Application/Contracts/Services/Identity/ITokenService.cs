﻿using Harmony.Application.Requests.Identity;
using Harmony.Application.Responses;
using Harmony.Shared.Wrapper;

namespace Harmony.Application.Contracts.Services.Identity
{
    public interface ITokenService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}