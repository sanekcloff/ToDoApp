using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Handlers
{
    public static class InputHandler
    {
        const string ENG_PATTERN = "A-Za-z";
        const string RU_PATTERN = "А-Яа-яёЁ";
        const string NUM_PATTERN = "0-9";

        public static bool Handle(User user)
        {
            return Regex.IsMatch(user.Firstname,$"^[{ENG_PATTERN}{RU_PATTERN}]+$") 
                && Regex.IsMatch(user.Lastname, $"^[{ENG_PATTERN}{RU_PATTERN}]+$")
                && Regex.IsMatch(user.Middlename, $"^[{ENG_PATTERN}{RU_PATTERN}]+$")
                && Regex.IsMatch(user.Login, $"^[{ENG_PATTERN}{NUM_PATTERN}]+$")
                && Regex.IsMatch(user.Login, $"^[{ENG_PATTERN}{NUM_PATTERN}]+$");
        }

        public static bool Handle(Objective objective)
        {
            return Regex.IsMatch(objective.Title, $"^[{ENG_PATTERN}{RU_PATTERN} {NUM_PATTERN}]+$");
        }
    }
}
