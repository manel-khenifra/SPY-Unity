using UnityEngine;
using FYFY;
using DIG.GBLXAPI;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Timers;
using System.Diagnostics;

public class SendStatements1 : FSystem
{

    private Family f_actionForLRS = FamilyManager.getFamily(new AllOfComponents(typeof(ActionPerformedForLRS)));

    public static SendStatements1 instance;

    public static DateTime date;

    public SendStatements1()
    {
        if (Application.isPlaying)
        {
            initGBLXAPI();
        }
        instance = this;
    }

    public void initGBLXAPI()
    {
        if (!GBLXAPI.IsInit)
            GBLXAPI.Init(GBL_Interface.lrsAddresses);

        GBLXAPI.debugMode = false;

        if (GBL_Interface.playerName == null)
        {
            string sessionID = Environment.MachineName + "-" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss");
            //Generate player name unique to each playing session (computer name + date + hour)
            GBL_Interface.playerName = String.Format("{0:X}", sessionID.GetHashCode());
            //Generate a UUID from the player name
            GBL_Interface.userUUID = GBLUtils.GenerateActorUUID(GBL_Interface.playerName);
            //UnityEngine.Debug.Log("TEST" + GBL_Interface.playerName);
        }
        
    }

    // Use to process your families.
    protected override void onProcess(int familiesUpdateCount)
    {
        // Do not use callbacks because in case in the same frame actions are removed on a GO and another component is added in another system, family will not trigger again callback because component will not be processed
        foreach (GameObject go in f_actionForLRS)
        {
            ActionPerformedForLRS[] listAP = go.GetComponents<ActionPerformedForLRS>();
            int nb = listAP.Length;
            ActionPerformedForLRS ap;
            if (!this.Pause)
            {
                for (int i = 0; i < nb; i++)
                {
                    ap = listAP[i];
                    //If no result info filled
                    if (!ap.result)
                    {
                        GBL_Interface.SendStatement(ap.verb, ap.objectType, ap.objectName, ap.activityExtensions);
                    }
                    else
                    {
                        bool? completed = null, success = null;

                        if (ap.completed > 0)
                            completed = true;
                        else if (ap.completed < 0)
                            completed = false;

                        if (ap.success > 0)
                            success = true;
                        else if (ap.success < 0)
                            success = false;

                        GBL_Interface.SendStatementWithResult(ap.verb, ap.objectType, ap.objectName, ap.activityExtensions, ap.resultExtensions, completed, success, ap.response, ap.score, ap.duration);
                    }
                }
            }
            for (int i = nb - 1; i > -1; i--)
            {
                GameObjectManager.removeComponent(listAP[i]);
            }            
        }
    }

    public void testSendStatement()
    {
        UnityEngine.Debug.Log(GBL_Interface.playerName + " asks to send statement...");
        GameObjectManager.addComponent<ActionPerformedForLRS>(MainLoop.instance.gameObject, new
        {
            verb = "interacted",
            objectType = "menu",
            objectName = "myButton"
        });
    }

    public void sendStartedStatement(String nb_level)
    {
        date = DateTime.Now/*.ToString("yyyy.MM.dd.hh.mm.ss")*/;
        
        if (nb_level.Equals("")){
            nb_level = TitleScreenSystem.nb_level.ToString();
        }
        else
        {
            TitleScreenSystem.nb_level = Int32.Parse(nb_level);
        }
        
        UnityEngine.Debug.Log("test2: " + nb_level);
        UnityEngine.Debug.Log(GBL_Interface.playerName + " asks to send statement...");
        GameObjectManager.addComponent<ActionPerformedForLRS>(MainLoop.instance.gameObject, new
        {
            verb = "started",
            objectType = "level",
            objectName = "Niveau " + nb_level
        });

         
    }

    public void sendCompletedStatement()
    {
        TimeSpan duration = DateTime.Now - date;
        UnityEngine.Debug.Log(duration);

        string nb_level = TitleScreenSystem.nb_level.ToString();
        UnityEngine.Debug.Log(GBL_Interface.playerName + " asks to send statement...");
        GameObjectManager.addComponent<ActionPerformedForLRS>(MainLoop.instance.gameObject, new
        {
            verb = "completed",
            objectType = "level",
            objectName = "Niveau " + nb_level,
            activityExtensions = new Dictionary<string, string>() {
                { "duration", duration.ToString() }
            }
        }); ;

    }

    public void sendActionStatement(string action)
    {
        string[] action1 = action.Split('(');
        UnityEngine.Debug.Log(GBL_Interface.playerName + " asks to send statement...");
        GameObjectManager.addComponent<ActionPerformedForLRS>(MainLoop.instance.gameObject, new
        {
            verb = "interacted",
            objectType = "level",
            objectName = action1[0]
        });
    }

    public void sendExecuteStatement()
    {
        UnityEngine.Debug.Log(GBL_Interface.playerName + " asks to send statement...");
        GameObjectManager.addComponent<ActionPerformedForLRS>(MainLoop.instance.gameObject, new
        {
            verb = "interacted",
            objectType = "level",
            objectName = "ExecuteButton"
        });
    }
}