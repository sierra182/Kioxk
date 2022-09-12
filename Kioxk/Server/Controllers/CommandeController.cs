using Kioxk.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kioxk.Shared.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Net.Imap;
using System.Web;
using MailKit;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using System.Net;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Pkcs;
//using iTextSharp.text.pdf.draw;
//using SQLitePCL;
//using static iTextSharp.text.pdf.events.IndexEvents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Metrics;

using System.Data.SqlTypes;
namespace Kioxk.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {

        public IIncludableQueryable<Commande, HashSet<Datetime>> comContext;
        public IIncludableQueryable<Livret, HashSet<Datetime>> livContext;

        public readonly AppDBContext _context;

        string milog;
        string mipass;
        Shared.Mails.Mails mails;
        Shared.Mails.Paiement paiement = new();
        public CommandeController(AppDBContext context)
        {
            this._context = context;
            comContext = _context.Commandes.Include(e => e.Seasons).ThenInclude(e => e.hs).Include(e => e.Prices).Include(e => e.Selected);
            livContext = _context.Livret.Include(e => e.Seasons).ThenInclude(e => e.hs).Include(e => e.Prices).Include(e => e.UnSelectable);

            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            milog = configuration.GetValue<string>("micred:milog");
            mipass = configuration.GetValue<string>("micred:mipass");
            // _context.Database.ExecuteSqlRaw("ALTER TABLE Commandes AUTO_INCREMENT = 1");

        }

        //static bool t;
        //public void toggle()
        //{

        //    if (!t)
        //    {

        //        t1();

        //        t = !t;

        //    }
        //    else
        //    {

        //        t2();

        //        t = !t;

        //    }
        //}

        void t1()
        {
            Console.WriteLine("testt008)");
            if (_context.Livret is null)
            { Console.WriteLine("testtnuuuule)"); }
            if (_context.Livret is not null)
            {
                Console.WriteLine("testtNOnuuuule) ");
                // Console.WriteLine(_context.Livret.Count());
                Console.WriteLine("testtNOnuuuulefin");
            }
            if (_context.Livret.Count() < 1)

            {
                Console.WriteLine("testt1)");
                var ndt = new Datetime() { dt = new DateTime(2021, 11, 15) };   // Unselectable
                var ndt1 = new Datetime() { dt = new DateTime(2021, 11, 16) };
                var ndt2 = new Datetime() { dt = new DateTime(2021, 11, 17) };

                var ndt3 = new Datetime() { dt = new DateTime(2021, 12, 15) };
                var ndt4 = new Datetime() { dt = new DateTime(2021, 12, 16) };
                var ndt5 = new Datetime() { dt = new DateTime(2021, 12, 17) };

                var nhs = new HashSet<Datetime>();
                nhs.Add(ndt);
                nhs.Add(ndt1);
                nhs.Add(ndt2);
                nhs.Add(ndt3);
                nhs.Add(ndt4);
                nhs.Add(ndt5);

                foreach (var cc in comContext.ToList())
                {
                    foreach (var cu in cc.Selected.ToList())
                    {
                        nhs.Add(cu);
                    }

                }

                var ndts = new Datetime() { dt = new DateTime(2000, 12, 19) };   // Saison 0
                var ndts1 = new Datetime() { dt = new DateTime(2000, 12, 20) };
                var ndts2 = new Datetime() { dt = new DateTime(2000, 12, 21) };
                var nhst = new HashSet<Datetime>();
                nhst.Add(ndts);
                nhst.Add(ndts1);
                nhst.Add(ndts2);

                var ndtsh = new Datetime() { dt = new DateTime(2000, 10, 23) };  // saison 1
                var ndts1h = new Datetime() { dt = new DateTime(2000, 10, 24) };
                var ndts2h = new Datetime() { dt = new DateTime(2000, 10, 25) };
                var nhsth = new HashSet<Datetime>();
                nhsth.Add(ndtsh);
                nhsth.Add(ndts1h);
                nhsth.Add(ndts2h);

                var nhstf = new List<Hashset> { new Hashset() { hs = nhst }, new Hashset() { hs = nhsth } };

                var nint = new Int() { it = 100 };
                var nint1 = new Int() { it = 120 };
                var nint2 = new Int() { it = 150 };
                var nintf = new List<Int> { nint, nint1, nint2 };

                _context.Livret.Add(new() { UnSelectable = nhs, Seasons = nhstf, Prices = nintf });
                _context.SaveChangesAsync();
            }
        }
        //void t2()
        //{
        //    if (_context.Livret.Count() < 1)
        //    {
        //        var ndt = new Datetime() { dt = new DateTime(2021, 11, 12) };
        //        var ndt1 = new Datetime() { dt = new DateTime(2021, 11, 13) };
        //        var ndt2 = new Datetime() { dt = new DateTime(2021, 11, 14) };
        //        var nhs = new HashSet<Datetime>();
        //        nhs.Add(ndt);
        //        nhs.Add(ndt1);
        //        nhs.Add(ndt2);

