using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntro : MonoBehaviour {

	Animator anim;

	void Awake () {
		anim = GetComponent<Animator>();
		Debug.Log(anim);	
	}
	
	public IEnumerator ShowLevel(string name){
		anim.Play("level_show");
		GetComponent<Text>().text = name;
		yield return new WaitForSeconds(1.5f);
		anim.Play("level_fadeOut");
		}
}
