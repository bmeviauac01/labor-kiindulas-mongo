using MongoDB.Bson;
using MongoDB.Driver;
using MongoLabor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoLabor.DAL
{
    public class AdatvezRepository : IAdatvezRepository
    {
        private readonly IMongoCollection<Entities.Termek> termekCollection;
        private readonly IMongoCollection<Entities.Kategoria> kategoriaCollection;
        private readonly IMongoCollection<Entities.Vevo> vevoCollection;
        private readonly IMongoCollection<Entities.Megrendeles> megrendelesCollection;

        public AdatvezRepository(IMongoDatabase database)
        {
            this.termekCollection = database.GetCollection<Entities.Termek>("termekek");
            this.kategoriaCollection = database.GetCollection<Entities.Kategoria>("kategoriak");
            this.vevoCollection = database.GetCollection<Entities.Vevo>("vevok");
            this.megrendelesCollection = database.GetCollection<Entities.Megrendeles>("megrendelesek");
        }

        public IList<Termek> ListTermekek()
        {
            var dbTermekek = termekCollection
                .Find(_ => true)
                .ToList();

            return dbTermekek
                .Select(t => new Termek
                {
                    ID = t.ID.ToString(),
                    Nev = t.Nev,
                    NettoAr = t.NettoAr,
                    Raktarkeszlet = t.Raktarkeszlet
                })
                .ToList();
        }

        public Termek FindTermek(string id)
        {
            var dbTermek = termekCollection
                .Find(t => t.ID == ObjectId.Parse(id))
                .SingleOrDefault();

            if (dbTermek == null)
                return null;
            else
                return new Termek { ID = dbTermek.ID.ToString(), Nev = dbTermek.Nev, NettoAr = dbTermek.NettoAr, Raktarkeszlet = dbTermek.Raktarkeszlet };
        }

        public void InsertTermek(Termek termek)
        {
            var dbTermek = new Entities.Termek
            {
                Nev = termek.Nev,
                NettoAr = termek.NettoAr,
                Raktarkeszlet = termek.Raktarkeszlet,
                AFA = new Entities.AFA { Nev = "Általános", Kulcs = 20 },
                KategoriaID = ObjectId.Parse("5d7e42adcffa8e1b64f7dbbb"),
            };

            termekCollection.InsertOne(dbTermek);
        }

        public bool TermekElad(string id, int mennyiseg)
        {
            var result = termekCollection.UpdateOne(
                filter: t => t.ID == ObjectId.Parse(id) && t.Raktarkeszlet >= mennyiseg,
                update: Builders<Entities.Termek>.Update.Inc(t => t.Raktarkeszlet, -mennyiseg),
                options: new UpdateOptions { IsUpsert = false });

            return result.MatchedCount > 0;
        }

        public void DeleteTermek(string id)
        {
            termekCollection.DeleteOne(t => t.ID == ObjectId.Parse(id));
        }

        public IList<Kategoria> ListKategoriak()
        {
            var dbKategoriak = kategoriaCollection
                .Find(_ => true)
                .ToList();

            var termekDarabok = termekCollection
                .Aggregate()
                .Group(t => t.KategoriaID, g => new { KategoriaID = g.Key, TermekDarab = g.Count() })
                .ToList();

            return dbKategoriak
                .Select(k =>
                {
                    string szuloKategoriaNev = null;
                    if (k.SzuloKategoriaID.HasValue)
                        szuloKategoriaNev = dbKategoriak.Single(sz => sz.ID == k.SzuloKategoriaID.Value).Nev;

                    var termekDarab = termekDarabok.SingleOrDefault(td => td.KategoriaID == k.ID)?.TermekDarab ?? 0;

                    return new Kategoria { Nev = k.Nev, SzuloKategoriaNev = szuloKategoriaNev, TermekDarab = termekDarab };
                })
                .ToList();
        }

        public IList<Megrendeles> ListMegrendelesek(string keresettStatusz = null)
        {
            var filter = string.IsNullOrEmpty(keresettStatusz)
                ? Builders<Entities.Megrendeles>.Filter.Empty
                : Builders<Entities.Megrendeles>.Filter.Eq(m => m.Statusz, keresettStatusz);

            var dbMegrendelesek = megrendelesCollection
                .Find(filter)
                .Project(m => new { m.ID, m.Datum, m.Hatarido, m.Statusz, m.FizetesMod.Mod, OsszErtek = m.MegrendelesTetelek.Sum(mt => mt.Mennyiseg * mt.NettoAr) })
                .ToList();

            return dbMegrendelesek
                .Select(m => new Megrendeles { ID = m.ID.ToString(), Datum = m.Datum, Hatarido = m.Hatarido, Statusz = m.Statusz, FizetesMod = m.Mod, OsszErtek = m.OsszErtek })
                .ToList();
        }

        public Megrendeles FindMegrendeles(string id)
        {
            var dbMegrendeles = megrendelesCollection
                .Find(m => m.ID == ObjectId.Parse(id))
                .Project(m => new { m.ID, m.Datum, m.Hatarido, m.Statusz, m.FizetesMod.Mod, OsszErtek = m.MegrendelesTetelek.Sum(mt => mt.Mennyiseg * mt.NettoAr) })
                .SingleOrDefault();

            if (dbMegrendeles == null)
                return null;
            else
                return new Megrendeles { ID = dbMegrendeles.ID.ToString(), Datum = dbMegrendeles.Datum, Hatarido = dbMegrendeles.Hatarido, Statusz = dbMegrendeles.Statusz, FizetesMod = dbMegrendeles.Mod, OsszErtek = dbMegrendeles.OsszErtek };
        }

        public void InsertMegrendeles(Megrendeles megrendeles, Termek termek, int mennyiseg)
        {
            var dbMegrendeles = new Entities.Megrendeles
            {
                VevoID = ObjectId.Parse("5d7e42adcffa8e1b64f7dbb9"),
                TelephelyID = ObjectId.Parse("5d7e42adcffa8e1b64f7dbba"),
                Datum = megrendeles.Datum,
                Hatarido = megrendeles.Hatarido,
                Statusz = megrendeles.Statusz,
                FizetesMod = new Entities.FizetesMod { Mod = megrendeles.FizetesMod },
                MegrendelesTetelek = new Entities.MegrendelesTetel[]
                {
                    new Entities.MegrendelesTetel { TermekID = ObjectId.Parse(termek.ID), Mennyiseg = mennyiseg, NettoAr = termek.NettoAr, Statusz = megrendeles.Statusz },
                },
            };

            megrendelesCollection.InsertOne(dbMegrendeles);
        }

        public bool UpdateMegrendeles(Megrendeles megrendeles)
        {
            var update = Builders<Entities.Megrendeles>.Update.Combine(
                Builders<Entities.Megrendeles>.Update.Set(m => m.Datum, megrendeles.Datum),
                Builders<Entities.Megrendeles>.Update.Set(m => m.Hatarido, megrendeles.Hatarido),
                Builders<Entities.Megrendeles>.Update.Set(m => m.Statusz, megrendeles.Statusz),
                Builders<Entities.Megrendeles>.Update.Set(m => m.FizetesMod.Mod, megrendeles.FizetesMod));

            var result = megrendelesCollection.UpdateOne(
                filter: m => m.ID == ObjectId.Parse(megrendeles.ID),
                update: update,
                options: new UpdateOptions { IsUpsert = false });

            return result.MatchedCount > 0;
        }

        public void DeleteMegrendeles(string id)
        {
            megrendelesCollection.DeleteOne(m => m.ID == ObjectId.Parse(id));
        }

        public IList<Vevo> ListVevok()
        {
            var dbVevok = vevoCollection
                .Find(_ => true)
                .ToList();

            var osszMegrendelesek = megrendelesCollection
                .Aggregate()
                .Group(m => m.VevoID, group => new { VevoID = group.Key, OsszMegrendeles = group.Sum(m => m.MegrendelesTetelek.Sum(mt => mt.NettoAr * mt.Mennyiseg)) })
                .ToList();

            return dbVevok
                .Select(v =>
                {
                    var kozpontiTelephely = v.Telephelyek.Single(t => t.ID == v.KozpontiTelephelyID);
                    var osszMegrendeles = osszMegrendelesek.SingleOrDefault(om => om.VevoID == v.ID)?.OsszMegrendeles;

                    return new Vevo { Nev = v.Nev, IR = kozpontiTelephely.IR, Utca = kozpontiTelephely.Utca, Varos = kozpontiTelephely.Varos, OsszMegrendeles = osszMegrendeles };
                })
                .ToList();
        }

        public MegrendelesCsoportok MegrendelesCsoportosit(int csoportDarab)
        {
            var hatarertekek = megrendelesCollection
                .Aggregate()
                .Group(_ => (bool?)null, group => new { Min = group.Min(m => m.Datum.Value), Max = group.Max(m => m.Datum.Value) })
                .Single();
            var max = hatarertekek.Max.AddHours(1);

            var intervallum = (max - hatarertekek.Min) / csoportDarab;
            var hatarok = new List<DateTime>(capacity: csoportDarab + 1);
            for (int i = 0; i < csoportDarab; i++)
                hatarok.Add(hatarertekek.Min + intervallum * i);
            hatarok.Add(max);

            var dbCsoportok = megrendelesCollection
                .Aggregate()
                .Bucket(
                    groupBy: m => m.Datum.Value,
                    boundaries: hatarok,
                    output: group => new { id = group.Key, darab = group.Count(), osszErtek = group.Sum(m => m.MegrendelesTetelek.Sum(mt => mt.NettoAr * mt.Mennyiseg)) })
                .ToList();

            return new MegrendelesCsoportok
            {
                Hatarok = hatarok,
                Csoportok = dbCsoportok.Select(cs => new MegrendelesCsoport { Datum = cs.id, Darab = cs.darab, OsszErtek = cs.osszErtek }).ToList(),
            };
        }
    }
}
