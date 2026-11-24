namespace pizzafutar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Adatbazis db = new();
            List<Rendeles> rendelesek = db.Feltolt();
            Feladatok f = new(rendelesek, db.Pizzak);

            f.Olcso();
            f.Nepszeru();
            f.Esti();
            f.Jolfogy();
            f.Reklam();
            f.Osszeg();
        }
    }
}
