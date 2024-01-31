using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class Card
    {
        public Guid Id { get; set; }
        public Type CardType { get; set; }
        
    }
}

public enum Type
{
    Standart,
    Premium
}
