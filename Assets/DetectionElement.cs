using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionElement : MonoBehaviour
{
    // Enorme zone qui renvoie tout les �l�ments interractibles � proximit�
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
        // Retourne tout les GPE pr�sent dans la zone de d�tection
        Collider2D[] l_InteractibleDetecte = Physics2D.OverlapCircleAll(transform.position, m_DetectionRange, m_LayerToDetect);

        // On v�rifie si le tableau n'est pas vide
        if (l_InteractibleDetecte.Length > 0)
        {
            // Pour chaque �l�ment (collider2D) dans ce tableau
            foreach (Collider2D item in l_InteractibleDetecte)
            {
                // On envoie un linecast dans sa direction
                RaycastHit2D l_TestCollision = Physics2D.Linecast(transform.position, item.transform.position, m_Ground);

                // Si l'objet touch� par le linecast est le m�me que celui d�tect� � l'origine,
                // �a veut dire que la vision n'est pas occult� par un �l�ment
                // Et si la distance de l'�l�ment d�tect� est inf�rieur � la distance d'activation
                if (l_TestCollision.collider == item && Vector2.Distance(item.transform.position, transform.position) < m_ActivationRange)
                {
                    // L'�l�ment est d�t�ct�
                    item.GetComponent<DetectionBehaviour>().IsDetected = true;
                }
                // sinon
                else
                {
                    // L'�l�ment n'est pas/plus d�t�ct�
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
