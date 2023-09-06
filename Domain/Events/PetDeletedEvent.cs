using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class PetDeletedEvent : BaseEvent
    {
        public PetDeletedEvent(Pet pet)
        {
            Pet = pet;
        }
        public Pet Pet { get; set; }
    }
}
