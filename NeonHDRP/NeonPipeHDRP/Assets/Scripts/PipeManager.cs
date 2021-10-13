using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PipeManager : MySingleton<PipeManager>
{
    public GameObject player;
    public List<GameObject> chunksPipe;
    public GameObject chunkPrefab;
    public int maxNumberChunk = 3;
    public GameObject tagPrefab;
    public GameObject obstaclePrefab;

    [SerializeField]
    private float distanceParcouru = 0f;
    private float pipeLength = 32f;
    private Vector3 lastPositionPlayer;
    private Vector3 offsetObstacle = new Vector3(0, 0, 4);

    private int numberVerticalSlice = 15;
    private int numberHorizontalSlice = 5;

    private float difficulty = 0f;

    private void Awake()
    {
        lastPositionPlayer = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceParcouru += Mathf.Abs(lastPositionPlayer.z - player.transform.position.z);
        lastPositionPlayer = player.transform.position;

        if(distanceParcouru >= pipeLength)
        {
            GameObject newChunk = null;
            if (chunksPipe.Count >= maxNumberChunk)
            {
                GameObject chunk = chunksPipe[0];
                List<GameObject> tags = chunk.GetComponent<Pipe>().listTag;
                foreach(GameObject obj in tags)
                {
                    Destroy(obj);
                }
                tags.Clear();

                chunksPipe.RemoveAt(0);
                newChunk = chunk;
            }
            else
            {
                newChunk = Instantiate(chunkPrefab, player.transform.position, Quaternion.identity);
            }

            if(chunksPipe.Count > 0)
            {
                AttachPipe(chunksPipe[chunksPipe.Count - 1].GetComponent<Pipe>(), newChunk.GetComponent<Pipe>());
            }
            chunksPipe.Add(newChunk);
            StartCoroutine(SetUpPipe(newChunk.GetComponent<Pipe>()));

            distanceParcouru = 0;
        }

        difficulty += Time.deltaTime;
    }

    //Attache le pipe2 au pipe1
    void AttachPipe(Pipe pipe1, Pipe pipe2)
    {
        pipe2.transform.position = pipe1.endPoint.position;
        pipe2.transform.rotation = pipe1.endPoint.rotation;
    }

    IEnumerator SetUpPipe(Pipe pipe)
    {
        yield return new WaitForSeconds(0.2f);
        
        Vector3 pipePosition = pipe.transform.position;
        Vector3 offset = new Vector3(0,0,Random.Range(0, 16));
        float angle = Random.Range(0, 360);
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
        
        Debug.DrawLine(pipePosition + offset, pipePosition + offset + dir * 8, Color.red, 10f);

        RaycastHit hit;

        /*if (Physics.Raycast(pipePosition + offset, dir, out hit, 100)) 
        {
            Debug.Log("hello");
            Instantiate(tagPrefab, hit.point, Quaternion.identity);
        }*/

        GameObject tag = Instantiate(tagPrefab, pipePosition + offset + dir * 5f, Quaternion.LookRotation(dir, Vector3.up));
        tag.transform.SetParent(pipe.transform);
        pipe.listTag.Add(tag);
        //tag.transform.localEulerAngles = new Vector3(tag.transform.localEulerAngles.x, 90, 180);

        CreateObstacle(pipePosition + offsetObstacle);
    }

    void CreateObstacle(Vector3 startPoint)
    {
        //int verticalSlice = Random.Range(0, numberVerticalSlice);
        List<int> horizontalDirection = GetNumbersFromList(0, numberHorizontalSlice, 3);

        foreach(int horizontalSlice in horizontalDirection)
        {
            Debug.Log(horizontalSlice);
            List<int> verticalDirection = GetNumbersFromList(0, numberVerticalSlice, 5);
            foreach (int verticalSlice in verticalDirection)
            {
                float angle = (360 / numberVerticalSlice * verticalSlice);
                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

                Debug.DrawLine(startPoint + new Vector3(0,0, (pipeLength/numberHorizontalSlice) * horizontalSlice), startPoint + dir * 8, Color.blue, 10f);

                RaycastHit hit;
                if (Physics.Raycast(startPoint + new Vector3(0, 0, (pipeLength / numberHorizontalSlice) * horizontalSlice), dir, out hit, 100))
                {
                    Debug.Log("hello");
                    //Instantiate(tagPrefab, hit.point, Quaternion.identity);
                }
                Instantiate(obstaclePrefab, startPoint + new Vector3(0, 0, (pipeLength / numberHorizontalSlice) * horizontalSlice) + dir * 8, Quaternion.AngleAxis(angle + 90, Vector3.forward));

            }
        }
    }

    private List<int> GetNumbersFromList(int minInclusive, int maxExclusive, int numberToChoose)
    {

        if(maxExclusive - minInclusive + 1< numberToChoose)
        {
            Debug.Log("Error : Not Enough Range");
            return null;
        }

        List<int> range = new List<int>();
        for(int i = minInclusive; i< maxExclusive; i++)
        {
            range.Add(i);
        }

        List<int> randomNumbers = new List<int>();
        

        for(int i = 0; i<numberToChoose; i++)
        {
            randomNumbers.Add(GetRandomfromList(ref range));
        }




        return randomNumbers;
    }

    int GetRandomfromList(ref List<int> myList)
    {
        int pos = Random.Range(0, myList.Count);
        int x = myList[pos];
        myList.RemoveAt(pos);

        return x;
    }
}
