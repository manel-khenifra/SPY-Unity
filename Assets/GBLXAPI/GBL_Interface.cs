// -------------------------------------------------------------------------------------------------
// GBL_Interface.cs
// Project: GBLXAPI-Unity
// Created: 2018/07/06
// Copyright 2018 Dig-It! Games, LLC. All rights reserved.
// This code is licensed under the MIT License (see LICENSE.txt for details)
// -------------------------------------------------------------------------------------------------

// required for GBLXAPI
using DIG.GBLXAPI;
using System.Collections.Generic;

using DIG.GBLXAPI.Builders;

// --------------------------------------------------------------------------------------
// --------------------------------------------------------------------------------------
public static class GBL_Interface {

	public enum durationSlots
	{
		Application,
		Game,
		Tutorial,
		Level
	};

    // Fill in these fields for GBLxAPI setup.
    //Statements will be sent to all addresses in this list
    public static List<GBLConfig> lrsAddresses = new List<GBLConfig>() {
        new GBLConfig("https://lrsmocah.lip6.fr/data/xAPI", "84b8ac4d3ea63cad09e364ffd94d5f0d1d73e274", "16110a446b7c1beb3112d00a22aa07575704dadd")
    };
	public static string userUUID; // overrided in SendStatements system
    public static string playerName; // overrided in SendStatements system

    // ------------------------------------------------------------------------
	// Sample Gameplay GBLxAPI Triggers
	// ------------------------------------------------------------------------
	/*
	Here is where you will put functions to be called whenever you want to send a GBLxAPI statement.
	 */
	
	public static void SendStatement(string verb, string activityType, string activityName, Dictionary<string, string> activityExtensions = null, float duration = 0)
    {
        ActivityDefinitionBuilder.IOptional definitionBuilder = GBLXAPI.ActivityDefinition
            .WithType(activityType)
            .WithName(activityName);
        if (activityExtensions != null)
        {
            //set extensions
            ExtensionsBuilder extensions = GBLXAPI.Extensions;
            foreach (KeyValuePair<string, string> entry in activityExtensions)
                extensions = extensions.WithStandard(entry.Key, entry.Value);
            definitionBuilder.WithExtensions(extensions.Build());
        }

        GBLXAPI.Statement
            .WithActor(GBLXAPI.Agent
                .WithAccount(userUUID, "https://www.lip6.fr/mocah/")
                .WithName(playerName)
                .Build())
            .WithVerb(verb)
            .WithTargetActivity(GBLXAPI.Activity
                .WithID("https://www.lip6.fr/mocah/invalidURI/" + activityType)
                .WithDefinition(definitionBuilder.Build())
                .Build())
            .Enqueue();
	}
	
	public static void SendStatementWithResult(string verb, string activityType, string activityName, Dictionary<string, string> activityExtensions = null, Dictionary<string, string> resultExtensions = null, bool? completed = null, bool? success = null, string response = null, int? score = null,
        float duration = 0)
    {
        ActivityDefinitionBuilder.IOptional definitionBuilder = GBLXAPI.ActivityDefinition
            .WithType(activityType)
            .WithName(activityName);
        if (activityExtensions != null)
        {
            //set extensions
            ExtensionsBuilder extensions = GBLXAPI.Extensions;
            foreach (KeyValuePair<string, string> entry in activityExtensions)
                extensions = extensions.WithStandard(entry.Key, entry.Value);
            definitionBuilder.WithExtensions(extensions.Build());
        }

        ResultBuilder resultBuilder = GBLXAPI.Result;
        if (completed != null)resultBuilder = resultBuilder.WithCompletion(completed == true);
        if (success != null) resultBuilder = resultBuilder.WithSuccess(success == true);
        if (score != null) resultBuilder = resultBuilder.WithScore(score);
        if (response != null) resultBuilder = resultBuilder.WithResponse(response);
        if (resultExtensions != null)
        {
            //set extensions
            ExtensionsBuilder extensions = GBLXAPI.Extensions;
            foreach (KeyValuePair<string, string> entry in resultExtensions)
                extensions = extensions.WithStandard(entry.Key, entry.Value);
            resultBuilder.WithExtensions(extensions.Build());
        }

        GBLXAPI.Statement
            .WithActor(GBLXAPI.Agent
                .WithAccount(userUUID, "https://www.lip6.fr/mocah/")
                .WithName(playerName)
                .Build())
            .WithVerb(verb)
            .WithTargetActivity(GBLXAPI.Activity
                .WithID("https://www.lip6.fr/mocah/invalidURI/" + activityType + "/" + activityName)
                .WithDefinition(definitionBuilder.Build())
                .Build())
            .WithResult(resultBuilder)
            .Enqueue();
	}
}