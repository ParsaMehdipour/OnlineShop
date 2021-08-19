using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application.CustomerDiscount
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _repository;

        public CustomerDiscountApplication(ICustomerDiscountRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            var customerDiscount = new Domain.CustomerDiscountAgg.CustomerDiscount(
                command.ProductId, command.DiscountRate, startDate, endDate
                , command.Reason);

            _repository.Create(customerDiscount);

            _repository.SaveChanges();

            return operationResult.Succeeded();

        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operationResult = new OperationResult();

            var customerDiscount = _repository.GetById(command.Id);

            if (customerDiscount == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            if (_repository.Exists(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id)) 
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            customerDiscount.Edit(command.Id,command.DiscountRate,startDate,endDate,command.Reason);

            _repository.SaveChanges();

            return operationResult.Succeeded();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }
    }
}
