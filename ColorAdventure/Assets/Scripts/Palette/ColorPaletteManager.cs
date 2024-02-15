using UnityEngine;
using UnityEngine.UI;
using FreeDraw;
public class ColorPaletteManager : MonoBehaviour
{
    private void Start()
    {
        // Attach button click listeners for each button in the ColorPaletteManager GameObject
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => {
                Image image = button.GetComponent<Image>();
                Drawable.Pen_Colour = image.color;
            });
        }
    }
}
