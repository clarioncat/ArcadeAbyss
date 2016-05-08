using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public AudioSource backgroundMusic;
    public AudioSource flapSound;
    public AudioSource goodItemSound;
    public AudioSource badItemSound;
	public AudioSource deathSound;
    public AudioSource bgmCountdown;
    public AudioSource countdownBing;
    public AudioSource countdownFly;
    public AudioSource ButtonRestart;

    private bool fadingOutEnded = false;
    public bool FadingOutEnded
    {
        get { return fadingOutEnded; }
    }

    public void playMusic()
    {
        backgroundMusic.Play();
    }

    public void playFlapSound()
    {
        flapSound.Play();
    }

    public void playGoodItemSound()
    {
		goodItemSound.Play();
    }

    public void playBadItemSound()
    {
        badItemSound.Play();
    }

	public void playDeathSound()
	{
		deathSound.Play();
	}
    
    public void PlayBGMCountdown()
    {
        bgmCountdown.Play();
    }

    public void PlayCountdownBing()
    {
        countdownBing.Play();
    }

    public void PlayCountdownFly()
    {
        countdownFly.Play();
    }

    public void PlayButtonRestart()
    {
        ButtonRestart.Play();
    }

    public void fadeMusicOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        for(float i = backgroundMusic.volume; i > 0.0f; i -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            backgroundMusic.volume -= 0.1f;
        }
        fadingOutEnded = true;        
        yield return null;
    }
}
