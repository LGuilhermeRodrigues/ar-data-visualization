using MyNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBallBehaviourScript : MonoBehaviour
{
    public Color myColor;
    private Color oldColor;

    static float luisScale = 1f;
        private Vector3 commonScale = new Vector3(luisScale*0.3f, luisScale* 0.3f, luisScale*0.3f);
    private Vector3 labelScale = new Vector3(luisScale * 0.2f, luisScale * 0.2f, luisScale * 0.6f);

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
        var targetName = other.gameObject.transform.parent.name.Substring(11).ToLower();
        // if its the red ball
        if (other.gameObject.name.Contains("Deleter"))
        {
            
            Debug.Log("marker "+ targetName+ " has been touched");
            Debug.Log(other.gameObject.name + " saved");
            PlayerPrefs.DeleteKey(targetName + "_chartName");
            PlayerPrefs.DeleteKey(targetName + "_datasource");
            PlayerPrefs.DeleteKey(targetName + "_dim0");
            PlayerPrefs.DeleteKey(targetName + "_metric0");
            //Delete Everything
            ChartLoadScript loadScript = other.gameObject.transform.parent
                .GetComponentInChildren<ChartLoadScript>(true);
            loadScript.isLoaded = false;
            foreach (Transform child in other.gameObject.transform.parent.transform)
            {
                if (!string.Equals(child.gameObject.name, "GameObject"))
                {
                    if(!string.Equals(child.gameObject.name, "Deleter"))
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
            StartCoroutine(LoadAfterTime(3, loadScript, targetName));
        } else
        {
            // if its a choosing option
            if (other.gameObject.name.Split(":"[0]).Length > 1)
            {
                Debug.Log(other.gameObject.name + " saved");
                PlayerPrefs.SetString(other.gameObject.name.Split(":"[0])[0], other.gameObject.name.Split(":"[0])[1]);
                //Delete Everything
                ChartLoadScript loadScript = other.gameObject.transform.parent
                    .GetComponentInChildren<ChartLoadScript>(true);
                loadScript.isLoaded = false;
                foreach (Transform child in other.gameObject.transform.parent.transform)
                {
                    if (!string.Equals(child.gameObject.name, "GameObject"))
                    {
                        if (!string.Equals(child.gameObject.name, "Deleter"))
                        {
                            Destroy(child.gameObject);
                        }
                    }
                }
                StartCoroutine(LoadAfterTime(3, loadScript, targetName));
            } else { // if its a bar
                if (other.gameObject.name.Split("_"[0]).Length == 1)
                {
                    var parent = other.gameObject.transform.parent;
                    // criar uma _label, uma tooltip em cima da barra que está sendo colidida
                    GameObject tooltip = new GameObject(other.gameObject.name + "tooltip_label");
                    tooltip.transform.parent = other.gameObject.transform.parent.transform;
                    TextMesh text = tooltip.AddComponent<TextMesh>();
                    text.text = other.gameObject.name.Split("-"[0])[1];
                    text.anchor = TextAnchor.MiddleCenter;
                    tooltip.transform.rotation = other.gameObject.transform.rotation;
                    tooltip.transform.localScale = labelScale;
                    var tooltipPos = new Vector3(other.gameObject.transform.localPosition.x -0.1f, other.gameObject.transform.localPosition.y * 2 + labelScale.y, 0.0f);
                    //tooltip.transform.localPosition = new Vector3(other.gameObject.transform.localPosition.x -0.2f, other.gameObject.transform.localPosition.y * 2 + labelScale.y, 0.0f);
                    tooltip.transform.localPosition = tooltipPos;
                } else { // labels is ignored e.g. 6_label january_label

                }
            }
        }
    }

    IEnumerator LoadAfterTime(float time, ChartLoadScript loadScript, string imageTargetName)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        loadScript.Load(imageTargetName);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit=" + other.gameObject.name);
        var mesh = other.gameObject.GetComponent<MeshRenderer>();
        if (mesh)
        {
            mesh.material.color = oldColor;
        }

        //delete the tooltip
        foreach (Transform child in other.gameObject.transform.parent.transform)
        {
            if (child.gameObject.name.Contains("tooltip"))
            {
                Destroy(child.gameObject);
            }
        }




    }
}
