using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageController : MonoBehaviour 
{
	public GameObject canvasText;
	public float msgShowTime = 5f;

	#region Patron Singleton
	private static MessageController _instancia = null;
	public static MessageController Instancia {
		get
		{
			if(_instancia == null)
			{
				_instancia = FindObjectOfType<MessageController>();
			}
			return _instancia;
		}
	}
	#endregion

	public void ShowMessage(string text)
	{
		canvasText.gameObject.SetActive(true);
		canvasText.GetComponentInChildren<Text>().text = text;
		Invoke("HideMessage", msgShowTime);
	}

	public void HideMessage()
	{
		canvasText.gameObject.SetActive(false);
	}

	// Use this for initialization
	void Start () 
	{
		canvasText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
