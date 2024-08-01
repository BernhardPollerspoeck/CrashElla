# CrashElla

CrashElla aims to simplify the crash reporting landscape. The platform and framework-independent approach allows CrashElla to be used in any application.

---
## CrashElla.Core 
[![NuGet Badge](https://buildstats.info/nuget/CrashElla.Core)](https://www.nuget.org/packages/CrashElla.Core/)

The core library contains the base classes, interfaces, and functions needed to implement CrashElla.

---
## Ingest
Ingest providers are the interface between the CrashElla core library and various logging and monitoring tools. The naming convention for ingest providers follows the schema `CrashElla.Ingest.<Protocol>.<Provider>`, where the protocol is optional. All ingest providers are registered via extensions.

*Note: We welcome the implementation of additional ingest providers. Feel free to submit them as a pull request.*
### CrashElla.Ingest.Http.Seq 
[![NuGet Badge](https://buildstats.info/nuget/CrashElla.Ingest.Http.Seq)](https://www.nuget.org/packages/CrashElla.Ingest.Http.Seq/)
---
```csharp
builder.Logging.AddHttpSeqCrashElla(config =>
{
	config.ApiKey = "<API_KEY>";
	config.IngestUri = "http://localhost:5341";
})
```



---
## Framework
Framework providers are the interface between the CrashElla core library and various frameworks. The naming convention for framework providers follows the schema `CrashElla.Framework.<Framework>`. The framework providers implement the specific crash stores.

*Note: We welcome the implementation of additional framework providers. Feel free to submit them as a pull request.*

### CrashElla.Framework.Maui 
[![NuGet Badge](https://buildstats.info/nuget/CrashElla.Framework.Maui)](https://www.nuget.org/packages/CrashElla.Framework.Maui/)
```csharp
builder.Logging.Add<IngestRegistration>()
	.WithMauiCrashStore();
```


---
## Contribution

We welcome contributions to CrashElla! If you would like to contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Make your changes and commit them with clear and concise messages.
4. Push your changes to your fork.
5. Open a pull request to the main repository.

Please ensure that your code adheres to our coding standards and includes appropriate tests. Thank you for your contributions!
