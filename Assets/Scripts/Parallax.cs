using UnityEngine;

public class Parallax : MonoBehaviour
{
	public Vector2 velocity = new Vector2(-4, 0);
	public float positionY = 4;
	public float positionX = 4;
	public float lifetime;

	
	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = velocity;
		transform.position = new Vector3(transform.position.x+positionX, transform.position.y+positionY, transform.position.z);
		Destroy (gameObject, lifetime);
	}
}