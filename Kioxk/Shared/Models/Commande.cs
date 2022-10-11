
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kioxk.Shared.Models
{    public class Livret
    {
        public int LivretId { get; set; }
        public  HashSet<Datetime>? UnSelectable { get; set; }
        public List<Hashset>? Seasons { get; set; } 
        public  IList<Int>? Prices { get; set; }
             
    }
 
    public class Datetime
    {
        public int DatetimeId { get; set; }
        public int Index { get; set; }
        public DateTime Dt { get; set; }       

        [ForeignKey("LivretId")]
        public Livret? Livret { get; set; }
                
        [ForeignKey("CommandeId")]
        public Commande? Commande { get; set; }

        [ForeignKey("HashsetId")]
        public Hashset? Hashset { get; set; }
    }
   
    public class Hashset
    {
        public int HashsetId { get; set; }       
        public HashSet<Datetime>? Hs { get; set; }
                  
        [ForeignKey("LivretId")]
        public Livret? Livret { get; set; }

        [ForeignKey("CommandeId")]
        public Commande? Commande { get; set; }
    }
   
    public class Int
    {
        public int IntId { get; set; }
        public int Index { get; set; }
        public int It { get; set; }
                  
        [ForeignKey("LivretId")]
        public Livret? Livret { get; set; }
        
        [ForeignKey("CommandeId")]
        public Commande? Commande { get; set; }
    }

    public class Commande
    {
        public int CommandeId { get; set; }

        public DateTime Date { get; set; }
        public long Ref { get; set; }
        public bool Valide { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public Nullable<long> Phone { get; set; }
        [Required]
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? RgtsCompl { get; set; }
        public HashSet<Datetime>? Selected { get; set; }
        public List<Hashset>? Seasons { get; set; }
        public List<Int>? Prices { get; set; }
        public float Total { get; set; }               
    } 
}
