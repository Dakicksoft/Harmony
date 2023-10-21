﻿using Harmony.Application.Contracts.Repositories;
using Harmony.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Harmony.Application.Contracts.Services;
using AutoMapper;
using Harmony.Application.Contracts.Services.Hubs;
using System.Collections.Generic;

namespace Harmony.Application.Features.Labels.Commands.RemoveCardLabel
{
    public class RemoveCardLabelCommandHandler : IRequestHandler<RemoveCardLabelCommand, IResult<bool>>
    {
        private readonly IBoardLabelRepository _boardLabelRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHubClientNotifierService _hubClientNotifierService;
        private readonly IStringLocalizer<RemoveCardLabelCommandHandler> _localizer;
        private readonly IMapper _mapper;

        public RemoveCardLabelCommandHandler(IBoardLabelRepository boardLabelRepository,
            ICurrentUserService currentUserService,
            IHubClientNotifierService hubClientNotifierService,
            IStringLocalizer<RemoveCardLabelCommandHandler> localizer,
            IMapper mapper)
        {
            _boardLabelRepository = boardLabelRepository;
            _currentUserService = currentUserService;
            _hubClientNotifierService = hubClientNotifierService;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<IResult<bool>> Handle(RemoveCardLabelCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                return await Result<bool>.FailAsync(_localizer["Login required to complete this operator"]);
            }

            var label = await _boardLabelRepository.GetLabel(request.LabelId);

            if (label == null)
            {
                return await Result<bool>.FailAsync(_localizer["Label not found"]);
            }

            var dbResult = await _boardLabelRepository.Delete(label);
            if (dbResult > 0)
            {
                await _hubClientNotifierService
                        .RemoveCardLabel(label.BoardId, label.Id);

                return await Result<bool>.SuccessAsync(true, _localizer["Label removed"]);
            }

            return await Result<bool>.FailAsync(_localizer["Operation failed"]);
        }
    }
}
