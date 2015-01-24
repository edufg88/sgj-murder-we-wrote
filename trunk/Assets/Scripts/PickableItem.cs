using UnityEngine;
using System.Collections;

public class PickableItem : Item 
{
	public int picked = 0;
	private SpriteRenderer _sprite = null;
	public Sprite image;
	
	public override bool IsPickable()
	{
		return true;
	}

	public void Pick(Character character)
	{
		this.picked++;
		if (this.itemType == ItemType.CORPSE)
		{
			this.gameObject.GetComponent<Corpse>().Attach(character);
		}
		else
		{
			this.gameObject.SetActive(false);
			//this.enabled = false;
			//_sprite.renderer.enabled = false;
		}
	}

	public void Drop(Character character)
	{
		//this.enabled = true;
		//_sprite.renderer.enabled = true;
		this.gameObject.SetActive(true);
		if (this.itemType == ItemType.CORPSE)
		{
			this.gameObject.GetComponent<Corpse>().Detach(character);
			character.Detach();
		}
		this.transform.position = character.transform.position;
	}

	// Use this for initialization
	void Start () 
	{
		_sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
