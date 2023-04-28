namespace Kioxk.Client.Pages.Main.Photos;
public class LegendManager
{
    public Dictionary<int, string> Legends { get; set; }

    public LegendManager()
    {
        Legends = new Dictionary<int, string>
        {
            { 0, "Nuit étoilée" },
            { 1, "Pièce à vivre" },
            { 2, "Mezzanine" },
            { 3, "Coin Glandouille" },
            { 4, "Chambre" },
            { 5, "Salle de bain" },
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


