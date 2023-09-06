using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class PetCompletedEvent : BaseEvent
    {
        public PetCompletedEvent(Pet pet)
        {
            Pet = pet;
        }
        public Pet Pet { get; set; }
    }
}
