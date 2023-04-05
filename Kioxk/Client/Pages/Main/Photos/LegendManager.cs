namespace Kioxk.Client.Pages.Main.Photos;
    public class LegendManager
    {
        public Dictionary<int, string> Legends { get; set; }

        public LegendManager()
        {
            Legends = new Dictionary<int, string>
        {
            { 0, "Légende de la première photo." },
            { 1, "Légende de la seconde photo." },
            // Ajoutez d'autres légendes ici
        };
        }

        public string GetLegend(int entry)
        {
            if (Legends.ContainsKey(entry))
            {
                return Legends[entry];
            }
            return string.Empty;
        }
    }


