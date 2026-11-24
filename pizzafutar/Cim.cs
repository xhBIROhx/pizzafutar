using System;
using System.Collections.Generic;
using System.Text;

namespace pizzafutar
{
    internal class Cim
    {
        int id;
        string nev;
        string utca;
        string hsz;

        public Cim(int id, string nev, string utca, string hsz)
        {
            this.id = id;
            this.nev = nev;
            this.utca = utca;
            this.hsz = hsz;
        }

        public int Id { get => id; }
        public string Nev { get => nev; set => nev = value; }
        public string Utca { get => utca; set => utca = value; }
        public string Hsz { get => hsz; set => hsz = value; }

        public override string ToString()
        {
            return $"Cim: {nev}, {utca}, {hsz}";
        }
    }
}
