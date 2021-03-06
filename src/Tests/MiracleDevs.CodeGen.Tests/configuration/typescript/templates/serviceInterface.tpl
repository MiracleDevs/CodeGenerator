﻿@include "shared.tpl"
@{
	var Name = Raw(GetServiceName(Model.Definition, isInterface: true));
	var contracts = GetServiceRelatedContracts(Model.Definition);
}//////////////////////////////////////////////////////////////////////////////////
//  Name: @(Name + ".ts")
//
//  Generated with miracledevs proygen tool.
//  Do not modify this file in any way.
//
//  Copyright (c) 2016 miracledevs. All rights reserved.
//////////////////////////////////////////////////////////////////////////////////

///<reference path="@Raw(Model.Configuration["AngularTypingPath"])" />
@foreach(var contract in contracts)
{
<text>///<reference path="@Raw(GetRelativeModelDirectoryForServices(Model.Configuration, contract, isInterface: true))" /></text>
}

module @Model.Configuration["Namespace"]
{
	import IHttpPromise = angular.IHttpPromise;
	@foreach(var contract in contracts)
	{
		var modelName = GetModelName(contract, isInterface: true);
	<text>import @Raw(modelName) = @Raw(Model.Configuration["ModelNamespace"] + "." +  modelName);</text>	
	}

	export interface @Name
	{
	@foreach(var method in Model.Definition.Methods)
	{	
		<text>@Raw(MethodFirm(method, true))
		</text>
	}
	}
}