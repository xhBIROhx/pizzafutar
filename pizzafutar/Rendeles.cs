using System;
using System.Collections.Generic;
using System.Text;

namespace pizzafutar
{
    internal class Rendeles
    {
        int id;
        Pizza pizza;
        int db;
        Cim cim;
        TimeSpan ido;

        public Rendeles(int id, Pizza pizza, int db, Cim cim, TimeSpan ido)
        {
            this.id = id;
            this.pizza = pizza;
            this.db = db;
            this.cim = cim;
            this.ido = ido;
        }

        public int Id { get => id; }
        public int Db { get => db; set => db = value; }
        public TimeSpan Ido { get => ido; set => ido = value; }
        internal Pizza Pizza { get => pizza; set => pizza = value; }
        internal Cim Cim { get => cim; set => cim = value; }

        public override string ToString()
        {
            return $"Rendeles: {pizza.Nev}, {db} db, {cim.Nev}, {ido}";
        }
    }
}
