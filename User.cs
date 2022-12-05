using ePharm.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePharm
{
    internal static class User
    {
        public static int Id { get; set; }

        public static string Name { get; set; }

        public static string Family { get; set; }

        public static string Mail { get; set; }

        public static decimal Balance { get; set; }

        public static bool IsAdmin { get; set; }

        public static void SetUserData(users user)
        {
            Id = user.id;
            Name = user.name;
            Family = user.family;
            Mail = user.mail;
            Balance = user.balance;
            IsAdmin = user.isAdmin;
        }
    }
}
