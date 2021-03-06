using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
	[Header("Target Object")]
	public GameObject targetObject;
	[Header("Movement")]
	public bool toMove;
	public bool movementX;
	public bool movementY;
	public MinigameManager manager;

	[Header("Speed Increment")]
	public float speed = .5f;

	public GameObject trashcan;
	Vector2 currentMousePosition;
	Vector2 mouseDeltaPosition;
	Vector2 lastMousePosition;
	bool istouchpadactive;
	bool draging;

	GameObject dragingObj;

	public List<GameObject> machines = new List<GameObject>();
	private List<GameObject> currents = new List<GameObject>();
	
	Color highlight;
	
	void Start()
	{
		ResetMousePosition();

		ColorUtility.TryParseHtmlString("#6AFF4F", out highlight);
		manager.onRunningMinigame += deactivatetrash;
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(targetObject.transform.position);
		RaycastHit hit;


		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.tag == "Plant")
			{
				if(dragingObj!=null)
				foreach (GameObject child in machines)
				{
					if (child.GetComponent<Machine>() != null)
						if (child.GetComponent<Machine>().Done) continue;

					if (child.transform.GetChild(0).GetComponent<Renderer>() != null)
						if (child.transform.GetChild(0).GetComponent<Renderer>().materials.Length > 1)
						{
							child.transform.GetChild(0).GetComponent<Renderer>().materials[1].SetFloat("_position", 0.05f);
						}
				}
			}
            else
            {
				foreach (GameObject child in machines)
				{
					if (child.GetComponent<Machine>() != null)
						if (child.GetComponent<Machine>().Done) continue;

					if (child.transform.GetChild(0).GetComponent<Renderer>() != null)
						if (child.transform.GetChild(0).GetComponent<Renderer>().materials.Length > 1)
						{
							child.transform.GetChild(0).GetComponent<Renderer>().materials[1].SetFloat("_position", -1f);
						}
				}
			}
			if (hit.collider.tag == "Plant" || hit.collider.tag == "TeProduct" || hit.collider.tag == "HuProduct")
			{
				for(int i=0; i < hit.transform.childCount; i++)
                {
					if (hit.transform.GetChild(i).GetComponent<Renderer>() != null)
						currents.Add(hit.transform.GetChild(i).gameObject);
                }

				foreach(GameObject child in currents)
                {
					if (child.GetComponent<Renderer>().materials.Length > 1)
					{
						child.GetComponent<Renderer>().materials[1].SetFloat("_position", 0.1f);
					}
				}

			}
			else if (hit.collider.tag == "EnProduct")
			{
				for (int i = 0; i < hit.transform.GetChild(0).childCount; i++)
				{
					if (hit.transform.GetChild(0).GetChild(i).GetComponent<Renderer>() != null)
						currents.Add(hit.transform.GetChild(0).GetChild(i).gameObject);
				}

				foreach (GameObject child in currents)
				{
					if (child.GetComponent<Renderer>().materials.Length > 1)
					{
						child.GetComponent<Renderer>().materials[1].SetFloat("_position", 0.1f);
					}
				}
			}
			else
			{
				if (currents.Count>0)
				{
					foreach (GameObject child in currents)
					{
						if(child!=null)
						if (child.GetComponent<Renderer>().materials.Length > 1)
						{
							child.GetComponent<Renderer>().materials[1].SetFloat("_position", -1f);
						}
					}
					currents.Clear();
				}
			}		
		}
		//controll mouse
		if (istouchpadactive)
		{
			currentMousePosition = Input.mousePosition;
			mouseDeltaPosition = currentMousePosition - lastMousePosition;

			if (toMove)
			{
				if (movementX && movementY)
				{
					//Debug.Log(Camera.main.ScreenToWorldPoint(targetObject.transform.position));
					targetObject.transform.Translate(mouseDeltaPosition.x * speed, mouseDeltaPosition.y * speed, 0f);
					if (Display.displays.Length > 1)
					{
						//targetObject.transform.position = new Vector3(Mathf.Clamp(targetObject.transform.position.x, 0, Display.displays[1].renderingWidth), Mathf.Clamp(targetObject.transform.position.y * speed, 0, Display.displays[1].renderingHeight), targetObject.transform.position.z);
					}
				}
				else
				if (movementX)
				{
					targetObject.transform.Translate(mouseDeltaPosition.x * speed, 0f, 0f);

				}
				else
				if (movementY)
				{
					targetObject.transform.Translate(0f, mouseDeltaPosition.y * speed, 0f);

				}

			}


			lastMousePosition = currentMousePosition;
		}
	}

	//reset mouse pos
	public void ResetMousePosition()
	{
		currentMousePosition = Input.mousePosition;
		lastMousePosition = currentMousePosition;
		mouseDeltaPosition = currentMousePosition - lastMousePosition;
	}

	//active onmouse click
	public void TouchpadActivate()
	{
		ResetMousePosition();
		istouchpadactive = true;

	}

	//on mouse drag
	public void TouchpadHold()
	{
		if (draging)
		{
			if (dragingObj != null)
            {
				dragingObj.GetComponent<dragObject>().IsMoving();
				if (!dragingObj.GetComponent<dragObject>().canNotBeDestroy)
					trashcan.SetActive(true);
            }
            
				
				
		}

	}

	//on mouse up
	public void TouchpadDeactivate()
	{
		if (dragingObj == null)
		{
			Ray ray = Camera.main.ScreenPointToRay(targetObject.transform.position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				
				if (hit.collider.tag == "Plant" || hit.collider.tag == "EnProduct" || hit.collider.tag == "HuProduct" || hit.collider.tag == "TeProduct")
				{
					dragingObj = hit.collider.gameObject;
					dragingObj.GetComponent<dragObject>().MovingStart();
					draging = true;
					if(hit.collider.gameObject.GetComponent<Renderer>()!=null)
					if (hit.collider.gameObject.GetComponent<Renderer>().materials.Length > 1)
					{
						hit.collider.gameObject.GetComponent<Renderer>().materials[1].SetColor("_color", Color.blue);
						
					}
				}

			}
		}
		else
		{
			draging = false;
			dragingObj = null;
		}
		trashcan.SetActive(false);
		istouchpadactive = false;
	}

	void deactivatetrash(int i)
    {
		trashcan.SetActive(false);
	}

}
