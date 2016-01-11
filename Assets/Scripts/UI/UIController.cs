using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{

    GameObject player;
    PlayerController playerController;

    // Pause
<<<<<<< HEAD
    bool paused, controls;
    public Canvas InventoryMenu, SettingsMenu, ControlsMenu;
    public Text xSens, ySens, crouchText, standText, boostText;

    //HUD
    public Canvas StandingNotification, CrouchingNotification;
=======
    bool paused, inv, settings, controls;
    public Canvas Menu;
    public GameObject InventoryMenu, SettingsMenu, ControlsMenu;
    public Text xSens, ySens, crouchText, standText, boostText;

    //HUD
    public Canvas GroundedNotification, ActiveNotification;
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48

    // Use this for initialization
    void Start()
    {

        //Player scripts
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        //Initial values to show for the sliders
        xSens.text = "" + playerController.mouseSensX;
        ySens.text = "" + playerController.mouseSensY;
<<<<<<< HEAD
        crouchText.text = "" + playerController.crouchJump;
        standText.text = "" + playerController.standJump;
        boostText.text = "" + playerController.boostJump;

        SettingsMenu.enabled = false;
        ControlsMenu.enabled = false;

        //Inventory menu
        InventoryMenu.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);

        Debug.Log("Could not find crouch text or standing text");
        
=======

        SettingsMenu.SetActive(false);
        ControlsMenu.SetActive(false);

        //Inventory menu
        Menu.enabled = false;
        controls = false;
        inv = false;
        settings = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);        
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48
    }

    // Update is called once per frame
    void Update()
    {

        OpenPauseMenu();
        HUD();

    }

    void HUD()
    {
<<<<<<< HEAD

        //notifies whether player is standing or crouching
        /*
        if (!playerController.isCrouched)
        {
            CrouchingNotification.enabled = false;
            StandingNotification.enabled = true;
        }
        else if (playerController.isCrouched)
        {
            CrouchingNotification.enabled = true;
            StandingNotification.enabled = false;
        }
        */

=======
        /*
        if (playerController.Grounded())
            GroundedNotification.enabled = true;
        else
            GroundedNotification.enabled = false;
        */
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48
    }

    void OpenPauseMenu()
    {

        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
<<<<<<< HEAD
=======
            if(paused == true)
            {
                inv = true;
            }
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48
        }
        if (paused)
        {
            // Stops time scale, pops up pause menu and brings cursor back to view
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            
<<<<<<< HEAD
            InventoryMenu.enabled = true;           
=======
            if(!InventoryMenu.activeSelf && inv == true)
            {
                InventoryMenu.SetActive(true);
                controls = false;
                settings = false;
            }
            if(controls || settings)
            {
                InventoryMenu.SetActive(false);
            }
            Menu.enabled = true;
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48
        }
        if (!paused)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
<<<<<<< HEAD
           
            InventoryMenu.enabled = false;
            SettingsMenu.enabled = false;
            ControlsMenu.enabled = false;
=======

            Menu.enabled = false;
            SettingsMenu.SetActive(false);
            ControlsMenu.SetActive(false);
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48
        }

    }

    public void OpenSettingsMenu()
    {
<<<<<<< HEAD
        SettingsMenu.enabled = true;
        ControlsMenu.enabled = false;
=======
        Debug.Log("Settings Menu.");
        SettingsMenu.SetActive(true);
        ControlsMenu.SetActive(false);
        InventoryMenu.SetActive(false);

        settings = true;
        controls = false;
        inv = false;
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48
    }

    public void OpenControlsMenu()
    {
<<<<<<< HEAD
        SettingsMenu.enabled = false;
        ControlsMenu.enabled = true;
=======
        Debug.Log("Control Menu.");
        SettingsMenu.SetActive(false);
        ControlsMenu.SetActive(true);
        InventoryMenu.SetActive(false);

        settings = false;
        controls = true;
        inv = false;
        
    }

    public void OpenInventoryMenu()
    {
        Debug.Log("Inventory Menu.");
        SettingsMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        InventoryMenu.SetActive(true);

        settings = false;
        controls = false;
        inv = true;
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48
    }

    public void AdjustSensitivityX(float newSensX)
    {
        playerController.mouseSensX = newSensX;
        xSens.text = "" + playerController.mouseSensX;
    }
    public void AdjustSensitivityY(float newSensY)
    {
        playerController.mouseSensY = newSensY;
        ySens.text = "" + playerController.mouseSensY;
    }

<<<<<<< HEAD
    public void ChangeCrouchJump(float newCrouch)
    {
        playerController.crouchJump = newCrouch;
        crouchText.text = "" + playerController.crouchJump;
    }
    public void ChangeStandJump(float newStand)
    {
        playerController.standJump = newStand;
        standText.text = "" + playerController.standJump;
    }
    public void ChangeBoostJump(float newBoost)
    {
        playerController.boostJump = newBoost;
        boostText.text = "" + playerController.boostJump;
    }

=======
>>>>>>> d0c75dbac65bd6cd59344a9dc37c935a9ca72a48
}
