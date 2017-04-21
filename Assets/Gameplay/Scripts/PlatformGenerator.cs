using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public GameObject thePlatorm;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    //Coin Generator
    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;

    //Spikes
    public float randomSpikeThreshold;
    public ObjectPooler spikePool;

	// Use this for initialization
	void Start () {
        //platformWidth = thePlatorm.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[theObjectPools.Length];

        for(int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
	}
	


	// Update is called once per frame
	void Update () {
		if(transform.position.x < generationPoint.position.x)
        {
            //Randomly generates the range between the platforms
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            //Randomly selects the size of the platform
            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            } else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            //The x and y distance for each platform
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, heightChange, transform.position.z);

            //Instantiating objects (Currently best for testing)
            //Instantiate(/*thePlatorm*/ thePlatforms[platformSelector], transform.position, transform.rotation);

            //Pooling helps to use memory efficiently instead of creating and destroying
            //which uses more memory than required.
            GameObject newPlatform = theObjectPools[platformSelector].getPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            //Coin Generator
            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }

            //Spike Generator
            if(Random.Range(0f, 100f) < randomSpikeThreshold)
            {
                GameObject newSpike = spikePool.getPooledObject();

                float spikeXPosition = Random.Range(-platformWidths[platformSelector] / 2f + 1f, platformWidths[platformSelector] / 2f - 1f);

                Vector3 spikePosition = new Vector3(spikeXPosition, .5f, 0f);

                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, transform.position.y, transform.position.z);

        }
    }
}
