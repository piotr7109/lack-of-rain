using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public class GFXManager : MonoBehaviour {

    #region Singleton

    public static GFXManager instance;

    void Awake() {
        instance = this;
        SetUpRefs();
    }

    #endregion

    private PostProcessingProfile postProcessingProfile;
    private Camera cam;
    private CameraPerlinShake cameraShake;
    private TimeManager timeManager;

    void SetUpRefs() {
        cam = Camera.main;
        postProcessingProfile = cam.GetComponent<PostProcessingBehaviour>().profile;
        cameraShake = cam.GetComponent<CameraPerlinShake>();
        timeManager = TimeManager.instance;
        SetUp();
    }

    void SetUp() {
        postProcessingProfile.motionBlur.enabled = false;
        postProcessingProfile.chromaticAberration.enabled = false;
        postProcessingProfile.bloom.enabled = false;
        GrainEffect(0);
        VignetteEffect(0);
        ChangeSaturation(1f);
    }


    public void DrunkEffect() {
        StartCoroutine(MakeDrunkEffect());
    }

    IEnumerator MakeDrunkEffect() {
        timeManager.SlowDownTime(.7f);
        postProcessingProfile.motionBlur.enabled = true;

        yield return new WaitForSeconds(10f);

        timeManager.ResetTime();
        postProcessingProfile.motionBlur.enabled = false;
    }

    public void DrugEffect() {
        StartCoroutine(MakeDrugEffect());
    }

    IEnumerator MakeDrugEffect() {
        postProcessingProfile.chromaticAberration.enabled = true;
        postProcessingProfile.bloom.enabled = true;

        yield return new WaitForSeconds(10f);

        postProcessingProfile.chromaticAberration.enabled = false;
        postProcessingProfile.bloom.enabled = false;
    }

    private HashSet<float> ids = new HashSet<float>();

    public void GrainEffect(float level, float instanceId = -1) {
        GrainModel.Settings settings = postProcessingProfile.grain.settings;

        settings.intensity = level / 5;
        postProcessingProfile.grain.settings = settings;

        if (instanceId != -1) {
            ids.Add(instanceId);
        }
    }

    public void GrainEffectTurnOff(float instanceId) {
        ids.Remove(instanceId);

        if (ids.Count == 0) {
            GrainEffect(0);
        }
    }

    public void VignetteEffect(float level) {
        VignetteModel.Settings settings = postProcessingProfile.vignette.settings;

        settings.intensity = level;
        postProcessingProfile.vignette.settings = settings;
    }

    public void CameraShakeEffect(float duration) {
        StartCoroutine(ShakeCamera(duration));
    }

    IEnumerator ShakeCamera(float duration) {
        cameraShake.Enable();

        yield return new WaitForSeconds(duration);

        cameraShake.Disable();
    }

    public void ChangeSaturation(float level) {
        ColorGradingModel.Settings settings = postProcessingProfile.colorGrading.settings;

        settings.basic.saturation = level;
        postProcessingProfile.colorGrading.settings = settings;
    }
}
