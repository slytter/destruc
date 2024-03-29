using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour {
	public Light[] lights;
	float passedTime = 0f;

	public int current;
	public bool emptyClip;

	// Use this for initialization

	void Start () {
		reset (true);
	}
	
	// Update is called once per frame
	void Update () {
		passedTime += Time.deltaTime;
		if (current > 11) {
			indicateError ();
			return;
		}

		for (int i = 0; i < lights.Length; i++) {
			if (i == (current % 3)) {
				lights [i].intensity += (lights [i].intensity < 0.02f) ? passedTime / 50 : 0;
			} else {
				lights [i].intensity = 0f;
			}
		}
	}

	public void switchLight () {
		current++;
		passedTime = 0;
	}

	public void indicateError () {
		if (!emptyClip)
			reset (false);
		emptyClip = true;

		for (int i = 0; i < lights.Length; i++) {
			if (lights [i].intensity < 0.08f)
				lights [i].intensity += passedTime / 10;
			//lights [i].intensity = 0.12f;
			lights [i].color = new Color (255, 10, 10);
			
		}
	}

	public void reset (bool reload) {
		emptyClip = !reload;	
		current = reload ? 0 : current;
		foreach (var light in lights) {
			light.color = new Color (255, 255, 255);
			light.intensity = 0;
		}
	}
}
