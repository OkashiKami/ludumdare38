using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioCollection
{
    public AudioSource source;

    public AudioClip footstep;
    public AudioClip attack;
    
}
public class MobControl : MonoBehaviour
{
    public Animator anim;
    private CharacterControllerLogic player;
    public AudioCollection collection = new AudioCollection();
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        collection.source = GetComponent<AudioSource>();
	}
	
    public void DealDamageToPlayer()
    {
        if(player != null)
            player.GetComponent<StatSystem>().reduceHealth(UnityEngine.Random.Range(1, 5));
    }

    public void FootStep()
    {
        if (collection.footstep)
            collection.source.clip = collection.footstep;
        collection.source.Play();
    }

	// Update is called once per frame
	void Update ()
    {
        
        if (FindObjectOfType<CharacterControllerLogic>())
            player = FindObjectOfType<CharacterControllerLogic>();
        else return;
        if (!player) return;
        if (player.GetComponent<StatSystem>().Health <= 0)
        {
            player = null;
            anim.SetBool("attack01", false);
        }
        if(player)
            transform.LookAt(player.transform.position, Vector3.up);
        if(player)
        {
            RaycastHit ray;
            if(Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.forward, out ray, 5f))
            {
                if(ray.collider.GetComponent<MobControl>())
                {
                    transform.Translate(Vector3.right * 1);
                }
            }
            if (Vector3.Distance(transform.position, player.transform.position) > 2.5f)
            {

                anim.SetBool("attack01", false);
                anim.SetBool("run", true);
                transform.Translate(Vector3.forward * 0.5f);
            }
            else
            {
                anim.SetBool("run", false);
                anim.SetBool("attack01", true);
            }
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("attack01", true);
        }

        
    }
}
