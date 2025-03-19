using Unity.Cinemachine;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject projectile; // The projectile prefab
    public Transform ShootPoint; // The point where the projectile will be spawned
   
    //Spawn the projectile when the space key is pressed
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
}
    
    //Instantiate the projectile at the ShootPoint position and rotation
    
public void Shoot()
{
    Instantiate(projectile, ShootPoint.position, ShootPoint.rotation);
}
}