using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Abstractions.Entities
{
    public class Card: Entity
    {
        public Guid ClientId { get; init; }
        public Type CardType { get; set; }
        public bool IsActive { get; set; }

    }
}

public enum Type
{
    Sport,
    SportPool,
    Premium,
    LuxSpa
}
