using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
public class VehicleController : MonoBehaviour
{

    LogitechGSDK.LogiControllerPropertiesData proprieties;


    [Space(25)]
    [Header("                                               Wheel Colliders")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;


    [Space(25)]
    [Header("                                               Wheel Meshes")]
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;


    [Space(25)]
    [Header("                                               Sounds")]
    public AudioSource AcceleratorSound;
    public AudioSource RpmSound;
    public float Rpm0_1;

    public float AcceleratorPitch;
    public bool AcceleratorSoundisPlaying = false;
    public bool RpmSoundisPlaying = false;


    [Space(25)]
    [Header("                                               Text in Scene")]
    public Text speedText;
    public GameObject ErrorMessage;
    public Text ErrorMessageText;
    public Text RpmText;
    public Text GearsText;


    [Space(25)]
    [Header("                                               Motor Force")]
    public double motorForce;

    [Header("                                               Car Speed")]
    public float CarSpeed;


    [Space(25)]
    [Header("                                               Pedals")]
    public double ClutchPedal;

    [Space(15)]
    public float BrakePedal;
    public float BrakePower;
    public float BrakeForce;

    [Space(15)]
    public double AcceleratorPedal;


    [Space(25)]
    [Header("                                               RPM")]
    public float minRPM = 450f;
    public float maxRPM = 6000f;


    [Space(50)]
    [Header("                                               Gear R")]
    public float ClutchSpeed = 2f;
    public float MaxSpeedCarReverse;

    [Header("                                               Gear 1")]
    public float Gear1Speed = 5f;
    public float MaxSpeedCarGear1 = 0f;

    [Header("                                               Gear 2")]
    public float Gear2Speed = 10f;
    public float MaxSpeedCarGear2 = 0f;

    [Header("                                               Gear 3")]
    public float Gear3Speed = 20f;
    public float MaxSpeedCarGear3 = 0f;

    [Header("                                               Gear 4")]
    public float Gear4Speed = 35f;
    public float MaxSpeedCarGear4 = 0f;

    [Header("                                               Gear 5")]
    public float Gear5Speed = 45f;
    public float MaxSpeedCarGear5 = 0f;

    [Header("                                               Gear 6")]
    public float Gear6Speed = 60f;
    public float MaxSpeedCarGear6 = 0f;



    [Space(50)]
    [Header("Viteza")]
    public bool ClutchPressed = false;

    public int CurrentGear = 0;
    public int CurrentGearPrev = 0;
    public int ActualGear = 0;



    [Space(50)]
    public GameObject Idle;
    public GameObject StartEngineRPM;
    public GameObject StartEngine;
    public GameObject StopEngine;



    private float Speed;
    private float GasInput, ClutchInput, BrakeInput;



    [Space(100)]
    public float count = 0f;
    public float count2 = 0f;


    public bool istrue = false;
    public bool istrue2 = false;
    public bool istrue3 = false;



    public float targetRpm;

    public float SpeedRatio;
    public float SpeedRatio1;
    public float SpeedRatio2;


    public bool GearR = false;


    public bool istrue10 = false;
    public bool istrue20 = false;



    public Text RecomendedNextGear;
    public GameObject ArrowNext;


    public Text RecomendedPreviousGear;
    public GameObject ArrowPrevious;




    public GameObject TextPanta;

    public bool Gear1 = false;
    public bool Gear2 = false;
    public bool Gear3 = false;
    public bool Gear4 = false;
    public bool Gear5 = false;
    public bool Gear6 = false;

    public bool Gear33 = false;


    public GameObject EngineOFF;


    private void FixedUpdate()
    {


        StartCoroutine(CalculateSpeed());

        UpdateRPM();

        TextonScreen();

        AccSound();

        CarSpeed = (float)(Speed * 0.898);

        speedText.text = CarSpeed.ToString("F2");



        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);


            GasInput = 32767 - rec.lY;

            AcceleratorPedal = GasInput / 65535;
        }


        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);
            HShifter(rec);

            if (rec.rglSlider[0] > 0)
            {
                ClutchInput = 0;
            }
            else if (rec.rglSlider[0] < 0)
            {
                ClutchInput = rec.rglSlider[0] / -32768f;
            }
            ClutchInput = 32767 - rec.rglSlider[0];

            ClutchPedal = ClutchInput / 65535;
        }


        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            BrakeInput = 32767 - rec.lRz;
            BrakePedal = BrakeInput / 655.35f;
            BrakeForce = BrakePedal * BrakePower;
        }

        frontLeftWheelCollider.brakeTorque = BrakeForce;
        frontRightWheelCollider.brakeTorque = BrakeForce;
        rearLeftWheelCollider.brakeTorque = BrakeForce;
        rearRightWheelCollider.brakeTorque = BrakeForce;

        if (BrakePedal > 0.1)
        {
            WheelFrictionCurve forwardFrictionfrontLeft = frontLeftWheelCollider.forwardFriction;
            forwardFrictionfrontLeft.extremumSlip = 1;
            forwardFrictionfrontLeft.extremumValue = 2;
            frontLeftWheelCollider.forwardFriction = forwardFrictionfrontLeft;

            WheelFrictionCurve forwardFrictionfrontRight = frontRightWheelCollider.forwardFriction;
            forwardFrictionfrontRight.extremumSlip = 1;
            forwardFrictionfrontRight.extremumValue = 2;
            frontRightWheelCollider.forwardFriction = forwardFrictionfrontRight;

            WheelFrictionCurve forwardFrictionbackLeft = rearLeftWheelCollider.forwardFriction;
            forwardFrictionbackLeft.extremumSlip = 1;
            forwardFrictionbackLeft.extremumValue = 2;
            rearLeftWheelCollider.forwardFriction = forwardFrictionfrontLeft;

            WheelFrictionCurve forwardFrictionbackRight = rearRightWheelCollider.forwardFriction;
            forwardFrictionbackRight.extremumSlip = 1;
            forwardFrictionbackRight.extremumValue = 2;
            rearRightWheelCollider.forwardFriction = forwardFrictionfrontRight;
        }
        else
        {
            WheelFrictionCurve forwardFrictionfrontLeft = rearLeftWheelCollider.forwardFriction;
            forwardFrictionfrontLeft.extremumSlip = 0.1f;
            forwardFrictionfrontLeft.extremumValue = 0.5f;
            rearLeftWheelCollider.forwardFriction = forwardFrictionfrontLeft;

            WheelFrictionCurve forwardFrictionfrontRight = frontRightWheelCollider.forwardFriction;
            forwardFrictionfrontRight.extremumSlip = 0.1f;
            forwardFrictionfrontRight.extremumValue = 0.5f;
            frontRightWheelCollider.forwardFriction = forwardFrictionfrontRight;

            WheelFrictionCurve forwardFrictionbackLeft = rearLeftWheelCollider.forwardFriction;
            forwardFrictionbackLeft.extremumSlip = 0.1f;
            forwardFrictionbackLeft.extremumValue = 0.5f;
            rearLeftWheelCollider.forwardFriction = forwardFrictionfrontLeft;

            WheelFrictionCurve forwardFrictionbackRight = rearRightWheelCollider.forwardFriction;
            forwardFrictionbackRight.extremumSlip = 0.1f;
            forwardFrictionbackRight.extremumValue = 0.5f;
            rearRightWheelCollider.forwardFriction = forwardFrictionfrontRight;
        }






        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);


        void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
        {
            Vector3 pos;
            Quaternion rot;
            wheelCollider.GetWorldPose(out pos, out rot);
            trans.rotation = rot;
            trans.position = pos;
        }



        void HShifter(LogitechGSDK.DIJOYSTATE2ENGINES shifter)
        {
            GetComponent<ChangeCamera>();

            bool isNeutral = true;
            for (int i = 0; i < 128; i++)
            {
                if (shifter.rgbButtons[i] == 128)
                {
                    if (i == 12)
                        CurrentGear = 1;
                    else if (i == 13)
                        CurrentGear = 2;
                    else if (i == 14)
                        CurrentGear = 3;
                    else if (i == 15)
                        CurrentGear = 4;
                    else if (i == 16)
                        CurrentGear = 5;
                    else if (i == 17)
                        CurrentGear = 6;
                    else if (i == 18)
                        CurrentGear = -1;
                    isNeutral = false;
                }
            }
            if (isNeutral == true)
                CurrentGear = 0;
            else
            {
                CurrentGearPrev = CurrentGear;
            }

            if (ClutchPedal >= 0.5f)
            {
                ActualGear = CurrentGear;
            }
            else
            {
                if (CurrentGear == 0)
                {
                    ActualGear = 0;

                }
            }



            if (CurrentGearPrev > 0 && ClutchPedal <= 0.5f && (ActualGear == 0))
            {
                CurrentGearPrev = 0;
            }

            if (ClutchPedal >= 0.5f)
            {
                ErrorMessageText.text = "";
                ErrorMessage.SetActive(false);
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (CurrentGear == -1)
            {
                GearsText.text = "R";
            }
            else if (CurrentGear == 0)
            {
                GearsText.text = "N";
            }
            else if (CurrentGear == 1)
            {
                GearsText.text = "1";
            }
            else if (CurrentGear == 2)
            {
                GearsText.text = "2";
            }
            else if (CurrentGear == 3)
            {
                GearsText.text = "3";
            }
            else if (CurrentGear == 4)
            {
                GearsText.text = "4";
            }
            else if (CurrentGear == 5)
            {
                GearsText.text = "5";
            }
            else if (CurrentGear == 6)
            {
                GearsText.text = "6";
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////




            if (ActualGear == 1 && ClutchPedal >= 0.5f && ClutchPedal <= 0.95f)
            {
                if (istrue == false && CarSpeed <= 0.6533f)
                    count = 0;

                count += Time.deltaTime;

                istrue = true;
            }
            else
                istrue = false;


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (ActualGear == 1 && !ClutchPressed && (count >= 1f - 0.7f * AcceleratorPedal) && Idle.activeSelf)
            {
                ClutchPressed = true;
                if (AcceleratorPedal > 0)
                {
                    MaxSpeedCarGear1 = (float)(Gear1Speed + 33f * AcceleratorPedal);
                }
                else
                {
                    MaxSpeedCarGear1 = Gear1Speed;
                }


                if (ClutchPedal < 0.5f)
                {
                    if (CarSpeed >= MaxSpeedCarGear1)
                    {
                        float torqueMultiplier = 0.05f - (((CarSpeed - MaxSpeedCarGear1) / MaxSpeedCarGear1) / MaxSpeedCarGear1);
                        frontLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        frontRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                    }
                    else
                    {
                        frontLeftWheelCollider.motorTorque = (float)(motorForce);
                        frontRightWheelCollider.motorTorque = (float)(motorForce);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce);
                        rearRightWheelCollider.motorTorque = (float)(motorForce);
                    }
                }
                else if (istrue3 == false)
                {
                    frontLeftWheelCollider.motorTorque = 0f;
                    frontRightWheelCollider.motorTorque = 0f;
                    rearLeftWheelCollider.motorTorque = 0f;
                    rearRightWheelCollider.motorTorque = 0f;
                }

            }
            else
            {
                ClutchPressed = false;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (ActualGear == 2 && !ClutchPressed && Idle.activeSelf && CarSpeed >= 5.71f) // 500 RPM
            {
                ClutchPressed = true;
                if (AcceleratorPedal > 0)
                {
                    MaxSpeedCarGear2 = (float)(Gear2Speed + 59.5f * AcceleratorPedal);
                }
                else
                {
                    MaxSpeedCarGear2 = Gear2Speed;
                }


                if (ClutchPedal < 0.5f)
                {
                    if (CarSpeed > MaxSpeedCarGear2)
                    {
                        float torqueMultiplier = 0.05f - (((CarSpeed - MaxSpeedCarGear2) / MaxSpeedCarGear2) / MaxSpeedCarGear2);
                        frontLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        frontRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                    }
                    else
                    {
                        frontLeftWheelCollider.motorTorque = (float)(motorForce);
                        frontRightWheelCollider.motorTorque = (float)(motorForce);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce);
                        rearRightWheelCollider.motorTorque = (float)(motorForce);
                    }
                }
                else
                {
                    frontLeftWheelCollider.motorTorque = 0f;
                    frontRightWheelCollider.motorTorque = 0f;
                    rearLeftWheelCollider.motorTorque = 0f;
                    rearRightWheelCollider.motorTorque = 0f;
                }
            }
            else
            {
                ClutchPressed = false;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (ActualGear == 3 && !ClutchPressed && Idle.activeSelf && CarSpeed >= 10.75f) // 500 RPM
            {
                ClutchPressed = true;
                if (AcceleratorPedal > 0)
                {
                    MaxSpeedCarGear3 = (float)(Gear3Speed + 120.5f * AcceleratorPedal);
                }
                else
                {
                    MaxSpeedCarGear3 = Gear3Speed;
                }


                if (ClutchPedal < 0.5f)
                {
                    if (CarSpeed > MaxSpeedCarGear3)
                    {
                        float torqueMultiplier = 0.05f - (((CarSpeed - MaxSpeedCarGear3) / MaxSpeedCarGear3) / MaxSpeedCarGear3);
                        frontLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        frontRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                    }
                    else
                    {
                        frontLeftWheelCollider.motorTorque = (float)(motorForce);
                        frontRightWheelCollider.motorTorque = (float)(motorForce);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce);
                        rearRightWheelCollider.motorTorque = (float)(motorForce);
                    }
                }
                else
                {
                    frontLeftWheelCollider.motorTorque = 0f;
                    frontRightWheelCollider.motorTorque = 0f;
                    rearLeftWheelCollider.motorTorque = 0f;
                    rearRightWheelCollider.motorTorque = 0f;
                }
            }
            else
            {
                ClutchPressed = false;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (ActualGear == 4 && !ClutchPressed && Idle.activeSelf && CarSpeed >= 14.6f) // 500 RPM
            {
                ClutchPressed = true;
                if (AcceleratorPedal > 0)
                {
                    MaxSpeedCarGear4 = (float)(Gear4Speed + 145f * AcceleratorPedal);
                }
                else
                {
                    MaxSpeedCarGear4 = Gear4Speed;
                }


                if (ClutchPedal < 0.5f)
                {
                    if (CarSpeed > MaxSpeedCarGear4)
                    {
                        float torqueMultiplier = 0.05f - (((CarSpeed - MaxSpeedCarGear4) / MaxSpeedCarGear4) / MaxSpeedCarGear4);
                        frontLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        frontRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                    }
                    else
                    {
                        frontLeftWheelCollider.motorTorque = (float)(motorForce);
                        frontRightWheelCollider.motorTorque = (float)(motorForce);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce);
                        rearRightWheelCollider.motorTorque = (float)(motorForce);
                    }
                }
                else
                {
                    frontLeftWheelCollider.motorTorque = 0f;
                    frontRightWheelCollider.motorTorque = 0f;
                    rearLeftWheelCollider.motorTorque = 0f;
                    rearRightWheelCollider.motorTorque = 0f;
                }
            }
            else
            {
                ClutchPressed = false;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (ActualGear == 5 && !ClutchPressed && Idle.activeSelf && CarSpeed >= 18.39f) // 500 RPM
            {
                ClutchPressed = true;
                if (AcceleratorPedal > 0)
                {
                    MaxSpeedCarGear5 = (float)(Gear5Speed + 162f * AcceleratorPedal);
                }
                else
                {
                    MaxSpeedCarGear5 = Gear5Speed;
                }


                if (ClutchPedal < 0.5f)
                {
                    if (CarSpeed > MaxSpeedCarGear5)
                    {
                        float torqueMultiplier = 0.05f - (((CarSpeed - MaxSpeedCarGear5) / MaxSpeedCarGear5) / MaxSpeedCarGear5);
                        frontLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        frontRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                    }
                    else
                    {
                        frontLeftWheelCollider.motorTorque = (float)(motorForce);
                        frontRightWheelCollider.motorTorque = (float)(motorForce);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce);
                        rearRightWheelCollider.motorTorque = (float)(motorForce);
                    }
                }
                else
                {
                    frontLeftWheelCollider.motorTorque = 0f;
                    frontRightWheelCollider.motorTorque = 0f;
                    rearLeftWheelCollider.motorTorque = 0f;
                    rearRightWheelCollider.motorTorque = 0f;
                }
            }
            else
            {
                ClutchPressed = false;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (ActualGear == 6 && !ClutchPressed && Idle.activeSelf && CarSpeed >= 29.16f) // 500 RPM
            {
                ClutchPressed = true;
                if (AcceleratorPedal > 0)
                {
                    MaxSpeedCarGear6 = (float)(Gear6Speed + 182f * AcceleratorPedal);
                }
                else
                {
                    MaxSpeedCarGear6 = Gear6Speed;
                }


                if (ClutchPedal < 0.5f)
                {
                    if (CarSpeed > MaxSpeedCarGear6)
                    {
                        float torqueMultiplier = 0.07f - (((CarSpeed - MaxSpeedCarGear6) / MaxSpeedCarGear6) / MaxSpeedCarGear6);
                        frontLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        frontRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                        rearRightWheelCollider.motorTorque = (float)(motorForce * torqueMultiplier);
                    }
                    else
                    {
                        frontLeftWheelCollider.motorTorque = (float)(motorForce);
                        frontRightWheelCollider.motorTorque = (float)(motorForce);
                        rearLeftWheelCollider.motorTorque = (float)(motorForce);
                        rearRightWheelCollider.motorTorque = (float)(motorForce);
                    }
                }
                else
                {
                    frontLeftWheelCollider.motorTorque = 0f;
                    frontRightWheelCollider.motorTorque = 0f;
                    rearLeftWheelCollider.motorTorque = 0f;
                    rearRightWheelCollider.motorTorque = 0f;
                }
            }
            else
            {
                ClutchPressed = false;
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (ActualGear == -1 && !ClutchPressed && Idle.activeSelf)
            {
                ClutchPressed = true;

                if (AcceleratorPedal > 0)
                {
                    MaxSpeedCarReverse = (float)(ClutchSpeed + 24f * AcceleratorPedal);
                }
                else
                {
                    MaxSpeedCarReverse = ClutchSpeed;
                }

                if (ClutchPedal < 0.5f)
                {
                    if (CarSpeed > MaxSpeedCarReverse)
                    {
                        float torqueMultiplier = 0.03f - (((CarSpeed - MaxSpeedCarReverse) / MaxSpeedCarReverse) / MaxSpeedCarReverse);
                        frontLeftWheelCollider.motorTorque = (float)(-motorForce * torqueMultiplier);
                        frontRightWheelCollider.motorTorque = (float)(-motorForce * torqueMultiplier);
                        rearLeftWheelCollider.motorTorque = (float)(-motorForce * torqueMultiplier);
                        rearRightWheelCollider.motorTorque = (float)(-motorForce * torqueMultiplier);
                    }
                    else
                    {
                        frontLeftWheelCollider.motorTorque = (float)(-motorForce);
                        frontRightWheelCollider.motorTorque = (float)(-motorForce);
                        rearLeftWheelCollider.motorTorque = (float)(-motorForce);
                        rearRightWheelCollider.motorTorque = (float)(-motorForce);
                    }
                }
                else
                {
                    frontLeftWheelCollider.motorTorque = 0f;
                    frontRightWheelCollider.motorTorque = 0f;
                    rearLeftWheelCollider.motorTorque = 0f;
                    rearRightWheelCollider.motorTorque = 0f;
                }
            }
            else
            {
                ClutchPressed = false;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        IEnumerator CalculateSpeed()
        {
            Vector3 lastPosition = transform.position;
            yield return new WaitForFixedUpdate();
            Speed = (lastPosition - transform.position).magnitude / Time.deltaTime;
        }
    }


    private void UpdateRPM()
    {



        if (!StartEngineRPM.activeSelf && Idle.activeSelf)
        {
            if (ActualGear == 1)
            {
                SpeedRatio = Mathf.Clamp01((CarSpeed - 5.88f) / (39f - 5.88f));

                SpeedRatio1 = Mathf.Clamp01((CarSpeed - 0f) / (5.88f - 0f));



                if (CarSpeed >= 5f && istrue2 == false)
                {
                    istrue2 = true;
                }



                if (istrue2 == false)
                {
                    if (CarSpeed < 5.88 && CarSpeed > 0)
                    {
                        targetRpm = Mathf.Lerp(500f, 900f, SpeedRatio1);
                    }
                    else
                    {
                        targetRpm = Mathf.Lerp(900f, maxRPM, SpeedRatio);
                    }
                }

                if (istrue2 == true)
                {
                    if (CarSpeed > 0.6)
                    {
                        if (CarSpeed < 5.88 && CarSpeed > 0)
                        {
                            targetRpm = Mathf.Lerp(450f, 900f, SpeedRatio1);
                        }
                        else
                        {
                            targetRpm = Mathf.Lerp(900f, maxRPM, SpeedRatio);
                        }
                    }
                    else
                    {
                        istrue2 = false;
                    }

                }


                RpmText.text = targetRpm.ToString("F0");
            }
            else if (ActualGear == 2)
            {
                SpeedRatio = Mathf.Clamp01((CarSpeed - 5.14f) / (68.54f - 5.14f));

                targetRpm = Mathf.Lerp(minRPM, maxRPM, SpeedRatio);

                RpmText.text = targetRpm.ToString("F0");
            }
            else if (ActualGear == 3)
            {
                SpeedRatio = Mathf.Clamp01((CarSpeed - 9.68f) / (129f - 9.68f));

                targetRpm = Mathf.Lerp(minRPM, maxRPM, SpeedRatio);

                RpmText.text = targetRpm.ToString("F0");
            }
            else if (ActualGear == 4)
            {
                SpeedRatio = Mathf.Clamp01((CarSpeed - 13.14f) / (175.2f - 13.14f));

                targetRpm = Mathf.Lerp(minRPM, maxRPM, SpeedRatio);

                RpmText.text = targetRpm.ToString("F0");
            }
            else if (ActualGear == 5)
            {
                SpeedRatio = Mathf.Clamp01((CarSpeed - 16.551f) / (220.68f - 16.551f));

                targetRpm = Mathf.Lerp(minRPM, maxRPM, SpeedRatio);

                RpmText.text = targetRpm.ToString("F0");
            }
            else if (ActualGear == 6)
            {
                SpeedRatio = Mathf.Clamp01((CarSpeed - 29.16f) / (350f - 29.16f));

                targetRpm = Mathf.Lerp(minRPM, 6000, SpeedRatio);

                RpmText.text = targetRpm.ToString("F0");
            }
            else if (ActualGear == -1)
            {
                SpeedRatio = Mathf.Clamp01((CarSpeed - 2.55f) / (28.7f - 2.55f));

                SpeedRatio1 = Mathf.Clamp01((CarSpeed - 0f) / (2.55f - 0f));

                if (CarSpeed >= 1f && istrue2 == false)
                {
                    istrue2 = true;
                }



                if (istrue2 == false)
                {
                    if (CarSpeed < 2.55 && CarSpeed > 0)
                    {
                        targetRpm = Mathf.Lerp(500f, 900f, SpeedRatio1);
                    }
                    else
                    {
                        targetRpm = Mathf.Lerp(900f, maxRPM, SpeedRatio);
                    }
                }

                if (istrue2 == true)
                {
                    if (CarSpeed > 0.142)
                    {
                        if (CarSpeed < 2.55 && CarSpeed > 0)
                        {
                            targetRpm = Mathf.Lerp(450f, 900f, SpeedRatio1);
                        }
                        else
                        {
                            targetRpm = Mathf.Lerp(900f, maxRPM, SpeedRatio);
                        }
                    }
                    else
                    {
                        istrue2 = false;
                    }
                }
                RpmText.text = targetRpm.ToString("F0");
            }
        }
    }


    private void AccSound()
    {
        AcceleratorSound.pitch = Mathf.Lerp(1f, 1.5f, SpeedRatio2);
        if (StartEngine.activeSelf)
        {
            AcceleratorSound.Stop();
        }
        else if (SpeedRatio2 == 0)
        {
            if (AcceleratorSoundisPlaying)
            {
                AcceleratorSound.Stop();
                AcceleratorSoundisPlaying = false;
            }
        }
        else if (ClutchPedal < 0.5f && CurrentGear != 0)
        {
            AcceleratorSoundisPlaying = false;
        }
        else
        {
            if (!AcceleratorSoundisPlaying)
            {
                if (ClutchPedal >= 0.5f || CurrentGear == 0 || (ClutchPedal >= 0.5f && CurrentGear != 0))
                {
                    AcceleratorSound.Play();
                    AcceleratorSoundisPlaying = true;
                }
            }
        }


        if (targetRpm >= 800)
            Rpm0_1 = (targetRpm - 800f) / (6000f - 800f);

        if (Rpm0_1 > 0)
            RpmSound.pitch = Mathf.Lerp(1f, 2f, Rpm0_1);


        if ((ClutchPedal >= 0.5f || CurrentGear == 0) && Idle.activeSelf && AcceleratorPedal >= 0f)
        {

            SpeedRatio2 = Mathf.Clamp01(((float)AcceleratorPedal - 0f) / (1f - 0f));


            targetRpm = Mathf.Lerp(800f, maxRPM, SpeedRatio2);


            if (Rpm0_1 > 0.01f)
            {
                if (RpmSoundisPlaying)
                {
                    RpmSound.Stop();
                    RpmSoundisPlaying = false;
                }
            }


            RpmText.text = targetRpm.ToString("F0");
        }
        else if (ClutchPedal < 0.5f)
        {


            if (StartEngine.activeSelf)
            {
                RpmSound.Stop();
            }
            if (targetRpm >= 800f)
            {
                if (!RpmSoundisPlaying)
                {
                    AcceleratorSound.Stop();
                    RpmSound.Play();
                    RpmSoundisPlaying = true;
                }
            }
            else
            {
                RpmSound.Stop();
                RpmSoundisPlaying = false;
            }



        }
        else if (AcceleratorPedal >= 0 && (ClutchPedal >= 0.5f && ActualGear != 0))
        {
            RpmSound.Stop();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Panta"))
        {
            istrue3 = true;
        }

        if (other.CompareTag("Panta2"))
        {
            istrue3 = false;
        }
    }


    private void TextonScreen()
    {

        if (ClutchPedal > 0.25 && CurrentGear != 0)
        {
            istrue10 = true;
        }

        if (ClutchPedal > 0.25 && CurrentGear == 0 && istrue10)
        {
            istrue20 = true;
        }
        else if (CurrentGear != 0)
        {
            istrue20 = false;
        }



        if (ClutchPedal < 0.25 && CurrentGear == 0 && istrue10 && !istrue20)
        {
            if (!StartEngine.activeSelf)
            {
                StartEngine.SetActive(true);
                StopEngine.SetActive(true);
                Idle.SetActive(false);
                ErrorMessage.SetActive(true);
                ErrorMessageText.text = "Nu ai apăsat pedala de ambreiaj atunci când ai schimbat treapta de viteză";
            }
            istrue10 = false;
            istrue20 = false;
        }


        if (StartEngine.activeSelf)
        {
            istrue2 = false;
        }




        if (StartEngine.activeSelf)
        {
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
            rearLeftWheelCollider.motorTorque = 0f;
            rearRightWheelCollider.motorTorque = 0f;
        }


        if (istrue3 == true)
        {
            TextPanta.SetActive(true);
            frontLeftWheelCollider.motorTorque = (float)(-100);
            frontRightWheelCollider.motorTorque = (float)(-100);
            rearLeftWheelCollider.motorTorque = (float)(-100);
            rearRightWheelCollider.motorTorque = (float)(-100);
        }
        else if (istrue3 == false)
        {
            TextPanta.SetActive(false);
        }




        if (CurrentGear == -1 && CarSpeed > 3)
        {
            GearR = true;
        }
        else
        {
            GearR = false;
        }














        if (CarSpeed >= 0 && CarSpeed < 11.42) // 1000 RPM
        {
            if (CurrentGear >= 1)
            {
                ArrowNext.SetActive(false);
                RecomendedNextGear.text = "";
            }
            else
            {
                ArrowNext.SetActive(true);
                RecomendedNextGear.text = "1";
            }
        }
        else if (CarSpeed >= 11.42 && CarSpeed < 25.8) // 1200 RPM
        {
            if (CurrentGear >= 2)
            {
                ArrowNext.SetActive(false);
                RecomendedNextGear.text = "";
            }
            else
            {
                ArrowNext.SetActive(true);
                RecomendedNextGear.text = "2";
            }
        }
        else if (CarSpeed >= 25.8 && CarSpeed < 35) // 1200 RPM
        {
            if (CurrentGear >= 3)
            {
                ArrowNext.SetActive(false);
                RecomendedNextGear.text = "";
            }
            else
            {
                ArrowNext.SetActive(true);
                RecomendedNextGear.text = "3";
            }
        }
        else if (CarSpeed >= 35 && CarSpeed < 44.13) // 1200 RPM
        {
            if (CurrentGear >= 4)
            {
                ArrowNext.SetActive(false);
                RecomendedNextGear.text = "";
            }
            else
            {
                ArrowNext.SetActive(true);
                RecomendedNextGear.text = "4";
            }
        }
        else if (CarSpeed >= 44.13 && CarSpeed < 70) // 1200 RPM
        {
            if (CurrentGear >= 5 )
            {
                ArrowNext.SetActive(false);
                RecomendedNextGear.text = "";
            }
            else
            {
                ArrowNext.SetActive(true);
                RecomendedNextGear.text = "5";
            }
        }
        else if (CarSpeed >= 70) // 1200 RPM
        {
            if (CurrentGear == 6)
            {
                ArrowNext.SetActive(false);
                RecomendedNextGear.text = "";
            }
            else
            {
                ArrowNext.SetActive(true);
                RecomendedNextGear.text = "6";
            }
        }








































        if (CurrentGear == 0)
        {
            if (CarSpeed < 1)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "R";
            }
            else
            {
                ArrowPrevious.SetActive(false);
                RecomendedPreviousGear.text = "";
            }
        }
        else if (CurrentGear == 1)
        {
            ArrowPrevious.SetActive(false);
            RecomendedPreviousGear.text = "";
        }
        else if (CurrentGear == 2)
        {
            if (CarSpeed < 11.42)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "1";
            }
            else
            {
                ArrowPrevious.SetActive(false);
                RecomendedPreviousGear.text = "";
            }
        }
        else if (CurrentGear == 3)
        {
            if (CarSpeed >= 25.8)
            {
                ArrowPrevious.SetActive(false);
                RecomendedPreviousGear.text = "";
            }
            else if (CarSpeed >= 11.42 && CarSpeed < 25.8)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "2";
            }
            else if (CarSpeed < 11.42)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "1";
            }
        }
        else if (CurrentGear == 4)
        {
            if (CarSpeed >= 35)
            {
                ArrowPrevious.SetActive(false);
                RecomendedPreviousGear.text = "";
            }
            else if (CarSpeed >= 25.8 && CarSpeed < 35)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "3";
            }
            else if (CarSpeed >= 11.42 && CarSpeed < 25.8)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "2";
            }
            else if (CarSpeed < 11.42)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "1";
            }
        }
        else if (CurrentGear == 5)
        {
            if (CarSpeed >= 44.13)
            {
                ArrowPrevious.SetActive(false);
                RecomendedPreviousGear.text = "";
            }
            else if (CarSpeed >= 35 && CarSpeed < 44.13)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "4";
            }
            else if (CarSpeed >= 25.8 && CarSpeed < 35)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "3";
            }
            else if (CarSpeed >= 11.42 && CarSpeed < 25.8)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "2";
            }
            else if (CarSpeed < 11.42)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "1";
            }
        }
        else if (CurrentGear == 6)
        {
            if (CarSpeed >= 70)
            {
                ArrowPrevious.SetActive(false);
                RecomendedPreviousGear.text = "";
            }
            else if (CarSpeed >= 44.13 && CarSpeed < 70)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "5";
            }
            else if (CarSpeed >= 35 && CarSpeed < 44.13)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "4";
            }
            else if (CarSpeed >= 25.8 && CarSpeed < 35)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "3";
            }
            else if (CarSpeed >= 11.42 && CarSpeed < 25.8)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "2";
            }
            else if (CarSpeed < 11.42)
            {
                ArrowPrevious.SetActive(true);
                RecomendedPreviousGear.text = "1";
            }
        }

            if (StartEngine.activeSelf || GearsText.text == "R")
            {
                ArrowPrevious.SetActive(false);
                ArrowNext.SetActive(false);

                RecomendedPreviousGear.text = "";
                RecomendedNextGear.text = "";
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            if (ClutchPedal < 0.5)
            {

            if (CurrentGear == 2 && CarSpeed >= 9.14)
                {
                    Gear2 = false;
                }

                if (CurrentGear != 2)
                {
                    Gear2 = true;
                }

                if (CurrentGear == 2 && CarSpeed < 9.14 && Gear2)
                {
                    if (!StartEngine.activeSelf)
                    {
                        StartEngine.SetActive(true);
                        StopEngine.SetActive(true);
                        Idle.SetActive(false);
                        ErrorMessage.SetActive(true);
                        ErrorMessageText.text = "Viteza mașinii este prea mică pentru a fi cuplată în treapta a 2-a";
                    }
                }




                if (CurrentGear == 3 && CarSpeed >= 21.5)
                {
                    Gear3 = false;
                }

                if (CurrentGear != 3)
                {
                    Gear3 = true;
                }

                if (CurrentGear == 3 && CarSpeed < 21.5 && Gear3)
                {
                    if (!StartEngine.activeSelf)
                    {
                        StartEngine.SetActive(true);
                        StopEngine.SetActive(true);
                        Idle.SetActive(false);
                        ErrorMessage.SetActive(true);
                        ErrorMessageText.text = "Viteza mașinii este prea mică pentru a fi cuplată în treapta a 3-a";
                    }
                }



                if (CurrentGear == 4 && CarSpeed >= 29.2)
                {
                    Gear4 = false;
                }

                if (CurrentGear != 4)
                {
                    Gear4 = true;
                }

                if (CurrentGear == 4 && CarSpeed < 29.2 && Gear4)
                {
                    if (!StartEngine.activeSelf)
                    {
                        StartEngine.SetActive(true);
                        StopEngine.SetActive(true);
                        Idle.SetActive(false);
                        ErrorMessage.SetActive(true);
                        ErrorMessageText.text = "Viteza mașinii este prea mică pentru a fi cuplată în treapta a 4-a";
                    }
                }


                if (CurrentGear == 5 && CarSpeed >= 36.78)
                {
                    Gear5 = false;
                }

                if (CurrentGear != 5)
                {
                    Gear5 = true;
                }

                if (CurrentGear == 5 && CarSpeed < 36.78 && Gear5)
                {
                    if (!StartEngine.activeSelf)
                    {
                        StartEngine.SetActive(true);
                        StopEngine.SetActive(true);
                        Idle.SetActive(false);
                        ErrorMessage.SetActive(true);
                        ErrorMessageText.text = "Viteza mașinii este prea mică pentru a fi cuplată în treapta a 5-a";
                    }
                }


                if (CurrentGear == 6 && CarSpeed >= 58.3)
                {
                    Gear6 = false;
                }

                if (CurrentGear != 6)
                {
                    Gear6 = true;
                }

                if (CurrentGear == 6 && CarSpeed < 58.3 && Gear6)
                {
                    if (!StartEngine.activeSelf)
                    {
                        StartEngine.SetActive(true);
                        StopEngine.SetActive(true);
                        Idle.SetActive(false);
                        ErrorMessage.SetActive(true);
                        ErrorMessageText.text = "Viteza mașinii este prea mică pentru a fi cuplată în treapta a 6-a";
                    }
                }
            }


        if (CurrentGear != 0 && ClutchPedal < 0.25f && CarSpeed == 0)
        {
            if (!StartEngine.activeSelf)
            {
                StartEngine.SetActive(true);
                StopEngine.SetActive(true);
                Idle.SetActive(false);
                if (CurrentGear == 1)
                {
                    ErrorMessage.SetActive(true);
                    ErrorMessageText.text = "Ai ridicat prea repede piciorul de pe pedala de ambreiaj";
                }
                else
                {
                    ErrorMessage.SetActive(true);
                    ErrorMessageText.text = "Schimbarea treptei de viteză poate fi efectuată doar atunci când apeși pedala de ambreiaj";
                }
            }
        }


        if (BrakePedal > 0 && (CurrentGear >= 1 || CurrentGear == -1) && StopEngine.activeSelf)
            {
                if (StartEngine.activeSelf)
                {
                    ErrorMessage.SetActive(true);
                    ErrorMessageText.text = "Pentru a evita oprirea motorului, asigură-te că, în timp ce apeși pedala de frână, menții și pedala de ambreiaj apăsată";
                }
            }

            if (!StartEngine.activeSelf)
            {
                EngineOFF.SetActive(false);
            }
            else
            {
                EngineOFF.SetActive(true);
            }

            if (ClutchPedal < 0.5f && targetRpm < 500 && CurrentGear != 0)
            {
                if (!StartEngine.activeSelf)
                {
                    StartEngine.SetActive(true);
                    StopEngine.SetActive(true);
                    Idle.SetActive(false);
                    ErrorMessage.SetActive(true);
                    ErrorMessageText.text = "Asigură-te că rotațiile motorului nu scad sub 500";
                }
            }
    }
}