        //        var ndts = new Datetime() { dt = new DateTime(2000, 11, 16) };
        //        var ndts1 = new Datetime() { dt = new DateTime(2000, 11, 17) };
        //        var ndts2 = new Datetime() { dt = new DateTime(2000, 11, 18) };
        //        var nhst = new HashSet<Datetime>();
        //        nhst.Add(ndts);
        //        nhst.Add(ndts1);
        //        nhst.Add(ndts2);


        //        var ndtsh = new Datetime() { dt = new DateTime(2000, 11, 20) };
        //        var ndts1h = new Datetime() { dt = new DateTime(2000, 11, 21) };
        //        var ndts2h = new Datetime() { dt = new DateTime(2000, 11, 22) };
        //        var nhsth = new HashSet<Datetime>();
        //        nhsth.Add(ndtsh);
        //        nhsth.Add(ndts1h);
        //        nhsth.Add(ndts2h);

        //        var nhstf = new List<Hashset> { new Hashset() { hs = nhst }, new Hashset() { hs = nhsth } };

        //        var nint = new Int() { it = 300 };
        //        var nint1 = new Int() { it = 320 };
        //        var nint2 = new Int() { it = 340 };
        //        var nintf = new List<Int> { nint, nint1, nint2 };

        //        _context.Livret.Add(new() { UnSelectable = nhs, Seasons = nhstf, Prices = nintf });
        //        _ = _context.SaveChangesAsync();
        //    }
        //}
        void t3()
        {
            try
            {
                //Console.WriteLine(" All:");
                //foreach (var cd in _context.Datetimes)
                //{
                //    Console.WriteLine(" All: " + cd);
                //}
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }


        }
        public void AfficheLivret()
        {
            // t3();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Livret:");
            Console.ResetColor();

            //comContext.ToList();                                  // Avant de retrouver la commande parente.
            if (livContext.Count() > 0)
            {
                foreach (var c in livContext)
                {                                                     // Affiche Unselectable et la Commande associée.
                    var unsel = new HashSet<DateTime>();              // Convertis Unselectable en objets.
                    foreach (var dt in c.UnSelectable.ToList())
                    {
                        unsel.Add(dt.dt);
                    }

                    var exAssosCom = new Commande();
                    var bl = true;
                    var exdt = new DateTime();
                    var exdtass = new DateTime();
                    //foreach (var dt in unsel.OrderBy(key => key))                               // Ordonne par date.
                    //{
                    //    Commande associatedCom = (from Datetimeitem in c.UnSelectable.ToList()  // Cherche la commande assosiée pour chaque datetime.
                    //                              where Datetimeitem.dt == dt
                    //                              select Datetimeitem.Commande).Single();
                    //    if (associatedCom is not null)                                          // Le Dt correspond a une commande.
                    //    {
                    //        if (associatedCom != exAssosCom)                                    // Séparation entre les commandes différentes.
                    //        {
                    //            Console.WriteLine("");
                    //        }
                    //        else if (!bl)                                                       // Séparation aprés un dt sans commande liée.
                    //        {
                    //            Console.WriteLine("");
                    //        }
                    //        else if (dt.AddDays(-1) != exdtass)                                 // Séparation entre les périodes d'une même commande.
                    //        {
                    //            Console.WriteLine("");
                    //        }
                    //        exdtass = dt;
                    //        bl = true;
                    //        exAssosCom = associatedCom;
                    //       // Console.WriteLine(" UnSelectable: " + dt.ToShortDateString() + " " + associatedCom.FirstName);      // Affiche la commande associée.
                    //    }
                    //    else                                                                     // Le Dt n'est pas lié à une commande.
                    //    {
                    //        if (bl)                                                              // Séparation pour Le premier sans commande liée.
                    //        {
                    //         //   Console.WriteLine("");
                    //            bl = false;
                    //        }
                    //        else if (dt.AddDays(-1) != exdt)                                     // Séparation entre les périodes sans commandes liées.
                    //        {
                    //          //  Console.WriteLine("");
                    //        }
                    //        exdt = dt;
                    //       // Console.WriteLine(" UnSelectable sans com: " + dt.ToShortDateString());
                    //    }
                    //}

                    Console.WriteLine("");
                    foreach (var it in c.Prices)                                                    // Affiche le tableau de prix.
                    {
                        Console.WriteLine(" Price: " + it.it);
                    }

                    Console.WriteLine("");
                    var j = 0;
                    foreach (var Hs in c.Seasons)                                                   // Affiche les Saisons.
                    {
                        foreach (var dt in Hs.hs)
                        {
                            Console.WriteLine($" Seasons {j}: {dt.dt.ToShortDateString()}");
                        }
                        j++;
                    }
                }

            }
            else { Console.WriteLine(" aucun livret"); }
        }

