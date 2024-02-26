using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair
{
    public int x;
    public int y;
    
    public Pair(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"({x},{y})";
    }
}
