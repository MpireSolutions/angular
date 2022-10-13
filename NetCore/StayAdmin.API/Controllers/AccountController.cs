using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StayAdmin.API.Filters;
using StayAdmin.API.Helper;
using StayAdmin.BLL.Helpers;
using StayAdmin.Data.Interfaces;
using StayAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StayAdmin.API.Controllers
{
    [ValidateToken]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;

        public AccountController(ILog log, IAccountService service, IMapper mapper, IConfiguration config) : base(log, mapper, config)
        {
            _service = service;

            _messageFormatter = new MessageFormatter(Constants.Messages.Account);
        }

        [HttpGet(Name = "GetAccount")]
        public IActionResult GetAllAccount([FromQuery] PagingParams pagingParams)
        {
            try
            {
                var accounts = _service.GetAccounts(pagingParams);
                Response.Headers.Add("X-Pagination", accounts.GetHeader().ToJson());
                var outputModel = new PageOutputModel<AccountResponse>
                {
                    Paging = accounts.GetHeader(),
                    Links = GetLinks(accounts),
                    Items = _mapper.Map<List<AccountResponse>>(accounts.List.Select(x => x)).ToList()
                };
                string message = _messageFormatter.RetrievedAllSuccessfully();
                _log.Info(message);
                return Ok(ResponseHelper.Success(outputModel, message));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.HandleException(_log, $"{ _messageFormatter.ErrorRetrievingAll()} {ex.Message.ToString()}");
                return BadRequest(ResponseHelper.ErrorResponse(APIErrors.BadRequest, errorMessage));
            }
        }

        [HttpGet("searchaccount/{name}")]
        public IActionResult SearchAccount([FromQuery]PagingParams pagingParams, string name)
        {
            try
            {

                var accounts = _service.SearchAccount(pagingParams, name);

                Response.Headers.Add("X-Pagination", accounts.GetHeader().ToJson());
                var outputModel = new PageOutputModel<object>
                {
                    Paging = accounts.GetHeader(),
                    Links = GetLinks(accounts, "searchaccount/" + name),
                    Items = accounts.List.Select(x => x).ToList()
                };
                string message = _messageFormatter.RetrievedAllSuccessfully();
                _log.Info(message);
                return Ok(ResponseHelper.Success(outputModel, message));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.HandleException(_log, $"{ _messageFormatter.ErrorRetrievingAll()} {ex.Message.ToString()}");
                return BadRequest(ResponseHelper.ErrorResponse(APIErrors.BadRequest, errorMessage));
            }
        }

        [HttpPost("searchaccount")]
        public IActionResult SearchAccount([FromBody]PagingParams pagingParams)
        {
            try
            {

                var accounts = _service.SearchAccount(pagingParams, pagingParams.Keyword.ToLower());

                Response.Headers.Add("X-Pagination", accounts.GetHeader().ToJson());
                var outputModel = new PageOutputModel<object>
                {
                    Paging = accounts.GetHeader(),
                    Links = GetLinks(accounts, "searchaccount/" + pagingParams.Keyword),
                    Items = accounts.List.Select(x => x).ToList()
                };
                string message = _messageFormatter.RetrievedAllSuccessfully();
                _log.Info(message);
                return Ok(ResponseHelper.Success(outputModel, message));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.HandleException(_log, $"{ _messageFormatter.ErrorRetrievingAll()} {ex.Message.ToString()}");
                return BadRequest(ResponseHelper.ErrorResponse(APIErrors.BadRequest, errorMessage));
            }
        }

        [HttpPost("searchaccountcontact")]
        public IActionResult SearchAccountContact([FromBody]PagingParams pagingParams)
        {
            try
            {
                var accounts = _service.SearchAccountContact(pagingParams);
               
                string message = _messageFormatter.RetrievedAllSuccessfully();
                _log.Info(message);
                return Ok(ResponseHelper.Success(accounts, message));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.HandleException(_log, $"{ _messageFormatter.ErrorRetrievingAll()} {ex.Message.ToString()}");
                return BadRequest(ResponseHelper.ErrorResponse(APIErrors.BadRequest, errorMessage));
            }
        }

        [HttpGet("individual/{Id}")]
        public async Task<IActionResult> GetIndividual(int id)
        {
            try
            {
                var Account = await _service.GetIndividualById(id);
                if (Account == null)
                {
                    return NotFound(ResponseHelper.ErrorResponse(APIErrors.NotFound, _messageFormatter.NotFound()));
                }
                var AccountResponse = _mapper.Map<AccountResponse>(Account);
                string message = _messageFormatter.RetrievedAllSuccessfully();
                _log.Info(message);
                return Ok(ResponseHelper.Success(AccountResponse, message));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.HandleException(_log, $"{ _messageFormatter.ErrorRetrievingAll()} {ex.Message.ToString()}");
                return BadRequest(ResponseHelper.ErrorResponse(APIErrors.BadRequest, errorMessage));
            }
        }


        [ValidateModel]
        [HttpPost("AddIndividual")]
        public IActionResult AddIndividual([FromBody] PersonRequest personRequest)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];

                var person = _service.AddIndividual(personRequest, token);


                string message = _messageFormatter.AddedSuccessfully();
                _log.Info(message);

                return Ok(ResponseHelper.Success(person, message));
            }
            catch (Exception ex)
            {
                string errorMessage = ex.HandleException(_log, $"{ _messageFormatter.ErrorRetrievingAll()} {ex.Message.ToString()}");
                return BadRequest(ResponseHelper.ErrorResponse(APIErrors.BadRequest, errorMessage));
            }
        }
    }
}