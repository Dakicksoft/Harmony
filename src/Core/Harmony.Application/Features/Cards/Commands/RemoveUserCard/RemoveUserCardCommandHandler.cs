﻿using Harmony.Application.Contracts.Repositories;
using Harmony.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Harmony.Application.Contracts.Services;
using Harmony.Application.Contracts.Services.Identity;
using AutoMapper.Execution;
using Harmony.Application.Contracts.Services.Hubs;
using Harmony.Domain.Entities;
using Harmony.Application.DTO;
using AutoMapper;

namespace Harmony.Application.Features.Cards.Commands.RemoveUserCard
{
    public class RemoveUserCardCommandHandler : IRequestHandler<RemoveUserCardCommand, Result<RemoveUserCardResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IHubClientNotifierService _hubClientNotifierService;
        private readonly IUserCardRepository _userCardRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<RemoveUserCardCommandHandler> _localizer;

        public RemoveUserCardCommandHandler(ICurrentUserService currentUserService,
            IHubClientNotifierService hubClientNotifierService,
            IUserCardRepository userCardRepository,
            ICardRepository cardRepository,
            IUserService userService,
            IMapper mapper,
            IStringLocalizer<RemoveUserCardCommandHandler> localizer)
        {
            _currentUserService = currentUserService;
            _hubClientNotifierService = hubClientNotifierService;
            _userCardRepository = userCardRepository;
            _cardRepository = cardRepository;
            _userService = userService;
            _mapper = mapper;
            _localizer = localizer;
        }
        public async Task<Result<RemoveUserCardResponse>> Handle(RemoveUserCardCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                return await Result<RemoveUserCardResponse>.FailAsync(_localizer["Login required to complete this operator"]);
            }

            var user = (await _userService.GetAsync(request.UserId)).Data;

            var userCard = await _userCardRepository.GetUserCard(request.CardId, request.UserId);

            if (userCard != null)
            {
                var boardId = await _cardRepository.GetBoardId(request.CardId);

                var dbResult = await _userCardRepository.Delete(userCard);

                if (dbResult > 0)
                {
                    var result = new RemoveUserCardResponse(request.CardId, request.UserId)
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    var member = _mapper.Map<CardMemberDto>(user);

                    await _hubClientNotifierService.RemoveCardMember(boardId, request.CardId, member);

                    return await Result<RemoveUserCardResponse>.SuccessAsync(result, _localizer["User removed from card"]);
                }
            }

            return await Result<RemoveUserCardResponse>.FailAsync(_localizer["User doesn't belong to card"]);
        }
    }
}
