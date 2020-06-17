using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject pipes;
    public float delayTimer;
    private float timer;

    private void Start()
    {
        timer = delayTimer;
    }

    private void Update()
    {
        if (GameManager.gameOver == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                int randomValue = Random.Range(-1,4);
                Instantiate(pipes, new Vector3(transform.position.x, randomValue, -3f), transform.rotation);
                timer = delayTimer;
            }
        }
    }
}
