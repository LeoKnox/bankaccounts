using System;
using System.ComponentModel.DataAnnotations;

namespace bankAccount.Models
{
    public class Transactions
    {
        [Key]
        public int TransactionsId {get; set;}
        public Decimal Decimal {get; set;}
        public DateTime CreatedAt {get; set;}

        public int UsersId {get; set;}
        public Users Banker {get; set;}
    }
}