        public void AfficheCommandes()
        {
            //_context.Database.ExecuteSqlRaw("Create table [IF NOT EXISTS] Commandes");
            //_context.SaveChanges();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Commandes:");
            Console.ResetColor();
            if (comContext is not null && comContext.Count() > 0)
            {
                //Console.WriteLine("COUNT...");
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
                    //if (c.Date != null)
                    //{

                    Console.WriteLine(" Date: " + c.Date);
                    Console.WriteLine(" Name: " + c.FirstName + " " + c.LastName);
                    Console.WriteLine(" Links: " + "0" + c.Phone + " " + c.Email);
                    Console.WriteLine(" Address: " + c.Address.ToString().Replace("\n", " "));
                    if (c.RgtsCompl is not null)
                    {
                        Console.WriteLine(" Rgts: " + c.RgtsCompl.Replace("\n", " "));
                    }
                    //.Replace(LineSeparator, string.Empty)
                    //.Replace(paragraphSeparator, string.Empty));

                    var sel = new HashSet<DateTime>();
                    foreach (var dt in c.Selected.ToList())                 // Convertis Selected en en objet 
                    {
                        sel.Add(dt.dt);
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
                    foreach (var it in c.Prices.ToList().OrderBy(key => key.index))    // Classe le tableau de prix par ordre de la propriété index.
                    {
                        Console.WriteLine($" Price{indexprice}: " + it.it);         // Affiche le tableau de prix.
                        indexprice++;
                    }
                    Console.WriteLine("");

                    try
                    {
                        int j = 0;
                        List<List<DateTime>> seasConf = new() { new(), new() };

                        foreach (var Hs in c.Seasons)             //  Pour chaque élément du tableau convertion en objet, l'ordonne et l'affiche.
                        {
                            foreach (var dt in Hs.hs)
                            {
                                seasConf[j].Add(dt.dt);
                            }
                            //  var seasConfO[j] = seasConf[j].OrderBy(key => key);
                            j++;
                        }

                        //int k = 0;
                        //if (seasConf[0].Count == seasConf[1].Count)
                        //{
                        //for (int k = 0; k < seasConf[0].Count ;k++)
                        //{
                        //var seaconforder0 = seasConf[0].OrderBy(key => key);
                        //  var seaconforder1 = seasConf[1].OrderBy(key => key);

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



                        //else
                        //{
                        //    for (int k = 0; k < seasConf[1].Count; k++)
                        //    {
                        //        string stg = seasConf[0][k].ToShortDateString() + " ";
                        //        if (seasConf[1].Count > k)
                        //        {
                        //            stg += " Seasons 1: " + seasConf[1][k].ToShortDateString();
                        //        }
                        //        Console.WriteLine(" Seasons 0: " + stg);
                        //    }
                        //}

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }

                }

            }
            else { Console.WriteLine(" pas de commandes..."); }
        }
        public void ClearAll()
        {
            // Console.WriteLine("ClearAll all f1");
            _context.RemoveRange(comContext);
            _context.RemoveRange(livContext);

            _context.SaveChanges();
            //  Console.WriteLine("ClearAll all f");
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
                // comContext.Attach(employer);
                // comContext.Employ.Remove(employer);
                // ctx.SaveChanges();
                //  Console.WriteLine("Remove 2");
                var arem = comContext.First(k => k.CommandeId == value);
                // _context.Livret.
                foreach (var ar in arem.Selected)
                {
                    _context.Remove(livContext.First().UnSelectable.First(k => k.dt == ar.dt));
                    _context.SaveChanges();
                }
                foreach (var sl in arem.Selected)
                {
                    arem.Selected.Remove(sl);
                    _context.SaveChanges();
                }
                //var arem2 = livContext.First().UnSelectable. (k => k.dt == arem.Selected.);
                //arem2.UnSelectable
                //  _context.Remove(arem2);
                //_context.Remove(arem);

                //     Console.WriteLine("Remove 3");
                //   _context.SaveChanges();
                //     Console.WriteLine("Remove 4");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

        }
        public void ClearLivret()
        {
            //   Console.WriteLine("Clearlivret 1");
            _context.Livret.RemoveRange(_context.Livret);
            _context.SaveChanges();
            //   Console.WriteLine("Clearlivret 2");
        }

