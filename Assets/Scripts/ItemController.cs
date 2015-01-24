using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour 
{
	#region Singleton Logic
	private static ItemController _instance = null;

	public static ItemController Instance 
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<ItemController>();
			}
			return _instance;
		}
	}
	#endregion


	// All item instances
	public Item[] items = null; 
	public GameObject itemContainer = null;
	// Items that may appear as a result of others;
	public GameObject hole = null;
	public GameObject bathAndCorpse = null;
	public GameObject fire = null;

	public void ShowItem(string name)
	{
		Transform item = itemContainer.transform.FindChild(name);
		item.gameObject.SetActive(true);
	}
	public void HideItem(string name)
	{
		Transform item = itemContainer.transform.FindChild(name);
		item.gameObject.SetActive(false);
	}

	public void CreateAllItems()
	{
		itemContainer.SetActive(true);
		foreach (Item it in items)
		{
			// TODO: Place every item in scene
		}
	}

	// Use this for initialization
	void Start () 
	{
		hole.SetActive(false);
		bathAndCorpse.SetActive(false);
		fire.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
