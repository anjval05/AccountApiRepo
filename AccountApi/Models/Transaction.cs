using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountApi.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public double Amount { get; set; }

        public DateTime Date { get; set; }
        
        public Guid AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
    }
}