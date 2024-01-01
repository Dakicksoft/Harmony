﻿using Harmony.Application.Extensions;
using Harmony.Application.Specifications.Base;
using Harmony.Domain.Entities;

namespace Harmony.Application.Specifications.Cards
{
    public class CardFilterSpecification : HarmonySpecification<Card>
    {
        public CardFilterSpecification(Guid? cardId, CardIncludes includes)
        {
            if(includes.Attachments)
            {
                Includes.Add(card => card.Attachments);
            }

            if (includes.BoardList)
            {
                Includes.Add(card => card.BoardListId);
            }

            if (cardId.HasValue)
            {
                Criteria = card => card.Id == cardId;
            }
        }

        public CardFilterSpecification(string term, CardIncludes includes)
        {
            if (includes.Attachments)
            {
                Includes.Add(card => card.Attachments);
            }

            if (includes.Board)
            {
                Includes.Add(card => card.BoardList.Board);
            }
            else if (includes.BoardList)
            {
                Includes.Add(card => card.BoardList);
            }

            if (!string.IsNullOrEmpty(term))
            {
                Criteria = card => card.Title.Contains(term);
            }
        }

        public CardFilterSpecification(CardFilters filters, CardIncludes includes)
        {
            if (includes.Attachments)
            {
                Includes.Add(card => card.Attachments);
            }

            if (includes.Board)
            {
                Includes.Add(card => card.BoardList.Board);
            }
            else if (includes.BoardList)
            {
                Includes.Add(card => card.BoardList);
            }

            if(filters.CombineCriteria)
            {
                if (!string.IsNullOrEmpty(filters.Title))
                {
                    Criteria = And(card => card.Title.Contains(filters.Title));
                }

                if (!string.IsNullOrEmpty(filters.Description))
                {
                    Criteria = And(card => card.Description.Contains(filters.Description));
                }

                if (filters.HasAttachments)
                {
                    Criteria = And(card => card.Attachments.Count > 0);
                }

                if (filters.BoardId.HasValue)
                {
                    Criteria = And(card => card.BoardList.BoardId == filters.BoardId.Value);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(filters.Title))
                {
                    Criteria = Or(card => card.Title.Contains(filters.Title));
                }

                if (!string.IsNullOrEmpty(filters.Description))
                {
                    Criteria = Or(card => card.Description.Contains(filters.Description));
                }

                if (filters.BoardId.HasValue)
                {
                    Criteria = And(card => card.BoardList.BoardId == filters.BoardId.Value);
                }
            }
        }

        private void SaveOr()
        {

        }

        private void SafeAnd()
        {

        }
    }
}
