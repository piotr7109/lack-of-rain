﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public class GFXManager : MonoBehaviour {

    #region Singleton

    public static GFXManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    private PostProcessingProfile postProcessingProfile;
    private Camera cam;
    private TimeManager timeManager;

    void Start() {
        cam = Camera.main;
        postProcessingProfile = cam.GetComponent<PostProcessingBehaviour>().profile;
        timeManager = TimeManager.instance;
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
}