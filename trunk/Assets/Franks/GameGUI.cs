using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour {
	
	private Image imageHUDP1;
	private Image imageHUDP2;
	private GameObject BSP1;
	private GameObject BSP2;
	public Sprite emptyHUDP;
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

	void Start(){

		imageHUDP1 = GameObject.Find("HUDP1").GetComponent<Image>();
		imageHUDP2 = GameObject.Find("HUDP2").GetComponent<Image>();

		BSP1 = GameObject.Find("BubbleSpeechP1");
		BSP2 = GameObject.Find("BubbleSpeechP2");

		BSP1.SetActive (false);
		BSP2.SetActive (false);

	}

	
	public void SetIcon(Sprite sprite, int player){

		//Si player = 0 ==> P1
		//Si player = 1 ==> P2
		if(sprite == null)
		{
			sprite = emptyHUDP;
		}
		if(player == 0){
			imageHUDP1.sprite = sprite;
		}else if(player == 1){
			imageHUDP2.sprite = sprite;
		}

	}

	public void ShowHelp(string message, Vector2 posicion, int player){

		if(player == 0){
			BSP1.SetActive(true);
			BSP1.GetComponent<RectTransform>().anchoredPosition = new Vector3(posicion.x, posicion.y, 0);

			Transform t = GetChildByName("Placeholder", BSP1.transform);
		
			t.GetComponent<Text>().text = message;

		}

		if(player == 1){
			BSP2.SetActive(true);
			BSP2.GetComponent<RectTransform>().anchoredPosition = new Vector3(posicion.x, posicion.y, 0);

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
	private Transform GetChildByName(string name, Transform t){
	
		for(int i = 0; i < t.childCount; i++){

			Debug.Log(t.name);

			if(t.GetChild(i).name == name){
				return t.GetChild(i);
			}
		}
		return null;

	}
}
