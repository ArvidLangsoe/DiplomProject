using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Persistence
{
    public interface IProductStorageRepository
    {
        ProductStorage GetProductStorage(Guid productId);
    }
}
