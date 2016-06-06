﻿using UnityEngine;
using System.Collections;

public class FirstPersonShooting : MonoBehaviour {

    public Camera playerCamera;
    public GameObject orbPrefab;
	public FPSPanelScript fpsPanel;

    public GameObject[] spellPrefab;
    public GameObject[] spellAreaPrefab;
    public float spellRange = 10f;

    public GameObject Map;

    public float orbSpeed = 20f;

    private GameObject currentSpellArea;
    private Vector3 spellAreaPosition;
    private int spellIndex = 0;

    void Start () {
        currentSpellArea = null;
    }
	
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject currentOrb = (GameObject)Instantiate(orbPrefab, playerCamera.transform.position + playerCamera.transform.forward, playerCamera.transform.rotation);
            currentOrb.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * orbSpeed, ForceMode.Impulse);

            Physics.IgnoreCollision(currentOrb.GetComponent<Collider>(), GetComponent<Collider>());
            if (currentSpellArea != null)
            {
                Destroy(currentSpellArea);
                currentSpellArea = null;
            }
			fpsPanel.TurnAllSpellsToFalse ();
        }

        if (Input.GetButtonDown("Fire2") && currentSpellArea != null)
        {
            Transform BaseSpellTransform = spellPrefab[spellIndex].GetComponent<Transform>();
            Vector3 positionSpell = new Vector3(currentSpellArea.transform.position.x, BaseSpellTransform.position.y, currentSpellArea.transform.position.z);
            Instantiate(spellPrefab[spellIndex], positionSpell, BaseSpellTransform.transform.rotation);


            if (currentSpellArea != null)
            {
                Destroy(currentSpellArea);
                currentSpellArea = null;
            }
			fpsPanel.StartCoolDownCurrentSpell ();
			fpsPanel.TurnAllSpellsToFalse ();
            fpsPanel.removeTuto();
        }
		/*
        if (Input.GetKeyDown("1"))
        {
            if (spellArea != null)
            {
                Destroy(spellArea);
                spellArea = null;
            }
            spellArea = (GameObject)Instantiate(spellAreaPrefab, gameObject.transform.position + (gameObject.transform.forward * 10), gameObject.transform.rotation);
            spellIndex = 0;
        }
        else if (Input.GetKeyDown("2"))
        {
            if (spellArea != null)
            {
                Destroy(spellArea);
                spellArea = null;
            }
            spellArea = (GameObject)Instantiate(spellAreaPrefab, gameObject.transform.position + (gameObject.transform.forward * 10), gameObject.transform.rotation);
            spellIndex = 1;
        }*/
    }

	public void newSpellIsSelected(int newSpellId) {
		if (currentSpellArea != null)
		{
			Destroy(currentSpellArea);
            currentSpellArea = null;
		}
        currentSpellArea = (GameObject)Instantiate(spellAreaPrefab[newSpellId], gameObject.transform.position + (gameObject.transform.forward * 10), gameObject.transform.rotation);
		spellIndex = newSpellId;
	}

}
