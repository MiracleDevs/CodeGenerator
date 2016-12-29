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
///<reference path="@Raw(Model.Configuration["HttpServiceBasePath"])" />
///<reference path="@(InterfaceName + ".ts")" />

module @Model.Configuration["Namespace"]
{
	import IHttpPromise = angular.IHttpPromise;
	import IHttpService = angular.IHttpService;
	import AngularServices = Angular.UI.Web.Services.AngularServices;
	import IServiceRegister = Angular.UI.Web.Interfaces.IServiceRegister;
	import BuildInfo = Angular.UI.Web.BuildInfo;

	@foreach(var contract in contracts)
	{
		var modelName = GetModelName(contract, isInterface: true);
	<text>import @Raw(modelName) = @Raw(Model.Configuration["ModelNamespace"] + "." +  modelName);</text>	
	}

	export class @Name extends HttpServiceBase implements @InterfaceName
	{
		public static register: IServiceRegister = 
		{
			name: HttpServices.@Raw(ToCamelCase(GetServiceName(Model.Definition, isInterface: false))),
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

		static factory($http: IHttpService): @Name
        {
            return new @Name ($http, BuildInfo.instance.getData<string>("host"));
        }
	}

	////////////////////////////////////////////////////////////
    // Register service
    ////////////////////////////////////////////////////////////
    Application.instance.registerService(@(Name).register);
}