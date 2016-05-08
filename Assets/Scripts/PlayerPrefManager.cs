using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerPrefManager
{
    private static string _playerScoreName;
    public static string PlayerScoreName {
        get
        {
            return _playerScoreName ?? "hs";
        }

        set
        {
            _playerScoreName = value;
        }
    }

    private static bool IsPlayerHighscoreInTop10(int playerHighscore)
    {
        var highscores = GetAllHighscores();
        highscores.Sort(CompareKey);
        var top10 = false;
        foreach (var score in highscores.Where(score => playerHighscore > score.Key))
        {
            top10 = true;
            Debug.Log(score);
        }

        return top10;
    }

    public static int CompareKey(KeyValuePair<int, string> a, KeyValuePair<int, string> b)
    {
        return -a.Key.CompareTo(b.Key);
    }

    public static List<KeyValuePair<int, string>> GetAllHighscores()
    {
        var highscores = new List<KeyValuePair<int, string>>();
        for (var i = 0; i < 10; i++)
        {
            highscores.Add(new KeyValuePair<int, string>(GetHighscore(i), GetHighscoreName(i)));
        }
        return highscores;
    }

    public static int GetHighscore(int place)
    {
        return PlayerPrefs.GetInt(PlayerScoreName + place);
    }

    public static string GetHighscoreName(int place)
    {
        return PlayerPrefs.GetString(PlayerScoreName + "n" + place);
    }

    public static int GetWinningPlayerHighcore()
    {
        return PlayerPrefs.GetInt(PlayerScoreName);
    }

    public static void InsertHighScore(int score, string name)
    {
        var highscores = GetAllHighscores();
        highscores.Add(new KeyValuePair<int, string>(score, name));
        highscores.Sort(CompareKey);
        for (var i = 0; i < 10; i++)
        {
            if (highscores.Count > i)
            {
                SetHighscore(i, highscores[i]);
            }
        }
    }

    private static void SetHighscore(int place, KeyValuePair<int, string> scoreAndName)
    {
        PlayerPrefs.SetInt(PlayerScoreName + place, scoreAndName.Key);
        PlayerPrefs.SetString(PlayerScoreName + "n" + place, scoreAndName.Value);
    }
}

