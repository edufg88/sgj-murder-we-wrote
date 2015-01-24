﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour 
{
	#region Exposed attributes
	public AudioClip ambientSound;
	public AudioClip flipSound;
	public AudioClip collectSound;
	public AudioClip damageSound;
	public AudioClip dieSound;
	#endregion

	#region Private attributes
	private Dictionary<string, AudioClip> _audioLibrary = new Dictionary<string, AudioClip>();
	private ArrayList _activeSoundObjects = new ArrayList();
	private const float _volume = 0.75f;
	#endregion

	#region Public methods
	public AudioSource Play (string soundName, bool loop = false)
	{
		if (_audioLibrary.ContainsKey(soundName))
		{
			GameObject go = new GameObject("(Audio) " + soundName);
			AudioSource source = go.AddComponent<AudioSource>();
			source.clip = _audioLibrary[soundName];
			source.loop = loop;
			source.volume = _volume;
			//Instantiate(go);
			source.Play();

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
		_audioLibrary["flip"] = flipSound;
		_audioLibrary["collect"] = collectSound;
		_audioLibrary["damage"] = damageSound;
		_audioLibrary["die"] = dieSound;

		// TODO: Remove as it is only for testing
		Play ("ambient", true);
		Play ("collect");
		StopAll();
	}

	void Update () 
	{
	
	}

	#endregion
}