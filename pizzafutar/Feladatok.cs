using System;
using System.Collections.Generic;
using System.Linq;

namespace pizzafutar
{
    internal class Feladatok
    {
        readonly List<Rendeles> rendelesek;
        readonly List<Pizza> pizzak;

        public Feladatok(List<Rendeles> rendelesek, List<Pizza> pizzak)
        {
            this.rendelesek = rendelesek;
            this.pizzak = pizzak;
        }

        public void Olcso()
        {
            Console.WriteLine("3. 1500 Ft-nal olcsobb pizzak (3olcso)");
            var result = pizzak
                .Where(p => p.Ar < 1500)
                .OrderByDescending(p => p.Meret)
                .ThenBy(p => p.Nev);
            foreach (Pizza p in result)
                Console.WriteLine(p);
            Console.WriteLine();
        }

        public void Nepszeru()
        {
            Console.WriteLine("4. A legtobbet rendelt pizza (4nepszeru)");
            var result = rendelesek
                .GroupBy(r => r.Pizza.Id)
                .Select(g => new { Pizza = g.First().Pizza, Darab = g.Sum(r => r.Db) })
                .OrderByDescending(x => x.Darab)
                .First();
            Console.WriteLine($"{result.Pizza.Nev}, {result.Pizza.Meret} cm - {result.Darab} db");
            Console.WriteLine();
        }

        public void Esti()
        {
            Console.WriteLine("5. Rendelt darabszam 18-19 ora kozott (5esti)");
            var result = rendelesek
                .Where(r => r.Ido >= new TimeSpan(18, 0, 0) && r.Ido <= new TimeSpan(19, 0, 0))
                .GroupBy(r => new { r.Pizza.Nev, r.Pizza.Meret })
                .Select(g => new { g.Key.Nev, g.Key.Meret, Darab = g.Sum(r => r.Db) })
                .OrderBy(x => x.Nev)
                .ThenBy(x => x.Meret);
            foreach (var x in result)
                Console.WriteLine($"{x.Nev}, {x.Meret} cm - {x.Darab} db");
            Console.WriteLine();
        }

        public void Jolfogy()
        {
            Console.WriteLine("6. Mindharom meretben rendelt pizzak (6jolfogy)");
            var result = rendelesek
                .Select(r => new { r.Pizza.Id, r.Pizza.Nev, r.Pizza.Meret })
                .Distinct()
                .GroupBy(x => x.Nev)
                .Where(g => g.Select(x => x.Meret).Distinct().Count() == 3)
                .Select(g => g.Key);
            foreach (string nev in result)
                Console.WriteLine(nev);
            Console.WriteLine();
        }

        public void Reklam()
        {
            Console.WriteLine("7. Uditoitalat kapo rendelok (7reklam)");
            var result = Szallitasok()
                .Where(x => x.Db > 3)
                .OrderBy(x => x.Ido);
            foreach (var x in result)
                Console.WriteLine($"{x.Ido}, {x.Nev}, {x.Utca}, {x.Hsz}");
            Console.WriteLine();
        }

        public void Osszeg()
        {
            Console.WriteLine("8. Fizetendo osszeg szallitasi idopontonkent (8osszeg)");
            var result = Szallitasok()
                .OrderBy(x => x.Ido)
                .ThenBy(x => x.Nev);
            foreach (var x in result)
                Console.WriteLine($"{x.Ido}, {x.Nev}, {x.Utca}, {x.Hsz} - {x.Osszeg} Ft");
            Console.WriteLine();
        }

        private record Csoportositas(TimeSpan Ido, string Nev, string Utca, string Hsz, int Db, int Osszeg);

        private IEnumerable<Csoportositas> Szallitasok()
        {
            return rendelesek
                .GroupBy(r => new { r.Ido, CimId = r.Cim.Id })
                .Select(g => new Csoportositas(
                    g.Key.Ido,
                    g.First().Cim.Nev,
                    g.First().Cim.Utca,
                    g.First().Cim.Hsz,
                    g.Sum(r => r.Db),
                    g.Sum(r => r.Db * r.Pizza.Ar)));
        }
    }
}
