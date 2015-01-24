using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{
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
		ITEM_COUNT,
		PLANT,
		// MIX (FIXED)
		BATHANDCORPSE,
		HOLE,
		FIRE
	}

	private ActionController _ac = null;
	public ItemType itemType = ItemType.NONE;

	public void UseWith(Item other)
	{
		// 1 LEVEL COMBINATIONS
		if (other == null)
		{
			// Using object only
			if (itemType == ItemType.ROCKSTAR)
			{
				_ac.AddActionResult(ActionController.ART.ETHIC, -5);
				// TODO: Añadir tiempo
			}
			else if (itemType == ItemType.WC)
			{
				_ac.AddActionResult(ActionController.ART.ETHIC, 1);
			}
			else if (itemType == ItemType.SINK)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 1);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 3);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 1);
				_ac.AddActionResult(ActionController.ART.ETHIC, -1);
			}
			else if (itemType == ItemType.PHONE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 5);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 1);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 1);
				_ac.AddActionResult(ActionController.ART.ETHIC, -1);
				// TODO: Decrease timer
			}
			else if (itemType == ItemType.KNIFE)
			{
				// TODO: Check proximity with the other player
				// if (nearEnough)
				{
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
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.BED)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 6);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 2);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 2);
				_ac.AddActionResult(ActionController.ART.ETHIC, -5);
			}
			else if (itemType == ItemType.CAT && other.itemType == ItemType.SINK)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 0);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, -3);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, -5);
				_ac.AddActionResult(ActionController.ART.ETHIC, -8);
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.CARPET)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 3);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 2);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 1);
				_ac.AddActionResult(ActionController.ART.ETHIC, 2);
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.FRIDGE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 6);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 2);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 2);
				_ac.AddActionResult(ActionController.ART.ETHIC, -5);
			}
			// 3 LEVEL COMBINATION
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.PLANT)
			{
				// TODO: Create HOLE object
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.HOLE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 10);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 5);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 8);
				_ac.AddActionResult(ActionController.ART.ETHIC, -1);
			}
			else if (itemType == ItemType.CORPSE && other.itemType == ItemType.BATH)
			{
				// TODO: Create BATHANDCORPSE object
			}
			else if (itemType == ItemType.ACID && other.itemType == ItemType.BATHANDCORPSE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 10);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 8);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 5);
				_ac.AddActionResult(ActionController.ART.ETHIC, -10);
			}
			else if (itemType == ItemType.CAT && other.itemType == ItemType.OVEN)
			{
				// TODO: Create FIRE object
			}
			else if (itemType == ItemType.EXTINGUISHER && other.itemType == ItemType.FIRE)
			{
				_ac.AddActionResult(ActionController.ART.HIDDEN_CORPSE, 5);
				_ac.AddActionResult(ActionController.ART.CLEAN_HOUSE, 4);
				_ac.AddActionResult(ActionController.ART.HOUSE_INTEGRITY, 8);
				_ac.AddActionResult(ActionController.ART.ETHIC, 5);
			}
		}
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
