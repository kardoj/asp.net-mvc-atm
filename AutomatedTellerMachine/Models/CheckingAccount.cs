using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class CheckingAccount
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName="varchar")]
        // Pikkus vahemikus 6 kuni 10
        [RegularExpression(@"\d{6,10}", ErrorMessage="Kontonumber peab olema pikkusega 6 kuni 10 tähemärki.")]
        [Display(Name = "Konto #")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Eesnimi")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Perekonnanimi")]
        public string LastName { get; set; }

        [Display(Name = "Ees- ja perekonnanimi")]
        public string Name {
            get {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        [DataType(DataType.Currency)]
        [Display(Name = "Kontojääk")]
        public decimal Balance { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        // Võimaldab seotud tehinguid küsida
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}