using UnityEngine;
using System.Collections;

public class CuentaAtras : MonoBehaviour {

	private float tiempo = 0;
	public float tiempoCuentaAtras = 3;
	public float resta = 0;

	public int offsetX = 0;
	public int offsetY = 0;

	public GUISkin timerSkinGUI;

	public string hora = "";

	void Start(){
		NotificationCenter.DefaultCenter().AddObserver(this, "AumentarTiempo");
		NotificationCenter.DefaultCenter().AddObserver(this, "FinTiempo");
	}

	void Update(){

		tiempo = Time.time;

		Debug.Log(tiempoCuentaAtras);

		resta = tiempoCuentaAtras - tiempo;

		float segundos = resta % 60;
		float minutos = resta / 60;

		hora = ((int)minutos).ToString() +  ":" + ((int)segundos).ToString();

		if(resta <= 0){
			NotificationCenter.DefaultCenter().PostNotification(this, "FinTiempo");

		}


	}

	void OnGUI(){

		GUI.skin = timerSkinGUI;
		GUI.Label(new Rect(Screen.width / 2 + offsetX, Screen.height / 2 + offsetY, 1000, 100), hora);

		if(GUI.Button(new Rect(10, 130, 50, 50), "Sumar tiempo")){
			NotificationCenter.DefaultCenter().PostNotification(this, "AumentarTiempo");
		}

	}

	void AumentarTiempo(Notification notification){

		tiempoCuentaAtras = tiempoCuentaAtras + 60.0f;
	}

	void FinTiempo(){

		Debug.Log("Se ha acabado el tiempo");
		//Debug.Break();
	}

}
