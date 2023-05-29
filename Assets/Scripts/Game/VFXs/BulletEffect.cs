using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

public class BulletEffect : MonoBehaviour
{
    [SerializeField] private VisualEffect _vfx;
    [SerializeField] private Light2D _light;
    private float _lifeTimer = 1f;
    private float _lightLifeTimer = 0.5f;
    private string _effectPath = "Prefabs/VFXs/BulletEffect";

    public void Init(Vector3 inputVec)
    {
        gameObject.SetActive(true);
        _vfx.SetVector3("InputVector3", inputVec * -1f);
        _lifeTimer = 1f;
        _lightLifeTimer = 0.5f;
        _light.enabled = true;
    }

    void Update()
    {
        _lightLifeTimer -= Time.deltaTime;
        if (_lightLifeTimer < 0f)
            _light.enabled = false;

        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer < 0f)
        {
            gameObject.SetActive(false);
            Managers.Pool.StorePoolObject(_effectPath, gameObject);
        }
    }
}
