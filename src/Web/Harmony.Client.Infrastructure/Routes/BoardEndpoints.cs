﻿using Harmony.Domain.Enums;

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

        public static string GetBoardLists(string boardId)
        {
            return $"{Index}{boardId}/boardlists/";
        }

        public static string Member(string boardId, string userId)
        {
            return $"{Index}{boardId}/members/{userId}/";
        }

        public static string MemberStatus(string boardId, string userId)
        {
            return $"{Index}{boardId}/members/{userId}/status/";
        }

        public static string MoveCardsToSprint(string boardId)
        {
            return $"{Index}{boardId}/movecardstosprint/";
        }

        public static string MoveCardsToBacklog(string boardId)
        {
            return $"{Index}{boardId}/movecardstobacklog/";
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

        public static string SprintCards(string boardId, int pageNumber, int pageSize, string searchTerm, string[] orderBy, SprintStatus? status)
        {
            var url = $"{Index}{boardId}/sprints/cards/?pageNumber={pageNumber}" +
                $"&pageSize={pageSize}&searchTerm={searchTerm}" + (status == null ? string.Empty : $"&status={status}") +
                $"&orderBy=";

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

        public static string GetSprintPendingCards(Guid boardId, Guid sprintId)
        {
            return $"{Index}{boardId}/sprints/{sprintId}/cards/pending/";
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