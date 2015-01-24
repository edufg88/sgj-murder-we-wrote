using UnityEngine;
using System.Collections;

public class ControlLuces : MonoBehaviour {

	public Animation animationLightRoomOne;
	public Animation animationLightRoomTwo;
	public Animation animationLightRoomThree;
	

	#region Patron Singleton
	private static ControlLuces _instancia = null;
	public static ControlLuces Instancia{
		get
		{
			if(_instancia == null)
			{
				_instancia = FindObjectOfType<ControlLuces>();
			}
			return _instancia;
		}
	}
	#endregion

	void Start(){
	
		animationLightRoomOne = GetChildByName("LuzOne", this.transform).GetComponent<Animation>();

		animationLightRoomTwo = GetChildByName("LuzTwo", this.transform).GetComponent<Animation>();

		animationLightRoomThree = GetChildByName("LuzThree", this.transform).GetComponent<Animation>();
	}

	public void TurnOnRoomOne(){
		animationLightRoomOne.Play("Luz");
	}

	public void TurnOnRoomTwo(){
		animationLightRoomTwo.Play("Luz");
	}

	public void TurnOnRoomThree(){
		animationLightRoomThree.Play("Luz");
	}


	private Transform GetChildByName(string name, Transform t){

		for(int i = 0; i < t.childCount; i++){

			if(t.GetChild(i).name == name){
				return t.GetChild(i);
			}

		}

		return null;
	}

}
