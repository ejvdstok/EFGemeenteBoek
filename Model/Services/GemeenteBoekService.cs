using Model.Entities;
using Model.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System;

namespace Model.Services
{
    public class GemeenteBoekService
    {
        private readonly GemeenteBoekContext context;

        // Constructor
        public GemeenteBoekService(GemeenteBoekContext context)
        {
            this.context = context;
        }

        public Persoon GetAccount(string naam, string paswoord)
        {
            Persoon persoon = context.Personen.Where(k => k.LoginNaam.ToUpper() == naam.ToUpper() && paswoord.Equals(k.LoginPaswoord)).FirstOrDefault();

            if (persoon is Profiel)
                persoon = context.Personen.Include("Adres")
                    .Include("Adres.Straat")
                    .Include("Adres.Straat.Gemeente")
                    .Include("Adres.Straat.Gemeente.Provincie")
                    .Include("Adres.Straat.Gemeente.HoofdGemeente")
                    .Include("ProfielInteresses")
                    .Include("ProfielInteresses.InteresseSoort")
                    .Where(k => k.LoginNaam.ToUpper() == naam.ToUpper() && paswoord.Equals(k.LoginPaswoord)).FirstOrDefault();
            else // Medewerker
                persoon = context.Personen.Include("Adres")
                    .Include("Adres.Straat")
                    .Include("Adres.Straat.Gemeente")
                    .Include("Adres.Straat.Gemeente.Provincie")
                    .Where(k => k.LoginNaam.ToUpper() == naam.ToUpper() && paswoord.Equals(k.LoginPaswoord)).FirstOrDefault();

            context.ChangeTracker.DetectChanges();

            if (persoon != null)
                if (paswoord.Equals(persoon.LoginPaswoord))
                {
                    persoon.LoginAantal++;
                    context.SaveChanges();
                    return persoon;
                }

            return null;
        }

        public Profiel GetProfielByName(string naam)
        {
            return context.Profielen.Where(k => k.LoginNaam.ToUpper() == naam.ToUpper()).FirstOrDefault();
        }

        public void GoedkeurenProfiel(Profiel profiel)
        {
            Profiel profiel1 = context.Profielen.Where(k => k.PersoonId == profiel.PersoonId).FirstOrDefault();
            profiel1.GoedgekeurdTijdstip = DateTime.Now;
            context.SaveChanges();
        }

        public void BlokkerenProfiel(Profiel profiel, bool blok)
        {
            Profiel profiel1 = context.Profielen.Where(k => k.PersoonId == profiel.PersoonId).FirstOrDefault();
            profiel1.Geblokkeerd = blok;
            context.SaveChanges();
        }

        public bool IsGoedgekeurdProfiel(Profiel profiel)
        {
            return profiel.GoedgekeurdTijdstip == null ? false : true;
        }

        public bool IsGeblokkeerdProfiel(Profiel profiel)
        {
            return profiel.Geblokkeerd;
        }

        public void VoegProfielToe(Profiel nieuwProfiel)
        {
            context.Profielen.Add(nieuwProfiel);
            context.SaveChanges();
        }

        public List<Taal> GetAllTalen()
        {
            return context.Talen.OrderBy(t => t.TaalNaam).ToList();
        }

        public List<Gemeente> GetFilteredGemeenten(string naam)
        {
            return context.Gemeenten.Include("Provincie").Include("Taal").Include("HoofdGemeente").Where(g => g.GemeenteNaam.Contains(naam)).ToList();
        }

        public List<Straat> GetFilteredStraten(Gemeente gemeente, string naam)
        {
            return context.Straten.Where(g => g.StraatNaam.Contains(naam) && g.Gemeente == gemeente).ToList();
        }

        public Adres GetAdres(Straat straat, string huisNummer, string busNummer)
        {
            return context.Adressen.Include("Straat").Where(g => g.Straat == straat && g.HuisNr == huisNummer && g.BusNr == busNummer).FirstOrDefault();
        }

        public List<InteresseSoort> GetAllInteresseSoorten()
        {
            return context.InteresseSoorten.OrderBy(t => t.InteresseSoortNaam).ToList();
        }

        public void VerwijderPersoon(Persoon persoon)
        {
            context.Personen.Remove(persoon);
            context.SaveChanges();
        }

        public void WijzigProfiel()
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.RepeatableRead
            };

