using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveControllerInput : MonoBehaviour
{
    // 콘트롤러 셋팅
    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;      // 왼손
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;     // 오른손
    public SteamVR_Input_Sources any = SteamVR_Input_Sources.Any;           // 양손 아무대나

    // 콘트롤러의 인풋 버튼 셋팅
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Boolean trackPadClick = SteamVR_Actions.default_Teleport;
    public SteamVR_Action_Boolean trackPadTouch = SteamVR_Actions.default_TrackpadTouch;
    public SteamVR_Action_Vector2 trackPadPosition = SteamVR_Actions.default_TrackpadPosition;
    public SteamVR_Action_Boolean grip = SteamVR_Input.GetBooleanAction("GrabGrip");
    //public SteamVR_Action_Boolean grip2 = SteamVR_Actions.default_GrabGrip;

    // 콘트롤러의 진동 셋팅
    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    // 콘트롤러 정보
    public SteamVR_Behaviour_Pose controllerPose_L;
    public SteamVR_Behaviour_Pose controllerPose_R;

    void Update()
    {
        //if(SteamVR_Actions.default_InteractUI.GetStateDown(SteamVR_Input_Sources.LeftHand))
        //{
        //    print("좌측 트리거 클릭!");
        //}

        //if(SteamVR_Actions.default_InteractUI.GetStateDown(SteamVR_Input_Sources.RightHand))
        //{
        //    print("우측 트리거 클릭!");    
        //}

        if(trigger.GetStateDown(rightHand))
        {
            print("우측 트리거를 눌렀엉!");
        }

        if(trigger.GetStateUp(rightHand))
        {
            print("우측 트리거를 놨엉!");
        }

        if(trackPadClick.GetStateDown(any))
        {
            print("아무 손이나 터치패드를 클릭했옹!");
        }

        if(trackPadTouch.GetState(rightHand))
        {
            Vector2 pos = trackPadPosition.GetAxis(rightHand);
            print("현재 위치: " + pos);
        }

        if(grip.GetStateDown(leftHand))
        {
            // 진동 주기(시작 시간, 지속 시간, 진동 빈도, 진폭 세기, 콘트롤러)
            haptic.Execute(0.5f, 0.5f, 50.0f, 0.5f, leftHand);
            print("왼손 진동~~~");
        }

        if (grip.GetStateDown(rightHand))
        {
            // 진동 주기(시작 시간, 지속 시간, 진동 빈도, 진폭 세기, 콘트롤러)
            haptic.Execute(2.0f, 1.0f, 50.0f, 0.5f, rightHand);
            print("오른손 진동~~~");
        }

        // 오른손 콘트롤러의 속도
        controllerPose_R.GetVelocity();
    }
}
