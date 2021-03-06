﻿@include "shared.tpl"
@{
	var Name = Raw(Model.Definition.Name);
}//////////////////////////////////////////////////////////////////////////////////
//  Name: @(Name + ".ts")
//
//  Generated with miracledevs proygen tool.
//  Do not modify this file in any way.
//
//  Copyright (c) 2016 miracledevs. All rights reserved.
//////////////////////////////////////////////////////////////////////////////////

module @Model.Configuration["Namespace"]
{
	export enum @Name
	{
	@foreach(var value in Model.Definition.Values)
	{	
		<text>@Raw(value.Name) = @Raw(value.Value),</text>
	}
	}
}