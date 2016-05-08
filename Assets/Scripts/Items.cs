using UnityEngine;

public abstract class Items : MonoBehaviour
{
    public string itemName;
    public float lifetime = 8;
    protected float startTime;
    public float rangeY = 4;
    public float rangeX = 4;
    public Vector3 startPosition = new Vector3(12, 5, 0);

    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody
    {
        get
        {
            if (_rigidbody == null)
            {
                return _rigidbody = GetComponent<Rigidbody2D>();
            }

            return _rigidbody;
        }
    }

    protected virtual void Start()
    {
        startTime = Time.time; 
    }

    protected void Update()
    {
        if (Time.time - startTime > lifetime)
        {
            DestroyObject();
            startTime = Time.time;
        }
    }

    protected void DestroyObject()
    {
        Generate.ReturnToPool(this);
    }

    public abstract void InitializeItem(float velocityIncrease);
}

