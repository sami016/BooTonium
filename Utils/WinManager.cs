using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class WinManager
{
    public static async Task LevelWasWon(int index)
    {
        if (index == SaveManager.SaveData.Position)
        {
            SaveManager.SaveData.Position++;
            await SaveManager.Save();
        }
    }
}
