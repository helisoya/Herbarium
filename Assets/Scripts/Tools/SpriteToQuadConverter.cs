using UnityEngine;
using UnityEditor;

public class SpriteToQuadConverter
{
    [MenuItem("Tools/Convert SpriteRenderers To Quads")]
    static void ConvertSprites()
    {
        SpriteRenderer[] sprites = Object.FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sr in sprites)
        {
            if (sr.sprite == null) continue;

            GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

            quad.transform.position = sr.transform.position;
            quad.transform.rotation = sr.transform.rotation;

            Vector2 worldSize = sr.bounds.size;

            quad.transform.localScale = new Vector3(
                worldSize.x,
                worldSize.y,
                1f
            );

            Material mat = new Material(Shader.Find("Unlit/Texture"));
            mat.mainTexture = sr.sprite.texture;
            quad.GetComponent<MeshRenderer>().sharedMaterial = mat;

            quad.name = sr.gameObject.name + "_Quad";
        }
    }
}