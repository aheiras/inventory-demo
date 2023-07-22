using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Entities;

namespace BlazorUI.Global
{
    public class LoggedUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }


        public void SetUser(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            Type = user.Type;

        }

        public void LogoutUser()
        {
            Id = 0;
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Type = string.Empty;
        }

    }
}
