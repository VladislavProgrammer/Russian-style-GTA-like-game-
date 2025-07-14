using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject shootEffectMP5;

    private void Awake()
    {
        StopEffect();
    }

    private void OnEnable()
    {
        EventManager.StartShootEvent += PlayEffect;
        EventManager.StopShootEvent += StopEffect;
    }

    private void OnDisable()
    {
        EventManager.StartShootEvent -= PlayEffect;
        EventManager.StopShootEvent -= StopEffect;
    }

    void PlayEffect() => shootEffectMP5.SetActive(true);
    void StopEffect() => shootEffectMP5.SetActive(false);
}
