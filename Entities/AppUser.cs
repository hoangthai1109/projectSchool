using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        [Key]
        public int id {get; set;}
        public string userName {get;set;}
        public byte[] passwordHash {get;set;}
        public byte[] passwordSalt {get;set;}
        public int Gender{get;set;}
        public string AccountType {get;set;}
        public DateOnly DateOfBirth {get;set;}
        public string FullName{get;set;}
        public string Address{get;set;}
        public int PhoneNumber{get;set;}
        public string ListQaCode {get;set;}
        public string Email {get;set;}
        public string? RoleDefault {get;set;}
        public DateTime CreateDate {get;set;}
        public List<image> Images {get;set;} = new();
        public List<Qa> Qas {get;set;} = new();
    }}