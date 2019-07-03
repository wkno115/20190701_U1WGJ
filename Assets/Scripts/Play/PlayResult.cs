using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Play
{
    /// <summary>
    /// プレイ結果
    /// </summary>
    public struct PlayResult
    {
        /// <summary>
        /// 時間
        /// </summary>
        public float Time;
        /// <summary>
        /// 撃破スコア
        /// </summary>
        public int DefeatScore;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="time">時間</param>
        /// <param name="defeatScore">撃破スコア</param>
        public PlayResult(float time,int defeatScore)
        {
            Time = time;
            DefeatScore = defeatScore;
        }
    }
}
