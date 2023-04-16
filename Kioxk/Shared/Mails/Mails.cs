using Kioxk.Shared.Models;

namespace Kioxk.Shared.Mails
{
    public readonly struct Mails
    {
        public readonly string mysub = "Votre réservation";
        public readonly string mybodymail;

        private readonly int CommandeId;    
        private readonly string FirstName;
        private readonly string LastName;
        private readonly Nullable<long> Phone;
        private readonly string Address;
        private readonly string Email;
        private readonly string RgtsCompl;
        private readonly HashSet<Datetime> Selected;
        private readonly float Total;

        public Mails(int comid, string fn, string ln, long? ph, string ad, string em, string rgc, HashSet<Datetime> sel, float tot)
        {
            CommandeId = comid;          
            FirstName = fn;
            LastName = ln;
            Phone = ph;
            Address = ad;
            Email = em;
            RgtsCompl = rgc;
            Selected = sel;                    
            Total = tot;

            mybodymail = @"
            <div style='background-color: #17202A; color:white; text-align:center'>
            <div style='color:black;background-color:white; font-weight:bold;'> Récapitulatif de votre Réservation </div>
            </br>
            <div style='font-size:1em'>
            <div>" + DateTime.Now.ToShortDateString() + @"
            <div id='numcomdeco'> Numéro de commande: " + CommandeId + "/" + DateTime.Now.Year + @"</div> 
            </br>
            <div style='font-size:inherit'> Prénom: " + FirstName + @"</div>
            <div style=''> Nom: " + LastName + @"</div>
            <div style='font-size:inherit'> Tel: 0" + Phone + @"</div>
            <div style='font-size:inherit'> Adresse: " + Address + @"</div>
            <div style='font-size:inherit'> em@il: " + Email + @"</div>
            <div style='font-size:inherit'> RgtsCompl: " + RgtsCompl + @"</div>
            </br>
            <div> Jours selectionnés: 
            ";

            var sil = new HashSet<DateTime>();
            foreach (var dt in Selected)                 // Convertis Selected en en objet 
            {
                sil.Add(dt.Dt);
            }
            var exdtsel = new DateTime();

            foreach (var sl in sil.OrderBy(key => key))             // Classe l'objet par date.
            {
                if (sl.AddDays(-1) != exdtsel)                      // Séparation entre les périodes selectionnées.
                {
                    mybodymail += "</br>";
                }
                exdtsel = sl;
                mybodymail += "</br>" + sl.ToShortDateString();
            }

            mybodymail += @"</div>   
            </br>
            </div>
            <div style='text-underline-offset:7px; text-decoration-line:underline; text-decoration-thickness:6px; '> Total: " + Total + @" €</div>
            </br>
            </div>
            ";
        }
    }

    public struct Paiement
    {
        public readonly string ajoutmail = @"

        <div> 
        <p>  
        Votre réservation est enregistrée et en attente de paiement. Pour procéder, utilisez votre application bancaire: allez à l'onglet paylib 
        ou lyf, et faites un virement du montant total au 0692104886.
        Vous recevrez alors un nouveau mail de confirmation. 
        Attention, à défaut, votre réservation sera annulée sous 24h.</p> 
        <p>Bien sûr, nous restons joignables au même numéro pour toute demande particulière 
        ou en cas de difficultées pour le paiement. Merci de votre intérêt.
        </p>
        </div>
        ";

        public Paiement()
        {
        }
    }
}