namespace Source.Scripts
{
    public class CubeListener
    {
        private readonly Painter _painter;
        private readonly Cube _cube;
        
        public CubeListener(Cube cube, Painter painter)
        {
            _painter = painter;
            _cube = cube;
        }

        public void Subscribe()
        {
            _cube.TouchedPlatform += _painter.RandomRepaint;
            
            _cube.NeedsBaseColor += _painter.BaseRepaint;
            
            _cube.Destroyed += Unsubscribe;
        }

        private void Unsubscribe()
        {
            _cube.TouchedPlatform -= _painter.RandomRepaint;
            
            _cube.NeedsBaseColor -= _painter.BaseRepaint;
            
            _cube.Destroyed -= Unsubscribe;
        }
    }
}