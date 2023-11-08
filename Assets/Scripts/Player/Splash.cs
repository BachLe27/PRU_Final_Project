using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    public List<SpriteRenderer> playerRenderers;
    public float duration = 0.5f;
    public Material flashMaterial;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    void Start()
    {
        playerRenderers = new List<SpriteRenderer>();
        playerRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
        originalMaterial = playerRenderers[0].material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    public IEnumerator FlashRoutine()
    {
        for (int i = 0; i < playerRenderers.Count; i++)
        {
            playerRenderers[i].material = flashMaterial;
        }
        // Swap to the flashMaterial.

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        for (int i = 0; i < playerRenderers.Count; i++)
        {
            playerRenderers[i].material = originalMaterial;
        }

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
}
