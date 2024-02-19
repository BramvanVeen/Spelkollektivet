using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeDraw;

public class GameManagerScript : MonoBehaviour
{
    public GameObject startButton;
    public GameObject colorPalette;
    public GameObject lockItButtons;
    public GameObject fiveStarsImage; // Reference to the image of five stars

    private List<LockButtonScript> allLockButtons = new List<LockButtonScript>();

    // Start is called before the first frame update
    void Start()
    {
        SetPaletteAndlockItButtonsVisibility(false);
        SetFiveStarsImageVisibility(false);

        // Populate the list of all lock buttons
        LockButtonScript[] lockButtons = lockItButtons.GetComponentsInChildren<LockButtonScript>(true);
        allLockButtons.AddRange(lockButtons);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all target characters are null
        if (AreAllTargetCharactersNull())
        {
            // Show the image of five stars
            SetFiveStarsImageVisibility(true);
        }
    }

    public void OnStartButtonClick()
    {
        // Hide the start button
        SetStartButtonVisibility(false);

        // Show the color palette and LockItButtons
        SetPaletteAndlockItButtonsVisibility(true);
    }

    bool AreAllTargetCharactersNull()
    {
        foreach (var lockButton in allLockButtons)
        {
            if (lockButton != null && lockButton.targetCharacter != null)
            {
                // Check if the associated brush (Drawable component) is not null
                Drawable drawableComponent = lockButton.targetCharacter.GetComponent<Drawable>();
                if (drawableComponent != null)
                {
                    return false; // At least one target character or brush is not null
                }
            }
        }
        return true; // All target characters and associated brushes are null
    }

    void SetStartButtonVisibility(bool isVisible)
    {
        if (startButton != null)
        {
            startButton.SetActive(isVisible);
        }
    }

    void SetPaletteAndlockItButtonsVisibility(bool isVisible)
    {
        // Show/hide the color palette
        if (colorPalette != null)
        {
            colorPalette.SetActive(isVisible);
        }

        // Show/hide the LockItButtons and their child buttons
        if (lockItButtons != null)
        {
            lockItButtons.SetActive(isVisible);
        }
    }

    void SetFiveStarsImageVisibility(bool isVisible)
    {
        if (fiveStarsImage != null)
        {
            fiveStarsImage.SetActive(isVisible);
        }
    }

    public void OnLockButtonPressed(LockButtonScript lockButton)
    {
        // Check if the target character associated with the lock button is not null
        if (lockButton != null && lockButton.targetCharacter != null)
        {
            // Disable drawing on the target character
            Drawable drawableComponent = lockButton.targetCharacter.GetComponent<Drawable>();
            if (drawableComponent != null)
            {
                drawableComponent.DisableDrawing();
            }
        }

        // Check if all target characters are null
        if (AreAllTargetCharactersNull())
        {
            // Show the image of five stars or trigger any other desired action
            SetFiveStarsImageVisibility(true);
        }
    }
}
