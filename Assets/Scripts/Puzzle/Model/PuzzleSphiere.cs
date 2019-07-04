namespace Puzzle
{
    public struct PuzzleSphere
    {
        public byte Lane;
        public PieceColor Color;
        int _damage;

        public PuzzleSphere(byte lane,PieceColor color,int damage)
        {
            Lane = lane;
            Color = color;
            _damage = damage;
        }
    }
}
