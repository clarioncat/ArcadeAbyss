using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameOverController : MonoBehaviour
{
    [ContextMenuItem("ResetHighscore", "ResetHighscore")]
    public string playerScoreName; 
	public Text highscore;
	public int playerHighscore;

	public GameObject panelTitle;
	public GameObject score;
	public GameObject enterName;
	public GameObject inputText;
	public GameObject enterButton;
	public GameObject exitButton;
	public GameObject restartButton;
	public GameObject exitGameButton;
	public AudioController audioController;

	public RectTransform highscorePanel;

	private int place = 0;
	private int highscoreLength = 10;
    private static int dragonNameIndex;
    private Dicitonary<string, string> newOrder; 

    private void Start()
    {
        playerHighscore = PlayerPrefManager.GetWinningPlayerHighcore();
        var randomPlayerName = HighscoreData.DragonNames[UnityEngine.Random.Range(0, HighscoreData.DragonNames.Count)];
        PlayerPrefManager.InsertHighScore(this.playerHighscore, randomPlayerName);
        for (int i = 0; i < PlayerPrefManager.GetAllHighscores().Count; i++)
        {
            SpawnScoreText(i);
        }

        ShowDefaultScorePanel(randomPlayerName);
    }


    // display scores
    void updateHighscores()
	{
		// insert player names into array
		for(int i = 0; i < HighscoreData.HighscoreNames.Length; i++)
		{
            if (!PlayerPrefs.HasKey("n" + i))
            {
                PlayerPrefs.SetString("n" + i, "unknown");
            }
            HighscoreData.HighscoreNames[i] = PlayerPrefs.GetString("n" + i);
            Debug.Log(PlayerPrefs.GetString("n" + i) + " string for n"+i);
            Debug.Log(PlayerPrefs.GetInt("h" + i) + " int for h" + i);
        }

		// insert scores into array
		for (int i = 0; i < HighscoreData.HighscoreValues.Length; i++)
		{
            HighscoreData.HighscoreValues[i] = PlayerPrefs.GetInt("h" + i);
		}
		Array.Sort(HighscoreData.HighscoreValues);
		Array.Reverse(HighscoreData.HighscoreValues);

        for (int i = 0; i < HighscoreData.HighscoreValues.Length; i++)
		{
			if (HighscoreData.HighscoreValues != null)
			{
				SpawnScoreText(i);
			}
		}
		highscore.text = "" + playerHighscore;
	}

    private void SpawnScoreText(int i)
	{
		GameObject go = (GameObject)Instantiate(score);
		go.transform.SetParent(highscorePanel.transform, false);
		Text scoreText = go.GetComponent<Text>();
		RectTransform panel = go.GetComponent<RectTransform>();
		panel.name = "Score " + i;
		// setup placement
		panel.anchorMin = new Vector2(0.0f, (float)(10 - i - 1) / 10);
		panel.anchorMax = new Vector3(1.0f, (float)(10 - i) / 10);
		panel.offsetMin = new Vector2(10, 0);
		panel.offsetMax = new Vector2(0, 0);
		// setup content
		scoreText.text = (i + 1) + ". \t" + HighscoreData.HighscoreNames[i] + " \t" + HighscoreData.HighscoreValues[i];
	}

	// check if new score achieved is new highscore
	void checkHighscores()
	{   
		for (int i = 0; i < HighscoreData.HighscoreValues.Length; i++)
		{
			if (HighscoreData.HighscoreValues[i] < playerHighscore)
			{
				panelTitle.GetComponent<Text>().text = "New Highscore! "+ HighscoreData.DragonNames[dragonNameIndex];
				enterButton.SetActive(true);
				exitButton.SetActive(true);
				place = i;
				break;
			}
		}
		if (!enterName.activeSelf)
		{
			ShowDefaultScorePanel(HighscoreData.DragonNames[dragonNameIndex]);
		}
	}

	// just score and new game button
	private void ShowDefaultScorePanel(string randomPlayerName)
	{
		enterName.SetActive(false);
		enterButton.SetActive(false);
		exitButton.SetActive(false);
		panelTitle.GetComponent<Text>().text = "Your Score: " + randomPlayerName;
		restartButton.SetActive(true);
		exitGameButton.SetActive(true);
	}

	// called by enter button
	public void OnNameEntered()
	{
		if(place != highscoreLength-1) // make sure new score isn't already in the end of the list
		{
			correctOldScore();
		}
		PlayerPrefs.SetString("n" + place, HighscoreData.DragonNames[dragonNameIndex]);
		PlayerPrefs.SetInt("h" + place, playerHighscore);

		deleteOldScorePanel();
		updateHighscores();
		ShowDefaultScorePanel(HighscoreData.DragonNames[dragonNameIndex]);    
	}

	// push older scores down
	void correctOldScore()
	{
		for(int i = highscoreLength-1; i > place-1; i--)
		{
			PlayerPrefs.SetInt("h" + (i + 1), PlayerPrefs.GetInt("h" + i)); // save old int-data to higher number key
			PlayerPrefs.SetString("n" + (i + 1), PlayerPrefs.GetString("n" + i)); // save old string-data to higher number key
		}
		PlayerPrefs.DeleteKey("h" + highscoreLength);
		PlayerPrefs.DeleteKey("n" + highscoreLength);
	}

	// delete the old score entries
	void deleteOldScorePanel()
	{
		for(int i = 0; i < highscoreLength; i++)
		{
			GameObject oldScore = GameObject.Find("Score " + i);
			Destroy(oldScore);
		}
	}

	public void OnExitEntered()
	{
		ShowDefaultScorePanel(HighscoreData.DragonNames[dragonNameIndex]);
	}

	public void OnNewGameEntered()
	{
		audioController.fadeMusicOut();
		StartCoroutine(OpenNextLevel());  
	}

	public void OnGameExitEntered()
	{
		Application.Quit();
	}

	private IEnumerator OpenNextLevel()
	{
		yield return new WaitForSeconds(1.1f);
		if (audioController.FadingOutEnded)
		{
			SceneManager.LoadScene("Play");
		}
		else
		{
			StartCoroutine(OpenNextLevel());
		}
	}

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteAll();
    }
}

internal class Dicitonary<T1, T2>
{
}