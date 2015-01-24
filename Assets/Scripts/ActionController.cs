using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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

	public enum ART_RES
	{
		LOW = 0,
		MEDIUM,
		HIGH
	}

	public GameObject blackbg = null;
	public GameObject finaleText = null;
	private int[] _actionResults = new int[(int)ART.ART_SIZE];
	private ART_RES[] _actionFinalResults = new ART_RES[(int)ART.ART_SIZE];

	public void AddActionResult(ART resultType, int value)
	{
		_actionResults[(int)resultType] = value;
	}

	private const int hcvL = 5;
	private const int hcvH = 10;
	private const int chL = 5;
	private const int chH = 10;
	private const int hiL = 5;
	private const int hiH = 10;
	private const int ethL = 5;
	private const int ethH = 10;

	public void ProcessAllART()
	{
		int hcv = _actionResults[(int)ART.HIDDEN_CORPSE];
		if (hcv < hcvL){ _actionFinalResults[(int)ART.HIDDEN_CORPSE] = ART_RES.LOW; }
		else if (hcv >= hcvL && hcv < hcvH){ _actionFinalResults[(int)ART.HIDDEN_CORPSE] = ART_RES.MEDIUM; }
		else if (hcv >= hcvH){ _actionFinalResults[(int)ART.HIDDEN_CORPSE] = ART_RES.HIGH; } 

		int ch = _actionResults[(int)ART.CLEAN_HOUSE];
		if (ch < chL){ _actionFinalResults[(int)ART.CLEAN_HOUSE] = ART_RES.LOW; }
		else if (ch >= chL && ch < chH){ _actionFinalResults[(int)ART.CLEAN_HOUSE] = ART_RES.MEDIUM; }
		else if (ch >= chH){ _actionFinalResults[(int)ART.CLEAN_HOUSE] = ART_RES.HIGH; }

		int hi = _actionResults[(int)ART.HOUSE_INTEGRITY];
		if (hi < hiL){ _actionFinalResults[(int)ART.HOUSE_INTEGRITY] = ART_RES.LOW; }
		else if (hi >= hiL && hi < hiH){ _actionFinalResults[(int)ART.HOUSE_INTEGRITY] = ART_RES.MEDIUM; }
		else if (hi >= hiH){ _actionFinalResults[(int)ART.HOUSE_INTEGRITY] = ART_RES.HIGH; }

		int eth = _actionResults[(int)ART.ETHIC];
		if (eth < ethL){ _actionFinalResults[(int)ART.ETHIC] = ART_RES.LOW; }
		else if (eth >= ethL && eth < ethH){ _actionFinalResults[(int)ART.ETHIC] = ART_RES.MEDIUM; }
		else if (eth >= ethH){ _actionFinalResults[(int)ART.ETHIC] = ART_RES.HIGH; }
	}

	public void ShowFinale()
	{
		// TODO: Depending on _action final result get finale and show it
		string final = Texts.incendio5;	
		blackbg.SetActive(true);
		finaleText.SetActive(true);
		Text t = finaleText.GetComponent<Text>();
		finaleText.GetComponentInChildren<Text>().text = final;

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
