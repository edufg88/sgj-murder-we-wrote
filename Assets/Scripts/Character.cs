using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


	#region ATTRIBUTES
	private Animator _animator;
	private int _horizontal;
	private int _vertical;
	private PickableItem _item;
	private Character _attachment = null;

	public KeyCode keyUp;
	public KeyCode keyDown;
	public KeyCode keyLeft;
	public KeyCode keyRight;
	public KeyCode keyPick;
	public KeyCode keyUse;
	public float speed;
	#endregion

	#region PUBLIC METHODS
	// Use this for initialization
	public void Awake () {
		_horizontal = 0;
		_vertical = 0;
		_animator = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	public void Update () {
		if(Input.anyKeyDown || (Input.GetKeyUp(keyUp) || Input.GetKeyUp(keyDown) || Input.GetKeyUp(keyLeft) || Input.GetKeyUp(keyRight)))
		{
			if(Input.GetKey(keyUp))
			{
				_horizontal = 0;
				_vertical = 1;
			}
			if(Input.GetKey(keyDown))
			{
				_horizontal = 0;
				_vertical = -1;
			}
			if(Input.GetKey(keyLeft))
			{
				_horizontal = -1;
				_vertical = 0;
			}
			if(Input.GetKey(keyRight))
			{
				_horizontal = 1;
				_vertical = 0;
			}

			if(Input.GetKeyDown(keyUp))
			{
				_horizontal = 0;
				_vertical = 1;
			}
			if(Input.GetKeyDown(keyDown))
			{
				_horizontal = 0;
				_vertical = -1;
			}
			if(Input.GetKeyDown(keyLeft))
			{
				_horizontal = -1;
				_vertical = 0;
			}
			if(Input.GetKeyDown(keyRight))
			{
				_horizontal = 1;
				_vertical = 0;
			}
		}
		else if(!Input.GetKey(keyUp) && !Input.GetKey(keyDown) && !Input.GetKey(keyLeft) && !Input.GetKey(keyRight))
		{
			_horizontal = 0;
			_vertical = 0;
		}
		Move ();
		SetAnimatorState ();

		if(Input.GetKeyDown(keyPick))
		{
			if(_item == null)
			{
				if(IntersectSomePickableItem(out _item))
				{
					PickItem(_item);
				}
			}
			else
			{
				DropItem();
			}
		}

		if(Input.GetKeyDown(keyUse))
		{
			Item item;
			if(IntersectSomeUsableItem(out item))
			{
				if (_item != null)
				{
					_item.UseWith(item);
				}
				else if (item != null)
				{
					item.UseWith(null);
				}
			}
		}
	}

	public void Attach(Character character)
	{
		_attachment = character;
	}

	public void Detach()
	{
		if (_attachment != null)
		{
			Character aux = _attachment;
			_attachment = null;
			aux.Detach();
		}
	}

	public void PickItem(PickableItem item)
	{
		_item = item;
		_item.Pick(this);
		NotificationCenter.DefaultCenter().AddObserver(this, "PonerTextureOne");
	}

	public void DropItem()
	{
		_item.Drop(this);
		_item = null;	
		NotificationCenter.DefaultCenter().AddObserver(this, "PonerTextureTwo");
	}
	#endregion

	#region PRIVATE METHODS
	private bool IntersectSomeUsableItem(out Item item)
	{
		Bounds characterBound = this.gameObject.GetComponent<BoxCollider2D> ().bounds;
		foreach(Item pi in GameObject.FindObjectsOfType<Item>())
		{
			if(characterBound.Intersects(pi.GetComponent<BoxCollider2D>().bounds))
			{
				item = pi;
				return true;
			}
		}
		item = null;
		return false;
	}
	private bool IntersectSomePickableItem(out PickableItem item)
	{
		Bounds characterBound = this.gameObject.GetComponent<BoxCollider2D> ().bounds;
		foreach(PickableItem pi in GameObject.FindObjectsOfType<PickableItem>())
		{
			if(characterBound.Intersects(pi.GetComponent<BoxCollider2D>().bounds))
			{
				item = pi;
				return true;
			}
		}
		item = null;
		return false;
	}

	private void Move()
	{
		Vector2 newPos = new Vector2 (this.gameObject.transform.localPosition.x  + _horizontal * speed, this.gameObject.transform.localPosition.y + _vertical * speed);
		if(_horizontal != 0 || _vertical != 0)
		{
			this.gameObject.transform.localPosition = Vector3.MoveTowards(this.gameObject.transform.localPosition, newPos,1);
			if (_attachment != null)
			{
				Vector2 newPos2 = new Vector2 (_attachment.transform.localPosition.x  + _horizontal * speed, _attachment.transform.localPosition.y + _vertical * speed);
				_attachment.transform.localPosition = Vector3.MoveTowards(_attachment.transform.localPosition, newPos2,1);
			}
		}
	}
	private void SetAnimatorState()
	{
		_animator.SetInteger("horizontal",_horizontal);
		_animator.SetInteger("vertical",_vertical);
		if(_horizontal == 0 && _vertical == 0)
		{
			_animator.speed = 0;
		}
		else
		{
			_animator.speed = 1;
		}
	}
	#endregion
}
