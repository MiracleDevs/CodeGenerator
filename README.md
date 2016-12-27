Miracle Devs
============

Code Generation Tool
--------------------
This tool allows to convert via razor any c# object into a different target language.
The main idea was to convert webapi code into client proxies using the power of
reflection, and an expresive language like razor.

With this tool you can virtually port and standardize any c# code for other languages.
It could be use to create better proxies for WCF, port your domain classes,  DTOs or any
other object to a different language like java, javascript, typescript, objective-c, etc.

It's specially powerful when working with distributed systems with many different 
client applications written in different languages.

Configuring a New Targeting Language
-----
This tool is a command line tool, and it requieres configuration files in order to work. All the configuration
is written in json. The first thing to understand about this tool is that it can produce code for potentially any
language out there. Inside the tool's configuration folder, you'll find folders for targeting languages, like typscript. Inside these folders you'll find the main configuration files:

###translations.json
This file contains native raw translations from c# to the targeting language. For example, Int16, Int32, Float, Double, etc will be natively translated to numbers in typescript. In here you should provide native translations:
 
**Structure**
 Field          | Type    | Meaning
----------------| --------|-----------
Name            | string  | C# native type name (I.E: System.Byte, System.Int32, etc)
Translation     | string  | Targeting language type

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
###codegeneration.json 
This file contains the codegeneartion items. The only purpose of this file is to give an alias and description to any existing generation item, and referring the path of the razor templates for each case:

**Structure**
Field           | Type    | Meaning
----------------| --------|-----------
Name            | string  | Custom name for the generation item. Should be something meaningful.
Description     | string  | Short description about the purpose of the item.
TemplateFile    | string  | Path to the razor template file.

**Example**
```javascript
[{
    "Name": "ServiceInterface",
    "Description": "Generates a proxy service interface",
    "TemplateFile": "configuration/typescript/templates/serviceInterface.tpl"
}]
```

### templates folder
Inside this folder you'll find the razor templates. You can separate shared code between template in a different files, and include it later on the code using the custom sentence **@include**


Adding Output file Configurations
-----
Once you created your templates for a given targeting language, you must feed some data to the tool in order to generate the code. Basically, this configuration tells the tool which assembly to process, which type is the entry point, and from there, how and what to generate from the data. 



Tool Line Arguments
-----
**-f, --filename** Indicates the path of a output configuration json file.

**-d, --directory** Indicates a directory path in which all the output configuration files will be used to generate code.

**-h, --help** Prompts the help.

