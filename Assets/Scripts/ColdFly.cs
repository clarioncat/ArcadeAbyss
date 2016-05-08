using UnityEngine;

public class ColdFly : MonoBehaviour
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
		if (Input.GetKeyUp("n") || Input.GetKeyUp("2") || Input.GetKeyDown(KeyCode.DownArrow))
		{
		    anim.SetTrigger (flyHash);
		}

		// Forward
		if (Input.GetKeyDown("m") || Input.GetKeyUp("c"))
		{
			anim.SetTrigger (flyHash);
		}
		
		// Back
		if (Input.GetKeyDown("b") || Input.GetKeyUp("a"))
		{
			anim.SetTrigger (brakeHash);
		}		
	}

	
}
