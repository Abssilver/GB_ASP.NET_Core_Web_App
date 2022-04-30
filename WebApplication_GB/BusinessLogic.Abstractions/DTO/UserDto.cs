
using System;

namespace BusinessLogic.Abstractions.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
    }
}