using Kioxk.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kioxk.Shared.Models;
using Microsoft.EntityFrameworkCore.Query;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using c = System.Console;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using SQLitePCL;

namespace Kioxk.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        public IIncludableQueryable<Commande, HashSet<Datetime>> comContext;
        public IIncludableQueryable<Livret, HashSet<Datetime>> livContext;
        public readonly AppDBContext _context;
        readonly string milog;
        readonly string mipass;
        Shared.Mails.Mails mails;
        readonly Shared.Mails.Paiement paiement = new();
        public CommandeController(AppDBContext context)
        {
            this._context = context;

            comContext = _context.Commandes!.Include(e => e.Seasons!).ThenInclude(e => e.Hs).Include(e => e.Prices).Include(e => e.Selected!);
            livContext = _context.Livret!.Include(e => e.Seasons!).ThenInclude(e => e.Hs).Include(e => e.Prices).Include(e => e.UnSelectable!);

            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            milog = configuration.GetValue<string>("micred:milog");
            mipass = configuration.GetValue<string>("micred:mipass");
        }

        [Route("[action]")]
        [HttpGet]
        public int GetQtePh() =>
        Directory.GetFiles(@"C:\Users\sierr\source\repos\Kioxk\Kioxk\Client\wwwroot\photo", "maison*.*").Length;

        [HttpGet]
        public ActionResult<Livret> Get()
        {
            if (livContext.Any())  
                return Ok(livContext.Single()); 
           
             return new StatusCodeResult(StatusCodes.Status204NoContent);      
        }

        public void AfficheLivret()
        {
            ManagementLivret();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Livret:");
            Console.ResetColor();

            foreach (var c in livContext)
            {                                                    // Affiche Unselectable et la Commande associée.
                Console.WriteLine("");
                foreach (var it in c.Prices!)                                                    // Affiche le tableau de prix.
                {
                    Console.WriteLine(" Price: " + it.It);
                }

                Console.WriteLine("");
                var j = 0;
                foreach (var Hs in c.Seasons!)                                                   // Affiche les Saisons.
                {
                    foreach (var dt in Hs.Hs!)
                    {
                        Console.WriteLine($" Seasons {j}: {dt.Dt.ToShortDateString()}");
                    }
                    j++;
                }
            }
        }

        void ManagementLivret()
        {
            if (_context.Livret!.Any()) c.WriteLine($"Livret ok! :{_context.Livret!.Count()}...");

            else
            {
                c.WriteLine("Il n'y a pas de livret...");
                var ndt = new Datetime() { Dt = new DateTime(2022, 12, 12) };   // Unselectable
                var ndt1 = new Datetime() { Dt = new DateTime(2022, 12, 13) };
                var ndt2 = new Datetime() { Dt = new DateTime(2022, 12, 14) };
                var ndt3 = new Datetime() { Dt = new DateTime(2022, 12, 15) };
                var ndt4 = new Datetime() { Dt = new DateTime(2022, 12, 16) };
                var ndt5 = new Datetime() { Dt = new DateTime(2022, 12, 17) };
                var ndt6 = new Datetime() { Dt = new DateTime(2022, 12, 18) };              

                var nhs = new HashSet<Datetime>
                {
                    ndt,
                    ndt1,
                    ndt2,
                    ndt3,
                    ndt4,
                    ndt5,
                    ndt6
                };

                if (comContext.Any())                                           // rajoute les anciennes commandes dans unselectable
                    foreach (var com in comContext)
                        if (com.Selected is not null)
                            foreach (var sel in com.Selected)
                                nhs.Add(sel);

                var ndts = new Datetime() { Dt = new DateTime(2000, 12, 26) };   // Saison 0
                var ndts1 = new Datetime() { Dt = new DateTime(2000, 12, 22) };
                var ndts2 = new Datetime() { Dt = new DateTime(2000, 12, 21) };
                var nhst = new HashSet<Datetime>
                {
                    ndts,
                    ndts1,
                    ndts2
                };

                var ndtsh = new Datetime() { Dt = new DateTime(2000, 10, 23) };  // saison 1
                var ndts1h = new Datetime() { Dt = new DateTime(2000, 10, 24) };
                var ndts2h = new Datetime() { Dt = new DateTime(2000, 10, 25) };
                var nhsth = new HashSet<Datetime>
                {
                    ndtsh,
                    ndts1h,
                    ndts2h
                };

                var nhstf = new List<Hashset> { new Hashset() { Hs = nhst }, new Hashset() { Hs = nhsth } };

                var nint = new Int() { It = 90 };
                var nint1 = new Int() { It = 120 };
                var nint2 = new Int() { It = 950 };
                var nintf = new List<Int> { nint, nint1, nint2 };

                _context.Livret!.Add(new() { UnSelectable = null, Seasons = nhstf, Prices = nintf });
                _context.SaveChanges();

                Console.WriteLine("livret done!");
                c.WriteLine("");
            }
        }
        public void AfficheCommandes()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Commandes:");
            Console.ResetColor();

            if (!_context.Commandes!.Any()) c.WriteLine("Il n'y a pas de Commandes...");
            else
            {
                foreach (var c in comContext.ToList())
                {
                    Console.WriteLine("");
                    Console.WriteLine("##################################################################################");
                    Console.WriteLine("");
                    if (c.Valide)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("**********************Validée**********************");
                        Console.ResetColor();
                    }
                    Console.WriteLine(" Id: " + c.CommandeId);
                    Console.WriteLine(" Date: " + c.Date);
                    Console.WriteLine(" Name: " + c.FirstName + " " + c.LastName);
                    Console.WriteLine(" Links: " + "0" + c.Phone + " " + c.Email);
                    Console.WriteLine(" Address: " + c.Address!.ToString().Replace("\n", " "));
                    if (c.RgtsCompl is not null)
                    {
                        Console.WriteLine(" Rgts: " + c.RgtsCompl.Replace("\n", " "));
                    }

                    var sel = new HashSet<DateTime>();
                    foreach (var dt in c.Selected!.ToList())                 // Convertis Selected en en objet 
                    {
                        sel.Add(dt.Dt);
                    }

                    var exdtsel = new DateTime();
                    Console.ForegroundColor = ConsoleColor.Green;
                    foreach (var sl in sel.OrderBy(key => key))             // Classe l'objet par date.
                    {
                        if (sl.AddDays(-1) != exdtsel)                      // Séparation entre les périodes selectionnées.
                        {
                            Console.WriteLine("");
                        }
                        exdtsel = sl;

                        Console.WriteLine(" Selected: " + sl.ToShortDateString());               // Affiche Selected
                    }
                    Console.ResetColor();
                    Console.WriteLine("");
                    int indexprice = 0;
                    foreach (var it in c.Prices!.ToList().OrderBy(key => key.Index))    // Classe le tableau de prix par ordre de la propriété index.
                    {
                        Console.WriteLine($" Price{indexprice}: " + it.It);         // Affiche le tableau de prix.
                        indexprice++;
                    }
                    Console.WriteLine("");

                    try
                    {
                        int j = 0;
                        List<List<DateTime>> seasConf = new() { new(), new() };

                        foreach (var Hs in c.Seasons!)             //  Pour chaque élément du tableau convertion en objet, l'ordonne et l'affiche.
                        {
                            foreach (var dt in Hs.Hs!)
                            {
                                seasConf[j].Add(dt.Dt);
                            }
                            j++;
                        }

                        int iter = 0;
                        foreach (var se0 in seasConf[0].OrderBy(key => key))
                        {
                            string stg = se0.ToShortDateString() + " ";
                            int iter2 = 0;
                            foreach (var se1 in seasConf[1].OrderBy(key => key))
                            {
                                if (iter2 == iter)
                                {
                                    stg += " Seasons 1: " + se1.ToShortDateString();
                                }
                                iter2++;
                            }
                            iter++;
                            Console.WriteLine(" Seasons 0: " + stg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        public void Valided(int value)
        {
            var arem = comContext.First(k => k.CommandeId == value);
            arem.Valide = !arem.Valide;
            _context.Update(arem);
            _context.SaveChanges();
        }

        public void Remove(int value)
        {
            try
            {
                var arem = comContext.First(k => k.CommandeId == value);
                foreach (var ar in arem.Selected!)
                {
                    _context.Remove(livContext.First().UnSelectable!.First(k => k.Dt == ar.Dt));
                    _context.SaveChanges();
                }
                foreach (var sl in arem.Selected)
                {
                    arem.Selected.Remove(sl);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ClearComs()
        {
            try
            {
                foreach (var com in comContext)
                {
                    foreach (var ar in com.Selected!)
                    {
                        _context.Remove(livContext.First().UnSelectable!.First(k => k.Dt == ar.Dt));
                        _context.SaveChanges();
                    }
                }

                _context.Commandes!.RemoveRange(_context.Commandes);
                _context.SaveChanges();
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ClearLivret()
        {
            try
            {
                _context.RemoveRange(livContext);
                _context.SaveChanges();
            }
            catch (Exception ex) { c.WriteLine(ex); }
        }

        public void ClearAll()
        {
            try
            {
                _context.RemoveRange(comContext);
                _context.RemoveRange(livContext);
                _context.SaveChanges();
            }
            catch (Exception ex) { c.WriteLine(ex); }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Commande com)
        {
            try
            {
                if (Verification(com))
                {
                    foreach (var ci in com.Selected!)                            // Transfert de la selection dans Unselectable du Livret.
                    {
                        livContext.Single().UnSelectable!.Add(ci);
                    }
                    com.Date = DateTime.Now;
                    _context.Commandes!.Add(com);                                // Ajoute la commande à la bd.
                    await _context.SaveChangesAsync();
                    Mail(com);
                    return Ok(com.CommandeId);                                   // Retourne l'identifiant de confirmation.
                }
            }
            catch (Exception ex) { c.WriteLine(ex); }
            return new BadRequestResult();
        }

        void Mail(Commande com)
        {
            try
            {
                mails = new(com.CommandeId, com.FirstName!, com.LastName!, com.Phone, com.Address!, com.Email!, com.RgtsCompl!, com.Selected!, com.Total);
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(milog));
                email.To.Add(MailboxAddress.Parse(com.Email));
                email.Subject = mails.mysub;
                email.Body = new TextPart(TextFormat.Html) { Text = paiement.ajoutmail + mails.mybodymail };
                using var smtp = new SmtpClient();
                smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(milog, mipass);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex) { c.WriteLine(ex); }
        }
        private bool Verification(Commande com)
        {
            if (!Embrouille(com))
            {
                return false;
            }

            var query = from sel in com.Selected
                        from li in livContext.Single().UnSelectable!
                        where li.Dt == sel.Dt
                        select li;
            if (query.Count() > 1)
            {
                foreach (var frit in query)
                {
                    Console.WriteLine("QUERy: " + frit.Dt.ToShortDateString());
                }
                return false;
            }
            else
                return true;
        }

        private bool Embrouille(Commande com)
        {
            List<HashSet<DateTime>>? seasons = null;
            if (livContext.Single().Seasons!.Any())
            {               
                seasons = new List<HashSet<DateTime>>();
                var i = 0;
                foreach (var Hs in livContext.Single().Seasons!)
                {
                    seasons.Add(new());
                    foreach (var dt in Hs.Hs!)
                    {
                        seasons[i].Add(dt.Dt);
                    }
                    i++;
                }
                foreach (var f in seasons[1].ToList())                                          // Rend les moyennes et hautes saisons annuelles.
                {
                    seasons[1].Add(new DateTime(DateTime.Now.Year, f.Month, f.Day));
                    seasons[1].Add(new DateTime(DateTime.Now.Year + 1, f.Month, f.Day));
                    seasons[1].Remove(f);
                }

                foreach (var f in seasons[0].ToList())
                {
                    seasons[0].Add(new DateTime(DateTime.Now.Year, f.Month, f.Day));
                    seasons[0].Add(new DateTime(DateTime.Now.Year + 1, f.Month, f.Day));
                    seasons[0].Remove(f);
                }
            }
          
            var prices = new List<int>();
            var j = 1;
            prices.Add(new());

            foreach (var it in livContext.Single().Prices!)
            {
                Console.WriteLine("priCe " + it.It);
                prices.Add(new());
                prices[j] = it.It;
                j++;
            }

            var selected = new HashSet<DateTime>();
            foreach (var it in com.Selected!)
            {
                selected.Add(it.Dt);
            }
            Console.WriteLine("price0 " + prices[0]);
            Console.WriteLine("price1 " + prices[1]);
            Console.WriteLine("price2 " + prices[2]);
            Console.WriteLine("price3 " + prices[3]);
            HashSet<DateTime>[]? seasAr = null;

            if (seasons is not null)
                seasAr = seasons.ToArray();

            var emb = new Client.Shared.Periodes() { Selected = selected, Seasons = seasAr, Prices = prices.ToArray() };
            emb.Extern();

            Console.WriteLine("Prices Com : " + com.Prices![0].It);
            Console.WriteLine("Prices Verifiee : " + emb.Prices[0]);

            if (com.Prices[0].It != emb.Prices[0])
            {
                return false;
            }
            return true;
        }
    }
}

