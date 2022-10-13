using StayAdmin.Data.Entities;
using StayAdmin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StayAdmin.Data.Interfaces
{
    public interface IAccountService
    {
        PagedList<object> GetAccounts(PagingParams pagingParams);

        PagedList<object> SearchAccount(PagingParams pagingParams, string Name);
        List<SearchAccountContactResponse> SearchAccountContact(PagingParams pagingParams);
        Task<AccountResponse> CreateAccount(AccountRequest accountRequest,string token);
        Task<AccountResponse> UpdateAccount(int id,AccountRequest accountRequest,string token);

        void DeleteAccount(int id);
        SearchAccountResponse AddIndividual(PersonRequest personRequest, string token);
        SearchAccountResponse ResponseListing(int id);
        Task<Account> GetIndividualById(int id);
    }
}
