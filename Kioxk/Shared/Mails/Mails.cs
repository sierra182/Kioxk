using Kioxk.Shared.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;

namespace Kioxk.Shared.Mails
{
  
    public readonly struct Mails
    {

        public readonly string mysub = "Votre réservation";
        public readonly string mybodymail;

        readonly int CommandeId { get; init; }
        readonly long Ref { get; init; }
        readonly string FirstName { get; init; }
        readonly string LastName { get; init; }
        readonly Nullable<long> Phone { get; init; }
        readonly string Address { get; init; }
        readonly string Email { get; init; }
        readonly string RgtsCompl { get; init; }
        readonly HashSet<Datetime> Selected { get; init; }
        readonly List<Hashset> Seasons { get; init; }
        readonly List<Int> Prices { get; init; }
        readonly float Total { get; init; }


        public Mails(int comid, long @refe, string fn, string ln, long? ph, string ad, string em, string rgc, HashSet<Datetime> sel, List<Hashset> seas, List<Int> prx, float tot)
        {
            CommandeId = comid;
            Ref = @refe;
            FirstName = fn;
            LastName = ln;
            Phone = ph;
            Address = ad;
            Email = em;
            RgtsCompl = rgc;
            Selected = sel;
            Seasons = seas;
            Prices = prx;
            Total = tot;

            mybodymail = @"
<div style='background-color: #17202A; color:white; text-align:center'>

            <div style='color:black;background-color:white;font-size:1.2rem; font-weight:bold;'> Récapitulatif de votre Réservation </div>
            </br>

     <div style='font-size:1rem;'>
<div>" + DateTime.Now.ToShortDateString() +@"
            <div style='font-size:1rem;text-decoration-style:wavy;text-underline-offset:.1rem;text-decoration-line:underline;  text-decoration-thickness:.15rem'> Numéro de commande: " + comid + "/" + DateTime.Now.Year + @"</div> 
            </br>
            <div style='font-size:inherit'> Prénom: " + fn + @"</div>
            <div style=''> Nom: " + ln + @"</div>
            <div style='font-size:inherit'> Tel: 0" + ph + @"</div>
            <div style='font-size:inherit'> Adresse: " + ad + @"</div>
            <div style='font-size:inherit'> em@il: " + em + @"</div>
            <div style='font-size:inherit'> RgtsCompl: " + rgc + @"</div>
            </br>
            <div> Jours selectionnés: ";

            var sil = new HashSet<DateTime>();
            foreach (var dt in sel)                 // Convertis Selected en en objet 
            {
                sil.Add(dt.dt);
            }
            var exdtsel = new DateTime();

            foreach (var sl in sil.OrderBy(key => key))             // Classe l'objet par date.
            {
                if (sl.AddDays(-1) != exdtsel)                      // Séparation entre les périodes selectionnées.
                {
                    mybodymail += "</br>";
                    // Console.WriteLine("");
                }
                exdtsel = sl;
                mybodymail += "</br>" + sl.ToShortDateString();
                // Console.WriteLine(" Selected: " + sl.ToShortDateString());               // Affiche Selected
            }

            mybodymail += @"</div>   
            </br>
</div>
            <div style='text-underline-offset:7px; text-decoration-line:underline; text-decoration-thickness:6px; '> Total: " + tot + @" €</div>
 </br>
</div>

";
        }

    }

    public struct Paiement 
    {
        public readonly string ajoutmail = @"

<div style='height:auto; border:solid; border-top-left-radius:2vw;color:white;'> 
  <p style=""border:solid;border-top-left-radius:2vw;border-top-right-radius:2vw;padding:4px;background-color:white;color:green; font-weight:normal;text-indent: 30px;"">  
    Votre réservation est enregistrée et en attente de paiement. Pour procéder, utilisez votre application bancaire: allez à l'onglet paylib 
ou lyf, et faites un virement du montant total au 0692104886.
Vous recevrez alors un nouveau mail de confirmation. 
    Attention, à défaut, votre réservation sera annulée sous 24h.</p> 
    <p style=""padding:2px;"">Bien sûr, nous restons joignables au même numéro pour toute demande particulière 
ou en cas de difficultées pour le paiement. Merci de votre intérêt.
</p>
</div>

";

        public Paiement()
        {
        }
    }
}