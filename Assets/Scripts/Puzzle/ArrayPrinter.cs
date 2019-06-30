﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public static class ArrayPrinter<T>
    {
        public static void Print(T[,] array)
        {
            var result = "";
            for (int i = 0; i < array.GetLength(1); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result += array[i, j].ToString() + ",";
                }
                result += "\n ";
            }
            Debug.Log(result);
        }
    }
}
