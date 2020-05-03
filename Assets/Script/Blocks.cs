using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
	public bool vertical = false;
	public bool isThreeBlock = false;
	public ParticleSystem dust;
	private Rigidbody2D rb2d;
	private Vector3 cursorPosition;

	public void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.isKinematic = true;
		cursorPosition = transform.localPosition;
	}

	private void OnEnable()
	{
		GameManager.GameEndEventHandler += GameEndEvent;
	}

	public void GameEndEvent()
	{
		GameManager.GameEndEventHandler -= GameEndEvent;
		Destroy(gameObject);
	}

	void OnMouseDrag()
	{
		if (GameManager.isCollectionOpen)
			return;

		Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		rb2d.isKinematic = false;

		if (vertical)
		{
			cursorPosition = new Vector3(transform.localPosition.x, newPos.y, 0);
		}
		else
		{
			cursorPosition = new Vector3(newPos.x, transform.localPosition.y, 0);
		}
		
		rb2d.MovePosition(cursorPosition);
		if (dust != null && !dust.isPlaying)
			dust.Play();
	}

	void OnMouseUp()
	{
		if (GameManager.isCollectionOpen)
			return;

		rb2d.isKinematic = true;
		GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().AddMove();
		GetComponent<AudioSource>().Play();

		if (vertical)
		{
			SnapY();
		}
		else
		{
			SnapX();
		}
	}

	void SnapX(){
		// -1.8f가 최저, 1.8f가 최대
		float x;
		if (isThreeBlock)
			x = Mathf.Floor(transform.localPosition.x) + 0.5f;
		else
			x = Mathf.Round(transform.localPosition.x);

		rb2d.MovePosition(new Vector3(x, transform.localPosition.y, 0));
	}

	void SnapY(){
		float y;
		if(isThreeBlock)
			y = Mathf.Round(transform.localPosition.y);
		else
			y = Mathf.Floor(transform.localPosition.y) + 0.5f;

		rb2d.MovePosition(new Vector3(transform.localPosition.x, y, 0));
	}

	
}
