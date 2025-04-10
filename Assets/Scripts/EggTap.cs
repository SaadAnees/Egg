using UnityEngine;

public class EggTap : MonoBehaviour
{
    public GameObject crackedEggPrefab;
    public GameObject character;
    public int tapsToCrack = 4;

    public Texture[] crackStages; // Assign 4 textures in the Inspector
    private int tapCount = 0;

    public Renderer eggRenderer;
    public Animator eggAnimator;

    private void Start()
    {
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
                eggRenderer.material.mainTexture = crackStages[tapCount - 1];
                Debug.Log(tapCount - 1);
            }

            // Play animation
            if (eggAnimator != null)
            {
                switch (tapCount - 1)
                {
                    case 0:
                    eggAnimator.SetTrigger("Idle");
                        break;
                    case 1:
                        eggAnimator.SetTrigger("Idle");
                        break;
                    case 2:
                        eggAnimator.SetTrigger("Move");
                        break;
                    case 3:
                        eggAnimator.SetTrigger("Crack");
                        break;
                    default:
                        break;
                }
                
            }
            else
            {
                Debug.Log("No animator found!");
            }
        }
        else
        {
            CrackOpen();
        }
    }

    void CrackOpen()
    {
        //Instantiate(crackedEggPrefab, transform.position, transform.rotation);
        //Destroy(gameObject);

        if (character != null)
        {
            character.SetActive(true);
        }

        Debug.Log("Egg cracked open!");
    }
}
