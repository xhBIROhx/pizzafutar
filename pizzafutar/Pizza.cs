using System;
using System.Collections.Generic;
using System.Text;

namespace pizzafutar
{
    internal class Pizza
    {
        int id;
        string nev;
        int meret;
        int ar;

        public Pizza(int id, string nev, int meret, int ar)
        {
            this.id = id;
            this.nev = nev;
            this.meret = meret;
            this.ar = ar;
        }

        public int Id { get => id; }
        public string Nev { get => nev; set => nev = value; }
        public int Meret { get => meret; set => meret = value; }
        public int Ar { get => ar; set => ar = value; }

        public override string ToString()
        {
            return $"Pizza: {nev}, {meret} cm, {ar} Ft";
        }
    }
}
