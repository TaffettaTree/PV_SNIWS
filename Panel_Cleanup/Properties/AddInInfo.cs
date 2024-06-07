using Mono.Addins;

// Declares that this assembly is an add-in
[assembly: Addin("Panel_Cleanup", "1.0")]

// Declares that this add-in depends on the scada v1.0 add-in root
[assembly: AddinDependency("::scada", "1.0")]

[assembly: AddinName("Panel_Cleanup")]
[assembly: AddinDescription("")]