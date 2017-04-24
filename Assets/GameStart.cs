using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public UIPanel mainui, gameui;
    public UILabel levelLabel;
	public void StartMe ()
    {
        
        if (gameui) gameui.alpha = 1;
        if (mainui) mainui.alpha = 0;

        FindObjectOfType<CBL>().begin();

    }

    void Update()
    {
        if (levelLabel) levelLabel.text = "Level " + FindObjectOfType<CBL>().Level;
    }
}
