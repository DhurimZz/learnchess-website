﻿namespace learnchess.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}