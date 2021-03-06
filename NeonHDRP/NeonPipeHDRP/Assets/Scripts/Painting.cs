using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Painting : MonoBehaviour
{
    public float timeBetweenSpawn = 0.1f;
    public GameObject decalPrefab;
    public Transform decalSpawnTransform;
    public string spraySFX;
    public string scoringSFX;

    [SerializeField] private InGamePlayerUI playerUI;

    [SerializeField] private float timeBetweenSpawnDecrease = 0.001f;
    [SerializeField] private float timeBetweenSpawnMin = 0.0001f;

    private int score = 0;

    private float t = 0f;
    private GameObject peinture;

    private PlayerController playerController;

    private void Start() {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > timeBetweenSpawn && playerController.isPainting)
        {
            peinture = Instantiate(decalPrefab, decalSpawnTransform.position, Quaternion.AngleAxis(90,Vector3.right));
            peinture.transform.localEulerAngles = new Vector3(90 - transform.eulerAngles.z , 90, 0);

            if(spraySFX != null)
            {
                Debug.Log("spray");
                AudioManager.Si().Play(spraySFX, AudioManager.Si().gameObject);
            }
            CheckIfInTag();

            t = 0;
        }

        UpdatePaintingRate();
    }

    public void AddScore(int scoreModifier) {
        score += scoreModifier;
    }

    public int GetScore() {
        return score;
    }

    private void UpdatePaintingRate() {
        timeBetweenSpawn = Mathf.Clamp(timeBetweenSpawn - timeBetweenSpawnDecrease * Time.deltaTime, timeBetweenSpawnMin, 1);
    }

    void CheckIfInTag()
    {
        Vector3 projectionPeinture = peinture.transform.position;// + peinture.transform.forward * 7.5f;
        foreach(GameObject obj in PipeManager.Si().chunksPipe)
        {
            foreach (GameObject tag in obj.GetComponent<Pipe>().listTag)
            {
                Transform t = tag.transform;

                Vector3 newProjectPeinture = projectionPeinture - t.position;
                float up = Vector3.Dot(newProjectPeinture, t.up);
                float forward = Vector3.Dot(newProjectPeinture, t.forward);
                float right = Vector3.Dot(newProjectPeinture, t.right);

                if (Mathf.Abs(up) < t.GetComponent<DecalProjector>().size.y/2f && Mathf.Abs(right) < t.GetComponent<DecalProjector>().size.x / 2f && forward > 0)
                {
                    score += 10;
                    playerUI.UpdateScoreText(score);

                    if (scoringSFX != null)
                    {
                        AudioManager.Si().Play(scoringSFX, AudioManager.Si().gameObject);
                    }

                    break;
                }

            }
        }
    }
}
