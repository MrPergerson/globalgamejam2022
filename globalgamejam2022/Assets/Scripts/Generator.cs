using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    PlayerControls playerControls;
    Player player;

    [Header("Status")]
    [SerializeField] float repairProgress = 0;
    [SerializeField] float repairSpeed = 1;
    [SerializeField] bool isRepaired = false;
    [SerializeField] bool isRepairing = false;

    [Header("UI")]
    [SerializeField] Slider slider;

    [Header("Particles")]
    [SerializeField] ParticleSystem particles;

    public delegate void GeneratorRepaired();
    public event GeneratorRepaired OnGeneratorRepaired;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        Repair();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isRepaired && collision.tag == "Player")
        {
           
            BeginRepairing(collision);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isRepaired && collision.tag == "Player")
        {
            StopRepairing();

        }

    }
    
    private void Repair()
    {
        if (!isRepaired && isRepairing)
        {
            repairProgress += repairSpeed * Time.deltaTime;
            slider.value = repairProgress;

            if (repairProgress >= 1)
            {
                isRepaired = true;
                OnGeneratorRepaired?.Invoke();
                StopRepairing();
                //GameManager.Instance.EndGame();
            }
        }
    }

    private void BeginRepairing(Collider2D collision)
    {
        isRepairing = true;
        player = collision.gameObject.GetComponent<Player>();
        player.spotLight.gameObject.SetActive(false);
        player.pointLight.pointLightOuterRadius = 1.3f;
        player.fov.findingTargets = false;
        player.fov.ClearVisibleTargets();
        player.fov.ClearVisibleEnemyObjects();
    }

    private void StopRepairing()
    {
        isRepairing = false;
        if(player)
        {
            player.spotLight.gameObject.SetActive(true);
            player.pointLight.pointLightOuterRadius = 1f;
            player.fov.findingTargets = true;
            player = null;
        }
    }

}
