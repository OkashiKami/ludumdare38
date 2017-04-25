using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(MobControl mob in FindObjectsOfType<MobControl>())
            {
                Destroy(mob.gameObject);
            }
            other.transform.position = transform.position;
            other.GetComponent<CharacterControllerLogic>().Active = false;
            other.gameObject.GetComponent<Animator>().SetFloat("Speed", 0);
            other.gameObject.GetComponent<Animator>().SetFloat("Direction", 0);
            other.gameObject.GetComponent<Animator>().SetFloat("Angle", 0);
            other.gameObject.GetComponent<Animator>().SetBool("Angle", false);
            other.gameObject.GetComponent<Animator>().SetFloat("JumpCurve", 0);
            other.gameObject.GetComponent<Animator>().SetFloat("CapsuleCurve", 0);
            FindObjectOfType<TPPad>().StartCoroutine(FindObjectOfType<TPPad>().GameRestart());
        }
    }
}
