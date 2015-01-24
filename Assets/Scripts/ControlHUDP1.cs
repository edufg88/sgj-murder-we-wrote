using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlHUDP1 : MonoBehaviour {

	public Sprite texture1;
	public Sprite texture2;


	void Start(){

		NotificationCenter.DefaultCenter().AddObserver(this, "PonerTextureOne");
		NotificationCenter.DefaultCenter().AddObserver(this, "PonerTextureTwo");

	}

	void PonerTextureOne(Notification notification){
		GetComponent<Image>().sprite = texture1;

	}

	void PonerTextureTwo(Notification notification){
		GetComponent<Image>().sprite = texture2;
	}
	
}
