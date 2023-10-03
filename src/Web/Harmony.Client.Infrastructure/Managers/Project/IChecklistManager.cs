﻿using Harmony.Application.DTO;
using Harmony.Application.Events;
using Harmony.Application.Features.Cards.Commands.CreateCard;
using Harmony.Application.Features.Cards.Commands.CreateChecklist;
using Harmony.Application.Features.Cards.Commands.CreateCheckListItem;
using Harmony.Application.Features.Cards.Commands.MoveCard;
using Harmony.Application.Features.Cards.Commands.UpdateCardDescription;
using Harmony.Application.Features.Cards.Commands.UpdateCardTitle;
using Harmony.Application.Features.Cards.Queries.LoadCard;
using Harmony.Application.Features.Lists.Commands.UpdateListTitle;
using Harmony.Shared.Wrapper;

namespace Harmony.Client.Infrastructure.Managers.Project
{
    public interface IChecklistManager : IManager
    {
        event EventHandler<CardItemAddedEvent> OnCardItemAdded;
        Task<IResult<CheckListDto>> CreateCheckListAsync(CreateCheckListCommand request);
        Task<IResult<CheckListItemDto>> CreateCheckListItemAsync(CreateCheckListItemCommand request);
        Task<IResult<bool>> UpdateTitleAsync(UpdateListTitleCommand request);
    }
}
