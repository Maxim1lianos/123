using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class sch : MonoBehaviour
{
    public bool isdead = false;
    public float timeRemaining = 0.1f;
    [SerializeField] Text CountGoal;
    [SerializeField] private GameObject mon;
    [SerializeField] private GameObject goal;
    // Переменные для хранения MeshRenderer'a сигналов светофора
   [SerializeField] MeshRenderer redLightMesh;
     // Переменные для хранения материалов
  [SerializeField] float switchingSpeed = 2f;
     // IEnumerator для смены цветов
     private IEnumerator coroutine;     
	Material monLightMaterial;
     [SerializeField] Material redLightMaterial;
     [SerializeField] Material greenLightMaterial;
  [SerializeField] private GameObject[] itemsArray;
    [SerializeField] private GameObject itemsParent;
GameObject gameObjec1;
 private int _totalItems = 0;
	private int goalcount;
    
    // Start is called before the first frame update
    void Start()
    {	
        _totalItems = itemsParent.transform.childCount;
 
        itemsArray = new GameObject[_totalItems];
 
        for(int i = 0; i < _totalItems; i++)
        {
            itemsArray[i] = itemsParent.transform.GetChild(i).gameObject;
        }
        //GetComponent<Rigidbody>().isKinematic = true;
         // Присваиваем переменной coroutine, IEnumerator СhangeСolorsTrafficLight
         coroutine = СhangeСolorsTrafficLight();
         // Запускаем IEnumerator СhangeСolorsTrafficLight
         StartCoroutine(coroutine);
    }
    private IEnumerator СhangeСolorsTrafficLight()
	{  
	    
	    redLightMesh.material = redLightMaterial;
	    yield return new WaitForSeconds(1f * switchingSpeed);
	    redLightMesh.material = greenLightMaterial;

	}

    void OnCollisionEnter(Collision collision)
    {	
	if(collision.relativeVelocity.magnitude >= 4)
	{ 

	  Debug.Log("Cтолкновение.");
	  gameObjec1 = collision.gameObject;
	  if (gameObjec1 == goal) {
	 goalcount =  int.Parse(CountGoal.text);
	 goalcount++;
	CountGoal.text = goalcount.ToString();
	 monLightMaterial =  redLightMaterial; }
	else {
	monLightMaterial = greenLightMaterial;	}
	GetComponent<Rigidbody>().isKinematic = false;
	foreach(GameObject i in itemsArray)
	{
	    if(i.name != "Cylinder")
		{
	    i.GetComponent<MeshRenderer>().material =  monLightMaterial ;
  	    i.GetComponent<Rigidbody>().isKinematic = false;
	    print(i.name);
		}
	}
	Destroy(gameObject);
      Vector3 pos = gameObject.transform.position;
      isdead = true;
      Instantiate(mon,pos, Quaternion.identity);
	}
    }
    // Update is called once per frame
    void Update()
    {
	if(isdead)
	{
	  timeRemaining -= Time.deltaTime;
	  if(timeRemaining < 0)
	  {
	    Destroy(gameObject); 
	  }
	}
    }
}