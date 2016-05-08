using UnityEngine;

public class GemItems : Items
{
    public float startGravity = 0.5f;

    public override void InitializeItem(float velocityIncrease)
    {
        transform.position = new Vector3(startPosition.x - rangeX * Random.value, startPosition.y - rangeY * Random.value, startPosition.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z * Random.value);
        Rigidbody.gravityScale = velocityIncrease * startGravity; 
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        if (coll.gameObject.name == "HotDragon")
        {
            DestroyObject();
        }

        if (coll.gameObject.name == "ColdDragon")
        {
            DestroyObject();
        }
    }
}

