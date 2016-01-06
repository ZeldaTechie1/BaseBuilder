using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{

    GameObject player;
    PlayerController playerController;

    // Pause
    bool paused, controls;
    public Canvas InventoryMenu, SettingsMenu, ControlsMenu;
    public Text xSens, ySens, crouchText, standText, boostText;

    //HUD
    GameObject text, text2;
    Canvas standingText, crouchingText;

    // Use this for initialization
    void Start()
    {

        //Player scripts
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        //Initial values to show for the sliders
        xSens.text = "" + playerController.mouseSensX;
        ySens.text = "" + playerController.mouseSensY;
        crouchText.text = "" + playerController.crouchJump;
        standText.text = "" + playerController.standJump;
        boostText.text = "" + playerController.boostJump;

        SettingsMenu.enabled = false;
        ControlsMenu.enabled = false;

        //Inventory menu
        InventoryMenu.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);

        //HUD stuff
        text = GameObject.Find("StandingText");
        text2 = GameObject.Find("CrouchingText");
        if (text != null && text2 != null)
        {
            standingText = text.GetComponent<Canvas>();
            crouchingText = text2.GetComponent<Canvas>();
        }
        else if (text == null || text2 == null)
        {
            Debug.Log("Could not find crouch text or standing text");
        }

    }

    // Update is called once per frame
    void Update()
    {

        OpenPauseMenu();
        HUD();

    }

    void HUD()
    {

        //notifies whether player is standing or crouching
        if (!playerController.isCrouched)
        {
            crouchingText.enabled = false;
            standingText.enabled = true;
        }
        else if (playerController.isCrouched)
        {
            crouchingText.enabled = true;
            standingText.enabled = false;
        }

    }

    void OpenPauseMenu()
    {

        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        if (paused)
        {
            // Stops time scale, pops up pause menu and brings cursor back to view
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            
            InventoryMenu.enabled = true;           
        }
        if (!paused)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
           
            InventoryMenu.enabled = false;
            SettingsMenu.enabled = false;
            ControlsMenu.enabled = false;
        }

    }

    public void OpenSettingsMenu()
    {
        SettingsMenu.enabled = true;
        ControlsMenu.enabled = false;
    }

    public void OpenControlsMenu()
    {
        SettingsMenu.enabled = false;
        ControlsMenu.enabled = true;
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

}
