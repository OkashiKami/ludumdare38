using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public UIPanel mainui, gameui;
	
	public void StartMe ()
    {
        
        if (gameui) gameui.alpha = 1;
        if (mainui) mainui.alpha = 0;

        FindObjectOfType<CBL>().begin();

    }
}
