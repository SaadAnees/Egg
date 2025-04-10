using UnityEngine;

public class EggTap : MonoBehaviour
{
    public GameObject crackedEggPrefab;
    public GameObject character;
    public int tapsToCrack = 4;

    public Texture[] crackStages; // Assign 4 textures in the Inspector
    private int tapCount = 0;

    public Renderer eggRenderer;

    private void Start()
    {
       // eggRenderer = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        tapCount++;

        if (tapCount < tapsToCrack)
        {
            Debug.Log("Crack sound / effect here!");

            // Swap texture based on tap
            if (tapCount - 1 < crackStages.Length && eggRenderer != null)
            {
               // Debug.Log("Swap texture");
                eggRenderer.material.mainTexture = crackStages[tapCount - 1];
            }
        }
        else
        {
            CrackOpen();
        }
    }

    void CrackOpen()
    {
        Instantiate(crackedEggPrefab, transform.position, transform.rotation);
        //Destroy(gameObject);

        if (character != null)
        {
            character.SetActive(true);
        }

        Debug.Log("Egg cracked open!");
    }
}
