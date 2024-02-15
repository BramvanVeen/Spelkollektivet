using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject startButton;
    public GameObject colorPalette;
    // Start is called before the first frame update
    void Start()
    {
        SetPaletteVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartButtonClick()
    {
        // Hide the start button
        SetStartButtonVisibility(false);

        // Show the color palette
        SetPaletteVisibility(true);
    }
    void SetStartButtonVisibility(bool isVisible)
    {
        if (startButton != null)
        {
            startButton.SetActive(isVisible);
        }
    }
    void SetPaletteVisibility(bool isVisible)
    {
        if (colorPalette != null)
        {
            colorPalette.SetActive(isVisible);
        }
    }
}
