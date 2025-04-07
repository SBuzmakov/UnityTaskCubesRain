namespace Source.Scripts
{
    public class CubeListener
    {
        private readonly Painter _painter;
        private readonly Cube _cubeComponent;
        
        public CubeListener(Cube cube, Painter painter)
        {
            _painter = painter;

            _cubeComponent = cube;
        }

        public void Subscribe()
        {
            _cubeComponent.TouchedPlatform += _painter.RandomRepaint;
            
            _cubeComponent.TurnedOff += _painter.Repaint;
            
            _cubeComponent.Destroyed += Unsubscribe;
        }

        private void Unsubscribe()
        {
            _cubeComponent.TouchedPlatform -= _painter.RandomRepaint;
            
            _cubeComponent.TurnedOff -= _painter.Repaint;
            
            _cubeComponent.Destroyed -= Unsubscribe;
        }
    }
}