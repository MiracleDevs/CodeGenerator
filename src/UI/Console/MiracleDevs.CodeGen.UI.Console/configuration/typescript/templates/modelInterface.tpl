@include "shared.tpl"
@{
	var Name = Raw(GetModelName(Model.Definition, isInterface: true));
	var contracts = GetModelRelatedContracts(Model.Definition);
}//////////////////////////////////////////////////////////////////////////////////
//  Name: @(Name + ".ts")
//
//  Generated with miracledevs proygen tool.
//  Do not modify this file in any way.
//
//  Copyright (c) 2016 miracledevs. All rights reserved.
//////////////////////////////////////////////////////////////////////////////////

@foreach(var contract in contracts)
{
<text>///<reference path="@Raw(GetModelName(contract, isInterface: true) + ".ts")" /></text>
}

module @Model.Configuration["Namespace"]
{
	export interface @Name
	{
	@foreach(var property in Model.Definition.Properties)
	{	
		<text>@property.Name: @Raw(GetModelName(property.Type, isInterface: true));</text>
	}
	}
}