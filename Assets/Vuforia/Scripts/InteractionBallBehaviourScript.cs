using MyNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBallBehaviourScript : MonoBehaviour
{
    public Color myColor;
    private Color oldColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision="+other.gameObject.name);
        //other.GetComponent<MeshRenderer>().material.color = myColor;
        //this.GetComponent<MeshRenderer>().material.color = myColor;
        var mesh = other.gameObject.GetComponent<MeshRenderer>();
        if (mesh)
        {
            oldColor = mesh.material.color;
            mesh.material.color = myColor;
            //other.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
        if (other.gameObject.name.Split(":"[0]).Length>1)
        {
            Debug.Log(other.gameObject.name + " saved");
            PlayerPrefs.SetString(other.gameObject.name.Split(":"[0])[0], other.gameObject.name.Split(":"[0])[1]);
            //Delete Everything
            ChartLoadScript loadScript = other.gameObject.transform.parent
                .GetComponentInChildren<ChartLoadScript>(true);
            loadScript.isLoaded = false;
            foreach (Transform child in other.gameObject.transform.parent.transform)
            {
                if(!string.Equals(child.gameObject.name, "GameObject"))
                {
                    Destroy(child.gameObject);
                }
            }
        }
        if (other.gameObject.name.Contains("ImageTarget"))
        {
            Debug.Log("A marker has been touched");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit=" + other.gameObject.name);
        var mesh = other.gameObject.GetComponent<MeshRenderer>();
        if (mesh)
        {
            mesh.material.color = oldColor;
        }
    }
}
