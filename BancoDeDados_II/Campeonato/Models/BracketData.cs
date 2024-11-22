namespace Campeonato.Models
{
    public class BracketData
    {
        public List<string[]> Teams { get; set; } = new();
        public List<List<int[]>> Results { get; set; } = new();
    }
}
