# FamousAuctions

FamousAuctions is a full-stack web application for managing online auctions, designed to provide a complete flow from bidding to payment and delivery, all in a modern, cloud-hosted architecture.

## ⭐ Overview

- Real-time bidding system using **SignalR**, allowing users to see live updates instantly without refreshing.
- Automatic **escrow payment system** integrated with Stripe:
  - After an auction ends, payment is automatically processed from the buyer to the platform.
  - Funds are transferred to the seller only after the product has been marked as delivered.
- Built-in **delivery management module**:
  - Admins can assign drivers to each auction for delivery.
  - Drivers have a dedicated dashboard where they can mark deliveries as completed.
- Role-based access control (SuperAdmin, Admin, Driver, User) using JWT authentication stored securely in cookies.
- Stripe integration for secure management of customer payment methods and seller accounts.
- Comprehensive test coverage:
  - **236 backend tests**
  - **62 frontend tests**
  - All tests are executed automatically in CI/CD pipelines.
- Cloud deployment:
  - Backend and frontend hosted on **Azure**
  - Database hosted on **Railway** (PostgreSQL)
- Automated pipelines using Azure DevOps YAML workflows.

## ⚙️ Technologies

- ASP.NET Core (.NET 9)
- Blazor WebAssembly
- Entity Framework Core (Code First)
- PostgreSQL
- Stripe API
- SignalR
- Hangfire
- Azure DevOps (CI/CD with YAML pipelines)
- MudBlazor, Bootstrap

## 📄 Documentation

For detailed technical information, architecture diagrams, and implementation details, please see the [full documentation here](./Documentatie.pdf).

