using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour 
{
	public string[] ItemStrings = 
	{
		"None",
		"Corpse",
		"Rockstar",
		"Shovel",
		"Mop",
		"Knife",
		"Cat",
		"Acid",
		"Bag",
		"Extinguisher",
		"WC",
		"Sink",
		"Blood",
		"Phone",
		"Bed",
		"Carpet",
		"Fridge",
		"Oven",
		"Bath",
		"Plant",
		"BathAndCorpse",
		"Hole",
		"Fire",
		"Exit"
	};

	public enum ItemType
	{
		NONE = 0,
		// PICKABLE
		CORPSE,
		ROCKSTAR,
		SHOVEL,
		MOP,
		KNIFE,
		CAT,
		ACID,
		BAG,
		EXTINGUISHER,
		// FIXED
		WC,
		SINK,
		BLOOD,
		PHONE,
		BED,
		CARPET,
		FRIDGE,
		OVEN,
		BATH,
		PLANT,
		EXIT,
		// MIX (FIXED)
		BATHANDCORPSE,
		HOLE,
		FIRE,
		ITEM_COUNT
	}

	private ActionController _ac = null;
	public ItemType itemType = ItemType.NONE;

	public abstract bool IsPickable();

	public void UseWith(Character character, Item other)
	{
		_ac = ActionController.Instance;

		// 1 LEVEL COMBINATIONS
		if (other == null)
		{
			// Using object only
			if (itemType == ItemType.ROCKSTAR)
			{
				_ac.AddActionResult(ActionController.ART.ETHIC, -5);
				// Increase player speed
				character.speed *= 1.5f;
				this.gameObject.SetActive(false);
			}
			else if (itemType == ItemType.WC)
			{
				_ac.AddActionResult(ActionController.ART.ETHIC, 1);

				if(!character.isSitDown)
					character.SitDown(this.transform.position);
				else
					character.SitUp();
			}
			else if (itemType == ItemType.SINK)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 1);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 3);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 1);
				_ac.AddActionResult(ActionController.ART.ETHIC, -1);

				if (character.withBlood)
				{
					character.withBlood = false;
					character.CleanBlood();
				}
			}
			else if (itemType == ItemType.PHONE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 5);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 1);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 1);
				_ac.AddActionResult(ActionController.ART.ETHIC, -1);

				// TODO: Decrease timer
				GameGUI.Instancia.TimeTravel(30);
			}
			else if (itemType == ItemType.KNIFE)
			{
				// Check proximity with the other player
				if (character.IsCollidingOtherPlayer())
				{
					// Kill other player
					character.GetOtherPlayer().Die();

					_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, -10);
					_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, -10);
					_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, -15);
					_ac.AddActionResult(ActionController.ART.ETHIC, -10);
				}
			}
			else if (itemType == ItemType.BAG)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 3);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 1);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 1);
				_ac.AddActionResult(ActionController.ART.ETHIC, 3);

				this.gameObject.GetComponent<Bag>().Use(character);
			}
			else if (itemType == ItemType.EXIT)
			{
				if(character.id == 0)
				{
					character.gameObject.SetActive(false);
					EventController.Instance.TakeDecision(EventController.ED.P1_OUT);
				}
				else
				{
					character.gameObject.SetActive(false);
					EventController.Instance.TakeDecision(EventController.ED.P1_OUT);
				}
				GameGUI.Instancia.HideHelp(character.id);
			}
		}
		else
		{
			// 2 LEVEL COMBINATION
			// Using object together with another object
			if (itemType == ItemType.MOP && other.itemType == ItemType.BLOOD)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 2);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 8);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 5);
				_ac.AddActionResult(ActionController.ART.ETHIC, -3);

				other.gameObject.SetActive(false);
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.BED)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 6);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 2);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 2);
				_ac.AddActionResult(ActionController.ART.ETHIC, -5);

				this.gameObject.SetActive(false);
				character._item = null;
			}
			else if (itemType == ItemType.CAT && other.itemType == ItemType.SINK)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 0);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, -3);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, -5);
				_ac.AddActionResult(ActionController.ART.ETHIC, -8);

				// TODO: Play cat sound
				character._item = null;
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.CARPET)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 3);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 2);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 1);
				_ac.AddActionResult(ActionController.ART.ETHIC, 2);

				this.gameObject.SetActive(false);
				character._item = null;
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.FRIDGE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 6);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 2);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 2);
				_ac.AddActionResult(ActionController.ART.ETHIC, -5);

				this.gameObject.SetActive(false);
				character._item = null;
			}
			// 3 LEVEL COMBINATION
			else if (itemType == ItemType.SHOVEL && other.itemType == ItemType.PLANT)
			{
				ItemController.Instance.hole.SetActive(true);
				// Create HOLE object, hide PLANT
				other.gameObject.SetActive(false);
				ItemController.Instance.ShowItem(ItemStrings[(int)ItemType.HOLE]);

			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.HOLE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 10);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 5);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 8);
				_ac.AddActionResult(ActionController.ART.ETHIC, -1);

				this.gameObject.SetActive(false);
				character._item = null;
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.BATH)
			{
				this.gameObject.SetActive(false);
				other.gameObject.SetActive(false);
				character._item = null;

				ItemController.Instance.ShowItem(ItemStrings[(int)ItemType.BATHANDCORPSE]);
			}
			else if (itemType == ItemType.ACID && other.itemType == ItemType.BATHANDCORPSE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 10);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 8);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 5);
				_ac.AddActionResult(ActionController.ART.ETHIC, -10);

				other.gameObject.SetActive(false);
				character._item = null;
			}
			else if (itemType == ItemType.CAT && other.itemType == ItemType.OVEN)
			{
				// Create FIRE object
				this.gameObject.SetActive(false);
				other.gameObject.SetActive(false);
				ItemController.Instance.ShowItem(ItemStrings[(int)ItemType.FIRE]);
				character._item = null;

				// Trigger event
				EventController.Instance.LaunchEvent(EventController.ET.POLICE_COMING);
			}
			else if (itemType == ItemType.EXTINGUISHER && other.itemType == ItemType.FIRE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 5);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 4);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 8);
				_ac.AddActionResult(ActionController.ART.ETHIC, 5);

				// TODO:Check if both can use it
				this.gameObject.SetActive(false);
				other.gameObject.SetActive(false);
				character._item = null;
			}
		}
	}

	public void GetText(Item other, out string beforeUse, out string afterUse)
	{
		beforeUse = "";
		afterUse = "";
		// 1 LEVEL COMBINATIONS
		if (other == null)
		{
			// Using object only
			if (itemType == ItemType.ROCKSTAR)
			{
				beforeUse = Texts.rockstar;
			}
			else if (itemType == ItemType.WC)
			{
				beforeUse = Texts.wc;
				afterUse = Texts.wcResult;
			}
			else if (itemType == ItemType.SINK)
			{
				beforeUse = Texts.sink;
				afterUse = Texts.sinkUse;
			}
			else if (itemType == ItemType.PHONE)
			{
				beforeUse = Texts.phone;
			}
			else if (itemType == ItemType.KNIFE)
			{
				beforeUse = Texts.knife;
			}
			else if (itemType == ItemType.BAG)
			{
				beforeUse = Texts.garbagebag;
			}
			else if (itemType == ItemType.EXIT)
			{
				beforeUse = Texts.exit;
			}
		}
		else
		{
			// 2 LEVEL COMBINATION
			// Using object together with another object
			if (itemType == ItemType.MOP && other.itemType == ItemType.BLOOD)
			{
				beforeUse = Texts.bloodMop;
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.BED)
			{
				beforeUse = Texts.bedCorpse;
			}
			else if (itemType == ItemType.CAT && other.itemType == ItemType.SINK)
			{
				beforeUse = Texts.sinkCatUse;
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.CARPET)
			{
				beforeUse = Texts.carpetCorpse;
				afterUse = Texts.carpetCorpseResult;
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.FRIDGE)
			{
				beforeUse = Texts.fridgeCorpse;
				afterUse = Texts.fridgeCorpseResult;
			}
			// 3 LEVEL COMBINATION
			else if (itemType == ItemType.SHOVEL && other.itemType == ItemType.PLANT)
			{
				beforeUse = Texts.shovelPlant;
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.HOLE)
			{
				beforeUse = Texts.holeCorpse;
				afterUse = Texts.holeCorpseResult;
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.BATH)
			{
				beforeUse = Texts.bathCorpse;
			}
			else if (itemType == ItemType.ACID && other.itemType == ItemType.BATHANDCORPSE)
			{
				beforeUse = Texts.bathCorpseAcid;
				afterUse = Texts.bathCorpseAcid;
			}
			else if (itemType == ItemType.CAT && other.itemType == ItemType.OVEN)
			{
				beforeUse = Texts.ovenCat;
			}
			else if (itemType == ItemType.EXTINGUISHER && other.itemType == ItemType.FIRE)
			{
				beforeUse = Texts.fireExtinguisher;
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		Character character = collider.gameObject.GetComponent<Character> ();
		if(character != null)
		{
			string beforeUse = "";
			string afterUse = "";
			if(character._item != null)
			{
				character._item.GetText(this,out beforeUse,out afterUse);
			}
			else
			{
				this.GetText(character._item,out beforeUse, out afterUse);
			}
			if(beforeUse != "")
			{
				GameGUI.Instancia.ShowHelp(beforeUse,character.GetPositionForUI(),character.id);
			}
		}
	}
	
	public void OnTriggerExit2D(Collider2D collider)
	{
		Character character = collider.gameObject.GetComponent<Character> ();
		if(character != null)
			GameGUI.Instancia.HideHelp (character.id);
	}

	// Use this for initialization
	void Start () 
	{
		_ac = ActionController.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
