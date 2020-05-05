using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
using UnityEngine.UI;

public sealed class FacebookScript : MonoBehaviour
{
    //public Text friendsText;

    //private void Awake()
    //{
    //    if (!FB.IsInitialized)
    //    {
    //        FB.Init(() =>
    //        {
    //            if (FB.IsInitialized)
    //            {
    //                FB.ActivateApp();
    //            }
    //            else
    //            {
    //                Debug.LogError("Couldn't initialize");
    //            }
    //        }, 
    //        isGameShown =>
    //        {
    //            if (!isGameShown)
    //            {
    //                Time.timeScale = 0;
    //            }
    //            else
    //            {
    //                Time.timeScale = 1;
    //            }
    //        });
    //    }
    //    else
    //    {
    //        FB.ActivateApp();
    //    }
    //}

    //#region Login / Logout

    //public void FacebookLogin()
    //{
    //    var permissions = new List<string>() { "public_profile", "email", "user_friends" };
    //    FB.LogInWithReadPermissions(permissions);
    //}

    //public void FacebookLogout()
    //{
    //    FB.LogOut();
    //}

    //#endregion

    //public void FacebookShare()
    //{
    //    FB.ShareLink(new System.Uri("https://www.google.ru/"), "Check it ut!", "Test uri", 
    //        new System.Uri("https://uptu.work/wp-content/uploads/job-manager-uploads/company_logo/2020/02/21751306_10155724905022838_7192191338970086519_n.png"));
    //}

    //#region Inviting

    //public void FacebookGameRequest()
    //{
    //    FB.AppRequest("Come and play this game!", title: "Test unity app");
    //}

    //#endregion

    //public void GetFriendsPlayingThisGame()
    //{
    //    var querry = "me/friends";
    //    FB.API(querry, HttpMethod.GET, result =>
    //    {
    //        var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
    //        var friendsList = (List<object>)dictionary["data"];
    //        friendsText.text = string.Empty;
    //        foreach(var dict in friendsList)
    //        {
    //            friendsText.text += ((Dictionary<string, object>)dict)["name"];
    //        }
    //    });
    //}
}
