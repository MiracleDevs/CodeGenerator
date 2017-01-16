[![Build Status](https://travis-ci.org/MiracleDevs/CodeGenerator.svg?branch=master)](https://travis-ci.org/MiracleDevs/CodeGenerator)

Miracle Devs
============

Code Generation Tool
--------------------
This tool allows to automatically translate any c# object that is publicly exposed  into any
other pre-configured language.  
The main idea behind the development was to convert .NET WebApi controllers and models into client proxies using the power of reflection, and an expressiveness of razor.

With this tool you can virtually port and standardize any c# code to other languages.
It can be used to create better proxies for WCF, port your domain classes,  DTOs or any
other object to a different language like java, javascript, typescript, objective-c, etc.

It's specially powerful when it comes to distributed systems, having many different 
client applications written in different languages.

![Diagram](images/StructureDiagram.png)

Configuring a New Targeting Language
-----
This tool is a command line tool, and it requires configuration files in order to work. All the configuration
is written in json. Inside the tool's configuration folder, you'll find specific folders for targeting languages like typscript, java, objective-c, etc. Inside these folders you'll find the following configuration files:

###1. translations.json
This file contains native raw translations from c# to the targeting language. For example, Int16, Int32, Float, Double, etc will be natively translated to numbers in typescript. Here you should provide native translations:
 
 
**Structure**

| Field           | Type    | Description                                              |
|-----------------| --------|----------------------------------------------------------|
| Name            | string  | C# native type name (I.E: System.Byte, System.Int32, etc)|
| Translation     | string  | Targeting language type                                  |



**Example**

```javascript
[
	{ "Name": "System.UInt64",                      "Translation": "number" },                             
	{ "Name": "System.Char",                        "Translation": "string" },
	{ "Name": "System.String",                      "Translation": "string" },
	{ "Name": "System.Boolean",                     "Translation": "boolean"},
	{ "Name": "System.DateTime",                    "Translation": "Date"   },
	{ "Name": "System.Void",                        "Translation": "void"   }
]
```
###2. codegeneration.json 
This file contains codegeneartion items. Basically, each code-generation item configures a razor template, giving a readable alias and description, and referring the path to the aforementioned razor template. 
Later these configurations will be used by other files to reference what needs to be translated. The basic structure is:


**Structure**

| Field           | Type    | Description                                                         |
|-----------------| --------|---------------------------------------------------------------------|
| Name            | string  | Custom name for the generation item. Should be something meaningful.|
| Description     | string  | Short description about the purpose of the item.                    |
| TemplateFile    | string  | Path to the razor template file.                                    |



**Example**

```javascript
[{
    "Name": "ServiceInterface",
    "Description": "Generates a proxy service interface",
    "TemplateFile": "configuration/typescript/templates/serviceInterface.tpl"
}]
```

###3. templates folder
Inside this folder you'll find all the razor templates. When writing templates for a new language, changes are that you will need to call the same method, or to reuse some of the templating code.  For that purpose we added a custom sentence so you can separate shared code in different files, and then include it on the code using the custom sentence **@include**. Suppose you have a shared file at the same level as your templates, then on your template, you just add the following line of code:

```razor
@include "shared.tpl"
```

Adding Output file Configurations
-----
Once you created your templates for a given language, you must feed some data to the tool in order to generate the code. Basically, this configuration tells the tool which assembly to process, which type will be used as the starting point for the data extraction, and from there, how and what to generate from the data. 

### Structure of Output Configuration

**Output Configuration Object**

| Field | Type | Description |
| ---- | --- | --- |
| Language | string | Targeting language. Must match the targeting language folder. |
| Assembly | string | .NET assembly path in which the required code resides. |
| EntryPointType | string | Entry point object to start the data extraction process. |
| FileConfigurations | array of File Configurations | Array of all the different output configurations. |



**File Configuration Object**

| Field | Type | Description |
| ---- | --- | --- |
| CodeGenerationName | string | The code generation item name configured in the codegeneration.json. |
| OutputPath | string | Where to save the generated files. |
| TypeMatchers | array of Type Matcher Configurations | Array of type matches used to match different extracted native objects. All the matchers must match for an object to be considered elegible.
| NamingRules | array of Naming Rule Configurations | Array of naming rules applied to the file being generated. These rules will transform the final file name.
| Parameters | array of Parameter Configurations | Array of parameter passed to the razor templates in order to provide external information required by the templates. |
 


**Type Matcher Configuration Object**

| Field | Type | Description |
| ---- | --- | --- |
| Name | string | Name of the type matcher. There are a set of pre-programmed matchers like ContainsAttribute, IncludesInName, etc  |
| Parameters | array of string | Naming rules parameters. |


**List of available Type Matchers**

* **ContainsAttribute**
  * **Parameters**: AttributeName
  * **Description**:  Checks if a type is decorated with the given attribute. 
  * **Example**:  
```javascript
 { "Name":  "ContainsAttribute", "Parameters": [ "DataContract" ] } 
```

* **IncludesInName**
  * **Parameters**: Keywords
  * **Description**: Checks if a given string is included in the type name. 
  * **Example**:   
```javascript
{ "Name":  "IncludesInName", "Parameters": [ "Controller" ] } 
```

* **InheritFrom** 
  * **Parameters**: TypeFullName
  * **Description**: Checks if the type inherits from a given type. 
  * **Important:** Make sure to use the full name, including assembly, version, etc.
  * **Example:** 
```javascript
{ "Name":  "InheritsFrom", "Parameters": [ "System.Web.Http.ApiController, System.Web.Http, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" ] } 
```

* **IsArray**
  * **Parameters**:  None
  * **Description**: Checks if the type is an array. 
  * **Example**:  
```javascript
{ "Name":  "IsArray", "Parameters": [] } 
```


* **IsEnum**
  * **Parameters**:  None
  * **Description**: Checks if the type is an enumeration. 
  * **Example**:   
```javascript
{ "Name":  "IsEnum", "Parameters": [] } 
```


**Naming Rule Configuration Object**

| Field | Type | Description |
| ---- | --- | --- |
| Name | string | Name of the naming rule. There are a set of pre-programmed rules like format or replace |
| Parameters | array of string | Naming rules parameters. |


**List of available Naming Rules**

* **Format**
  * **Parameters**: FormatString
  * **Description**:  Inserts the current element name inside a format string, to produce a new name.
  * **Example**:  
```javascript
 { "Name": "Format",  "Parameters": [ "I{0}.ts" ] } 
```


* **Replace**
  * **Parameters**:  OldValue, NewValue
  * **Description**:  Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.
  * **Example**:  
```javascript
 { "Name": "Replace", "Parameters": [ "Controller", "Service" ] }
```


**Parameter Configuration Object**

| Field | Type | Description |
| ---- | --- | --- |
| Name | string | Name of the parameter. |
| Value | string | Self explanatory. |


### Example

```javascript
{
  "Language": "typescript",
  "Assembly": "..\\Host.WebApi\\bin\\Host.WebApi.dll",
  "EntryPointType": "SomeController",  

  "FileConfigurations": [
  {
      "CodeGenerationName": "ServiceInterface",
      "OutputPath":  "services", 

      "TypeMatchers":  [
        { "Name":  "InheritsFrom", "Parameters": [ "System.Web.Http.ApiController, System.Web.Http, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" ] }
      ],

      "NamingRules": [
        { "Name": "Replace", "Parameters": [ "Controller", "Service" ] },
        { "Name": "Format",  "Parameters": [ "I{0}.ts" ] }     
      ],

      "Parameters": [
        { "Name": "Namespace", "Value": "CompanyName.UI.Web.Services" },
        { "Name": "ModelOutputPath", "Value": "models" },
        { "Name": "ModelNamespace", "Value": "CompanyName.UI.Web.Models" },
        { "Name": "AngularTypingPath", "Value": "../../../../../typings/angularjs/angular.d.ts" }
      ]
   }]
}

```

Tool Line Arguments
-----
| Short Argument | Large Argument | Description |
| --- | --- | --- |
| `-f` | `--filename` | Indicates the path of a output configuration json file. |
| `-d` | `--directory` | Indicates a directory path in which all the output configuration files will be used to generate code. |
| `-h` | `--help` | Prompts the help. |
| `-t` | `--print-trans` | Prints all the translations. |
| `-o` | `--print-objdef` | Prints all the object definitions. |
