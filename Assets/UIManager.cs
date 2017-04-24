using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UITexture[] keys = new UITexture[4];
    public UILabel levelLabel;
    void Start()
    {
        foreach (UITexture key in keys) key.alpha = 0;
    }
    void Update()
    {
        levelLabel.text = "Level: " + FindObjectOfType<CBL>().Level.ToString() + "\n" +
        "Score: " + FindObjectOfType<CBL>().Score.ToString() + "\n" +
        StopWatch.GetLable;
    }



    

    public void ActivateKey(int id) { try { keys[id].alpha = 1; } catch { } }
}
