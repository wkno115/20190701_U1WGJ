﻿using System;
using System.Linq;

namespace Puzzle
{
    /// <summary>
    /// 列挙型に関する汎用クラス
    /// </summary>
    public static class EnumCommon
    {
        private static readonly Random mRandom = new Random();  // 乱数

        /// <summary>
        /// 指定された列挙型の値をランダムに返します
        /// </summary>
        public static T Random<T>()
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .OrderBy(c => mRandom.Next())
                .FirstOrDefault();
        }

        /// <summary>
        /// 指定された列挙型の値の数返します
        /// </summary>
        public static int GetLength<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}