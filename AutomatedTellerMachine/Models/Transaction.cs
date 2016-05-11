using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Summa")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Kontonumber")]
        public int CheckingAccountId { get; set; }

        // Võimaldab seotud konto andmeid küsida
        public virtual CheckingAccount CheckingAccount { get; set; }
    }
}