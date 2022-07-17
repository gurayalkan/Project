using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RayCast : MonoBehaviour
{
    RaycastHit hit;
    Door LeftDoor;
    [SerializeField] Door doorScript;
    [SerializeField] private bool _isHouseMode;
    private void Start()
    {
      
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 20))
            {
                if (hit.transform.tag == "Door")
                {
                    LeftDoor = hit.transform.gameObject.GetComponent<Door>();
                    LeftDoor.doorPlay();

                }
            }
        }
        if (doorScript.doorOpen == true)
        {

            
            StartCoroutine(WaitForSceneLoad());

        }

    }
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1.5f);
        if (_isHouseMode)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(2);

    }
}
