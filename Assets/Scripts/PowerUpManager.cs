using UnityEngine;
using System.Collections;
using System;

public class PowerUpManager : MonoBehaviour 
{
    public enum PowerUpType
    {
        PU_NONE = 0,
        PU_JOINT = 1,
        PU_DADS_WHISKEY = 2,
        PU_PILLS = 3
    };

    private static PowerUpManager entity = null;

    private bool hasDadsWhiskey = false;
    private float drunkTimer = 0.0f;
    private float drunkTimeMultiplier = 1.5f;
    private float drunkBlurMaxValue = 1.0f;

    [Header("Boosts parameters")]
    [Space(10)]
    //joint
    private bool isJointTime = false;
    public float jointTimeLength = 10.0f;
    private float jointTimeLeft = 0.0f;
    private float _jointTimeSpeedMultiplier = 1.0f;
    private float defaultJointTimeSpeedMultiplier = 1.0f;
    public float fixedJointTimeSpeedMultiplier = 0.5f;

    private float maxStrengthX = 0.4f;
    private float maxStrengthY = 0.4f;
    private float jointTimeMultiplier = 2.0f;
    private float jointTimer = 0.0f;

    //pills
    private bool isPillsTime = false;
    public float pillsTimeLength = 10.0f;
    private float pillsTimeLeft = 0.0f;
    private float _pillsSpeedMultiplier = 1.0f;
    private float defaultPillsSpeedMultiplier = 1.0f;
    private float fixedPillsSpeedMultiplier = 2.0f;

    [Header("Boosts GOs")]
    [Space(10)]
    public GameObject[] boosts = null;

    [Header("Boosts Effects")]
    [Space(20)]
    public Blur drunkBlur = null;
    public Fisheye jointEye = null;
    public MotionBlur pillsBlur = null;
    public GameObject trailGO = null;

    public static bool IsPilled
    {
        get
        {
            return PowerUpManager.entity.isPillsTime;
        }
    }
    public static float JointTimeSpeedMultiplier
    {
        get
        {
            return PowerUpManager.entity._jointTimeSpeedMultiplier;
        }
    }
    public static float PillsSpeedMultiplier
    {
        get
        {
            return PowerUpManager.entity._pillsSpeedMultiplier;
        }
    }

	void Start () 
    {
        PowerUpManager.entity = this;
	}
	void Update () 
    {
        if(this.hasDadsWhiskey)
        {
            this.drunkTimer += Time.deltaTime;
            this.drunkBlur.blurSize = this.drunkBlurMaxValue * ((1 + Mathf.Sin(-Mathf.PI/4 + this.drunkTimer * this.drunkTimeMultiplier)) / 2);
        }

	    if(this.isJointTime)
        {
            this.jointTimeLeft -= Time.deltaTime;
            this.jointTimer += Time.deltaTime;

            this.jointEye.strengthX = this.maxStrengthX * Mathf.Sin(Mathf.PI + this.jointTimer * this.jointTimeMultiplier);
            this.jointEye.strengthY = this.maxStrengthY * Mathf.Sin(this.jointTimer * this.jointTimeMultiplier);

            if(this.jointTimeLeft <= 0.0f)
            {
                this._jointTimeSpeedMultiplier = defaultJointTimeSpeedMultiplier;
                this.isJointTime = false;
                this.jointEye.enabled = false;
            }
        }
        if(this.isPillsTime)
        {
            this.pillsTimeLeft -= Time.deltaTime;
            if (this.pillsTimeLeft <= 0.0f)
            {
                this._pillsSpeedMultiplier = defaultPillsSpeedMultiplier;
                this.isPillsTime = false;
                this.pillsBlur.enabled = false;
                this.trailGO.SetActive(false);
            }
        }
        
	}
    public static void PickUp(PowerUpType type)
    {
        PowerUpManager.entity.EnablePowerUp(type);
    }

    public static bool CanLostMiniGame()
    {
        if(PowerUpManager.entity.hasDadsWhiskey)
        {
            PowerUpManager.entity.hasDadsWhiskey = false;
            PowerUpManager.entity.drunkBlur.enabled = false;
            return false;
        }
        else
        {
            return true;
        }

    }

    void EnablePowerUp(PowerUpType type)
    {
        switch(type)
        {
            case PowerUpType.PU_JOINT:
                this._jointTimeSpeedMultiplier = fixedJointTimeSpeedMultiplier;
                this.jointTimeLeft = this.jointTimeLength;
                this.isJointTime = true;
                this.jointTimer = 0.0f;
                this.jointEye.enabled = true;
                GameController.Instance.DeviceController.UsedJoint();
                break;
            case PowerUpType.PU_DADS_WHISKEY:
                this.hasDadsWhiskey = true;
                this.drunkBlur.enabled = true;
                this.drunkTimer = 0.0f;
                break;
            case PowerUpType.PU_PILLS:
                this._pillsSpeedMultiplier = fixedPillsSpeedMultiplier;
                this.pillsTimeLeft = this.pillsTimeLength;
                this.isPillsTime = true;
                this.pillsBlur.enabled = true;
                this.trailGO.SetActive(true );
                break;
        }
    }

    public static GameObject GetNewPowerUp(int index = -1)
    {
        GameObject tmpGO = null;
        if(index == -1)
        {
            System.Random rand = new System.Random();
            index = rand.Next(0, PowerUpManager.entity.boosts.Length);
        }
        if(PowerUpManager.entity.boosts != null)
        {
            if(index >= 0 && index < PowerUpManager.entity.boosts.Length)
            {
                tmpGO = (GameObject)GameObject.Instantiate(PowerUpManager.entity.boosts[index]);
            }
        }
        return tmpGO;
    }
}
