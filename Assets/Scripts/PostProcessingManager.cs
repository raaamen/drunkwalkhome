using System.Collections;
using System.Collections.Generic;
using Pixelplacement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : Singleton<PostProcessingManager>
{
    public VolumeProfile volumeProfile;
    Vignette vignette;
    ChromaticAberration chromaticAberration;
    LensDistortion lensDistortion;

    float distortionVal;
   
    void Awake()
    {
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if(!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));
        UnityEngine.Rendering.Universal.Vignette vignette;
        if(!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
        if(!volumeProfile.TryGet(out chromaticAberration)) throw new System.NullReferenceException(nameof(chromaticAberration));
        if(!volumeProfile.TryGet(out lensDistortion)) throw new System.NullReferenceException(nameof(lensDistortion));

        //not needed
        //if(!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));
    }
    
    void Start()
    {
        StartCoroutine("ProcessingFX");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessingFX(){
        while (GameManager.Instance.gameRunning){
            vignette.intensity.Override(Mathf.Sin(distortionVal*Time.time));
        }
    }
}
