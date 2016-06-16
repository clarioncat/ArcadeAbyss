using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Parallax backgroundrocks;
    public ColdPlayer coldDragon;
    public HotPlayer hotDragon;
    public Generate generate; 
    public Text coldCountdownText;
    public Text hotCountdownText;
    public Text hotYouDied;
    public Text coldYouDied;

    public int countdownValue = 3;
    public string startCommand = "Fly";
    public float countdownFrequence = 0.4f  ;
    public float velocityIncreaseAfterDeath = 0.2f;
    int deathCount = 0;
    public GameObject Controlls;

    private AudioController audioController; 

    void Start () {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();

        HotPlayer.OnDie += DieCount;
        ColdPlayer.OnDie += DieCount;

        coldDragon.DisableInput();
        hotDragon.DisableInput();

        generate.gameObject.SetActive(false);
        countdownValue++;
        StartCoroutine(Countdown());

        InitializeRocks();
        audioController.PlayBGMCountdown();


    }

    private void DieCount(string playerName, int scoreValue)
    {
        deathCount++;
        audioController.playDeathSound();
        generate.velocityIncreaseStep = velocityIncreaseAfterDeath;

        if (playerName == "cs")
        {
            PlayerPrefs.SetInt("cs", scoreValue);
            coldYouDied.enabled = true; 
        }
        else
        {
            PlayerPrefs.SetInt("hs", scoreValue);
            hotYouDied.enabled = true;
        }

        if (deathCount == 2)
        {
            StartCoroutine(DelayedDeath(playerName, scoreValue));
        }

        PlayerPrefs.Save();
    }

    public IEnumerator DelayedDeath(string playerName, int scoreValue)
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("GameOver");

        yield break;
    }

    private IEnumerator Countdown() 
    {
        while(countdownValue >= 0)
        {
            yield return new WaitForSeconds(countdownFrequence);

            coldCountdownText.text = (countdownValue - 1).ToString();
            hotCountdownText.text = (countdownValue - 1).ToString();

            if (countdownValue > 1)
            {
                audioController.PlayCountdownBing();
            }

            if (countdownValue == 1)
            {
                audioController.PlayCountdownFly();
                coldCountdownText.text = startCommand;
                hotCountdownText.text = startCommand;
                generate.gameObject.SetActive(true);
                coldDragon.EnableInput();
                hotDragon.EnableInput();
                Controlls.SetActive(false);
            }

            if (countdownValue == 0)
            {
                coldCountdownText.text = string.Empty;
                hotCountdownText.text = string.Empty; 
            }

            countdownValue--;
        }
    }

    private void InitializeRocks()
    {
        Instantiate(backgroundrocks, new Vector3(-12.8f, 0, 0), Quaternion.identity);
        InvokeRepeating("CreateParallax", 0f, 8.5f);
    }

    private void CreateParallax()
    {
        Instantiate(backgroundrocks);
    }

    public void OnDestroy()
    {
        HotPlayer.OnDie -= DieCount;
        ColdPlayer.OnDie -= DieCount;
    }
}
