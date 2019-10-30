using Application.Interfaces.Persistence;
using Application.Storage.DTO;
using Application.Storage.DTO.AddBatch;
using Domain;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class StorageService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IProductStorageRepository _storageRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StorageService(IProductStorageRepository storageRepository, IBatchRepository batchRepository, IUnitOfWork unitOfWork)
        {
            _storageRepository = storageRepository;
            _unitOfWork = unitOfWork;
            _batchRepository = batchRepository;

        }

        public ProductAvailabilityDTO ProductCount(Guid productId)
        {
            var productStorage = _storageRepository.GetProductStorage(productId);

            return new ProductAvailabilityDTO()
            {
                ProductId = productId,
                NumAvailable = productStorage.NumAvailableProduct
            };
        }


        public void AddBatch(AddBatchDTO batchDTO)
        {
            var batch = batchDTO.ToBatch();
            _batchRepository.AddBatch(batch);
            
            _unitOfWork.CommitChanges();
            Logger.Info("Batch added {@batch}", batch);
        }


    }
}
