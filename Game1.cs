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

        private Texture2D _playButton;
        private Texture2D _settingButton;
        private Texture2D _quitButton;
        private Texture2D _backgroundTexture;

        private Rectangle _trainingRect;
        private Rectangle _multiplayerRect;
        private Rectangle _exitRect;

        private Texture2D _boardTexture;
        private GameState _currentGameState = GameState.Menu;

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
            _renderTarget = new RenderTarget2D(GraphicsDevice, 1280, 720);

            // Загрузка текстуры для заднего фона
            _backgroundTexture = Content.Load<Texture2D>("background");

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
            _multiplayerRect = new Rectangle(300, 200, buttonWidth, buttonHeight);
            _exitRect = new Rectangle(300, 350, buttonWidth, buttonHeight);
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
                }
            }
            if (_currentGameState == GameState.Settings)
            {

            }

            else if (_currentGameState == GameState.CheckersGame)
            {
                // Логика обновления для игры "Шашки"
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    _currentGameState = GameState.Menu; // Возврат в меню
                }
            }

            _previousMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // Рисуем фон на весь экран
            _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

            var mouseState = Mouse.GetState();

            var trainingColor = _trainingRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
            _spriteBatch.Draw(_playButton, _trainingRect, trainingColor);

            var multiplayerColor = _multiplayerRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
            _spriteBatch.Draw(_settingButton, _multiplayerRect, multiplayerColor);

            var exitColor = _exitRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
            _spriteBatch.Draw(_quitButton, _exitRect, exitColor);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
