using UnityEngine;

public class BackGroundMov : MonoBehaviour
{
    private Material material;
    private Vector2 offset;
    public float xVel;
    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(xVel,0);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (GameManager.gameOver == false)
        {
            material.mainTextureOffset += offset * Time.deltaTime;
        }*/
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
