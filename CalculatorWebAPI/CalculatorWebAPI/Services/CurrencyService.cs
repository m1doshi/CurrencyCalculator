using CalculatorWebAPI.Database;
using CalculatorWebAPI.Entities;
using CalculatorWebAPI.Models;
using CalculatorWebAPI.Repositories.Interfaces;
using CalculatorWebAPI.Services.Interfaces;
using System.Data.Entity;

namespace CalculatorWebAPI.Services
{
    public class CurrencyService: ICurrencyService
    {
        private readonly ICurrencyRepository currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        public async Task<IEnumerable<CurrencyModel>> GetCurrencies()
        {
            return await currencyRepository.GetCurrencies();
        }

        public async Task<IEnumerable<ResultModel>> GetResults()
        {
            return await currencyRepository.GetResults();
        }
        public async Task<bool> SaveResult(ResultModel model)
        {
            return await currencyRepository.SaveResult(model);
        }

        public async Task<bool> SaveCurrenciesToday()
        {
            return await currencyRepository.SaveCurrenciesToday();
        }

        public  async Task<CurrencyModel> GetCurrencyByAbbreviation(string abbreviation)
        {
            return currencyRepository.GetCurrencyByAbbreviation(abbreviation);
        }

        //public async Task<decimal> Calculate(Params model)
        //{
        //    CurrencyModel first = new();
        //    CurrencyModel second = new();
        //    first = await GetCurrencyByAbbreviation(model.FirstCurrency);
        //    second = await GetCurrencyByAbbreviation(model.SecondCurrency);
        //    decimal firstExrate = first.Cur_OfficialRate;
        //    if (first.Cur_Scale > 1) firstExrate /= first.Cur_Scale;
        //    decimal result = 0;
        //    if (model.SecondCurrency != "")
        //    {
        //        decimal secondExrate = second.Cur_OfficialRate;
        //        if (second.Cur_Scale > 1) secondExrate /= second.Cur_Scale;
        //        switch (model.MathOperation)
        //        {
        //            case "+": result = decimal.Parse(model.FirstNumber) * firstExrate + decimal.Parse(model.SecondNumber) * secondExrate; break;
        //            case "-": result = decimal.Parse(model.FirstNumber) * firstExrate - decimal.Parse(model.SecondNumber) * secondExrate; break;
        //            case "*": result = (decimal.Parse(model.FirstNumber) * firstExrate) * (decimal.Parse(model.SecondNumber) * secondExrate); break;
        //            case "/": result = (decimal.Parse(model.FirstNumber) * firstExrate) / (decimal.Parse(model.SecondNumber) * secondExrate); break;
        //        }
        //    }
        //    else
        //    {
        //        switch (model.MathOperation)
        //        {
        //            case "*": result = decimal.Parse(model.FirstNumber) * firstExrate * decimal.Parse(model.SecondNumber); break;
        //            case "/": result = (decimal.Parse(model.FirstNumber) * firstExrate) / decimal.Parse(model.SecondNumber); break;
        //        }
        //    }
        //    return result;
        //}

        public async Task<decimal> Calculate(Params model)
        {
            CurrencyModel first = new();
            CurrencyModel second = new();
            first = await GetCurrencyByAbbreviation(model.FirstCurrency);
            second = await GetCurrencyByAbbreviation(model.SecondCurrency);
            decimal firstExrate = first.Cur_OfficialRate;
            if (first.Cur_Scale > 1) firstExrate /= first.Cur_Scale;
            decimal result = 0;
            switch(model.SecondCurrency)
            {
                 case "":
                    switch (model.MathOperation)
                    {
                        case "*": result = decimal.Parse(model.FirstNumber) * firstExrate * decimal.Parse(model.SecondNumber); break;
                        case "/": result = (decimal.Parse(model.FirstNumber) * firstExrate) / decimal.Parse(model.SecondNumber); break;
                    }
                    break;
                 default :
                    decimal secondExrate = second.Cur_OfficialRate;
                    if (second.Cur_Scale > 1) secondExrate /= second.Cur_Scale;
                    switch (model.MathOperation)
                    {
                        case "+": result = decimal.Parse(model.FirstNumber) * firstExrate + decimal.Parse(model.SecondNumber) * secondExrate; break;
                        case "-": result = decimal.Parse(model.FirstNumber) * firstExrate - decimal.Parse(model.SecondNumber) * secondExrate; break;
                        case "*": result = (decimal.Parse(model.FirstNumber) * firstExrate) * (decimal.Parse(model.SecondNumber) * secondExrate); break;
                        case "/": result = (decimal.Parse(model.FirstNumber) * firstExrate) / (decimal.Parse(model.SecondNumber) * secondExrate); break;
                    }
                    break;
            }
            return result;
        }
    }


}
