using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace CheckersGame
{
    internal class Debagger
    {
        public static void SetIcon(IntPtr windowHandle, string iconPath)
        {
            if (File.Exists(iconPath))
            {
                WindowIconHelper.SetWindowIcon(windowHandle, iconPath);
            }
            else if (File.Exists($"Content/{iconPath}"))
            {
                WindowIconHelper.SetWindowIcon(windowHandle, $"Content/{iconPath}");
            }
            else
            {
                Console.WriteLine($"Иконка не найдена в путях: {iconPath} и Content/{iconPath}");
            }
        }

        public static Texture2D LoadTexture(ContentManager content, string textureName)
        {
            try
            {
                return content.Load<Texture2D>(textureName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке текстуры '{textureName}': {ex.Message}");
                throw; // Пробрасываем исключение, чтобы можно было обработать его в вызывающем коде
            }
        }

    }
}
