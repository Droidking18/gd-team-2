using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this makes sure that the object has a collider for the script to run 
[RequireComponent(typeof(Collider))]
public class CoffeePickup : MonoBehaviour
{
    [SerializeField]
    private float _speedIncreaseAmount = 5f;
    [SerializeField]
    private float _powerupDuration = 3;

    [SerializeField]
    private GameObject _artToDisable = null;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerscript = other.gameObject.GetComponent<PlayerScript>();

        if (playerscript != null)
        {
            // power up sequence begins here
            StartCoroutine(PowerupSequence(playerscript));
        }
    }

    public IEnumerator PowerupSequence(PlayerScript playerscript)
    {
        //implement a soft disable
        _collider.enabled = false;
        _artToDisable.SetActive(false);
       
        ActivatePowerup(playerscript);

     
        yield return new WaitForSeconds(_powerupDuration);

        DeactivatePowerup(playerscript);


        Destroy(gameObject);
    }


    private void ActivatePowerup(PlayerScript playerscript)
    {
        playerscript.SetMoveSpeed(_speedIncreaseAmount);
    }

    private void DeactivatePowerup(PlayerScript playerscript)
    {
        playerscript.SetMoveSpeed(-_speedIncreaseAmount);
    }
}