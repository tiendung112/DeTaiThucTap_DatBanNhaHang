﻿namespace DatBanNhaHang.Payloads.Requests.NguoiDung.User
{
    public class Request_Register
    {
        public string UserName { get; set; }
        public string Name { get; set; }
       // public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
