using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {


	#region ATTRIBUTES
	private Animator _animator;
	private int _horizontal;
	private int _vertical;
	public PickableItem _item;
	private Character _attachment = null;

	public KeyCode keyUp;
	public KeyCode keyDown;
	public KeyCode keyLeft;
	public KeyCode keyRight;
	public KeyCode keyPick;
	public KeyCode keyUse;
	public float speed;
	public int id;
	public bool withBlood;
	public bool isSitDown;
	public bool dead = false;
	public GameObject corpsePrefab;
	public RuntimeAnimatorController controllerWithoutBlood;
	public RuntimeAnimatorController controllerWithBlood;
	#endregion

	#region PUBLIC METHODS
	public void Die()
	{
		dead = true;
		// TODO: sonido muerte
		GameObject corpse = (GameObject)Instantiate (corpsePrefab);
		corpse.transform.parent = GameObject.Find ("Items").transform;
		corpse.transform.position = this.transform.position;
		gameObject.SetActive (false);

	}
	public void CleanBlood()
	{
		//_animator.runtimeAnimatorController = controllerWithoutBlood; 
	}

	public void DirtyBlood()
	{
		//_animator.runtimeAnimatorController = controllerWithBlood; 
	}

	public void SitDown(Vector2 position)
	{
		this.transform.position = position;
		isSitDown = true;
		_animator.SetBool ("isSitDown", true);
	}
	public void SitUp()
	{
		isSitDown = false;
		_animator.SetBool ("isSitDown", false);
	}

	public Character GetOtherPlayer()
	{
		return (this == GameController.Instance.char1) ? GameController.Instance.char2 : GameController.Instance.char1;
	}

	public bool IsCollidingOtherPlayer()
	{
		Character other = GetOtherPlayer();

		Bounds characterBound = this.gameObject.GetComponent<BoxCollider2D> ().bounds;
		if(characterBound.Intersects(other.gameObject.GetComponent<BoxCollider2D>().bounds))
		{
			return true;
		}
		return false;
	}

	// Use this for initialization
	public void Awake () {
		_horizontal = 0;
		_vertical = 0;
		_animator = this.gameObject.GetComponent<Animator> ();
		DirtyBlood ();
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
					_item.UseWith(this, item);
				}
				else if (item != null)
				{
					item.UseWith(this, null);
				}

				string beforeUse = "";
				string afterUse = "";
				if(_item != null)
				{
					_item.GetText(item,out beforeUse,out afterUse);
				}
				else
				{
					item.GetText(_item,out beforeUse, out afterUse);
				}
				if(afterUse != "")
				{
					GameGUI.Instancia.ShowHelp(afterUse,GetPositionForUI(),id);
				}
			}
			else if (_item != null)
			{
				if (_item.itemType == Item.ItemType.KNIFE)
				{
					_item.UseWith(this, null);
				}
			}
		}
	}

	public Vector2 GetPositionForUI()
	{
		//first you need the RectTransform component of your canvas
		RectTransform CanvasRect=GameGUI.Instancia.GetComponent<RectTransform>();
		
		Camera Cam = Camera.main;
		//then you calculate the position of the UI element
		//0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
		Vector2 ViewportPosition=Cam.WorldToViewportPoint(gameObject.transform.position);
		Vector2 WorldObject_ScreenPosition=new Vector2(
			((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
			((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));
		//now you can set the position of the ui element
		return WorldObject_ScreenPosition;
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
		GameGUI.Instancia.SetIcon (_item.image, id);
	}

	public void DropItem()
	{
		_item.Drop(this);
		_item = null;	
		GameGUI.Instancia.SetIcon (null, id);
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
		if((_horizontal != 0 || _vertical != 0 ) && !isSitDown)
		{
			this.gameObject.transform.localPosition = Vector3.MoveTowards(this.gameObject.transform.localPosition, newPos,speed);
			if (_attachment != null)
			{
				Vector2 newPos2 = new Vector2 (_attachment.transform.localPosition.x  + _horizontal * speed, _attachment.transform.localPosition.y + _vertical * speed);
				_attachment.transform.localPosition = Vector3.MoveTowards(_attachment.transform.localPosition, newPos2,speed);
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
