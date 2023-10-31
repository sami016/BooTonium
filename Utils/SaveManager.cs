using Godot;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class SaveManager
{
	private static string FilePath = "./saves/data";

    public static SaveData SaveData { get; private set; } = new SaveData
    {
        Position = 0
    };


    public static async Task Load()
	{
		if (!File.Exists(FilePath))
		{
			await CreateFile();
		}
		SaveData = await ReadFile();
    }

	public static async Task Save()
	{
        try
        {
            var parent = Directory.GetParent(FilePath);
            Directory.CreateDirectory(parent.FullName);

            using (var file = File.OpenWrite(FilePath))
            using (var streamWriter = new StreamWriter(file))
            {
                var text = JsonSerializer.Serialize(SaveData);
                await streamWriter.WriteAsync(text);
                await streamWriter.FlushAsync();
                await file.FlushAsync();
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e.Message);
        }
    }

	private static async Task CreateFile()
	{
		try
        {
            var parent = Directory.GetParent(FilePath);
            Directory.CreateDirectory(parent.FullName);

            using (var file = File.Create(FilePath))
			using (var streamWriter = new StreamWriter(file))
			{
				var text = JsonSerializer.Serialize(SaveData);
				await streamWriter.WriteAsync(text);
				await streamWriter.FlushAsync();
				await file.FlushAsync();
			}
		} 
		catch (Exception e)
		{
			GD.PrintErr(e.Message);
		}
	}

	private static async Task<SaveData> ReadFile()
	{
        using (var file = File.OpenRead(FilePath))
        using (var streamReader = new StreamReader(file))
        {
			var text = await streamReader.ReadToEndAsync();
			return JsonSerializer.Deserialize<SaveData>(text);
        }
    }
}
