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
	public int charactersAvailable = 2;

	public void EndGame()
	{
		ActionController.Instance.ProcessAllART();
		ActionController.Instance.ShowFinale();
	}

	public void RestartGame()
	{
		Debug.Log ("deberia reiniciar cabrones");
		Application.LoadLevel(Application.loadedLevel);
	}
	// Use this for initialization
	void Start () {
		Invoke("GenerateRandomEvent", Random.Range (10, 40));
		GameGUI.Instancia.OnTimeUp = EndGame;
	}
	
	// Update is called once per frame
	void Update () {
		if (charactersAvailable == 0)
		{
			EndGame();
		}
	}

	private void GenerateRandomEvent()
	{
		switch(Random.Range(0,2))
		{
		case 0: EventController.Instance.LaunchEvent (EventController.ET.NEIGHBOUR_COMING);
			Debug.Log("pues vecino que viene");
			break;
		case 1: EventController.Instance.LaunchEvent (EventController.ET.POLICE_COMING);
			Debug.Log("pues ono");
			break;
		}
	}
}
