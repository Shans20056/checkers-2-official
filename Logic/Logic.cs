using System;

namespace Logic
{
    class LogicCheckers
    {
        public const int CellSize = 30; // Размер клетки (32x32 пикселей)

        public static (int row, int col) GetCellPosition(Microsoft.Xna.Framework.Point mousePosition)
        {
            int row = mousePosition.X / CellSize;
            int col = mousePosition.Y / CellSize;
            var position = (-1, -1);
            if (row >= 0 && mousePosition.X < 480 && col >= 0 && mousePosition.Y < 480)
            {
                position = (row, col);
                return position;
            }
            // Возвращаем (-1; -1), если координаты вне доски
            return (-1, -1);
        }
    }

    class LogicBoard
    {
        private static int[,] _board = new int[BoardSize, BoardSize]; // Единственная доска
        public const int BoardSize = 16;

        public static void InitializeBoard()
        {
            // Задаём начальную расстановку шашек
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    // Чёрные шашки (2) в верхней части доски
                    if (y - x > 7)
                    {
                        _board[x, y] = 1;
                    }
                    // Белые шашки (1) в нижней части доски
                    else if (x - y > 7)
                    {
                        _board[x, y] = 2;
                    }
                    // Пустые клетки
                    else
                    {
                        _board[x, y] = 0;
                    }
                }
                Console.WriteLine(); // Переходим на новую строку
            }
            _board[15, 0] = 3;
            _board[0, 15] = 4;
        }

        public static void PrintBoardToConsole()
        {
            for (int y = 0; y < BoardSize; y++) // Проходим по строкам
            {
                for (int x = 0; x < BoardSize; x++) // Проходим по столбцам
                {
                    Console.Write($"{_board[y, x]} "); // Выводим значение клетки с пробелом
                }
                Console.WriteLine(); // Переходим на новую строку
            }
            Console.WriteLine(new string('-', BoardSize * 2)); // Разделитель для лучшей читаемости
        }


        // Метод для обновления отдельной клетки
        public static void SetCell(int x, int y, int value)
        {
            if (x >= 0 && x < BoardSize && y >= 0 && y < BoardSize)
            {
                _board[x, y] = value;
            }

        }

        // Метод для получения значения клетки
        public static int GetCell(int x, int y)
        {

            return _board[x, y];


        }

        // Геттер для всей доски
        public static int[,] GetBoard()
        {
            return _board;
        }
    }

}
