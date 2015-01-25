using UnityEngine;
using System.Collections;

public class TurnOnLightTwo : MonoBehaviour {

	private GameObject luz;
	
	public void OnTriggerEnter2D(Collider2D other){
		
		Debug.Log("Triggereando en la puerta, desde TurnOnLightOTwo");
		Debug.Log("Trigereando con " + other.name);
		
		luz = GameObject.Find("LightController");
		luz.GetComponent<ControlLuces>().TurnOnRoomTwo();
		this.gameObject.SetActive(false);
		
	}
}
