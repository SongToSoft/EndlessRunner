using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Reader
{
    static public int HighScore = 0;
    static public void GetHighScore(string Path)
    {
        StreamReader SR = new StreamReader(Path);
        string line = SR.ReadLine();
        if (line != null)
        {
            string[] numbers = line.Split(' ');
            HighScore = int.Parse(numbers[0]);
        }
        SR.Close();
    }
    static public void SetHighScore(string Path, int Score)
    {
        HighScore = Score;
        StreamWriter SW = new StreamWriter(Path);
        SW.Write(HighScore);
        SW.Close();
    }
}