            using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions);

            context.SaveChanges();

            transactionScope.Complete();
        }

        public bool LoginBestaat(string login)
        {
            return context.Personen.Where(g => g.LoginNaam == login).FirstOrDefault() != null;
        }

        ///Berichten
        ///

        public List<BerichtType> GetAllBerichtTypes()
        {
            return context.BerichtTypes.OrderBy(t => t.BerichtTypeNaam).ToList();
        }

        public void ToevoegenBericht(Bericht nieuwBericht)
        {
            context.ChangeTracker.DetectChanges();      
            context.Berichten.Add(nieuwBericht);
            context.ChangeTracker.DetectChanges();      
            context.SaveChanges();
            context.ChangeTracker.DetectChanges();      
        }

        public void WijzigenBericht(Bericht bericht)
        {
            context.SaveChanges();
        }

        public void VerwijderBericht(Bericht bericht)
        {
            context.Berichten.Remove(bericht);
            context.SaveChanges();
        }

        public List<Bericht> GetBerichtenGemeente(Gemeente gemeente)
        {
            var sqlString =
            @$"WITH tree AS
            (
                 SELECT     BerichtId
                            ,hoofdberichtid
                            ,berichttypeid
                            ,BerichtTitel
                            ,BerichtTekst
                            ,BerichtTijdstip
                            ,b.GemeenteId
                            ,b.PersoonId
							,p.LoginNaam
                            ,b.Aangepast
                            ,1 AS Level1
						    ,CAST(right('0000'+convert(varchar(4), BerichtId), 4) as varchar(1000)) Hierarchy
                            ,convert(varchar(900),'') as SortDT
						    ,CAST('+' as varchar(100)) AS abc
						    ,convert(varchar(900),'-') as x
                  FROM  Berichten b
			            join Gemeenten g on b.GemeenteId=g.GemeenteId
				        join Personen  p on b.PersoonId =p.PersoonId
                  where	HoofdBerichtId is null
				  and	((g.GemeenteId={gemeente.GemeenteId})	or  (HoofdGemeenteId={gemeente.GemeenteId})
				   or	(g.gemeenteid = (select HoofdGemeenteId from Gemeenten where GemeenteId={gemeente.GemeenteId})
				   or	HoofdGemeenteId = (select HoofdGemeenteId from Gemeenten where GemeenteId={gemeente.GemeenteId}) ))
 
                  UNION ALL
              
			      SELECT     b.BerichtId
                            ,b.hoofdberichtid
                            ,b.berichttypeid
                            ,b.BerichtTitel
                            ,b.BerichtTekst
                            ,b.BerichtTijdstip
                            ,b.GemeenteId
                            ,b.PersoonId
							,p.LoginNaam
                            ,b.Aangepast
                            ,t.Level1 + 1
						    ,CAST(Hierarchy + ':' + CAST(right('0000'+convert(varchar(4), b.BerichtID), 4) as varchar(100)) as varchar(1000)) Hierarchy
                            ,convert(varchar(900),left(Hierarchy, len(Hierarchy) - charindex('_', reverse(Hierarchy) + ':'))+':'+convert(varchar(30),b.BerichtTijdstip,21)) as SortDT
						    ,CAST(abc+'-' as varchar(100)) AS abc
						    ,convert(varchar(900),REPLICATE('-',t.Level1+1)+b.BerichtTekst) as x
                  FROM	Berichten b
                  inner join tree t on b.HoofdBerichtId = t.BerichtId
						join Personen  p on b.PersoonId =p.PersoonId
            )
 
            SELECT *, Level=Level1
            FROM tree t
            order by Hierarchy
            ";

            var berichten = context.Berichten.FromSqlRaw(sqlString).ToList();   

            foreach (var bericht in berichten)
            {
                bericht.Profiel = context.Profielen.Where(t => t.PersoonId == bericht.PersoonId).FirstOrDefault();
                bericht.BerichtType = context.BerichtTypes.Where(t => t.BerichtTypeId == bericht.BerichtTypeId).FirstOrDefault();
            }

            //return context.Berichten.Include("BerichtType").Include("HoofdBericht")
            //      .Where(k => k.Gemeente == gemeente).OrderByDescending(k => k.BerichtTijdstip).ToList();

            return berichten;
        }

    }
}
