using CalculatorWebAPI.Entities;
using CalculatorWebAPI.Models;

namespace CalculatorWebAPI.Repositories.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<CurrencyModel>> GetCurrencies();
        Task<bool> SaveResult(ResultModel model);
        Task<bool> SaveCurrenciesToday();
        CurrencyModel GetCurrencyByAbbreviation(string abbreviation);
        Task<IEnumerable<ResultModel>> GetResults();
    }
}
