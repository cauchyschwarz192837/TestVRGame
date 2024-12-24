using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OncollisionTag : MonoBehaviour
{


	public string newTag;
	public Text TagText;

	void Start()
	{
		gameObject.tag = "Cube";	
	}


        void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Ground")
		{
			gameObject.tag = newTag;
			TagText.text = newTag;
		}
	}
}
