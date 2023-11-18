﻿using Harmony.Domain.Entities;

namespace Harmony.Client.Infrastructure.Routes
{
    public static class BoardEndpoints
    {
        public static string Index = "api/board/";

        public static string Get(string boardId, int size)
        {
            return $"{Index}{boardId}/?size={size}";
        }

        public static string GetMembers(string boardId)
        {
            return $"{Index}{boardId}/members/";
        }

        public static string GetIssueTypes(string boardId)
        {
            return $"{Index}{boardId}/issuetypes/";
        }

        public static string Member(string boardId, string userId)
        {
            return $"{Index}{boardId}/members/{userId}/";
        }

        public static string MemberStatus(string boardId, string userId)
        {
            return $"{Index}{boardId}/members/{userId}/status/";
        }

        public static string SearchMembers(string boardId, string term)
        {
            return $"{Index}{boardId}/members/search?term={term}";
        }

        public static string CreateCard(Guid boardId, Guid listId)
		{
			return $"{Index}{boardId}/lists/{listId}/cards/";
		}

        public static string Sprints(Guid boardId)
        {
            return $"{Index}{boardId}/sprints/";
        }

        public static string BoardListPositions(string boardId)
        {
            return $"{Index}{boardId}/positions/";
        }

        public static string Backlog(string boardId, int pageNumber, int pageSize, string searchTerm, string[] orderBy)
        {
            var url = $"{Index}{boardId}/backlog/?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={searchTerm}&orderBy=";
            
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1];
            }
            return url;
        }

        public static string SprintCards(string boardId, int pageNumber, int pageSize, string searchTerm, string[] orderBy)
        {
            var url = $"{Index}{boardId}/sprints/cards/?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={searchTerm}&orderBy=";

            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1];
            }
            return url;
        }

        public static string Sprints(string boardId, int pageNumber, int pageSize, string searchTerm, string[] orderBy)
        {
            var url = $"{Index}{boardId}/sprints/?pageNumber={pageNumber}&pageSize={pageSize}&searchTerm={searchTerm}&orderBy=";

            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1];
            }
            return url;
        }

        public static string GetBoardList(string boardId, Guid listId, int page, int maxCards)
        {
            return $"{Index}{boardId}/lists/{listId}/?page={page}&maxCards={maxCards}";
        }
    }
}