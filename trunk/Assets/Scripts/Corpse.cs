using UnityEngine;
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

	public void DetachAll()
	{
		if (_attachment[0] != null)
		{
			_attachment[0].Detach();
			_attachment[0]._item = null;
			_attachment[0] = null;
		}
		if (_attachment[1] != null)
		{
			_attachment[1].Detach();
			_attachment[1]._item = null;
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
			_attachment[0].speed = 0.01f;
//			InstantiateBloodSplash();
		}
		else if (_attachment[0] == null && _attachment[1] != null)
		{
			this.transform.position = _attachment[1].transform.position;
			_attachment[1].speed = 0.01f;
//			InstantiateBloodSplash();
		}
		else //if (_attachment[0] != null && _attachment[1] != null)
		{
			_attachment[0].speed = _attachment[0].prevSpeed;
			_attachment[1].speed = _attachment[1].prevSpeed;
			this.transform.position = (_attachment[0].transform.position + _attachment[1].transform.position) / 2;
		}
	}

	void InstantiateBloodSplash()
	{
		if(Physics2D.OverlapPointAll (transform.position).Length < 3)
		{
			GameObject blood = (GameObject)Instantiate (bloodPrefab);
			blood.transform.parent = this.transform.parent;
			blood.transform.position = this.transform.position;
			blood.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
}
