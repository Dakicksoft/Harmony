﻿namespace Harmony.Application.DTO
{
    public class CardMemberDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
