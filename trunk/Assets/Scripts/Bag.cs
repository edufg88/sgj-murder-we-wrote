using UnityEngine;
using System.Collections;

public class Bag : MonoBehaviour {

	public Sprite bagNormal;
	public Sprite bagHidden;

	private Character inChar = null;

	public void Use(Character character)
	{
		if (inChar != null)
		{
			TakeOut (character);
		}
		else
		{
			this.gameObject.GetComponent<SpriteRenderer>().sprite = bagHidden;
			character.gameObject.SetActive(false);
			inChar = character;
		}
	}

	public void TakeOut(Character outChar)
	{
		if (outChar != inChar)
		{
			inChar.gameObject.SetActive(true);
			this.gameObject.GetComponent<SpriteRenderer>().sprite = bagNormal;
			inChar = null;
		}
	}

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<SpriteRenderer>().sprite = bagNormal;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
