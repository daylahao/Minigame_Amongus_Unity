using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject containertranform;
    public Transform tranformexitdoor;
    public GameObject containerleaf;
    float widthcontainer, heightcontainer;
    public int itemcount;
    // Start is called before the first frame update
    void Start()
    {
        //Lấy độ dài của khung để tính phạm vi sinh ra lá 
        widthcontainer = containertranform.GetComponent<SpriteRenderer>().size.x/2;
       heightcontainer = containertranform.GetComponent<SpriteRenderer>().size.y/2;
        RandomPositionItem();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void RandomPositionItem() // sinh ra lá theo vị trí ngẫu nhiên trên khung
    {
        if (GameManager.Instance.NumberLeaf < 1)
        {
            for (int i = 0; i < itemcount; i++)
            {
                float x, y, z;
                x = Random.Range((containertranform.transform.position.x - widthcontainer), (containertranform.transform.position.x + widthcontainer));
                y = Random.Range((containertranform.transform.position.y - heightcontainer), (containertranform.transform.position.y + heightcontainer));
                z = 0;
                Leafbase leafitem = Instantiate(GameManager.Instance.GetResourceFile<Leafbase>("Prefab/Leaf Item"), new Vector3(x, y, z), Quaternion.identity);
                leafitem.containertranform = containerleaf;
                leafitem.tranformexitdoor = tranformexitdoor;
            }
            GameManager.Instance.NumberLeaf = itemcount;
        }
    }
}
