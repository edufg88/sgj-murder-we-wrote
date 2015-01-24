using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour
{
	#region Singleton Logic
	private static ActionController _instance = null;
	public static ActionController Instance 
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<ActionController>();
			}
			return _instance;
		}
	}
	#endregion

	public enum ART//ACTION_RESULT_TYPE
	{
		HIDDEN_CORPSE = 0,
		CLEAN_HOUSE = 1,
		PLAYERS_ALIVE = 2,
		HOUSE_INTEGRITY = 3,
		ETHIC = 4,
		ART_SIZE = 5
	}

	private int[] _actionResults = new int[(int)ART.ART_SIZE];

	public void AddActionResult(ART resultType, int value)
	{
		_actionResults[(int)resultType] = value;
	}

	// Use this for initialization
	void Start () 
	{
		// Init results
		int max = (int)ART.ART_SIZE;
		for (int i = 0; i < max; ++i)
		{
			_actionResults[i] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
