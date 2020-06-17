using UnityEngine;

public class PipeMov : MonoBehaviour
{
    public float speed = 0f;
    void FixedUpdate()
    {
        if (GameManager.gameOver == false)
        {
            transform.Translate(new Vector3(-1,0,0) * speed * Time.deltaTime);

        }
    }
}
