using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour 
{
	#region Singleton Logic
	private static EventController _instance = null;
	public static EventController Instance 
	{
		get 
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<EventController>();
			}
			return _instance;
		}
	}
	#endregion

	private ET _activeEvent = ET.NONE;
	private const float _eventDecisionTime = 30f;

	public enum ET// EventType
	{
		NONE = 0,
		NEIGHBOUR_COMING,
		POLICE_COMING,
		ONO_CALLING,
		P1_OUT,
		P2_OUT
	}
	
	public enum ED// EventDecision
	{
		OPEN_NEIGHBOUR = 0,
		NOT_OPEN_NEIGHBOUR,
		OPEN_POLICE,
		NOT_OPEN_POLICE,
		ANSWER_ONO,
		NOT_ANSWER_ONO,
		P1_OUT,
		P2_OUT
	}

	public void LaunchEvent(ET eventType)
	{
		switch (eventType)
		{
		case ET.NEIGHBOUR_COMING:
			MessageController.Instancia.ShowMessage(Texts.doorNeigbor);
			break;
		case ET.POLICE_COMING:
			MessageController.Instancia.ShowMessage(Texts.doorPolice);
			break;
		case ET.ONO_CALLING:
			break;
		case ET.P1_OUT:
			GameController.Instance.charactersAvailable--;
			ActionController.Instance.AddActionResult(ActionController.ART.HIDDEN_CORPSE, -2);
			ActionController.Instance.AddActionResult(ActionController.ART.ETHIC, -7);
			break;
		case ET.P2_OUT:
			GameController.Instance.charactersAvailable--;
			ActionController.Instance.AddActionResult(ActionController.ART.HIDDEN_CORPSE, -2);
			ActionController.Instance.AddActionResult(ActionController.ART.ETHIC, -7);
			break;
		default:
			return;
		}

		_activeEvent = eventType;
		Invoke("EndTimer", _eventDecisionTime);
	}

	public void TakeDecision(ED playerDecision)
	{
		switch (_activeEvent)
		{
		case ET.NEIGHBOUR_COMING:
			if (playerDecision == ED.OPEN_NEIGHBOUR)
			{
				GameController.Instance.EndGame();
			}
			else if (playerDecision == ED.NOT_OPEN_NEIGHBOUR)
			{
				GameGUI.Instancia.TimeTravel(60);
			}
			break;
		case ET.POLICE_COMING:
			if (playerDecision == ED.OPEN_POLICE)
			{
				GameController.Instance.EndGame();
			}
			else if (playerDecision == ED.NOT_OPEN_POLICE)
			{
				ActionController.Instance.AddActionResult(ActionController.ART.HIDDEN_CORPSE, -1);
				ActionController.Instance.AddActionResult(ActionController.ART.ETHIC, -4);
				GameController.Instance.EndGame();
			}
			break;
		case ET.ONO_CALLING:
			if (playerDecision == ED.ANSWER_ONO)
			{

			}
			else if (playerDecision == ED.NOT_ANSWER_ONO)
			{

			}
			break;
		default:
			return;
		}
	}

	void EndTimer()
	{
		switch (_activeEvent)
		{
		case ET.NEIGHBOUR_COMING:
			TakeDecision(ED.NOT_OPEN_NEIGHBOUR);
			break;
		case ET.POLICE_COMING:
			TakeDecision(ED.NOT_OPEN_POLICE);
			break;
		case ET.ONO_CALLING:
			TakeDecision(ED.NOT_ANSWER_ONO);
			break;
		default:
			return;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
}
