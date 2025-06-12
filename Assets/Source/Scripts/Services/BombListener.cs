using Source.Scripts.Objects;

namespace Source.Scripts.Services
{
    public class BombListener
    {
        private readonly Bomb _bomb;
        private readonly Painter _painter;
        private readonly Exploder _exploder;

        public BombListener(Bomb bomb, Painter painter, Exploder exploder)
        {
            _bomb = bomb;
            _painter = painter;
            _exploder = exploder;
        }

        public void Subscribe()
        {
            _bomb.Spawned += _bomb.StartActiveLife;
            _bomb.NeedsBaseColor += _painter.RepaintBlack;
            _bomb.ActiveLifeStarted += _painter.ChangeAlpha;
            _bomb.Destroyed += Unsubscribe;
            _bomb.ActiveLifeFinished += _exploder.Explode;
        }

        private void Unsubscribe(Bomb bomb)
        {
            bomb.ActiveLifeStarted -= _painter.ChangeAlpha;
            bomb.Spawned -= bomb.StartActiveLife;
            bomb.NeedsBaseColor -= _painter.RepaintBlack;
            bomb.Destroyed -= Unsubscribe;
            bomb.ActiveLifeFinished -= _exploder.Explode;
        }
    }
}