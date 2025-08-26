using UnityEngine;
using Vuforia; // Importante

public class D1_Angelita : DefaultObserverEventHandler
{
    [SerializeField] private DialogueController dialogue;
    [SerializeField] private int lineIndexOnFound = 1;  // Diálogo cuando se detecta el target
    [SerializeField] private int lineIndexOnLost = 0;   // Diálogo cuando se pierde el target
    [SerializeField] private bool triggerOnlyOnce = false;

    private bool _fired;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        if (triggerOnlyOnce && _fired) return;

        if (dialogue != null)
        {
            dialogue.ShowLineByIndex(lineIndexOnFound);
            _fired = true;
        }
        else
        {
            Debug.LogWarning("MyTargetEvent: asigna el DialogueController en el Inspector.");
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        if (dialogue != null)
        {
            dialogue.ShowLineByIndex(lineIndexOnLost); // 👈 vuelve a mostrar el diálogo 1
        }
    }
}
