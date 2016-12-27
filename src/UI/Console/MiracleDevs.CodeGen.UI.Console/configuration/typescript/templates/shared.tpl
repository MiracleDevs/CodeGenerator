@functions 
{
	public static string GetModelName(dynamic objectDefinition, bool isInterface)
	{
		if (objectDefinition.NativeType.IsEnum)
		    isInterface = false;

        if (objectDefinition.IsArray && objectDefinition.InnerObject != null)
            return string.Format("{0}[]", GetModelName(objectDefinition.InnerObject, isInterface: isInterface));

        if (!objectDefinition.HasTranslation)
            return string.Format("{0}{1}", isInterface ?  "I" : "", objectDefinition.Name.Replace("View", ""));

        return objectDefinition.Translator.Translation;
	}

	public static string GetServiceName(dynamic objectDefinition, bool isInterface)
	{
		return string.Format("{0}{1}", isInterface ?  "I" : "", objectDefinition.Name.Replace("Controller", "Service"));
	}

	public static string ToCamelCase(string value)
	{
		if (value == null)
			return null;

		if (value.Length == 0)
			return value;

		return char.ToLower(value[0]) + value.Substring(1, value.Length - 1);
	}
	
	public static string GetAbsoluteModelDirectory(dynamic configuration, dynamic objectDefinition, bool isInterface)
    {
		return System.IO.Path.Combine(configuration.ExecutingFullPath, configuration.OutputPath, GetModelName(objectDefinition, isInterface) + ".ts");     
    }

	public static string GetAbsoluteServiceDirectory(dynamic configuration, dynamic objectDefinition, bool isInterface)
    {
		return System.IO.Path.Combine(configuration.ExecutingFullPath, configuration.OutputPath, GetServiceName(objectDefinition, isInterface) + ".ts");     
    }

	public static string GetRelativeModelDirectoryForServices(dynamic configuration, dynamic objectDefinition, bool isInterface)
    {   
		var fakeFile = "file.txt";
		var fakeFileUri = "/file.txt";

        var serviceRoute = new System.Uri(System.IO.Path.Combine(configuration.ExecutingFullPath, configuration.OutputPath,  fakeFile), System.UriKind.Absolute);
        var modelRoute   = new System.Uri(System.IO.Path.Combine(configuration.ExecutingFullPath, configuration["ModelOutputPath"], fakeFile), System.UriKind.Absolute);
        var relativePath = System.Uri.UnescapeDataString(serviceRoute.MakeRelativeUri(modelRoute).ToString()).Replace(fakeFileUri, string.Empty);		
		
		return relativePath + "/" + GetModelName(objectDefinition, isInterface) + ".ts";           
    }

	public static IEnumerable<MiracleDevs.CodeGen.Logic.Translations.ObjectDefinition> GetServiceRelatedContracts(MiracleDevs.CodeGen.Logic.Translations.ObjectDefinition definition) 
	{
		var contracts = new Dictionary<string, MiracleDevs.CodeGen.Logic.Translations.ObjectDefinition>();
		var classDefinition = definition as MiracleDevs.CodeGen.Logic.Translations.StructDefinition;

		if (classDefinition == null)
			return contracts.Values;

		foreach(var method in classDefinition.Methods)
		{
			var objectDefinition = method.ReturnType;

			if (objectDefinition.IsArray)
				objectDefinition = objectDefinition.InnerObject;

			if (!contracts.ContainsKey(objectDefinition.Name) && 
				 objectDefinition.Attributes.Any(x => x.Name == "DataContractAttribute") && 
				 !objectDefinition.HasTranslation)
			{
				contracts.Add(objectDefinition.Name, objectDefinition);
			}

			foreach(var parameter in method.Parameters)
			{
				objectDefinition = parameter.Type;

				if (objectDefinition.IsArray)
					objectDefinition = objectDefinition.InnerObject;

				if (!contracts.ContainsKey(objectDefinition.Name) && 
					 objectDefinition.Attributes.Any(x => x.Name == "DataContractAttribute") && 
					 !objectDefinition.HasTranslation)
				{
					contracts.Add(objectDefinition.Name, objectDefinition);
				}			
			}
		}

		return contracts.Values;
	}

	public static IEnumerable<MiracleDevs.CodeGen.Logic.Translations.ObjectDefinition> GetModelRelatedContracts(MiracleDevs.CodeGen.Logic.Translations.ObjectDefinition definition) 
	{
		var contracts = new Dictionary<string, MiracleDevs.CodeGen.Logic.Translations.ObjectDefinition>();
		var classDefinition = definition as MiracleDevs.CodeGen.Logic.Translations.StructDefinition;

		if (classDefinition == null)
			return contracts.Values;

		foreach(var property in classDefinition.Properties)
		{
			var objectDefinition = property.Type;

			if (objectDefinition.IsArray)
				objectDefinition = objectDefinition.InnerObject;

			if (objectDefinition.Name != definition.Name &&
				!contracts.ContainsKey(objectDefinition.Name) && 
				objectDefinition.Attributes.Any(x => x.Name == "DataContractAttribute") && 
				!objectDefinition.HasTranslation)
			{
				contracts.Add(objectDefinition.Name, objectDefinition);
			}
		}

		return contracts.Values;
	}

	public static string MethodFirm(MiracleDevs.CodeGen.Logic.Translations.MethodDefinition method, bool includeSemicolon)
	{
		var parameters = string.Empty;

		foreach(var parameter in method.Parameters)
		{
			parameters += parameter.Name + ": " + GetModelName(parameter.Type, isInterface: true) + ", ";
		}

		if (parameters.EndsWith(", "))
		{
			parameters = parameters.Substring(0, parameters.Length - 2);
		}
	
		return string.Format("{0}({1}): IHttpPromise<{2}>{3}", ToCamelCase(method.Name), parameters, GetModelName(method.ReturnType, isInterface: true), includeSemicolon ? ";" : "");
	}

	public static string GetMethodVerb(MiracleDevs.CodeGen.Logic.Translations.MethodDefinition method)
	{
		if (method.Attributes.Any(x => x.Name == "HttpGetAttribute"))
			return "get";

		if (method.Attributes.Any(x => x.Name == "HttpPostAttribute"))
			return "post";

		if (method.Attributes.Any(x => x.Name == "HttpPutAttribute"))
			return "put";

		if (method.Attributes.Any(x => x.Name == "HttpDeleteAttribute"))
			return "delete";

		if (method.Name.ToLower() == "get")
			return "get";

		if (method.Name.ToLower() == "post")
			return "post";

		if (method.Name.ToLower() == "put")
			return "put";

		if (method.Name.ToLower() == "delete")
			return "delete";

		return "get";
	}

	public static string MethodBody(MiracleDevs.CodeGen.Logic.Translations.MethodDefinition method, string serviceName)
	{
		var parameters = string.Empty;
		var callParameters = string.Empty;
		var data = string.Empty;
		var verb = GetMethodVerb(method);

		foreach(var parameter in method.Parameters)
		{			
			if (parameter.Attributes.Any(x => x.Name == "FromBodyAttribute"))
				data = string.Format("{0}", parameter.Name);
			else
				callParameters += string.Format("{0}: {0}, ", parameter.Name);
		}

		if (callParameters.Any()) 
			callParameters = string.Format("{{ {0} }}", callParameters.Substring(0, callParameters.Length - 2)); 


		return string.Format("return this.{0}<{1}>('{2}/{3}', {4}, {5});", verb, GetModelName(method.ReturnType, isInterface: true), serviceName, method.Name, callParameters.Any() ? callParameters : "null", data.Any() ? data : "null");
	}
}