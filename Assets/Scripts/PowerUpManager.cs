using UnityEngine;
using System.Collections;

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

    private bool isJointTime = false;
    public float jointTimeLength = 10.0f;
    private float jointTimeLeft = 0.0f;
    private float _jointTimeSpeedMultiplier = 1.0f;
    private float defaultJointTimeSpeedMultiplier = 1.0f;
    public float fixedJointTimeSpeedMultiplier = 0.5f;


    private bool isPillsTime = false;
    public float pillsTimeLength = 10.0f;
    private float pillsTimeLeft = 0.0f;
    private float _pillsSpeedMultiplier = 1.0f;
    private float defaultPillsSpeedMultiplier = 1.0f;
    private float fixedPillsSpeedMultiplier = 2.0f;

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
	    if(this.isJointTime)
        {
            this.jointTimeLeft -= Time.deltaTime;
            if(this.jointTimeLeft <= 0.0f)
            {
                this._jointTimeSpeedMultiplier = defaultJointTimeSpeedMultiplier;
                this.isJointTime = false;
            }
        }
        if(this.isPillsTime)
        {
            this.pillsTimeLeft -= Time.deltaTime;
            if(this.jointTimeLeft <= 0.0f)
            {
                this._pillsSpeedMultiplier = defaultPillsSpeedMultiplier;
                this.isPillsTime = false;
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
            return true;
        }
        else
        {
            return false;
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
                break;
            case PowerUpType.PU_DADS_WHISKEY:
                this.hasDadsWhiskey = true;
                break;
            case PowerUpType.PU_PILLS:
                this._pillsSpeedMultiplier = fixedPillsSpeedMultiplier;
                this.jointTimeLeft = this.jointTimeLength;
                this.isPillsTime = true;
                break;
        }
    }
   
}
