using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{

    GameObject player;
    PlayerController playerController;

    // Pause
    bool paused, inv, settings, controls;
    public Canvas Menu;
    public GameObject InventoryMenu, SettingsMenu, ControlsMenu;
    public Text xSens, ySens, crouchText, standText, boostText;

    //HUD
    public Canvas GroundedNotification, ActiveNotification;

    // Use this for initialization
    void Start()
    {

        //Player scripts
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        //Initial values to show for the sliders
        xSens.text = "" + playerController.mouseSensX;
        ySens.text = "" + playerController.mouseSensY;

        SettingsMenu.SetActive(false);
        ControlsMenu.SetActive(false);

        //Inventory menu
        Menu.enabled = false;
        controls = false;
        inv = false;
        settings = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);        
    }

    // Update is called once per frame
    void Update()
    {

        OpenPauseMenu();
        HUD();

    }

    void HUD()
    {
        if (playerController.Grounded())
            GroundedNotification.enabled = true;
        else
            GroundedNotification.enabled = false;
    }

    void OpenPauseMenu()
    {

        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            if(paused == true)
            {
                inv = true;
            }
        }
        if (paused)
        {
            // Stops time scale, pops up pause menu and brings cursor back to view
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            
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
        }
        if (!paused)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);

            Menu.enabled = false;
            SettingsMenu.SetActive(false);
            ControlsMenu.SetActive(false);
        }

    }

    public void OpenSettingsMenu()
    {
        Debug.Log("Settings Menu.");
        SettingsMenu.SetActive(true);
        ControlsMenu.SetActive(false);
        InventoryMenu.SetActive(false);

        settings = true;
        controls = false;
        inv = false;
    }

    public void OpenControlsMenu()
    {
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

}
