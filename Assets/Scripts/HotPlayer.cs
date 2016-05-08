using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HotPlayer : PlayerBase
{
	// The force which is added when the player jumps
	// This can be changed in the Inspector window
	public Vector2 jumpForce = new Vector2(0, 50);
	public Vector2 speedForce = new Vector2(200, 0);

	public Text countHot;
	public int hotScore;

	private new AudioController audio;
    public static event Action<string, int> OnDie;
   
    void Start()
	{
        gravityValue = -1;
        OnDie += Die; 
		hotScore = 0;
		SetCountText ();
		audio = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
	}    

	void Update ()
	{
        if (canFlap)
        {
            // Jump
            if (Input.GetKeyDown("e") || Input.GetKeyDown("k"))
            {
                RigidBody.velocity = Vector2.zero;
                RigidBody.AddForce(-jumpForce);
                audio.playFlapSound();
            }

            // Speed Left
            if (Input.GetKeyDown("r") || Input.GetKeyDown("#") || Input.GetKeyDown("/"))
            {
                RigidBody.velocity = Vector2.zero;
                RigidBody.AddForce(-jumpForce * 0.25f);
                RigidBody.AddForce(-speedForce);
                audio.playFlapSound();
            }

            // Speed Right
            if (Input.GetKeyDown("w") || Input.GetKeyDown("i"))
            {
                RigidBody.velocity = Vector2.zero;
                RigidBody.AddForce(-jumpForce * 0.25f);
                RigidBody.AddForce(speedForce);
                audio.playFlapSound();
            }

            // Die by being off screen
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPosition.y > Screen.height + 200 || screenPosition.y < -200)
            {
                OnDie("hs", hotScore);
            }
        }

        if (canFlap)
        {
            hotScore++;
            SetCountText();
        }

    }
    
    // Die by collision
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (canFlap)
        {
            if (coll.gameObject.tag == "Rock")
            {
                OnDie("hs", hotScore);
            }

            if (coll.gameObject.name == "gem_cold(Clone)")
            {
                audio.playBadItemSound();
                if (hotScore > gemBadValue)
                {
                    hotScore = hotScore - gemBadValue;
                } else
                {
                    hotScore = 0;
                }
            }

            if (coll.gameObject.name == "gem_hot(Clone)")
            {
                audio.playGoodItemSound();
                hotScore = hotScore + gemGoodValue;
            }
        }
    }

	private void SetCountText ()
	{
		countHot.text = "Score: " + hotScore.ToString ();
	}

	private IEnumerator Wait()
	{
            Debug.Log ("Wait");
			yield return new WaitForSeconds (5.0f);

			yield return null;
	}
}
