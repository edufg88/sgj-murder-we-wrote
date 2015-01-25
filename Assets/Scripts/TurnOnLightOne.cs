using UnityEngine;
using System.Collections;

public class TurnOnLightOne : MonoBehaviour {

	private GameObject luz;
	public GameObject luzThree;

	public void OnTriggerEnter2D(Collider2D other){
		
		Debug.Log("Triggereando en la puerta, desde TurnOnLightOne");
		Debug.Log("Triggereo con ");

		luz = GameObject.Find("LightController");
		luz.GetComponent<ControlLuces>().TurnOnRoomOne();
		luz.GetComponent<ControlLuces>().TurnOnRoomThree();
		this.gameObject.SetActive(false);
		luzThree.SetActive(false);
		
	}
}
