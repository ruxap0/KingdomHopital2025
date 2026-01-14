namespace KindomHospital.Infrastructure.Seeders.Helpers
{
    public static class CsvReader
    {
        public static async Task<List<string[]>> ReadCsv(string filePath, char delimiter = ';')
        {
            var lines = new List<string[]>();
            using var reader = new StreamReader(filePath);
            string? line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                var values = line.Split(delimiter);
                lines.Add(values);
            }
            return lines;
        }
    }

}

