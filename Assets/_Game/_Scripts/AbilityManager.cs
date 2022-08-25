using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityManager : MonoBehaviour
{
    [Header("AbilityGen")]
    public int AbilitySelected;
    public GameObject[] Weapons;

    [Header("Player")]
    public TwinStickMovement player;
    public Renderer playerMesh;
    public Material[] playerMaterials;
   


   
    // Start is called before the first frame update
    void Start()
    {
        //failsafe so that Hack ability is default: 
        AbilitySelected = 1;
    }
    // Update is called once per frame
    void Update()
    {
        //press space to initiate ability change.
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ChangeAbility();
        }
    }
    //ability change starts a random # generation. #5 is to ensure that all four
    //options have a healthy chance of being picked. then run the ability select switch case.
     void ChangeAbility()
    {
            Debug.Log("Randomize...");
            int Ability = (UnityEngine.Random.Range(1, 5));
        AbilitySelected = Ability;
            Debug.Log(Ability);
        AbilitySelect();
        
    }
    //separate function in case you want to add any effects that don't require a specific mode.
    void AbilitySelect()
    {
        AbilityGenerate();
    }
    void TankMode()
    {
        //reduces player speed during tank mode
        player.playerSpeed = 3f;
    }

    void ExitTankMode()
    {
        //returns player speed
        player.playerSpeed = 10f;
    }
    //ability modes. "ability selected" int goes in for each case. turns on selected weapon, turns off unselected. 
    void AbilityGenerate()
    {

        int materialGo;
        materialGo = AbilitySelected;
        playerMesh.material = playerMaterials[materialGo];
        switch (AbilitySelected)
        {

            case 1:
                Debug.Log("SeverActivate");
                Weapons[3].SetActive(true);
                Weapons[0].SetActive(false);
                Weapons[1].SetActive(false);
                Weapons[2].SetActive(false);
                ExitTankMode();
                
                break;
            case 2:
                Debug.Log("HackActivate");
                Weapons[0].SetActive(true);
                Weapons[3].SetActive(false);
                Weapons[1].SetActive(false);
                Weapons[2].SetActive(false);
                ExitTankMode();

                break;
            case 3:
                Debug.Log("Tank");
                TankMode();
                Weapons[0].SetActive(false);
                Weapons[3].SetActive(false);
                Weapons[1].SetActive(true);
                Weapons[2].SetActive(false);
                break;
            case 4:
                Debug.Log("LauncherActivate");
                ExitTankMode();
                Weapons[2].SetActive(true);
                Weapons[0].SetActive(false);
                Weapons[1].SetActive(false);
                Weapons[3].SetActive(false);
                break;
            case 5:
                Debug.Log("buffer");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
