﻿using Harmony.Shared.Wrapper;
using MediatR;

namespace Harmony.Application.Features.Cards.Queries.LoadCard
{
    public class LoadCardQuery : IRequest<IResult<LoadCardResponse>>
    {
        public Guid CardId { get; set; }

        public LoadCardQuery(Guid cardId)
        {
            CardId = cardId;
        }
    }
}