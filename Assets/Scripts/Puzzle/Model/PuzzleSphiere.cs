namespace Puzzle
{
    public struct PuzzleSphere
    {
        byte _lane;
        PieceColor _color;
        int _damage;

        public PuzzleSphere(byte lane,PieceColor color,int damage)
        {
            _lane = lane;
            _color = color;
            _damage = damage;
        }
    }
}
