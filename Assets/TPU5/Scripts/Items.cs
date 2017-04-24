using System;
using UnityEngine;

[Serializable]
public class Items
{
    public GameObject on;
    public GameObject off;

    public Items(GameObject on, GameObject off)
    {
        this.on = on;
        this.off = off;
        TurnOff();
    }
    public Items(GameObject on, GameObject off, bool onByDefault)
    {
        this.on = on;
        this.off = off;
        if (onByDefault) TurnOn();
        else TurnOff();
    }

    public Items()
    {
    }

    public void TurnOn()
    {
        if (on) on.SetActive(true);
        if (off) off.SetActive(false);
    }
    public void TurnOff()
    {
        if(on) on.SetActive(false);
        if(off) off.SetActive(true);
    }

    public bool Active
    {
        get
        {
            try
            {
                if (on.activeSelf) return true;
                else if (off.activeSelf) return false;
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }

    internal void setOn(Transform t)
    {
        this.on = t.gameObject;
    }
    internal void setOff(Transform t)
    {
        this.off = t.gameObject;
    }
    internal void setDefaultOn(bool on)
    {
        if (on) TurnOn();
        else TurnOff();
    }
}