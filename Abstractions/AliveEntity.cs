using DataBase.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Abstractions
{
    public interface AliveEntity
    {
        public string RealName { get; set; }
        public string Username { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Bigender,
        Transgender,
        Gey,
        Lesbian,
        Bisexual,
        Demigirl,
        Demiboy,
        Asexual
    }
}
