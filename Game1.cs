using CheckersGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace checkers_2_official
{
    public enum GameState
    {
        Menu,
        CheckersGame,
        Settings
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D _renderTarget;

        // Счётчик ходов
        private SpriteFont _font; // Для отображения текста
        private int _moveCounter = 0;

        // Текстуры
        private Texture2D _playButton;
        private Texture2D _settingButton;
        private Texture2D _quitButton;
        private Texture2D _backgroundTexture;
        private Texture2D _checkerTexture;
        private Texture2D _nextTurnButton;
        private Texture2D _upgradeButton;
        private Texture2D _buildButton;
        private Texture2D _frog;
        private Texture2D _frog1;
        private Texture2D _chiken;
        private Texture2D _chiken1;

        private Rectangle _frogRect;
        private Rectangle _frog1Rect;
        private Rectangle _chikenRect;
        private Rectangle _chiken1Rect;
        private Rectangle _upgradeRect;
        private Rectangle _buildButtonRect;
        private Rectangle _nextTurnRect;
        private Rectangle _trainingRect;
        private Rectangle _settings;
        private Rectangle _exitRect;

        private GameState _currentGameState = GameState.Menu;

        // Данные доски
        private int[,] _board = new int[16, 16]; // 0 - пустая клетка, 1 - белая шашка, 2 - чёрная шашка, 3 - барьер
        private const int CellSize = 32; // Размер клетки (16x16 пикселей)

        private MouseState _previousMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            base.Initialize();

            Debagger.SetIcon(Window.Handle, "icon.ico");
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Установим внутреннее разрешение (например, 1280x720)
            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

            // Загрузка текстуры для ячейки 
            _checkerTexture = Content.Load<Texture2D>("checker 1");

            // Загрузка текстуры для заднего фона
            _backgroundTexture = Content.Load<Texture2D>("background");

            // Загрузка кнопки улучшения
            _upgradeButton = Content.Load<Texture2D>("upgrade");

            // Загрузка кнопки постройки препятсвия
            _buildButton = Content.Load<Texture2D>("build");

            // Загрузка шрифта
            _font = Content.Load<SpriteFont>("Font");

            _nextTurnButton = Content.Load<Texture2D>("NextTurnButton");

            try
            {
                // Загрузка текстур через IconHelper
                _playButton = Debagger.LoadTexture(Content, "play");
                _settingButton = Debagger.LoadTexture(Content, "settings");
                _quitButton = Debagger.LoadTexture(Content, "quit");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке текстур: {ex.Message}");
                Exit(); // Закрываем приложение при ошибке
            }

            int buttonWidth = 200;
            int buttonHeight = 50;

            _trainingRect = new Rectangle(300, 50, buttonWidth, buttonHeight);
            _settings = new Rectangle(300, 200, buttonWidth, buttonHeight);
            _exitRect = new Rectangle(300, 350, buttonWidth, buttonHeight);
            _nextTurnRect = new Rectangle(625, 400, 75, 75);
            _upgradeRect = new Rectangle(565, 300, 70, 70);
            _buildButtonRect = new Rectangle(685, 300, 65, 65);
        }

        protected override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if (_currentGameState == GameState.Menu)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                {
                    if (_trainingRect.Contains(mouseState.Position))
                    {
                        Console.WriteLine("Переход в игру");
                        _currentGameState = GameState.CheckersGame;
                    }

                    if (_settings.Contains(mouseState.Position))
                    {
                        Console.WriteLine("Переход в настройки");
                        _currentGameState = GameState.Settings;
                    }

                    if (_exitRect.Contains(mouseState.Position))
                    {
                        Console.WriteLine("Выход");
                        Exit();
                    }

                }
            }
            if (_currentGameState == GameState.Settings)
            {

            }

            else if (_currentGameState == GameState.CheckersGame)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                {
                    if (_nextTurnRect.Contains(mouseState.Position))
                    {
                        EndTurn(); // Увеличиваем счётчик
                    }
                }
            }

            _previousMouseState = mouseState;
            base.Update(gameTime);
        }

        private void EndTurn()
        {
            _moveCounter++; // Увеличиваем счётчик ходов
            Console.WriteLine($"Ход номер: {_moveCounter}");
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);



            _spriteBatch.Begin();


            if (_currentGameState == GameState.Menu)
            {
                // Рисуем фон на весь экран
                _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                var mouseState = Mouse.GetState();

                var trainingColor = _trainingRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_playButton, _trainingRect, trainingColor);

                var multiplayerColor = _settings.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_settingButton, _settings, multiplayerColor);

                var exitColor = _exitRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_quitButton, _exitRect, exitColor);
            }

            if (_currentGameState == GameState.Settings)
            {
                _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                var mouseState = Mouse.GetState();
            }


            if (_currentGameState == GameState.CheckersGame)
            {
                // Рисуем фон на весь экран
                _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                var mouseState = Mouse.GetState();

                var TurnButtonColor = _nextTurnRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_nextTurnButton, _nextTurnRect, TurnButtonColor);

                var upgradeButtonColor = _upgradeRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_upgradeButton, _upgradeRect, upgradeButtonColor);

                var buildButtonColor = _buildButtonRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_buildButton, _buildButtonRect, buildButtonColor);

                // Отрисовка клеток доски
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        int posX = x * CellSize;
                        int posY = y * CellSize;

                        // Отрисовка клетки
                        _spriteBatch.Draw(_checkerTexture, new Rectangle(posX, posY, CellSize, CellSize),
                             ((x + y) % 2 == 0) ? Color.White : Color.Gray);
                    }
                }

                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        if (_board[x, y] == 1) // Белая шашка
                        {
                            _spriteBatch.Draw(_frog, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                        else if (_board[x, y] == 2) // Чёрная шашка
                        {
                            _spriteBatch.Draw(_chiken, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                    }
                }

                // Отображение хода
                _spriteBatch.DrawString(_font, $"Turn: {_moveCounter}", new Vector2(605, 20), Color.BurlyWood);

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
