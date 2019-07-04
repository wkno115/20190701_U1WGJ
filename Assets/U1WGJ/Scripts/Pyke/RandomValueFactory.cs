using System;
using System.Linq;
using System.Numerics;

public class RandomValueFactory
{
    public static int CreateRandomValue(int min, int max)
    {
        return Enumerable.Range(min, max + 1 - min).OrderBy(n => Guid.NewGuid()).First();
    }
    public static int[] CreateRandomArray(int min, int max, int length)
    {
        var array = new int[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = CreateRandomValue(min, max);
        }
        return array;
    }
    public static int[] CreateRandomArrayWithoutOverlap(int min, int max, int length)
    {
        return Enumerable.Range(min, max - min).OrderBy(n => Guid.NewGuid()).Take(length).ToArray();
    }

    public static Vector2 CreateRandomVector2(int min, int max)
    {
        return new Vector2(CreateRandomValue(min, max), CreateRandomValue(min, max));
    }

    public static Vector2 CreateRandomNormalizedVector2()
    {
        return Vector2.Normalize(CreateRandomVector2(-1000, 1000));
    }

    public static Vector3 CreateRandomVector3(int min, int max)
    {
        return new Vector3(CreateRandomValue(min, max), CreateRandomValue(min, max), CreateRandomValue(min, max));
    }

    public static Vector3 CreateRandomNormalizedVector3()
    {
        return Vector3.Normalize(CreateRandomVector3(-1000, 1000));
    }
}
