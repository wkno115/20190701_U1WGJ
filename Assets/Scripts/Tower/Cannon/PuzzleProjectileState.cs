using Puzzle;
using System;
using UnityEngine;

namespace Tower.Cannon
{
    [Serializable]
    public struct PuzzleProjectileState
    {
        [SerializeField]
        PieceColor _pieceColor;
        [SerializeField]
        float _attackPower;
        [SerializeField]
        float _movementSpeed;

        public PieceColor PieceColor => _pieceColor;
        public float AttackPower => _attackPower;
        public float MovementSpeed => _movementSpeed;

        public PuzzleProjectileState(PieceColor pieceColor, float attackPower, float movementSpeed)
        {
            _pieceColor = pieceColor;
            _attackPower = attackPower;
            _movementSpeed = movementSpeed;
        }
    }
}
