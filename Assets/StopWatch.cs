using System;
using System.Threading;
using UnityEngine;

internal class StopWatch
{
    private static bool running;
    public static float hour = 0, minuts = 0, seconds = 0;
    public static bool isRunning { get { return running; } }
    public static void execute()
    {
        seconds += 1 * Time.deltaTime;
        if (seconds >= 60)
        {
            minuts += 1;
            seconds = 0;
        }
        if (minuts >= 60)
        {
            hour += 1;
            minuts = 0;
        }
        Debug.Log("Ct: " + hour + ":" + minuts + ":" + seconds);
    }

    internal static string GetLable
    {
        get
        {
            
            return  "Time: " +
                (hour > 0 ? hour.ToString("n0") + ":" : "") +  
                (minuts > 0 ? minuts.ToString("n0") + ":" : "") + 
                seconds.ToString("n0") + "";
        }
    }

    public static string ElapsTime
    {
        get
        {
            return "<-- Elaps Time -->\n" +
                hour + "h " + minuts + "m and " + seconds.ToString("n0") + "s\n" + 
                "To compleate This Level";
        }
    }

    internal static void Start() { running = true; Debug.Log("Stopwatch Started"); }
    internal static void Stop() { running = false; Debug.Log("Stopwatch Stopped"); }
}