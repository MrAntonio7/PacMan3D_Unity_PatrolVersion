using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public Material material1; // Primer material
    public Material material2; // Segundo material
    public Material materialOriginal; //Material original
    public Material transparente;
    private Renderer objectRenderer; // Renderer del objeto al que cambiar los materiales
    public int materialIndex; // Índice del material a cambiar
    public float switchInterval = 0.7f; // Intervalo de cambio de material en segundos
    private Material[] materials;
    public bool ghostVulnerable = false;
    private bool isMaterial1Active = true; // Bandera para alternar entre los materiales
    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        materials = objectRenderer.materials;
    }
    void Start()
    {

        // Comenzar la coroutine para cambiar el material cada segundo
        //StartCoroutine(SwitchMaterialPeriodically());
    }

    public void StartVulnerableGhost()
    {

            ghostVulnerable = true;
            StartCoroutine(SwitchMaterialPeriodically());
        
        
    }
    public void StopVulnerableGhost()
    {
        ghostVulnerable=false;
        StopCoroutine(SwitchMaterialPeriodically());
    }
    public IEnumerator SwitchMaterialPeriodically()
    {
        while (ghostVulnerable && !GetComponentInParent<EnemyController>().deadGhost) { 
            

            // Alternar el material
            isMaterial1Active = !isMaterial1Active;

            if (isMaterial1Active && ghostVulnerable)
            {
                materials[materialIndex] = material1;
                objectRenderer.materials = materials; // Establecer el primer material
            }
            else if (!isMaterial1Active && ghostVulnerable)
            {
                materials[materialIndex] = material2;
                objectRenderer.materials = materials; // Establecer el segundo material
            }
            else
            {

                materials[materialIndex] = materialOriginal;
                objectRenderer.materials = materials; //Establece el color original
            }
            yield return new WaitForSeconds(switchInterval); // Esperar el intervalo especificado
        }
        materials[materialIndex] = materialOriginal;
        objectRenderer.materials = materials; //Establece el color original
    }

}
