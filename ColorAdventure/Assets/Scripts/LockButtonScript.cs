using FreeDraw;
using UnityEngine;
using UnityEngine.UI;

public class LockButtonScript : MonoBehaviour
{
    public GameObject targetCharacter; // The character whose drawing will be disabled
    public GameManagerScript gameManager; // Reference to the GameManagerScript

    private void Start()
    {
        // Attach the DisableDrawingOnTarget method to be called when the button is clicked
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // Method to disable drawing on the target character
    private void DisableDrawingOnTarget()
    {
        if (targetCharacter != null)
        {
            Drawable drawableComponent = targetCharacter.GetComponent<Drawable>();
            if (drawableComponent != null)
            {
                drawableComponent.DisableDrawing();
                Debug.Log("Drawing disabled on target character.");
            }
        }
    }

    // Method to be called when the button is clicked
    private void OnButtonClick()
    {
        // Disable drawing on the target character
        DisableDrawingOnTarget();

        // Notify the GameManager that a lock button has been pressed
        if (gameManager != null)
        {
            gameManager.OnLockButtonPressed(this);
            Debug.Log("Lock button pressed.");
        }
    }
}
