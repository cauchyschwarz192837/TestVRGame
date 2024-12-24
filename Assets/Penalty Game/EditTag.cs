using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditTag : MonoBehaviour
{
    
 
    public string newTag;
    public Text TagText;


    public void Edit()
    {
	gameObject.tag = newTag;
	TagText.text = newTag;
    }
}
