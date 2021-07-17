using System.Collections.Generic;
using System.Data;
using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application.Inventory
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _repository;

        public InventoryApplication(IInventoryRepository repository)
        {
            _repository = repository;
        }
        public OperationResult Create(CreateInventory command)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.ProductId == command.ProductId))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var inventory = new Domain.InventoryAgg.Inventory(command.ProductId, command.UnitPrice);

            _repository.Create(inventory);

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var opertaionResult = new OperationResult();

            var inventory = _repository.GetById(command.Id);

            if (inventory == null)
                return opertaionResult.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return opertaionResult.Failed(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId,command.UnitPrice);

            _repository.SaveChanges();

            return opertaionResult.Succedded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operationResult = new OperationResult();

            var inventory = _repository.GetById(command.InventoryId);

            if (inventory == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;

            inventory.Increase(command.Count,operatorId,command.Description);

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operationResult = new OperationResult();
            const long operatorId = 1;

            foreach (var item in command)
            {
                var inventory = _repository.GetByProductId(item.ProductId);

                if (inventory == null)
                    return operationResult.Failed(ApplicationMessages.RecordNotFound);

                inventory.Reduce(item.Count,operatorId,item.OrderId,item.Description);
            }

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operationResult = new OperationResult();

            var inventory = _repository.GetById(command.InventoryId);

            if (inventory == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;

            inventory.Reduce(command.Count, operatorId, 0, command.Description);

            _repository.SaveChanges();

            return operationResult.Succedded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public EditInventory GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetLog(long id)
        {
            return _repository.GetLog(id);
        }
    }
}