        public void ClearComs()
        {
            //  var arem = comContext.First(k => k.CommandeId == value);
            // _context.Livret.
            try
            {
                foreach (var com in comContext)
                {
                    foreach (var ar in com.Selected)
                    {
                        _context.Remove(livContext.First().UnSelectable.First(k => k.dt == ar.dt));
                        _context.SaveChanges();
                    }

                }
                //  Console.WriteLine("LIIIIII");

                _context.Commandes.RemoveRange(_context.Commandes);
                _context.SaveChanges();
                //  _context.Database.ExecuteSqlRaw("ALTER TABLE Commandes AUTO_INCREMENT = 0");
                //  _context.SaveChanges();
                // _context.Database.ExecuteSqlRaw("Create table Commandes");

                //_context.Remove(comContext.);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

        }
        [HttpGet]
        public async Task<Livret> Get()
        {
            try
            {
                //Mail();

            }
            catch (Exception)
            {

                throw;
            }

            t1();
            return livContext.Single();
        }

        void Mail(Commande com)
        {
                   
            mails = new(com.CommandeId, com.Ref, com.FirstName, com.LastName, com.Phone, com.Address, com.Email, com.RgtsCompl, com.Selected, com.Seasons, com.Prices, com.Total);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(milog));
            email.To.Add(MailboxAddress.Parse(com.Email));
            email.Subject = mails.mysub;
            email.Body = new TextPart(TextFormat.Html) { Text = paiement.ajoutmail + mails.mybodymail };

            //using var smtp = new SmtpClient();
            //smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate(milog, mipass);

            //smtp.Send(email);
            //smtp.Disconnect(true);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Commande com)
        {
            //if (com.Selected.Count > 1)
            //{
            if (Verification(com))
            {
                foreach (var ci in com.Selected)                            // Transfert de la selection dans Unselectable du Livret.
                {
                    livContext.Single().UnSelectable.Add(ci);
                }
                com.Date = DateTime.Now;
                _context.Commandes.Add(com);                                // Ajoute la commande à la bd.
                await _context.SaveChangesAsync();
                Mail(com);
                return Ok(com.CommandeId);

            }

            // Retourne l'identifiant de confirmation.

            return new BadRequestResult();
        }

        private bool Verification(Commande com)
        {
            if (!Embrouille(com))
            {
                return false;
            }

            Console.WriteLine("veriFICATION");
            var query = from sel in com.Selected
                        from li in livContext.Single().UnSelectable
                        where li.dt == sel.dt
                        select li;
            if (query.Count() > 1)
            {
                foreach (var frit in query)
                {
                    Console.WriteLine("QUERy: " + frit.dt.ToShortDateString());
                }
                return false;
            }
            else
                return true;
        }
        private bool Embrouille(Commande com)
        {
            Console.WriteLine("embrouille");
            var seasons = new List<HashSet<DateTime>>();
            var i = 0;
            foreach (var Hs in livContext.Single().Seasons)
            {
                seasons.Add(new());
                foreach (var dt in Hs.hs)
                {
                    seasons[i].Add(dt.dt);

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
            var prices = new List<int>();
            var j = 1;
            prices.Add(new());
            foreach (var it in livContext.Single().Prices)
            {
                Console.WriteLine("priCe " + it.it);
                prices.Add(new());
                prices[j] = it.it;
                j++;
            }

            var selected = new HashSet<DateTime>();
            foreach (var it in com.Selected)
            {
                selected.Add(it.dt);
            }
            Console.WriteLine("price0 " + prices[0]);
            Console.WriteLine("price1 " + prices[1]);
            Console.WriteLine("price2 " + prices[2]);
            Console.WriteLine("price3 " + prices[3]);
            var emb = new Client.Shared.Periodes() { Selected = selected, Seasons = seasons.ToArray(), Prices = prices.ToArray() };
            emb.Extern();

            foreach (var myse in seasons[0])
            {
                Console.WriteLine("Prices SEAS 0 : " + myse.Date);
            }
            foreach (var myse in seasons[1])
            {
                Console.WriteLine("Prices SEAS 1 : " + myse.Date);
            }
            Console.WriteLine("Prices Com : " + com.Prices[0].it);
            Console.WriteLine("Prices Verifiee : " + emb.Prices[0]);

            if (com.Prices[0].it != emb.Prices[0])
            {
                return false;
            }
            return true;

        }
    }
}

