﻿using Harmony.Application.Contracts.Repositories;
using Harmony.Shared.Wrapper;
using Microsoft.Extensions.Localization;
using Harmony.Application.Contracts.Services;
using Harmony.Application.DTO;
using AutoMapper;
using Harmony.Application.Contracts.Services.Management;
using Harmony.Application.Contracts.Messaging;
using Harmony.Application.Notifications;
using MediatR;

namespace Harmony.Application.Features.Cards.Commands.MoveCard;

public class MoveCardCommandHandler : IRequestHandler<MoveCardCommand, Result<CardDto>>
{
	private readonly ICardService _cardService;
	private readonly ICardRepository _cardRepository;
    private readonly INotificationsPublisher _notificationsPublisher;
    private readonly ICurrentUserService _currentUserService;
	private readonly IStringLocalizer<MoveCardCommandHandler> _localizer;
	private readonly IMapper _mapper;

	public MoveCardCommandHandler(ICardService cardService,
		ICardRepository cardRepository,
		INotificationsPublisher notificationsPublisher,
		ICurrentUserService currentUserService,
		IStringLocalizer<MoveCardCommandHandler> localizer,
		IMapper mapper)
	{
		_cardService = cardService;
		_cardRepository = cardRepository;
        _notificationsPublisher = notificationsPublisher;
        _currentUserService = currentUserService;
		_localizer = localizer;
		_mapper = mapper;
	}
	public async Task<Result<CardDto>> Handle(MoveCardCommand request, CancellationToken cancellationToken)
	{
		var userId = _currentUserService.UserId;

		if (string.IsNullOrEmpty(userId))
		{
			return await Result<CardDto>.FailAsync(_localizer["Login required to complete this operator"]);
		}

		var card = await _cardRepository.Get(request.CardId);

		// commit all the changes
		var operationCompleted = await _cardService
			.PositionCard(card, request.ListId, request.Position, request.Status);

        if (operationCompleted)
		{
			var cardIsCompleted = await _cardService.CardCompleted(card.Id);
			if (cardIsCompleted)
			{
                _notificationsPublisher.Publish(new CardCompletedNotification(card.Id));
            }

            var result = _mapper.Map<CardDto>(card);
			return await Result<CardDto>.SuccessAsync(result);
		}

		return await Result<CardDto>.FailAsync(_localizer["Operation failed"]);
	}
}
