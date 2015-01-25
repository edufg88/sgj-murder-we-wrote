using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour {

	#region ATTRIBUTES
	//private Image imageHUDP1;
	//private Image imageHUDP2;
	private GameObject BSP1;
	private GameObject BSP2;
	public delegate void VoidDelegate();
	public VoidDelegate OnTimeUp;
	public Sprite emptyHUDP;
	public Text timer;
	private float _timeActual;

	public float gameTime = 30f;
	#endregion

	#region Patron Singleton
	private static GameGUI _instancia = null;
	public static GameGUI Instancia{
		get
		{
			if(_instancia == null)
			{
				_instancia = FindObjectOfType<GameGUI>();
			}
			return _instancia;
		}
	}
	#endregion


	#region PUBLIC METHODS
	void Start(){

		//imageHUDP1 = GameObject.Find("HUDP1").GetComponent<Image>();
		//imageHUDP2 = GameObject.Find("HUDP2").GetComponent<Image>();

		BSP1 = GameObject.Find("BubbleSpeechP1");
		BSP2 = GameObject.Find("BubbleSpeechP2");

		BSP1.SetActive (false);
		BSP2.SetActive (false);

		//temporal
		StartTimer(gameTime);
	}

	
	public void SetIcon(Sprite sprite, int player){

		//Si player = 0 ==> P1
		//Si player = 1 ==> P2
		if(sprite == null)
		{
			sprite = emptyHUDP;
		}
		/*
		if(player == 0){
			imageHUDP1.sprite = sprite;
		}else if(player == 1){
			imageHUDP2.sprite = sprite;
		}
		*/
	}

	public void ShowHelp(string message, Vector2 position, int player){

		if(player == 0){
			BSP1.SetActive(true);
			BSP1.GetComponent<RectTransform>().anchoredPosition = new Vector3(position.x, position.y, 0);
			int x = position.x >= 0 ? 1 : 0;
			int y = position.y >= 0 ? 1 : 0;
			BSP1.GetComponent<RectTransform>().pivot = new Vector2(x, y);
			Transform t = GetChildByName("Placeholder", BSP1.transform);
		
			t.GetComponent<Text>().text = message;

		}

		if(player == 1){
			BSP2.SetActive(true);
			BSP2.GetComponent<RectTransform>().anchoredPosition = new Vector3(position.x, position.y, 0);
			int x = position.x >= 0 ? 1 : 0;
			int y = position.y >= 0 ? 1 : 0;
			BSP2.GetComponent<RectTransform>().pivot = new Vector2(x, y);

			Transform t = GetChildByName("Placeholder", BSP2.transform);
		
			t.GetComponent<Text>().text = message;
		}

	}

	public void HideHelp(int player)
	{
		if (player == 0)
			BSP1.SetActive (false);
		else
			BSP2.SetActive(false);
	}

	public void StartTimer(float totalTime)
	{
		CancelInvoke ("TimePassed");
		_timeActual = totalTime;
		Invoke ("TimePassed", 0);

	}
	public void TimeTravel(float deltaTime)
	{
		CancelInvoke ("TimePassed");
		_timeActual = Mathf.Max(_timeActual - deltaTime,0);
		Invoke ("TimePassed", 0);
	}
	#endregion

	#region PRIVATE METHODS
	private Transform GetChildByName(string name, Transform t){
	
		for(int i = 0; i < t.childCount; i++){

			Debug.Log(t.name);

			if(t.GetChild(i).name == name){
				return t.GetChild(i);
			}
		}
		return null;

	}

	private void TimePassed()
	{
		_timeActual--;
		float segundos = _timeActual % 60;
		float minutos = _timeActual / 60;
		
		timer.text = ((int)minutos).ToString() +  ":" + ((int)segundos).ToString();
		if(_timeActual > 0)
		{
			Invoke("TimePassed",1);
		}
		else
		{
			if(OnTimeUp != null)
				OnTimeUp();
		}

	}
	#endregion
}
