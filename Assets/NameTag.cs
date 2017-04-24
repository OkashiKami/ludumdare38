using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour
{
    public StatSystem stats;
    TextMesh m;
    Canvas c;
    void Update () {
        if(!m) m = new GameObject("mesh").AddComponent<TextMesh>();
        if(!c) c = new GameObject("healthCanvas").AddComponent<Canvas>();

        setupNameTag(m);
        setupHealthbar(c);   
    }

    Image background, forground;
    public Sprite bbackground, fforground;
    void setupHealthbar(Canvas can)
    {
        can.transform.parent = transform;
        can.GetComponent<Transform>().localPosition = new Vector3(0, 0.079f, 0);
        can.GetComponent<Transform>().localScale = new Vector3(0.009353557f, 0.009353557f, 0.009353557f);
        can.transform.LookAt(Camera.main.transform);

        if (!background) background = new GameObject("background").AddComponent<Image>();
        background.transform.parent = can.transform;
        background.transform.localRotation = Quaternion.Euler(0 , 180, 0);
        background.transform.localScale = Vector3.one;
        bbackground = Resources.Load<Sprite>("background");
        background.GetComponent<Image>().sprite = bbackground;
        background.GetComponent<Image>().preserveAspect = true;
        background.GetComponent<Transform>().localPosition = Vector3.zero;


        if (!forground) forground = new GameObject("forground").AddComponent<Image>();
        forground.transform.parent = background.transform;
        forground.transform.localScale = Vector3.one;
        forground.transform.localRotation = Quaternion.Euler(0, 180, 0);
        fforground = Resources.Load<Sprite>("forground");
        forground.GetComponent<Image>().sprite = fforground;
        forground.GetComponent<Image>().preserveAspect = true;
        forground.GetComponent<Image>().type = Image.Type.Filled;
        forground.GetComponent<Image>().fillMethod = Image.FillMethod.Horizontal;
        forground.GetComponent<Image>().fillAmount = stats.Health;
        forground.GetComponent<Transform>().localPosition = Vector3.zero;

    }
    private void setupNameTag(TextMesh tm)
    {
        tm.transform.parent = transform;
        tm.transform.localPosition = Vector3.zero + new Vector3(0, 0.2f, 0);

        Transform head = null;
        foreach (Transform t in transform.parent.GetComponentInChildren<Transform>())
        {
            if (t.name.Contains("Head")) head = t;
        }
        if (!head) transform.localPosition = new Vector3(0, 2, 0);
        else if (head) transform.position = head.position + new Vector3(0, 0.5f, 0);
        tm.transform.localScale = new Vector3(-0.05f, 0.05f, 0.05f);
        tm.transform.LookAt(Camera.main.transform);
        tm.fontSize = 35;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.LowerCenter;
        string format = "{prefix}{main}{suffix}";

        format = format.Replace("{prefix}", stats.Prefix);
        format = format.Replace("{main}", stats.Main.Replace("[", "").Replace("]", ""));
        format = format.Replace("{suffix}", stats.Suffix);

        tm.text = format;
    }
}
