using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		//destroy the game object
		Destroy(other.gameObject);
	}

	private void OnEnable()
	{
		GameManager.DestroyAll += DestroyAll;
	}

	private void OnDisable()
	{
		GameManager.DestroyAll -= DestroyAll;
	}

	void DestroyAll()
	{
		var PipesClone = GameObject.FindGameObjectsWithTag("Pipe");
		foreach (var VARIABLE in PipesClone)
		{
			Destroy(VARIABLE);
		}
	}
}
