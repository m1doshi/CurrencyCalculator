using CalculatorWebAPI.Models;
using CalculatorWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorWebAPI.Controllers
{
    [ApiController]
    [Route("currencies")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <response code="200">Request succeeded.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("gets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCurrencies()
        {
            return Ok(await currencyService.GetCurrencies());
        }

        /// <summary>
        /// Add currency.
        /// </summary>
        /// <response code="200">Request succeeded.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("saveResult")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SaveResult(ResultModel model)
        {
            return Ok(await currencyService.SaveResult(model));
        }

        /// <summary>
        /// Add currency.
        /// </summary>
        /// <response code="200">Request succeeded.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("saveCurrenciesToday")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SaveCurrenciesToday()
        {
            return Ok(await currencyService.SaveCurrenciesToday());
        }

        /// <summary>
        /// Calculate currencies.
        /// </summary>
        /// <response code="200">Request succeeded.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("calculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Calculate(Params model)
        {
            return Ok(await currencyService.Calculate(model));
        }

        /// <summary>
        /// Get results.
        /// </summary>
        /// <response code="200">Request succeeded.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("getResults")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetResults()
        {
            return Ok(await currencyService.GetResults());
        }

    }
}
