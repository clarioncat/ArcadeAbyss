using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public bool canFlap = true;
    protected int gravityValue;
    public int gemGoodValue;
    public int gemBadValue; 

    private Rigidbody2D rigidBody;
    public Rigidbody2D RigidBody
    {
        get
        {
            if (rigidBody == null)
            {
                return rigidBody = GetComponent<Rigidbody2D>();
            }

            return rigidBody;
        }
    }

    public void DisableInput()
    {
        canFlap = false;
        RigidBody.gravityScale = 0;
    }

    public void EnableInput()
    {
        canFlap = true;
        RigidBody.gravityScale = gravityValue;
    }

    protected void Die(string playerName, int amount)
    {
        canFlap = false;
    }
}

