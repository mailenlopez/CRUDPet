using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class PetCreatedEvent : BaseEvent
    {
        public PetCreatedEvent(Pet pet) 
        { 
           Pet = pet;
        }
        public Pet Pet { get; set; }
    }
}
