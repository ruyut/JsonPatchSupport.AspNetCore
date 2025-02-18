# JsonPatchSupport.AspNetCore

[![NuGet version (JsonPatchSupport.AspNetCore)](https://img.shields.io/nuget/v/JsonPatchSupport.AspNetCore)](https://www.nuget.org/packages/JsonPatchSupport.AspNetCore)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/ruyut/JsonPatchSupport.AspNetCore/publish.yml)](https://github.com/ruyut/JsonPatchSupport.AspNetCore/actions/workflows/publish.yml)

## Overview

JsonPatchSupport.AspNetCore is a lightweight package that enables JSON Patch support in ASP.NET Core using the
Newtonsoft.Json library, allowing simple partial updates with minimal configuration.

## Installation

Install the package via .NET CLI:

```bash
dotnet add package JsonPatchSupport.AspNetCore
```

Alternatively, add a project reference to your ASP.NET Core application.

## Usage

**Register Services**

Add the auto service registration extension in your service configuration:

```csharp
// add using directive
using JsonPatchSupport.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// register service
builder.Services.AddJsonPatchSupport();

var app = builder.Build();
```

## API Example

Below is an example of how to use JSON Patch in an API controller:

```csharp
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    [HttpPatch("{id}")]
    public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<ProductDto> patchDocument)
    {
        // Simulate getting the entity from a data source
        var product = new ProductDto
        {
            Name = "product1",
            Price = 10,
        };

        // Apply the patch to the product
        patchDocument.ApplyTo(product, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Simulate saving the updated entity to a data source

        return Ok(product);
    }
}

public class ProductDto
{
    public string Name { get; set; }
    public int Price { get; set; }
}
```

Example JSON Patch document:

```json
[
  {
    "op": "replace",
    "path": "/Name",
    "value": "NewProductName"
  },
  {
    "op": "replace",
    "path": "/Price",
    "value": "99"
  }
]
```

Example cURL request:

```shell
curl -X 'PATCH' \
  'http://localhost:5183/api/Product/1' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json-patch+json' \
  -d '
[
  {
    "op": "replace",
    "path": "/Name",
    "value": "NewProductName"
  },
  {
    "op": "replace",
    "path": "/Price",
    "value": "99"
  }
]'
```

Expected response:

```text
{
  "name": "NewProductName",
  "price": 99
}
```
