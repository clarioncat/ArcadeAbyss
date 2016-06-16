using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public Text HotHighscoreText = null;
    public Text HotHighscoreValueText = null;
    public Text ColdHighscoreText = null;
    public Text ColdHighscoreValueText = null;
    public Image ButtonImage = null;
    public Sprite ButtonExitSpirte = null;
    public Sprite ButtonRestartSprite = null; 
	public GameObject MedalBronze;
	public GameObject MedalSilver;
	public GameObject MedalGold;


    private int _coldHighscoreValue = 0;
    private int _hotHighscoreValue = 0;

	private float coldMedalX = 2;
	private float coldMedalY = 2;
	private float hotMedalX = 2;
	private float hotMedalY = 2;

    private AudioController audioController;
    public AudioController AudioController
    {
        get
        {
            if (audioController == null)
                return audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
            return audioController;
        }
    }

    public void Start()
    {
        _coldHighscoreValue = PlayerPrefs.GetInt("cs");
        _hotHighscoreValue = PlayerPrefs.GetInt("hs");

        HotHighscoreText.text = string.Format("{0}'s Score: ", HighscoreData.DragonNames[Random.Range(0, HighscoreData.DragonNames.Count)]);
        ColdHighscoreText.text = string.Format("{0}'s Score: ", HighscoreData.DragonNames[Random.Range(0, HighscoreData.DragonNames.Count)]);

        HotHighscoreValueText.text = string.Format("{0}", _hotHighscoreValue.ToString());
        ColdHighscoreValueText.text = string.Format("{0}",  _coldHighscoreValue.ToString());



		if (_coldHighscoreValue >= 3000 && _coldHighscoreValue <= 6999)
		{
			Instantiate (MedalBronze, new Vector3(3.35f, -2.28f, 0), Quaternion.identity);
		}
		else if (_coldHighscoreValue >= 7000 && _coldHighscoreValue <= 10999)
		{
			Instantiate (MedalSilver, new Vector3(3.35f, -2.28f, 0), Quaternion.identity);
		}
		else if (_coldHighscoreValue >= 11000)
		{
			Instantiate (MedalGold, new Vector3(3.35f, -2.28f, 0), Quaternion.identity);
		}


		if (_hotHighscoreValue >= 3000 && _hotHighscoreValue <= 6999)
		{
			Instantiate (MedalBronze, new Vector3(-3.35f, 2.28f, 0), Quaternion.Euler(180, 0, 0));
		}
		else if (_hotHighscoreValue >= 7000 && _hotHighscoreValue <= 10999)
		{
			Instantiate (MedalSilver, new Vector3(-3.35f, 2.28f, 0), Quaternion.Euler(180, 0, 0));
		}
		else if (_hotHighscoreValue >= 11000)
		{
			Instantiate (MedalGold, new Vector3(-3.35f, 2.28f, 0), Quaternion.Euler(180, 0, 0));
		}




    }

    private void Update()
    {
        // Exit
        if (Input.GetKeyDown("n") || Input.GetKeyDown("e") || Input.GetKeyDown("2") || Input.GetKeyDown("k") || Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnGameExitEntered();
        }

        // Restart
        if (Input.GetKeyDown("b") || Input.GetKeyDown("m") || Input.GetKeyDown("a") || Input.GetKeyDown("w") || Input.GetKeyDown("c") || Input.GetKeyDown("#") || Input.GetKeyDown("i") || Input.GetKeyDown("r") || Input.GetKeyDown("/"))
        {
            OnNewGameEntered();
        }
    }

    public void OnNewGameEntered()
    {
        AudioController.PlayButtonRestart();
        AudioController.fadeMusicOut();
        StartCoroutine(OpenNextLevel("Play"));
        ButtonImage.sprite = ButtonRestartSprite;
    }

    public void OnGameExitEntered()
    {
        AudioController.playDeathSound();
        AudioController.fadeMusicOut();
        StartCoroutine(OpenNextLevel("StartScreen"));
        ButtonImage.sprite = ButtonExitSpirte;
    }

    private IEnumerator OpenNextLevel(string sceneName)
    {
                yield return new WaitForSeconds(1.1f);
        if (AudioController.FadingOutEnded)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            StartCoroutine(OpenNextLevel(sceneName));
        }
    }
}
