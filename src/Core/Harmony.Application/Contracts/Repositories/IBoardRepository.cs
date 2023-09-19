﻿using Harmony.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmony.Application.Contracts.Repositories
{
    public interface IBoardRepository
    {
        /// <summary>
        /// Create a Board
        /// </summary>
        /// <param name="Board"></param>
        /// <returns></returns>
        Task<int> CreateAsync(Board Board);

        /// <summary>
        /// Returns boards created by userId plus have access to
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Board>> GetAllForUser(string userId);
        Task<Board> LoadBoard(Guid boardId);
        Task<bool> Exists(Guid boardId);
    }
}
