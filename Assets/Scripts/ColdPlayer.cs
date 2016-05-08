using System;
using UnityEngine;
using UnityEngine.UI;

public class ColdPlayer : PlayerBase
{
	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public Vector2 jumpForce = new Vector2(0, 50);
	public Vector2 speedForce = new Vector2(200, 0);

	public Text countCold;
	public int coldScore;
    public static event Action<string, int> OnDie;

    private new AudioController audio;
    
    private void Start()
	{
        gravityValue = 1;
		coldScore = 0;
		SetCountText ();
        audio = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        OnDie += Die;
    }

	private void Update ()
	{
        if (canFlap)
        {
            // Jump
            if (Input.GetKeyDown("n") || Input.GetKeyDown("2") || Input.GetKeyDown(KeyCode.DownArrow))
            {
                RigidBody.velocity = Vector2.zero;
                RigidBody.AddForce(jumpForce);
                audio.playFlapSound();
            }

            // Speed right
            if (Input.GetKeyDown("m") || Input.GetKeyDown("c"))
            {
                RigidBody.velocity = Vector2.zero;
                RigidBody.AddForce(jumpForce * 0.25f);
                RigidBody.AddForce(speedForce);
                audio.playFlapSound();
            }

            // Speed left
            if (Input.GetKeyDown("b") || Input.GetKeyDown("a"))
            {
                RigidBody.velocity = Vector2.zero;
                RigidBody.AddForce(jumpForce * 0.25f);
                RigidBody.AddForce(-speedForce);
                audio.playFlapSound();
            }

            // Die by being off screen
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPosition.y > Screen.height + 200 || screenPosition.y < -200)
            {
                OnDie("cs", coldScore);
            }
        }

        if (canFlap)
        {
            coldScore++;
            SetCountText();
        }
    }

	// Die by collision
	private void OnCollisionEnter2D(Collision2D coll)
	{
        if (canFlap)
        {
            if (coll.gameObject.tag == "Rock")
            {
                OnDie("cs", coldScore);
            }

            if (coll.gameObject.name == "gem_cold(Clone)")
            {
                audio.playGoodItemSound();
                coldScore = coldScore + gemGoodValue;
            }

            if (coll.gameObject.name == "gem_hot(Clone)")
            {
                audio.playBadItemSound();
                if (coldScore > gemBadValue)
                {
                    coldScore = coldScore - gemBadValue;
                }
                else
                {
                    coldScore = 0;
                }
            }
        }
	}

	private void SetCountText ()
	{
		countCold.text = "Score: " + coldScore.ToString();
	}
}
