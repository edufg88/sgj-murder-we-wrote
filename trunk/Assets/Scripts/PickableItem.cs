using UnityEngine;
using System.Collections;

public class PickableItem : Item 
{
	public int picked = 0;
	private SpriteRenderer _sprite = null;
	public Sprite image;

	public Character attachedTo = null;
	
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
			//this.gameObject.SetActive(false);
			attachedTo = character;
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
		else
		{
			attachedTo = null;
		}
		this.transform.position = character.transform.position;
	}

	// Use this for initialization
	void Start () 
	{
		_sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.itemType != ItemType.CORPSE && attachedTo != null)
		{
			Vector3 newPos = new Vector3(attachedTo.transform.position.x+1, attachedTo.transform.position.y, attachedTo.transform.position.z);
			this.transform.position = newPos;
		}
	}
}
