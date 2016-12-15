using UnityEngine;
using System.Collections;

public class GlassBreak : MonoBehaviour
{

    public GameObject brokenGlassPrefab;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
            BreakGlass();
    }

    public void BreakGlass()
    {
        if (!brokenGlassPrefab) return;

        

        GameObject go = Instantiate(brokenGlassPrefab, transform.position, transform.rotation) as GameObject;

        gameObject.SetActive(false);
    }
}
