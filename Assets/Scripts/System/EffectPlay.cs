using System.Collections;
using MySystem;
using UnityEngine;


public class EffectPlay : MonoBehaviour
{
    const int EFFECT_DATA = 5;

    
    public Vector3 playPos = new Vector3(99999, 99999, 99999);

    [SerializeField]
    MyDictionary<string, GameObject> effects;

    [SerializeField] float lifeTime = 1.3f;

    // Start is called before the first frame update
    void Start()
    {
        effects.Initialize();

        if (playPos == new Vector3(99999, 99999, 99999))
        {
            playPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(in string effectname, in Vector3? addPos = null, in Vector3? rot = null)
    {
        playPos = transform.position;
        
        playPos += addPos ?? Vector3.zero;
        if (rot != null)
        {
            transform.Rotate(rot.Value);
        }

        if (effects.Dict[effectname] != null)
        {
            GameObject currentEffect = Instantiate(effects.Dict[effectname], playPos, Quaternion.identity);

            StartCoroutine(DestroyObject(currentEffect, lifeTime));
        }
    }

    /// <summary>
    /// ÉuÉåÉXópÇ…çÏÇ¡ÇΩ
    /// </summary>
    /// <param name="effectname"></param>
    /// <param name="Pos"></param>
    /// <param name="rot"></param>
    public void PlayEffect(in string effectname, in Transform target)
    {
        if (effects.Dict[effectname] != null)
        {
            GameObject currentEffect = Instantiate(effects.Dict[effectname], target.position, target.rotation, target);

            StartCoroutine(DestroyObject(currentEffect, lifeTime));
        }
    }


    private IEnumerator DestroyObject(GameObject obj, float deley)
    {
        yield return new WaitForSeconds(deley);
        Destroy(obj);
    }
}
