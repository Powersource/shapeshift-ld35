using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Explosion
	{
		// Insert coordinates later for visible explosions too?
		public Explosion ()
		{
			GameObject explosion = new GameObject ();
			explosion.name = "Explosion";
			AudioSource sound = explosion.AddComponent<AudioSource> ();
			AudioClip soundClip = Resources.Load ("Explosion") as AudioClip;
			sound.clip = soundClip;
			sound.Play ();
			sound.volume = 0.5f;
			sound.loop = false;

			UnityEngine.Object.Destroy (explosion, 4f);
		}
	}
}

