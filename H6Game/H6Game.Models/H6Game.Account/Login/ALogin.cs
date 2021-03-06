﻿using H6Game.Actor;
using H6Game.Base;
using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Entities.Enums;
using H6Game.Hotfix.Messages.OutNet;

namespace H6Game.Account
{
    public abstract class ALogin : ILogin
    {
        public abstract LoginType LoginType { get; }
        public abstract LoginResponeMessage VerifyLogin(LoginRequestMessage request, TAccount account);
        public void SetRespose(LoginResponeMessage response, TAccount account)
        {
            response.AccountId = Game.Scene.GetComponent<ActorComponentStorage>().GetActor(account.Id).Id;
            response.FType = account.FType;
            response.FAccountName = account.FAccountName;
            response.FVIPLevel = account.FVIPLevel;
            response.FWXOpenId = account.FWXOpenId;
            response.FWXHeadImgurl = account.FWXHeadImgurl;
            response.FNikeNmae = account.FNikeNmae;
            response.FEmail = account.FEmail;
            response.FPhoneNumber = account.FPhoneNumber;
            response.FBalance = account.FBalance;
        }
    }
}
