using UnityEngine;
using System.Collections;

public class PickableItem : Item 
{
	public int picked = 0;

	public void Pick(Character character)
	{
		this.picked++;		
	}

	public void Drop(Character character)
	{

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
