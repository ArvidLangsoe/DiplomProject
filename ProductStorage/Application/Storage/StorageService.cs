using Application.Interfaces.Persistence;
using Application.Storage.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class StorageService
    {
        private readonly IProductStorageRepository _storageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StorageService(IProductStorageRepository storageRepository, IUnitOfWork unitOfWork) {
            _storageRepository = storageRepository;
            _unitOfWork = unitOfWork;

        }

        public ProductAvailabiltyDTO ProductCount(Guid productId) {
            var productStorage = _storageRepository.GetProductStorage(productId);

            return new ProductAvailabiltyDTO()
            {
                ProductId = productId,
                NumAvailable = productStorage.NumAvailableProduct
            };
        }



    }
}
