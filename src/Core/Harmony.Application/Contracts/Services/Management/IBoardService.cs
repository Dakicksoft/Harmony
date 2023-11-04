﻿using Harmony.Domain.Entities;

namespace Harmony.Application.Contracts.Services.Management
{
    public interface IBoardService
	{
        Task<bool> HasUserAccessToBoard(string userId, Guid boardId);
        Task<List<Board>> GetUserBoards(Guid workspaceId, string userId);
        Task<Board> LoadBoard(Guid boardId, int maxCardsPerList);
        Task<List<Card>> LoadBoardListCards(Guid boardId, Guid boardListId, int page, int maxCardsPerList);
    }
}
