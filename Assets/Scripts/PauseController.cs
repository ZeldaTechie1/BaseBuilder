using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

    // Pause
    bool paused;
    public Canvas InventoryMenu;

    // Use this for initialization
    void Start ()
    {

        InventoryMenu.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);

    }
	
	// Update is called once per frame
	void Update ()
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
        }

    }

}
