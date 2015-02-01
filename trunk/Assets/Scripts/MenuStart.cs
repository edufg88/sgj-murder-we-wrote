using UnityEngine;
using System.Collections;

public class MenuStart : MonoBehaviour 
{
	public GameObject char1;
	public GameObject char2;
	public GameObject text1;
	public GameObject text2;
	public GameObject textHelp;
	public GameObject trigger;

	private bool pressed1 = false;
	private bool pressed2 = false;
	private bool help1 = false;
	private bool help2 = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		Bounds characterBound = char1.GetComponent<BoxCollider2D>().bounds;
		if(characterBound.Intersects(trigger.GetComponent<BoxCollider2D>().bounds))
		{
			text1.SetActive(true);
			help1 = true;
		}
		Bounds characterBound2 = char2.GetComponent<BoxCollider2D>().bounds;
		if(characterBound2.Intersects(trigger.GetComponent<BoxCollider2D>().bounds))
		{
			text2.SetActive(true);
			help2 = true;
		}
		if(help1 && help2)
		{
			textHelp.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			pressed1 = true;
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			pressed2 = true;
		}

		if (pressed1 && pressed2)
		{
			Application.LoadLevel("edu");
		}
	}
}
