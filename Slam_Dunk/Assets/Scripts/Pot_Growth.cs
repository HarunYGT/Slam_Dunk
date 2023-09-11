using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pot_Growth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI duration;
    [SerializeField] private int startDuration;
    
    IEnumerator Start()
    {
        duration.text = startDuration.ToString();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            startDuration--;
            duration.text = startDuration.ToString();
            if (startDuration == 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        GameManager.Instance.PotGrow(transform.position);
    }
}
