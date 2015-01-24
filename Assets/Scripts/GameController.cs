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
		Invoke("GenerateRandomEvent", Random.Range (10, 60));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GenerateRandomEvent()
	{
		switch(Random.Range(0,3))
		{
		case 0: EventController.Instance.LaunchEvent (EventController.ET.NEIGHBOUR_COMING);
			Debug.Log("pues vecino que viene");
			break;
		case 1: EventController.Instance.LaunchEvent (EventController.ET.ONO_CALLING);
			Debug.Log("pues ono");
			break;
		case 2: EventController.Instance.LaunchEvent (EventController.ET.POLICE_COMING);
			Debug.Log("pues llama la pulisia");
			break;
		}
	}
}
