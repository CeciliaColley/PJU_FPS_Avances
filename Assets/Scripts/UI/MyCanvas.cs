using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCanvas : MonoBehaviour
{
    Scope scope;

    // Start is called before the first frame update
    void Start()
    {
        scope = GetComponentInChildren<Scope>();
        StartCoroutine(ToggleScope());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ToggleScope()
    {
        while (true)
        {
            if (!playerHasGun())
            {
                yield return new WaitUntil(playerHasGun);
                scope.gameObject.SetActive(true);

            }
            else if (playerHasGun())
            {
                yield return new WaitUntil(() => !playerHasGun());
                scope.gameObject.SetActive(false);
            }
        }
    }

    private bool playerHasGun()
    {
        return true;
    }
}
