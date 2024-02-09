using UnityEngine;

public class DrawingScript : MonoBehaviour
{
    public Material drawingMaterial;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Renderer renderer = hit.collider.GetComponent<Renderer>();

                if (renderer != null && renderer.material == drawingMaterial)
                {
                    Vector2 textureCoord = hit.textureCoord;
                    drawingMaterial.SetVector("_TextureCoord", new Vector4(textureCoord.x, textureCoord.y, 0, 0));
                }
            }
        }
    }
}
