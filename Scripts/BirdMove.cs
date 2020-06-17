using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMove : MonoBehaviour
{
	#region Delegates And Events

	public delegate void PlayerDelegate();
	public static event PlayerDelegate OnPlayerDied ;
	public static event PlayerDelegate OnPlayerScored;
	public static event PlayerDelegate OnCoinCollect;

	#endregion

	#region Variables

	public float TapForce;
	public float TiltSmooth;
	public Vector3 startPos;

	#endregion

	#region GameObjects

	private Rigidbody2D rb;
	private Quaternion downRotation;
	private Quaternion forwardRotation;

	#endregion
	
	public void Start()
	{
		startPos = transform.localPosition;
		rb = GetComponent<Rigidbody2D>();
		downRotation = Quaternion.Euler(0, 0, -90);
		forwardRotation = Quaternion.Euler(0, 0, 35);
		rb.simulated = false;
	}

	public void Update()
	{
		if (GameManager.gameOver)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			transform.rotation = forwardRotation;
			rb.velocity = Vector3.zero;
			rb.AddForce(Vector2.up * TapForce, ForceMode2D.Force);
		}

		transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, TiltSmooth * Time.deltaTime);
	}

	
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "DeadZone")
		{
			//game over
			rb.simulated = false;
			OnPlayerDied(); //event sent to GameManager
		}

		if (other.tag == "ScoreZone")
		{
			//score increases
			OnPlayerScored(); //event sent to GameManager
		}

		if (other.tag == "Coin")
		{
			//coin score increases and coin gets destroyed
			Destroy(other.gameObject);
			OnCoinCollect();
		}
	}

	private void OnEnable()
	{
		GameManager.OnGameStarted += OnGameStarted;
		GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
	}

	private void OnDisable()
	{
		GameManager.OnGameStarted -= OnGameStarted;
		GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
	}

	void OnGameStarted() //event came from GameManager
	{
		rb.velocity = Vector3.zero;
		rb.simulated = true;
		
	}

	void OnGameOverConfirmed() //event came from GameManager
	{
		transform.localPosition = startPos;
		transform.rotation = Quaternion.identity;
	}
	
}
