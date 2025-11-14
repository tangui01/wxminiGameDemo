using sky_mirror;
//using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoleBody : MonoBehaviour
{
    string curName = "";

    // Start is called before the first frame update
    void Start()
    {
        //var db = GetComponent<SkeletonAnimation>();
        //var stateData = db.SkeletonDataAsset.GetAnimationStateData();
        ////设置动画混合，第一个参数是 当前动画，第二参数是下一个动画，第三个参数是从当前动画过度到下一个动画所需时间
        //stateData.SetMix("daiji", "zoulu", 0.1f);
        //stateData.SetMix("zoulu", "daiji", 0.1f);
        //stateData.SetMix("zoulu", "gongji", 0.1f);
        //stateData.SetMix("gongji", "daiji", 1.0f);
    }

    private void OnDestroy()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IdleAnima()
    {
        if (curName != "daiji")
        {
            //curName = "daiji";
            //var db = GetComponent<SkeletonAnimation>();
            //db.AnimationState.SetAnimation(0, "daiji", true);

        }
    }

    public void MoveAnima()
    {
        if (curName != "zoulu")
        {
            //var db = GetComponent<SkeletonAnimation>();
            //db.AnimationState.SetAnimation(0, "zoulu", true);
            //curName = "zoulu";
        }


    }

    public void FightAnima()
    {
        if(curName != "gongji")
        {
            //var db = GetComponent<SkeletonAnimation>();
            //db.AnimationState.SetAnimation(0, "gongji", false);
            //curName = "gongji";
        }
    }

    public void DeahAnima()
    {
        
    }

    public void Injured()
    {
        var shanbai = GetComponent<ShanBaiSpine>();
        shanbai.Begin();

    }

    public void Clear()
    {
        //var db = GetComponent<SkeletonAnimation>();
        //db.ClearState();
        //var db = GetComponent<UnityArmatureComponent>();
        //db.animation.Reset();

        //_curAnimation = null;
    }

    public void SetCamp(Camp camp)
    {
        //var db = GetComponent<SkeletonAnimation>();

        //switch (camp)
        //{
        //    case Camp.My:
        //        {
        //            db.initialSkinName = "lan";
        //            db.Initialize(true);
        //            //db.skeleton.SetSkin(db.initialSkinName);
        //        }
        //        break;
        //    case Camp.Target:
        //        {
        //            db.initialSkinName = "hong";
        //            db.Initialize(true);
        //        }
        //        break;
        //    default:
        //        break;
        //}
    }



}
