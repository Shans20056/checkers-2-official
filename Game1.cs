using CheckersGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace checkers_2_official
{
    public enum GameState
    {
        Menu,
        CheckersGame,
        Settings,
        Statistic,
        Rule
    }

    public class Game1 : Game
    {
        private Music _music = new Music();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private bool _hasFrogKing = true; // Наличие короля лягушек
        private bool _hasChickenKing = true; // Наличие короля куриц

        private RenderTarget2D _renderTarget;

        // Счётчик ходов
        public SpriteFont _font; // Для отображения текста
        public SpriteFont _font1;
        public int _moveCounter = 1;
        public int _IsActivity = 0;
        public string WINNER = "";
        public int _ActivityTurn = 0;
        public (int x, int y) _lastCellIndex;
        public int moneyChiken = 0;
        public int moneyFrog = 0;
        public int _upgradeIsActivity = 0;
        public int _buildIsActivity = 0;

        // Поле для музыки
        private bool _isMusicPlaying = true; // Флаг для состояния музыки
        private Song _buttleMusic;

        private Texture2D _playButton;
        private Texture2D _rule;
        private Texture2D _settingButton;
        private Texture2D _quitButton;
        private Texture2D _backgroundTexture;
        private Texture2D _checkerTexture;
        private Texture2D _nextTurnButton;
        private Texture2D _upgradeButton;
        private Texture2D _buildButton;
        private Texture2D _frog;
        private Texture2D _frog1;
        private Texture2D _frogking;
        private Texture2D _chiken;
        private Texture2D _chiken1;
        private Texture2D _chikenking;
        private Texture2D _wall;
        private Texture2D _MainMenuButton;
        private Texture2D _musicButtonTexture;

        private Rectangle _ruleRect;
        private Rectangle _musicButtonRect;
        private Rectangle _MainMenuRect;
        private Rectangle _MainMenuRect1;
        private Rectangle _MainMenuRect2;
        private Rectangle _upgradeRect;
        private Rectangle _buildButtonRect;
        private Rectangle _nextTurnRect;
        private Rectangle _trainingRect;
        private Rectangle _settings;
        private Rectangle _exitRect;

        private GameState _currentGameState = GameState.Menu;

        // Данные доски
        int CellSize = Logic.LogicCheckers.CellSize;


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
            Logic.LogicBoard.InitializeBoard();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _music.LoadContent(Content);
            _renderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);
            _checkerTexture = Content.Load<Texture2D>("checker 1");
            _backgroundTexture = Content.Load<Texture2D>("background");
            _upgradeButton = Content.Load<Texture2D>("upgrade");
            _buildButton = Content.Load<Texture2D>("build");
            _chiken = Content.Load<Texture2D>("chiken");
            _frog = Content.Load<Texture2D>("frog");
            _chiken1 = Content.Load<Texture2D>("chiken1");
            _frog1 = Content.Load<Texture2D>("frog1");
            _chikenking = Content.Load<Texture2D>("chikenking");
            _frogking = Content.Load<Texture2D>("frogking");
            _wall = Content.Load<Texture2D>("wall");
            _font = Content.Load<SpriteFont>("Font");
            _font1 = Content.Load<SpriteFont>("Font1");
            _MainMenuButton = Content.Load<Texture2D>("main menu");
            _nextTurnButton = Content.Load<Texture2D>("NextTurnButton");
            _rule = Content.Load<Texture2D>("rule");
            _playButton = Content.Load<Texture2D>("play");
            _settingButton = Content.Load<Texture2D>("settings");
            _quitButton = Content.Load<Texture2D>("quit");


            _ruleRect = new Rectangle(740, 430, 50, 50);
            _MainMenuRect = new Rectangle(300, 350, 200, 100);
            _MainMenuRect1 = new Rectangle(695, 430, 100, 50);
            _trainingRect = new Rectangle(300, 50, 200, 50);
            _settings = new Rectangle(300, 200, 200, 50);
            _exitRect = new Rectangle(300, 350, 200, 50);
            _nextTurnRect = new Rectangle(605, 340, 75, 75);
            _upgradeRect = new Rectangle(535, 240, 90, 85);
            _buildButtonRect = new Rectangle(660, 240, 75, 75);
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
                        _currentGameState = GameState.CheckersGame;
                    }

                    if (_settings.Contains(mouseState.Position))
                    {
                        _currentGameState = GameState.Settings;
                    }

                    if (_exitRect.Contains(mouseState.Position))
                    {
                        Exit();
                    }
                    if (_ruleRect.Contains(mouseState.Position))
                    {
                        _currentGameState = GameState.Rule;
                    }

                }
            }


            if (_currentGameState == GameState.Settings)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                {
                    if (_MainMenuRect.Contains(mouseState.Position))
                    {
                        _currentGameState = GameState.Menu;
                    }

                    _music.Update(mouseState, _currentGameState);
                }
            }

            else if (_currentGameState == GameState.CheckersGame)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                {
                    Logic.LogicBoard.PrintBoardToConsole();
                    if (_nextTurnRect.Contains(mouseState.Position))
                    {
                        EndTurn(); // Увеличиваем счётчик
                    }

                    if (_MainMenuRect1.Contains(mouseState.Position))
                    {
                        _currentGameState = GameState.Menu;
                    }

                    if (_upgradeRect.Contains(mouseState.Position) && _upgradeIsActivity == 0)
                    {
                        if ((moneyFrog >= 100 && _moveCounter % 2 == 1) || (moneyChiken >= 100 && _moveCounter % 2 == 0))
                        {
                            _upgradeIsActivity = 1; // Активируем режим улучшения
                            _buildIsActivity = 0;
                        }
                    }

                    if (_upgradeIsActivity == 1 && Logic.LogicCheckers.GetCellPosition(mouseState.Position) != (-1, -1))
                    {
                        var (row, col) = Logic.LogicCheckers.GetCellPosition(mouseState.Position);
                        int check = Logic.LogicBoard.GetCell(row, col);

                        if ((_moveCounter % 2 == 1) && (check == 1))
                        {
                            Logic.LogicBoard.SetCell(row, col, 5); // Устанавливаем улучшенную версию
                            moneyFrog -= 100; // Списываем деньги
                            _upgradeIsActivity = 0; // Выключаем режим улучшения
                        }
                        else if ((_moveCounter % 2 == 0) && (check == 2))
                        {
                            Logic.LogicBoard.SetCell(row, col, 6);
                            moneyChiken -= 100; // Списываем деньги
                            _upgradeIsActivity = 0; // Выключаем режим улучшения
                        }
                    }

                    if (_buildButtonRect.Contains(mouseState.Position) && _buildIsActivity == 0)
                    {
                        if ((moneyFrog >= 200 && _moveCounter % 2 == 1) || (moneyChiken >= 200 && _moveCounter % 2 == 0))
                        {
                            _buildIsActivity = 1; // Активируем режим улучшения
                            _upgradeIsActivity = 0;
                        }
                    }

                    if (_buildIsActivity == 1 && Logic.LogicCheckers.GetCellPosition(mouseState.Position) != (-1, -1))
                    {
                        var (row, col) = Logic.LogicCheckers.GetCellPosition(mouseState.Position);
                        int check = Logic.LogicBoard.GetCell(row, col);

                        if ((_moveCounter % 2 == 1) && (check == 0))
                        {
                            Logic.LogicBoard.SetCell(row, col, 7);
                            moneyFrog -= 200; // Списываем деньги
                            _buildIsActivity = 0; // Выключаем режим улучшения
                        }
                        else if ((_moveCounter % 2 == 0) && (check == 0))
                        {
                            Logic.LogicBoard.SetCell(row, col, 7);
                            moneyChiken -= 200; // Списываем деньги
                            _buildIsActivity = 0; // Выключаем режим улучшения
                        }
                    }


                    if (Logic.LogicCheckers.GetCellPosition(mouseState.Position) != (-1, -1) && _moveCounter % 2 == 1)
                    {
                        var (row, col) = Logic.LogicCheckers.GetCellPosition(mouseState.Position);
                        int check = Logic.LogicBoard.GetCell(row, col);
                        if (_IsActivity == 1)
                        {
                            var (lRow, lCol) = _lastCellIndex;
                            if ((check == 0) && (Math.Abs(lRow - row) <= 1) && (Math.Abs(lCol - col) <= 1) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 1);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                _ActivityTurn++;
                            }
                            if ((check == 2 || check == 3 || check == 6) && (Math.Abs(lRow - row) <= 1) && (Math.Abs(lCol - col) <= 1) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 1);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                moneyFrog = moneyFrog + 10;
                                _ActivityTurn++;
                            }
                            _IsActivity = 0;
                        }
                        if (_IsActivity == 2)
                        {
                            var (lRow, lCol) = _lastCellIndex;
                            if ((check == 0) && (Math.Abs(lRow - row) <= 2) && (Math.Abs(lCol - col) <= 2) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 4);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                _ActivityTurn++;
                            }
                            if ((check == 2 || check == 3 || check == 6) && (Math.Abs(lRow - row) <= 2) && (Math.Abs(lCol - col) <= 2) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 4);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                moneyFrog = moneyFrog + 100;
                                _ActivityTurn++;
                            }
                            _IsActivity = 0;
                        }
                        if (_IsActivity == 3)
                        {
                            var (lRow, lCol) = _lastCellIndex;
                            if ((check == 0) && (Math.Abs(lRow - row) <= 2) && (Math.Abs(lCol - col) <= 2) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 5);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                _ActivityTurn++;
                            }
                            if ((check == 2 || check == 3 || check == 6) && (Math.Abs(lRow - row) <= 2) && (Math.Abs(lCol - col) <= 2) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 5);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                moneyFrog = moneyFrog + 100;
                                _ActivityTurn++;
                            }
                            _IsActivity = 0;
                        }

                        if ((_IsActivity == 0) && (_previousMouseState.RightButton == ButtonState.Released))
                        {
                            if (check == 1)
                            {
                                _IsActivity = 1;
                                _lastCellIndex = (row, col);
                            }
                            if (check == 4)
                            {
                                _IsActivity = 2;
                                _lastCellIndex = (row, col);
                            }
                            if (check == 5)
                            {
                                _IsActivity = 3;
                                _lastCellIndex = (row, col);
                            }
                        }
                    }

                    if (Logic.LogicCheckers.GetCellPosition(mouseState.Position) != (-1, -1) && _moveCounter % 2 == 0)
                    {
                        var (row, col) = Logic.LogicCheckers.GetCellPosition(mouseState.Position);
                        int check = Logic.LogicBoard.GetCell(row, col);

                        if (_IsActivity == 1)
                        {
                            var (lRow, lCol) = _lastCellIndex;
                            if ((check == 0) && (Math.Abs(lRow - row) <= 1) && (Math.Abs(lCol - col) <= 1) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 2);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                _ActivityTurn++;
                            }
                            if ((check == 1 || check == 4 || check == 5) && (Math.Abs(lRow - row) <= 1) && (Math.Abs(lCol - col) <= 1) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 2);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                moneyChiken = moneyChiken + 10;
                                _ActivityTurn++;
                            }
                            _IsActivity = 0;
                        }
                        if (_IsActivity == 2)
                        {
                            var (lRow, lCol) = _lastCellIndex;
                            if ((check == 0) && (Math.Abs(lRow - row) <= 2) && (Math.Abs(lCol - col) <= 2) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 3);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                _ActivityTurn++;
                            }
                            if ((check == 1 || check == 4 || check == 5) && (Math.Abs(lRow - row) <= 2) && (Math.Abs(lCol - col) <= 2) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 3);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                moneyChiken = moneyChiken + 100;
                                _ActivityTurn++;
                            }
                            _IsActivity = 0;
                        }
                        if (_IsActivity == 3)
                        {
                            var (lRow, lCol) = _lastCellIndex;
                            if ((check == 0) && (Math.Abs(lRow - row) <= 2) && (Math.Abs(lCol - col) <= 2) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 6);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                _ActivityTurn++;
                            }
                            if ((check == 1 || check == 4 || check == 5) && (Math.Abs(lRow - row) <= 2) && (Math.Abs(lCol - col) <= 2) && _ActivityTurn == 0)
                            {
                                Logic.LogicBoard.SetCell(row, col, 6);
                                Logic.LogicBoard.SetCell(lRow, lCol, 0);
                                moneyChiken = moneyChiken + 100;
                                _ActivityTurn++;
                            }
                            _IsActivity = 0;
                        }
                        if (_IsActivity == 0)
                        {
                            if (check == 2)
                            {
                                _IsActivity = 1;
                                _lastCellIndex = (row, col);
                            }
                            if (check == 3)
                            {
                                _IsActivity = 2;
                                _lastCellIndex = (row, col);
                            }
                            if (check == 6)
                            {
                                _IsActivity = 3;
                                _lastCellIndex = (row, col);
                            }
                        }

                    }

                }
                if (mouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released)
                {
                    _upgradeIsActivity = 0;
                    _buildIsActivity = 0;
                    _IsActivity = 0;
                }
                // Проверка условий победы или поражения
                CheckForVictoryOrDefeat();

            }


            else if (_currentGameState == GameState.Statistic)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                {
                    if (_MainMenuRect.Contains(mouseState.Position))
                    {
                        _currentGameState = GameState.Menu;
                    }
                }
            }

            else if (_currentGameState == GameState.Rule)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                {
                    if (_MainMenuRect.Contains(mouseState.Position))
                    {
                        _currentGameState = GameState.Menu;
                    }
                }
            }

            _previousMouseState = mouseState;
            base.Update(gameTime);
        }

        private void EndTurn()
        {
            _ActivityTurn = 0;
            moneyChiken = moneyChiken + 10;
            moneyFrog = moneyFrog + 10;
            _moveCounter++; // Увеличиваем счётчик ходов
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

                var ruleColor = _ruleRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_rule, _ruleRect, ruleColor);

            }

            if (_currentGameState == GameState.Settings)
            {
                _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                var mouseState = Mouse.GetState();

                _music.Draw(_spriteBatch);


                var mainMenu = _MainMenuRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_MainMenuButton, _MainMenuRect, mainMenu);
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
                        int cellValue = Logic.LogicBoard.GetCell(x, y);
                        if (cellValue == 1) // Белая шашка
                        {
                            _spriteBatch.Draw(_frog, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                        else if (cellValue == 2) // Чёрная шашка
                        {
                            _spriteBatch.Draw(_chiken, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                        else if (cellValue == 3)
                        {
                            _spriteBatch.Draw(_chikenking, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                        else if (cellValue == 4)
                        {
                            _spriteBatch.Draw(_frogking, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                        else if (cellValue == 5)
                        {
                            _spriteBatch.Draw(_frog1, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                        else if (cellValue == 6)
                        {
                            _spriteBatch.Draw(_chiken1, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                        else if (cellValue == 7)
                        {
                            _spriteBatch.Draw(_wall, new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize), Color.White);
                        }
                    }
                }

                if (_IsActivity == 1 && _moveCounter % 2 != 0)
                {
                    _spriteBatch.Draw(_frog, new Rectangle(mouseState.X, mouseState.Y, CellSize, CellSize), Color.White);
                }

                if (_IsActivity == 1 && _moveCounter % 2 == 0)
                {
                    _spriteBatch.Draw(_chiken, new Rectangle(mouseState.X, mouseState.Y, CellSize, CellSize), Color.White);
                }

                if (_IsActivity == 2 && _moveCounter % 2 != 0)
                {
                    _spriteBatch.Draw(_frogking, new Rectangle(mouseState.X, mouseState.Y, CellSize, CellSize), Color.White);
                }

                if (_IsActivity == 2 && _moveCounter % 2 == 0)
                {
                    _spriteBatch.Draw(_chikenking, new Rectangle(mouseState.X, mouseState.Y, CellSize, CellSize), Color.White);
                }

                if (_IsActivity == 3 && _moveCounter % 2 != 0)
                {
                    _spriteBatch.Draw(_frog1, new Rectangle(mouseState.X, mouseState.Y, CellSize, CellSize), Color.White);
                }

                if (_IsActivity == 3 && _moveCounter % 2 == 0)
                {
                    _spriteBatch.Draw(_chiken1, new Rectangle(mouseState.X, mouseState.Y, CellSize, CellSize), Color.White);
                }

                if (_upgradeIsActivity == 1)
                {
                    _spriteBatch.Draw(_upgradeButton, new Rectangle(mouseState.X, mouseState.Y, CellSize, CellSize), Color.White);
                }

                if (_buildIsActivity == 1)
                {
                    _spriteBatch.Draw(_buildButton, new Rectangle(mouseState.X, mouseState.Y, CellSize, CellSize), Color.White);
                }

                // Отображение хода
                _spriteBatch.DrawString(_font, $"Turn: {_moveCounter}", new Vector2(575, 20), Color.BurlyWood);
                _spriteBatch.DrawString(_font, $"Money chiken:  {moneyChiken}$", new Vector2(525, 60), Color.BurlyWood);
                _spriteBatch.DrawString(_font, $"Money frog:       {moneyFrog}$", new Vector2(525, 100), Color.BurlyWood);

                var mainMenu = _MainMenuRect1.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_MainMenuButton, _MainMenuRect1, mainMenu);
            }

            if (_currentGameState == GameState.Statistic)
            {
                _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                var mouseState = Mouse.GetState();

                _spriteBatch.DrawString(_font, $"WIN: {WINNER}", new Vector2(325, 50), Color.BurlyWood);

                var mainMenu = _MainMenuRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_MainMenuButton, _MainMenuRect, mainMenu);

            }

            if (_currentGameState == GameState.Rule)
            {
                _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);

                var mouseState = Mouse.GetState();

                _spriteBatch.DrawString(_font1, " \n  The goal of the game is to eat the opponent's king, and the first one to do so wins." +
                                                " \n  A move is ended by pressing the end move button, and in one move you can either" +
                                                " \n  move one piece once, upgrade it several times, or put up any number of barriers." +
                                                " \n  Basic pawns can move 1 square around themselves, but they can be upgraded to move" +
                                                " \n  2 squares; kings move 2 squares at once and cannot be upgraded. Upgrading a piece" +
                                                " \n  costs -$100, and installing a barrier that blocks a square until the end of the match costs -$200." +
                                                " \n  The game's economy is designed so that each player gets +$10 for finishing a move, " +
                                                " \n  another +$10 for cutting down a regular or upgraded piece, and a whole +$100 for cutting down a king.", new Vector2(25, 50), Color.BurlyWood);

                var mainMenu = _MainMenuRect.Contains(mouseState.Position) ? Color.Gray : Color.White;
                _spriteBatch.Draw(_MainMenuButton, _MainMenuRect, mainMenu);
            }




            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void CheckForVictoryOrDefeat()
        {
            // Сбрасываем флаги перед проверкой
            _hasFrogKing = false;
            _hasChickenKing = false;

            // Проверяем всю доску
            for (int y = 0; y < Logic.LogicBoard.BoardSize; y++)
            {
                for (int x = 0; x < Logic.LogicBoard.BoardSize; x++)
                {
                    int cellValue = Logic.LogicBoard.GetCell(y, x);

                    if (cellValue == 3) // Король куриц
                    {
                        _hasChickenKing = true;
                    }
                    else if (cellValue == 4) // Король лягушек
                    {
                        _hasFrogKing = true;
                    }

                    // Если оба короля найдены, завершаем проверку доски
                    if (_hasFrogKing && _hasChickenKing)
                        return;
                }
            }

            // Если одного из королей нет, переключаем состояние игры
            if (!_hasFrogKing)
            {
                WINNER = "CHIKEN";
                _currentGameState = GameState.Statistic; // Переключение на экран победы куриц
            }
            else if (!_hasChickenKing)
            {
                WINNER = "FROG";
                _currentGameState = GameState.Statistic; // Переключение на экран победы лягушек
            }
        }
    }
}
