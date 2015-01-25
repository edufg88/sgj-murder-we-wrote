using UnityEngine;
using System.Collections;

public class TurnOnLightThree : MonoBehaviour {

	private GameObject luz;
	public GameObject luzOne;

	public void OnTriggerEnter2D(Collider2D other){

		Debug.Log("Triggereando en la puerta, desde TurnOnLightThree");
		Debug.Log("triggereando desde " + other.name);
	
		luz = GameObject.Find("LightController");
		luz.GetComponent<ControlLuces>().TurnOnRoomThree();
		luz.GetComponent<ControlLuces>().TurnOnRoomOne();
		this.gameObject.SetActive(false);
		luzOne.SetActive(false);

	}

}
