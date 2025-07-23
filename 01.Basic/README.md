# ASP.NET Core Web API Setup

This module sets up a **basic ASP.NET Core Web API** project for the **User Management System**, using:
- **.NET SDK Version:** 8.0.x (`8.0.412` was used)
- **Framework:** ASP.NET Core Web API

In this step:
- The project was initialized with `dotnet new webapi`
- A simple `UsersController` was added to test the API `/api/users/test`
- Swagger is enabled for easy testing and documentation

---
<img width="1366" height="731" alt="2" src="https://github.com/user-attachments/assets/9041fa1e-14a1-4713-ba10-8ed193118c3e" />

## `https://localhost:7262/api/users/test` **API is working**

<img width="400" height="165" alt="3" src="https://github.com/user-attachments/assets/395540b9-ab53-4635-83e1-95e57af6a8a2" />


# 🚀 ASP.NET Core Project Templates Catalog

## Table of Contents
- [API Templates](#-api-templates)
- [Web App Templates](#-web-app-templates)
- [Full-Stack Templates](#-full-stack-templates)
- [Specialized Templates](#-specialized-templates)
- [Legacy Templates](#-legacy-templates)

---

## API Templates

### `ASP.NET Core Web API (C#)`
- **Purpose**: Build RESTful HTTP APIs with Controllers or Minimal APIs
- **When to Use**:
  - Backend for SPAs/mobile apps
  - Microservices architecture
  - Requires OpenAPI (Swagger) support
- **Tags**: `C#` `Linux` `macOS` `Windows` `API` `Cloud` `Service` `Web`

### `ASP.NET Core Web API (native AOT)`
- **Purpose**: High-performance AOT-compiled Web APIs
- **When to Use**:
  - Serverless/edge computing
  - Fast startup/low memory scenarios
- **Tags**: `C#` `AOT` `Performance` `Microservices`

---

## Web App Templates

### `ASP.NET Core Web App (Razor Pages)`
- **Purpose**: Page-focused server-rendered apps
- **When to Use**:
  - Content sites/dashboards
  - Simple form handling
- **Tags**: `C#` `Server-side` `Razor` `Web`

### `ASP.NET Core Web App (MVC)`
- **Purpose**: Classic Model-View-Controller pattern
- **When to Use**:
  - Complex multi-page apps
  - Strong separation of concerns
- **Tags**: `C#/F#` `MVC` `Web` `Cross-platform`

---

## Full-Stack Templates

### `Angular + ASP.NET Core`
- **Stack**: Angular (TS) frontend + ASP.NET Core API
- **When to Use**:
  - Enterprise SPAs
  - Teams with Angular/.NET expertise
- **Tags**: `TypeScript` `SPA` `Full-stack`

### `React/Vue + ASP.NET Core`
- **Variants**:
  - JavaScript (`React|Vue + ASP.NET Core (JS)`)
  - TypeScript (`React|Vue + ASP.NET Core (TS)`)
- **When to Use**:
  - Modern interactive UIs
  - Progressive web apps
- **Tags**: `JavaScript/TS` `Component-based` `API-driven`

---

## Specialized Templates

### `ASP.NET Core gRPC Service`
- **Purpose**: High-performance RPC communication
- **When to Use**:
  - Microservices communication
  - Real-time systems
- **Tags**: `gRPC` `HTTP/2` `Protocol Buffers`

### `Web Driver Test for Edge`
- **Purpose**: Automated browser testing
- **When to Use**:
  - CI/CD test pipelines
  - Edge-specific UI validation
- **Tags**: `Testing` `C#` `WebDriver`

---

## Legacy Templates

### `.NET Framework Web App (C#/VB)`
- **Purpose**: Maintain legacy Windows web apps
- **Technologies**:
  - Web Forms
  - MVC
  - Web API
- **Tags**: `Windows-only` `Legacy` `.NET Framework`

---

## 📊 Template Selection Guide

| Use Case                      | Recommended Template(s)                     |
|-------------------------------|---------------------------------------------|
| Modern API backend            | Web API (C#) / Web API (AOT)               |
| Server-rendered web app       | Razor Pages (simple) / MVC (complex)       |
| Microservices communication   | gRPC Service                               |
| Full-stack SPA                | Angular/React/Vue + ASP.NET Core           |
| Legacy system maintenance     | .NET Framework (C#/VB)                     |
| Edge browser testing          | Web Driver Test                            |

