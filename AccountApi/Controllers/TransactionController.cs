using AccountApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AccountApi.Controllers
{
    public class TransactionController : ApiController
    {
        private readonly AccountDbContext db = new AccountDbContext();

        [HttpGet, Route("api/Accounts/{accountId}/Transactions")]
        public IHttpActionResult GetByAccountId(Guid accountId)
        {
            var transactions = db.Transactions.Where(x => x.AccountId == accountId)
                                .AsEnumerable().Select(x => new
                                {
                                    x.Date,
                                    x.Amount
                                }).ToList();

            return Ok(transactions);            
        }

        [HttpPost, Route("api/Accounts/{accountId}/Transactions")]
        public IHttpActionResult Add(Guid accountId, Transaction transaction)
        {
            var id = Guid.NewGuid();
            transaction.Id = id;

            transaction.AccountId = accountId;
            transaction.Date = DateTime.Now;
            db.Transactions.Add(transaction);

            Account account = db.Accounts.Find(accountId);
            account.Amount += transaction.Amount;

            db.SaveChanges();
            return Ok();
        }
    }
}