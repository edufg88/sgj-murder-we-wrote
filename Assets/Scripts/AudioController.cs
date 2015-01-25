using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour 
{
	#region Patron Singleton
	private static AudioController _instancia = null;
	public static AudioController Instancia{
		get
		{
			if(_instancia == null)
			{
				_instancia = FindObjectOfType<AudioController>();
			}
			return _instancia;
		}
	}
	#endregion

	#region Exposed attributes
	public AudioClip ambientSound;
	public AudioClip doorClose;
	public AudioClip doorOpen;
	public AudioClip footstepOne;
	public AudioClip footstepTwo;
	public AudioClip drink;
	public AudioClip fuerzaPolicial;
	public AudioClip miauGato;
	public AudioClip chicharra;
	public AudioClip doorBell;
	public AudioClip doorBellTwo;
	public AudioClip osHanPillado;
	public AudioClip phoneBell;
	public AudioClip pontAeri;
	public AudioClip bookFlip;
	public AudioClip cloth;
	public AudioClip knife;
	public AudioClip hit;
	public AudioClip metalClick;
	public AudioClip pain;
	public AudioClip wc;
	#endregion

	#region Private attributes
	private Dictionary<string, AudioClip> _audioLibrary = new Dictionary<string, AudioClip>();
	private ArrayList _activeSoundObjects = new ArrayList();
	private const float _volume = 0.75f;
	#endregion

	#region Public methods
	

	public AudioSource Play (string soundName,  float delay, bool loop = false)
	{
		if (_audioLibrary.ContainsKey(soundName))
		{
			GameObject go = new GameObject("(Audio) " + soundName);
			AudioSource source = go.AddComponent<AudioSource>();
			source.clip = _audioLibrary[soundName];
			source.loop = loop;
			source.volume = _volume;
			//Instantiate(go);
			//source.Play();
			source.PlayDelayed(delay);

			if (loop)
			{
				_activeSoundObjects.Add(go);
			}
			else
			{
				Destroy(go, source.clip.length);
			}

			return source;
		}
		else
		{
			return null;
		}
	}

	public void StopActive()
	{
		ArrayList copy = new ArrayList(_activeSoundObjects);
		foreach (GameObject go in _activeSoundObjects)
		{
			Destroy(go);
		}
		_activeSoundObjects.Clear();
		copy.Clear();
	}

	public void StopAll()
	{
		AudioSource[] sources = FindObjectsOfType<AudioSource>();
		foreach(AudioSource source in sources)
		{
			Destroy(source.gameObject);
		}
	}
	#endregion

	#region Monobehavior Events

	void Start () 
	{
		_audioLibrary["ambient"] = ambientSound;
		_audioLibrary["doorClose"] = doorClose;
		_audioLibrary["openDoor"] = doorOpen;
		_audioLibrary["footstepOne"] = footstepOne;
		_audioLibrary["footstepTwo"] = footstepTwo;
		_audioLibrary["drink"] = drink;
		_audioLibrary["fuerzaPolicial"] = fuerzaPolicial;
		_audioLibrary["miau"] = miauGato;
		_audioLibrary["chicharra"] = chicharra;
		_audioLibrary["doorBell"] = doorBell;
		_audioLibrary["doorBellTwo"] = doorBellTwo;
		_audioLibrary["phoneBell"] = phoneBell;
		_audioLibrary["pontAeri"] = pontAeri;
		_audioLibrary["bookFlip"] = bookFlip;
		_audioLibrary["cloth"] = cloth;
		_audioLibrary["knife"] = knife;
		_audioLibrary["hit"] = hit;
		_audioLibrary["metalClick"] = metalClick;
		_audioLibrary["pain"] = pain;
		_audioLibrary["wc"] = wc;
		// TODO: Remove as it is only for testing
		Play ("ambient", 0.0f, true);

	}

	void Update () 
	{
	
	}

	#endregion
}
