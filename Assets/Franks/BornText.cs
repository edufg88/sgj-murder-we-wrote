using UnityEngine;
using System.Collections;

public class BornText : MonoBehaviour {

	int prueba = 20;
	// Use this for initialization
	void Start () {
	
		NotificationCenter.DefaultCenter().AddObserver(this, "Bocadillo");

	}

	void Bocadillo(Notification notification){
		Debug.Log("Me llamas");
		TextoFlotante.Show(string.Format("+{0}!", prueba), "PointStarText", new FromWorldPointTextPositioner(Camera.main, this.transform.position, 1.5f, 50));
	}

}
