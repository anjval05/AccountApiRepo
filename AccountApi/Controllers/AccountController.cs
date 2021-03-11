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
    public class AccountController : ApiController
    {
        private readonly AccountDbContext db = new AccountDbContext();

        [HttpGet, Route("api/Accounts/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var account = db.Accounts.Where(x => x.Id == id).AsEnumerable().Select(x => new
            {
                x.Id,
                x.Name,
                x.Number,
                x.Amount
            });

            return Ok(account);
        }

        [HttpGet, Route("api/Accounts")]
        public IHttpActionResult GetAll()
        {
            var accounts = db.Accounts.AsEnumerable().Select(x => new
            {
                x.Id,
                x.Name,
                x.Number,
                x.Amount,
            }).ToList();

            return Ok(accounts);
        }


        [HttpPost, Route("api/Accounts")]
        public IHttpActionResult Add(Account account)
        {
            var id = Guid.NewGuid();
            account.Id = id;

            db.Accounts.Add(account);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut, Route("api/Accounts/{id}")]
        public IHttpActionResult Update(Guid id, Account account)
        {
            var accnt = db.Accounts.Find(id);
            accnt.Name = account.Name;
            db.Entry(accnt).Property(x => x.Name).IsModified = true;

            db.SaveChanges();
            return Ok();
        }

        [HttpDelete, Route("api/Accounts/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            db.Accounts.Remove(db.Accounts.Find(id));
            db.SaveChanges();

            return Ok();
        }
    }
}