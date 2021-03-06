# Globally Paid .NET SDK

[![NuGet](https://img.shields.io/nuget/v/globallypaid.net.svg)](https://www.nuget.org/packages/GloballyPaid.net/)
![CI](https://github.com/globallypaid/globallypaid-sdk-dotnet/workflows/CI/badge.svg)

The official [Globally Paid][gp] .NET library

Supporting [.NET Standard 2.0][netstandard]


## Installation

Using the [.NET Core CLI tools][dotnet-core-cli-tools]:

```sh
dotnet add package GloballyPaid.net
```

Using the [NuGet CLI][nuget-cli]:

```sh
nuget install GloballyPaid.net
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package GloballyPaid.net
```

From within Visual Studio:

1. Open Solution Explorer
2. Right-click on a project within your solution
3. Click on *Manage NuGet Packages*
4. Click on the *Browse* tab and search for **GloballyPaid.net**
5. Click on the **GloballyPaid.net** package, select version in the
   right-tab 
6. Click *Install*

## Documentation

For the Globally Paid API documentation, please visit [Globally Paid API documentation][gp-api-docs] 

## Samples

For a comprehensive list of examples, please visit [Globally Paid .NET SDK samples][gp-dotnet-samples].

## Usage

### Configuration

There are three ways to configure the Globally Paid SDK:

##### 1. Startup Extension

All SDK calls can be configured within `ConfigureServices` method in `Startup` class using the `AddGloballyPaid` extension.
Additionally, this extension call will register all Globally Paid services:

```c#
services.AddGloballyPaid("Your Publishable API Key", "Your Shared Secret", "Your APP ID", useSandbox: false, requestTimeoutSeconds: 90);
```

To register the Globally Paid services only, `AddGloballyPaidServices` extension can be used:

```c#
services.AddGloballyPaidServices();
```

##### 2. GloballyPaidConfiguration object

All SDK calls can be configured using the static `GloballyPaidConfiguration` object:

```c#
GloballyPaidConfiguration.PublishableApiKey = "Your Publishable API Key";
GloballyPaidConfiguration.SharedSecret = "Your Shared Secret";
GloballyPaidConfiguration.AppId = "Your APP ID";
GloballyPaidConfiguration.UseSandbox = false; //true if you need to test through Globally Paid sandbox
GloballyPaidConfiguration.RequestTimeoutSeconds = 90;
```
Or using the `GloballyPaidConfiguration` *Setup* method:

```c#
GloballyPaidConfiguration.Setup("Your Publishable API Key", "Your Shared Secret", "Your APP ID", useSandbox: false, requestTimeoutSeconds: 90);
```

##### 3. The `<appSettings>` section

All SDK calls can be configured using the `<appSettings>` section in configuration files (App.config or web.config):

```xml
<appSettings>
    <add key="GloballyPaidPublishableApiKey" value="Your Publishable API Key"></add>
    <add key="GloballyPaidSharedSecret" value="Your Shared Secret"></add>
    <add key="GloballyPaidAppId" value="Your APP ID"></add>
    <add key="GloballyPaidUseSandbox" value="false"></add> <!--true if you need to test through Globally Paid sandbox-->
    <add key="GloballyPaidRequestTimeoutSeconds" value="90"></add>
</appSettings>
```

#### Per-request configuration

All SDK service methods accept an optional `RequestOptions` object, additionally allowing per-request configuration:

```c#
var requestOptions = new RequestOptions("Your Publishable API Key", "Your Shared Secret", "Your APP ID", useSandbox: false, requestTimeoutSeconds: 90);
```
---

### Sample Charge Sale Transaction
```c#
var request = new ChargeRequest
            {
                Source = "source", //this can be the token or payment instrument identifier
                Amount = 1299,
                Capture = true, //sale charge
                ClientCustomerId = "12345", //set your customer id
                ClientInvoiceId = "IX213", //set your invoice id
                ClientTransactionDescription = "Tuition for CS" //set your transaction description
            };

//if Globally Paid services are registered, you can inject this as IChargeService in the constructor
var chargeService = new ChargeService(); 
var charge = chargeService.Charge(request);
```

---
For any feedback or bug/enhancement report, please [open an issue][issues] or [submit a
pull request][pulls].

[gp]: https://globallypaid.com/
[gp-api-docs]: https://docs.globallypaid.com/?c#
[gp-dotnet-samples]: https://github.com/globallypaid/globallypaid-sdk-dotnet-samples
[netstandard]: https://github.com/dotnet/standard/blob/master/docs/versions.md
[dotnet-core-cli-tools]: https://docs.microsoft.com/en-us/dotnet/core/tools/
[dotnet-format]: https://github.com/dotnet/format
[nuget-cli]: https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference
[package-manager-console]: https://docs.microsoft.com/en-us/nuget/tools/package-manager-console
[issues]: https://github.com/globallypaid/globallypaid-sdk-dotnet/issues/new
[pulls]: https://github.com/globallypaid/globallypaid-sdk-dotnet/pulls
