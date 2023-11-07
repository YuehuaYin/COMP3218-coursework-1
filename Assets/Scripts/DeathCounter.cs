using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeathCounter
{
    public static int deaths = 0;
    public static int score = 0;
    public static int timer = 0;
    public static int prevTimerScore = 0;

    public static void ResetTimer()
    {
        timer = 0;
    }
}
