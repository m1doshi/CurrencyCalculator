using CalculatorWebAPI.Entities;
using CalculatorWebAPI.Models;

namespace CalculatorWebAPI.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyModel>> GetCurrencies();

        Task<CurrencyModel> GetCurrencyByAbbreviation(string abbreviation);

        Task<bool> SaveResult(ResultModel model);
        Task<IEnumerable<ResultModel>> GetResults();
        Task<decimal> Calculate(Params model);

        Task<bool> SaveCurrenciesToday();
    }
}
