using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionElement : MonoBehaviour
{
    // Enorme zone qui renvoie tout les éléments interractibles à proximité
    public float m_DetectionRange = 30;
    // Zone d'activation
    public float m_ActivationRange = 10;
    public LayerMask m_LayerToDetect;
    public LayerMask m_Ground;

    void Update()
    {
        Detection();
    }

    public void Detection()
    {
        // Retourne tout les GPE présent dans la zone de détection
        Collider2D[] l_InteractibleDetecte = Physics2D.OverlapCircleAll(transform.position, m_DetectionRange, m_LayerToDetect);

        // On vérifie si le tableau n'est pas vide
        if (l_InteractibleDetecte.Length > 0)
        {
            // Pour chaque élément (collider2D) dans ce tableau
            foreach (Collider2D item in l_InteractibleDetecte)
            {
                // On envoie un linecast dans sa direction
                RaycastHit2D l_TestCollision = Physics2D.Linecast(transform.position, item.transform.position, m_Ground);

                // Si l'objet touché par le linecast est le même que celui détecté à l'origine,
                // ça veut dire que la vision n'est pas occulté par un élément
                // Et si la distance de l'élément détecté est inférieur à la distance d'activation
                if (l_TestCollision.collider == item && Vector2.Distance(item.transform.position, transform.position) < m_ActivationRange)
                {
                    // L'élément est détécté
                    item.GetComponent<DetectionBehaviour>().IsDetected = true;
                }
                // sinon
                else
                {
                    // L'élément n'est pas/plus détécté
                    item.GetComponent<DetectionBehaviour>().IsDetected = false;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_DetectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_ActivationRange);
    }
}
