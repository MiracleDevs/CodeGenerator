@include "shared.tpl"
@{
	var InterfaceName = Raw(GetServiceName(Model.Definition, isInterface: true));
	var Name = Raw(GetServiceName(Model.Definition, isInterface: false));
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
///<reference path="@Raw(Model.Configuration["MiracleTypingPath"])" />
///<reference path="@Raw(Model.Configuration["HttpServicesPath"])" />
@foreach(var contract in contracts)
{
<text>///<reference path="@Raw(GetRelativeModelDirectoryForServices(Model.Configuration, contract, isInterface: true))" /></text>
}

module @Model.Configuration["Namespace"]
{
	import HttpPromise = angular.IHttpPromise;
	import HttpServiceBase = MiracleDevs.Angular.UI.Web.Services.HttpServiceBase;
	import AngularServices = MiracleDevs.Angular.UI.Web.Services.AngularServices;
	import IServiceRegister = MiracleDevs.Angular.UI.Web.Interfaces.IServiceRegister;
	@foreach(var contract in contracts)
	{
	<text>import @Raw(Model.Configuration["ModelNamespace"] + "." +  GetModelName(contract, isInterface: true));</text>	
	}

	export class @Name extends HttpServiceBase implements @InterfaceName
	{
		public static register: IServiceRegister = 
		{
			name: HttpServices.@Name,
            factory: @(Name).factory,
            dependencies: [AngularServices.http]
		};
	@foreach(var method in Model.Definition.Methods)
	{	
		<text>
		@Raw(MethodFirm(method, false))
		{
			@Raw(MethodBody(method, GetServiceName(Model.Definition, isInterface: false)))
		}</text>
	}
	}

	////////////////////////////////////////////////////////////
    // Register service
    ////////////////////////////////////////////////////////////
    Application.instance.registerService(@(Name).register);
}