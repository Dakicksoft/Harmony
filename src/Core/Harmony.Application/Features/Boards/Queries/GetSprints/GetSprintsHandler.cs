﻿using AutoMapper;
using Harmony.Application.Contracts.Repositories;
using Harmony.Application.Contracts.Services;
using Harmony.Application.Contracts.Services.Identity;
using Harmony.Application.Contracts.Services.Management;
using Harmony.Application.DTO;
using Harmony.Application.Responses;
using Harmony.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Harmony.Application.Features.Boards.Queries.GetSprints
{
    public class GetSprintsHandler : IRequestHandler<GetSprintsQuery, PaginatedResult<SprintDto>>
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IMapper _mapper;

        public GetSprintsHandler(
            ISprintRepository sprintRepository,
            IMapper mapper)
        {
            _sprintRepository = sprintRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<SprintDto>> Handle(GetSprintsQuery request, CancellationToken cancellationToken)
        {
            request.PageNumber = request.PageNumber == 0 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize == 0 ? 10 : request.PageSize;

            var totalSprints =
                await _sprintRepository.CountSprints(request.BoardId);

            var sprints = await _sprintRepository.SearchSprints(request.BoardId,
                 request.SearchTerm, request.PageNumber, request.PageSize);

            var result = _mapper.Map<List<SprintDto>>(sprints);

            return PaginatedResult<SprintDto>
                .Success(result, totalSprints, request.PageNumber, request.PageSize);
        }
    }
}
