using System.Text.Json;

namespace AirportManagementSystem.Utilities
{
    public class GateDataLoader
    {
        public static List<Models.Gates> LoadGates(string filepath)
        {
            var json = File.ReadAllText(filepath);
            var gates = JsonSerializer.Deserialize<List<Models.Gates>>(json);
            return gates ?? new List<Models.Gates>();
        }

        public static void SaveGates(List<Models.Gates> gates, string filepath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(gates, options);
            File.WriteAllText(filepath, json);
        }
    }
}
