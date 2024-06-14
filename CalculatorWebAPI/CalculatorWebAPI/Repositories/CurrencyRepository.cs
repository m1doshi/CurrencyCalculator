using CalculatorWebAPI.Database;
using CalculatorWebAPI.Models;
using CalculatorWebAPI.Entities;
using CalculatorWebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorWebAPI.Repositories
{
    public class CurrencyRepository: ICurrencyRepository
    {
        private readonly MyDbContext dbContext;

        public CurrencyRepository(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CurrencyModel>> GetCurrencies()
        {
            return await GetAllCurrencies().ToListAsync();
        }

        public async Task<IEnumerable<ResultModel>> GetResults()
        {
            return await GetAllResults().ToListAsync();
        }

        public async Task<bool> SaveCurrenciesToday()
        {
            var item = GetCurrenciesIntoAPI();
            if (dbContext.Currencies.Where(x => x.Date == item[0].Date).FirstOrDefault() != null)
                return false;

            foreach (var it in item)
            {
                Currency model = new Currency();
                model.Cur_ID = it.Cur_ID;
                model.Cur_Name = it.Cur_Name;
                model.Cur_Scale = it.Cur_Scale;
                model.Cur_OfficialRate = it.Cur_OfficialRate;
                model.Cur_Abbreviation = it.Cur_Abbreviation;
                model.Id = it.Id;
                model.Date = it.Date;
                dbContext.Currencies.Add(model);
            }
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveResult(ResultModel model)
        {
            Result resultEnt = new();
            resultEnt.FirstNumber = model.FirstNumber;
            resultEnt.SecondNumber = model.SecondNumber;
            resultEnt.FirstCurrency = model.FirstCurrency;
            resultEnt.SecondCurrency = model.SecondCurrency;
            resultEnt.MathOperation = model.MathOperation;
            resultEnt.Results = model.Results;
            resultEnt.Date = DateTime.Now;
            dbContext.Results.AddAsync(resultEnt);
            return await dbContext.SaveChangesAsync() > 0;
        }
        
        public ResultModel GetDeparsedResult(string json)
        {
            ResultModel model = JsonSerializer.Deserialize<ResultModel>(json);
            return model;
        }
        public List<CurrencyModel> GetDeparsedCurrencies(string json)
        {
            List<CurrencyModel> data = JsonSerializer.Deserialize<List<CurrencyModel>>(json);
            return data;
        }
        
        public List<CurrencyModel> GetCurrenciesIntoAPI()
        {
            string url = "https://api.nbrb.by/exrates/rates?periodicity=0";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string responseBody = reader.ReadToEnd();
                return GetDeparsedCurrencies(responseBody); 
            }
        }

        public CurrencyModel GetCurrencyByAbbreviation(string abbreviation)
        {
            return dbContext.Currencies.Where(x=>x.Cur_Abbreviation == abbreviation).Select(d => new CurrencyModel
            {
                Cur_ID = d.Cur_ID,
                Date = d.Date,
                Cur_Abbreviation = d.Cur_Abbreviation,
                Cur_Scale = d.Cur_Scale,
                Cur_Name = d.Cur_Name,
                Cur_OfficialRate = d.Cur_OfficialRate
            }).FirstOrDefault();
        }

        private IQueryable<CurrencyModel> GetAllCurrencies()
        {
            return dbContext.Currencies.Select(d => new CurrencyModel
            {
                Cur_ID = d.Cur_ID,
                Date = d.Date,
                Cur_Abbreviation = d.Cur_Abbreviation,
                Cur_Scale = d.Cur_Scale,
                Cur_Name = d.Cur_Name,
                Cur_OfficialRate= d.Cur_OfficialRate
            });
        }

        private IQueryable<ResultModel> GetAllResults()
        {
            return dbContext.Results.Select(d => new ResultModel
            {
                FirstNumber = d.FirstNumber,
                SecondNumber = d.SecondNumber,
                FirstCurrency = d.FirstCurrency,
                SecondCurrency = d.SecondCurrency,
                MathOperation = d.MathOperation,
                Results = d.Results,
                Date = d.Date
            });
        }

    }
}
