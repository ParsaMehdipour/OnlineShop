using System.Collections.Generic;
using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository : IRepsoitory<long , ColleagueDiscount>
    {
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
        EditColleagueDiscount GetDetails(long id);
    }
}
