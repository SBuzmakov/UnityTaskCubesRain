using Source.Scripts.Objects;

namespace Source.Scripts.Services
{
    public class CubeListener
    {
        private readonly Painter _painter;
        private readonly Cube _cube;

        public CubeListener(Cube cube, Painter painter)
        {
            _cube = cube;
            _painter = painter;
        }

        public void Subscribe()
        {
            _cube.TouchedPlatform += _painter.RandomRepaint;
            _cube.NeedsBaseColor += _painter.RepaintWhite;
            _cube.Destroyed += Unsubscribe;
        }

        private void Unsubscribe(Cube cube)
        {
            cube.TouchedPlatform -= _painter.RandomRepaint;
            cube.NeedsBaseColor -= _painter.RepaintWhite;
            cube.Destroyed -= Unsubscribe;
        }
    }
}

