using checkers_2_official;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


internal class Music
{
    private Texture2D _musicButtonTexture;
    private Rectangle _musicButtonRect;
    private Rectangle _MainMenuRect;
    private bool _isMusicPlaying = true;
    private Song _buttleMusic;

    public void LoadContent(ContentManager content)
    {
        _musicButtonTexture = content.Load<Texture2D>("musicbutton");
        _buttleMusic = content.Load<Song>("buttle");
        MediaPlayer.IsRepeating = true; // Повтор музыки
        MediaPlayer.Play(_buttleMusic); // Запуск музыки
        MediaPlayer.Volume = 0.1f;
        _musicButtonRect = new Rectangle(350, 100, 100, 100);
    }
    public void Update(MouseState mouseState, GameState _currentGameState)
    {
        // Переключение состояния музыки

        if (_musicButtonRect.Contains(mouseState.Position))
        {
            _isMusicPlaying = !_isMusicPlaying;
            if (_isMusicPlaying)
            {
                MediaPlayer.Play(_buttleMusic);
            }
            else
            {
                MediaPlayer.Pause();
            }
        }
    }


    public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch)
    {
        var buttonColor = _isMusicPlaying ? Color.White : Color.Gray; // Цвет кнопки в зависимости от состояния
        _spriteBatch.Draw(_musicButtonTexture, _musicButtonRect, buttonColor);

    }
}

