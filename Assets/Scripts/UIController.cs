using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{

    GameObject player;
    PlayerController playerController;

    // Pause
    bool paused;
    public Canvas InventoryMenu;
    public Canvas SettingsMenu;

    //HUD
    GameObject text, text2;
    Canvas standingText;
    Canvas crouchingText;

    // Use this for initialization
    void Start()
    {

        //Player scripts
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        SettingsMenu.enabled = false;

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
        }

    }

    public void OpenSettingsMenu()
    {
        InventoryMenu.enabled = false;
        SettingsMenu.enabled = true;
    }

}
