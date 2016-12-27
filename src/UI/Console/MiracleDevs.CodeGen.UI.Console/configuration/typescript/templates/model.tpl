@include "shared.tpl"
@{
	var InterfaceName = Raw(GetModelName(Model.Definition, isInterface: true));
	var Name = Raw(GetModelName(Model.Definition, isInterface: false));
	var contracts = GetModelRelatedContracts(Model.Definition);
}//////////////////////////////////////////////////////////////////////////////////
//  Name: @(Name + ".ts")
//
//  Generated with miracledevs proygen tool.
//  Do not modify this file in any way.
//
//  Copyright (c) 2016 miracledevs. All rights reserved.
//////////////////////////////////////////////////////////////////////////////////

///<reference path="@Raw(Model.Configuration["MiracleTypingPath"])" />
///<reference path="@(InterfaceName + ".ts")" />

module @Model.Configuration["Namespace"]
{
	import ModelBase = MiracleDevs.Angular.UI.Web.Models.ModelBase;	

	export class @Name extends ModelBase implements @InterfaceName
	{
	@foreach(var property in Model.Definition.Properties)
	{	
		<text>@ToCamelCase(property.Name): @Raw(GetModelName(property.Type, isInterface: true));</text>
	}
	}
}