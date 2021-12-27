﻿namespace ToyShopAPI.Models
{
    public class AuthenticateResponseModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email{ get; set; }
        public string Token { get; set; }


        public AuthenticateResponseModel(UserModel user, string token)
        {
            ID = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Token = token;
        }
    }
}