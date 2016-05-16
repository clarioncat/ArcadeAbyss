using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
	public Obstacle hotrocks;
	public Obstacle coldrocks;
    public List<GemItems> Gems;
    private float velocityIncreasePercentage = 1f;
    public float velocityIncreaseStep = 0.1f;
    public float gravityStart = 0.8f;
    private static List<Items> poolGems = new List<Items>(); 

    void Start()
    {
        poolGems.Clear();
        StartCoroutine(CreateGems());
        StartCoroutine(CreateRocks());
        StartCoroutine(DifficultyIncrease());
    }

    private IEnumerator DifficultyIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            velocityIncreasePercentage += velocityIncreaseStep;
        }
    }
    
    public IEnumerator CreateGems()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1f / velocityIncreasePercentage, 3f / (velocityIncreasePercentage + 0.5f)));
            GetFromPool((Gems[Random.Range(0,2)])).InitializeItem(velocityIncreasePercentage);
        }
    }

    public IEnumerator CreateRocks()
    {
        while (true)
        {
            yield return new WaitForSeconds(2 / velocityIncreasePercentage);
            GetFromPool(hotrocks).InitializeItem(velocityIncreasePercentage);
            GetFromPool(coldrocks).InitializeItem(velocityIncreasePercentage);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public static void ReturnToPool(Items gem)
    {
        gem.gameObject.SetActive(false);
        poolGems.Add(gem);
    }

    private Items GetFromPool(Items gemItem)
    {
        Items itemBase = poolGems.Find(item => item.itemName == gemItem.itemName);

        if (itemBase != null)
        {
            itemBase.gameObject.SetActive(true);
            poolGems.Remove(itemBase);
            return itemBase; 
        }

        itemBase = Instantiate(gemItem);
        itemBase.gameObject.transform.SetParent(transform);

        return itemBase; 
    }
}