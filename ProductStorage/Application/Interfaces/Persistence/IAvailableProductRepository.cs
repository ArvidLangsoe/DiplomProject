using Domain;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Persistence
{
    public interface IAvailableProductRepository
    {
        void Replace(AvailableProduct product);
        void Remove(Guid id);
        AvailableProduct Get(Guid id);
        EventCounter GetEventCounter(string id);
        void AddEventCounter(EventCounter eventCounter);


    }
}
