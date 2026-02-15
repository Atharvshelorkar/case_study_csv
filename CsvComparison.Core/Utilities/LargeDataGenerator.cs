namespace CsvComparison.Core.Utilities
{
    public class LargeDataGenerator
    {
        public void Generate(string path, int count)
        {
            var directory = Path.GetDirectoryName(path);

            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using var writer = new StreamWriter(path);

            writer.WriteLine("AccountNumber,Name,SCOPE,Amount,Status");

            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(
                    $"{i:D6},User{i},{i * 1.234567},{i * 10},Active");
            }
        }
    }
}

