using UnityEngine;

public class Obstacle : Items
{
	public Vector2 velocity = new Vector2(-4, 0);

    public override void InitializeItem(float velocityIncrease)
    {
        Rigidbody.velocity = new Vector2(velocityIncrease * velocity.x, 0);
        transform.position = new Vector3(startPosition.x - rangeX * Random.value, startPosition.y + rangeY * Random.value, startPosition.z);
        float randomScaleX = (Random.Range(0.7f, 1.3f));
        float randomScaleY = (Random.Range(0.7f, 1.3f));
        transform.localScale = new Vector3(randomScaleX, randomScaleY, 1);
    }
}