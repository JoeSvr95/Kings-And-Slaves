using UnityEngine;

public class LevelChanger : MonoBehaviour {
	// Este script será utilizado para cambiar los niveles
	public Animator anim;

	public void FadeToLevel(GameObject room){
		anim.SetTrigger("fade_out");
	}
}
