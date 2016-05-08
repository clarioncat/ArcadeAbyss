using UnityEngine;

public class HotFly : MonoBehaviour
{

	Animator anim;
	int flyHash = Animator.StringToHash("Fly");
	int brakeHash = Animator.StringToHash("Brake");

	void Start ()
	{
		anim = GetComponent<Animator>();
	}



	// Update is called once per frame
	void Update ()
	{
		// Jump
		if (Input.GetKeyUp("e") || Input.GetKeyUp("k"))
		{
			anim.SetTrigger (flyHash);
		}

		// Forward
		if (Input.GetKeyDown("w") || Input.GetKeyUp("i"))
		{
			anim.SetTrigger (flyHash);
		}

		// Back
		if (Input.GetKeyDown("r") || Input.GetKeyUp("#"))
		{
			anim.SetTrigger (brakeHash);
		}



	}


}
