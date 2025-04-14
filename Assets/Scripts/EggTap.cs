using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SceneManagement;
public class EggTap : MonoBehaviour
{
    // === Prefabs and References ===
    [SerializeField] private GameObject crackedEggPrefab;
    [SerializeField] private GameObject smokeEffectPrefab;
    [SerializeField] private GameObject character;
    [SerializeField] private Renderer eggRenderer;
    [SerializeField] private Animator eggAnimator;
    //[SerializeField] private CanvasGroup eggCanvasGroup;

    // === Configuration ===
    [SerializeField] private int tapsToCrack = 4;
    [SerializeField] private float eggRevealDelay = 1f;
    [SerializeField] private Texture[] crackStages;

    // === Internal State ===
    private int tapCount = 0;
    private bool isShaking = false;

    private void Start()
    {
        if (character != null)
            character.SetActive(false);
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        tapCount++;

        if (tapCount <= tapsToCrack)
        {
            Debug.Log("Crack sound / effect here!");

            // Swap texture based on tap
            if (tapCount - 1 < crackStages.Length && eggRenderer != null)
            {
                eggRenderer.material.mainTexture = crackStages[tapCount - 1];
                Debug.Log(tapCount - 1);
            }

            if (!isShaking)
            {
                isShaking = true;

                // Shake the Egg
                this.gameObject.transform
                    //.DOShakePosition(0.3f, 0.2f, 10, 90, false, true)
                    .DOShakeRotation(0.3f, 10f)
                    .OnComplete(() => isShaking = false);

                // Shake the Camera
                Camera.main.transform
                    .DOShakePosition(0.3f, 0.2f, 10, 90, false, true)
                    .SetEase(Ease.InOutQuad);
            }

            // Play animation
            if (eggAnimator != null)
            {
                switch (tapCount - 1)
                {
                    case 0:
                    case 1:
                        eggAnimator.SetTrigger("Idle");
                        break;
                    case 2:
                    case 3:
                        eggAnimator.SetTrigger("Move");
                        break;
                    case 4:
                        eggAnimator.SetTrigger("Crack");

                        //Delay before reveal
                        DOVirtual.DelayedCall(eggRevealDelay, () =>
                        {
                            if (eggRenderer.material != null)
                            {
                                eggRenderer.material.DOFade(0f, 1f).OnComplete(() =>
                                {
                                    CrackOpen(); // Reveal character after fade
                                });
                            }
                            else
                            {
                                CrackOpen(); // Fallback
                            }
                        });
                        break;
                }
                
            }
            else
            {
                Debug.Log("No animator found!");
            }
            SoundManager.Instance.PlayRandomCrack();
        }
    }

    void CrackOpen()
    {
        Debug.Log("Egg cracked open!");
        GameObject smoke = null;
        if (smokeEffectPrefab != null)
        {
            smoke = Instantiate(smokeEffectPrefab, smokeEffectPrefab.transform.position, smokeEffectPrefab.transform.localRotation);
        }

        // Delay, then show character and scale in
        DOVirtual.DelayedCall(0.5f, () =>
        {
            this.gameObject.SetActive(false);
            character.SetActive(true);
            character.transform.localScale = Vector3.zero;

            Vector3 newScale = new Vector3(0.3f, 0.3f, 0.3f);
            character.transform.DOScale(newScale, 1f)
                .SetEase(Ease.OutBack)
                .SetDelay(0.2f);

            //Play roar
            SoundManager.Instance.PlayRoar();
        });

        //Destroy the smoke effect after 2 seconds
        if (smoke != null)
        {
            Destroy(smoke, 2f);
        }

    }
    public void Restart()
    {
        SoundManager.Instance.sfxSource.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
