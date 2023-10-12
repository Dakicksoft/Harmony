﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harmony.Application.Helpers
{
    public class CardHelper
    {
        public static string DisplayDates(DateTime? startDate, DateTime? dueDate)
        {
            var result = string.Empty;

            if (startDate.HasValue && dueDate.HasValue)
            {
                if (startDate.Value.Month == dueDate.Value.Month)
                {
                    return $"{startDate.Value.ToString("MMM")} {startDate.Value.ToString("dd")} - {dueDate.Value.ToString("dd")}";
                }


                return $"{startDate.Value.ToString("MMM dd")} - {dueDate.Value.ToString("MMM dd")}";
            }
            else if (startDate.HasValue)
            {
                return $"{startDate.Value.ToString("MMM dd")}";
            }
            else if (dueDate.HasValue)
            {
                return $"{dueDate.Value.ToString("MMM dd")}";
            }

            return result;
        }
    }
}
