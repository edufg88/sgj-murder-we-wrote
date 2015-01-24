﻿using UnityEngine;
using System.Collections;

public class Corpse : MonoBehaviour 
{
	public GameObject bloodPrefab = null;
	private Character[] _attachment = { null, null };

	public void Attach(Character character)
	{
		if (_attachment[0] == null)
		{
			_attachment[0] = character;
		}
		else if (_attachment[1] == null)
		{
			_attachment[1] = character;
		}

		if (_attachment[0] != null && _attachment[1] != null)
		{
			_attachment[0].Attach(_attachment[1]);
			_attachment[1].Attach(_attachment[0]);
		}
	}

	public void Detach(Character character)
	{
		if (_attachment[0] == character)
		{
			_attachment[0] = null;
		}
		else if (_attachment[1] == character)
		{
			_attachment[1] = null;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_attachment[0] == null && _attachment[1] == null)
		{
			return;
		}
		else if (_attachment[0] != null && _attachment[1] == null)
		{
			this.transform.position = _attachment[0].transform.position;
		}
		else if (_attachment[0] == null && _attachment[1] != null)
		{
			this.transform.position = _attachment[1].transform.position;
		}
		else //if (_attachment[0] != null && _attachment[1] != null)
		{
			this.transform.position = (_attachment[0].transform.position + _attachment[1].transform.position) / 2;
		}
	}
}
