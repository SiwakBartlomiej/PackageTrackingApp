﻿using PackageTrackingApp.Core.Domain;
using PackageTrackingApp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PackageTrackingApp.Core.Domains
{
    public class User
    {
        [Key]
        public Guid Guid { get; protected set; }
        [Required]
        public string Login { get; protected set; }
        [Required]
        public string Password { get; protected set; }
        [Required]
        public string Email { get; protected set; }
        public string Role { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; protected set; }

        public User()
        {

        }

        public User(string email, string login, string role,
            string password, string confirmPassword)
        {
            Guid = Guid.NewGuid();
            SetLogin(login);
            SetEmail(email);
            SetRole(role);
            SetPassword(password, confirmPassword);
        }

        private void SetLogin(string login)
        {
            if(string.Equals(Login, login))
            {
                return;
            }

            if(login.Length < 4)
            {
                throw new Exception("Login must be at least 4 characters long");
            }

            Login = login;
        }

        private void SetPassword(string password, string confirmPassword)
        {
            if (string.Equals(Password, password))
            {
                return;
            }

            if(password.Length < 6)
            {
                throw new Exception("Password must be at least 6 characters long");
            }

            if (!password.IsValidPassword())
            {
                throw new Exception("Please choose a stronger password");
            }

            if (!string.Equals(password, confirmPassword))
            {
                throw new Exception("Passwords do not match!");
            }

            var hashedPassword = BCryptNet.HashPassword(password);

            Password = hashedPassword;
        }

        private void SetRole(string role)
        {
            if(string.Equals(role, Role))
            {
                return;
            }

            if(!string.Equals(role, Roles.Admin) 
                && !string.Equals(role, Roles.User))
            {
                throw new Exception($"Role \"{role}\" does not exist.");
            }

            Role = role;
        }

        private void SetEmail(string email)
        {
            if(string.Equals(Email, email))
            {
                return;
            }

            if (!email.IsEmail())
            {
                throw new Exception("Email is not valid.");
            }

            Email = email;
        }
    }
}
