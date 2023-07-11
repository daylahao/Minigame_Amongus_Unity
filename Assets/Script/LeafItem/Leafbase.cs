using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Leafbase : MonoBehaviour
{
    private Vector3 screenPoint;
    float leafwidth, leafheight;
    public GameObject containertranform;
    public float widthcontainer, heightcontainer;
    private Vector3 offset;
    public Transform tranformexitdoor;
    Vector3 beginposition; 
    private void Start()
    {
        widthcontainer = containertranform.GetComponent<SpriteRenderer>().size.x / 2;
        heightcontainer = containertranform.GetComponent<SpriteRenderer>().size.y / 2;
        leafwidth = this.GetComponent<SpriteRenderer>().size.x/4;  //vì scale Object còn 0.5 nên chia cho 4
        leafheight = this.GetComponent<SpriteRenderer>().size.y/4;
    }
    private void OnMouseDown()
    {
        beginposition = this.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        this.GetComponent<Rigidbody2D>().velocity =curPosition - beginposition; // Tạo gia tốc môi trường không trọng lực khi kéo và buôn ra
        transform.position = curPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ExitDoor")
        {
            GameManager.Instance.NumberLeaf--;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RangeExit")
        {
            RunToExitDoor();
        }
    }
    private void Update()
    {
        float Xmin, Xmax, Ymin, Ymax;
        Xmin = containertranform.transform.position.x - widthcontainer + leafwidth;
        Xmax = containertranform.transform.position.x + widthcontainer - leafwidth;
        Ymin = containertranform.transform.position.y - heightcontainer + leafheight;
        Ymax = containertranform.transform.position.y + heightcontainer - leafheight;
        this.transform.position = new Vector3(Mathf.Clamp(transform.position.x,Xmin ,Xmax ), Mathf.Clamp(transform.position.y,Ymin, Ymax), transform.position.z); // Tính phạm vi mà lá có thể di chuyển
    }
    private void RunToExitDoor() //lá khi chạm vào collision tag RangeExit thì sẽ tự động di chuyển vào máy như bị hút
    {
        float x = this.transform.position.x, 
            y= this.transform.position.y, 
            z= this.transform.position.z;
        if (tranformexitdoor.position.x- leafwidth > this.transform.position.x)
        {
            x = 1f;
        }
        else if (tranformexitdoor.position.x- leafwidth < this.transform.position.x)
        {
            x = -1f;
        }
        if (tranformexitdoor.position.y > this.transform.position.y)
        {
            y=1f;
        }
        else if(tranformexitdoor.position.y < this.transform.position.y)
        {
            y = -1f;
        }
        this.transform.position += new Vector3(x, y, z)*Time.deltaTime;
    }
}
