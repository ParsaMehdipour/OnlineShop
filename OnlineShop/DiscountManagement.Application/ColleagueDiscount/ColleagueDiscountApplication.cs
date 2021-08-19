using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application.ColleagueDiscount
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _repository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository repository)
        {
            _repository = repository;
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var colleagueDiscount =
                new Domain.ColleagueDiscountAgg.ColleagueDiscount(command.ProductId, command.DiscountRate);

            _repository.Create(colleagueDiscount);

            _repository.SaveChanges();

            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operationResult = new OperationResult();

            var colleagueDiscount = _repository.GetById(command.Id);

            if (colleagueDiscount == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            colleagueDiscount.Edit(command.ProductId,command.DiscountRate);

            if (_repository.Exists(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            _repository.SaveChanges();

            return operationResult.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();

            var colleagueDiscount = _repository.GetById(id);

            if (colleagueDiscount == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            colleagueDiscount.Remove();

            _repository.SaveChanges();

            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();

            var colleagueDiscount = _repository.GetById(id);

            if (colleagueDiscount == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            colleagueDiscount.Restore();

            _repository.SaveChanges();

            return operationResult.Succeeded();
        }
    }
}