using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	#region Singleton Logic
	private static GameController _instance = null;
	public static GameController Instance 
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<GameController>();
			}
			return _instance;
		}
	}
	#endregion

	public Character char1 = null;
	public Character char2 = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
