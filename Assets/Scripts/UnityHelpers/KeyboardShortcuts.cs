using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyboardShortcuts : MonoBehaviour
{
    // Because I'm too lazy to pick up the vive controller and point and click
    public Button button;
    public KeyCode shortcutKey;

    private void Update()
    {
        if (Input.GetKeyDown(shortcutKey) && button != null)
        {
            if (button.onClick != null)
            {
                button.onClick.Invoke();
            }
        }
    }
}
