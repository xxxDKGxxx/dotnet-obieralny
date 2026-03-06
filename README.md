# LoanHub

LoanHub is a modern loan offer aggregator and application management platform designed to streamline the process of finding and applying for financial products. The project implements a microservices-inspired architecture using .NET 9 and Angular 20, demonstrating advanced patterns in web development.

This project was developed for the **"Web applications utilising .NET framework"** course at the Faculty of Mathematics and Information Science (MiNI), Warsaw University of Technology.

## Core Features

- **Unified Offer Aggregation**: Fetch and compare loan offers from multiple simulated bank providers through a single interface.
- **Application Lifecycle Management**: Complete workflow for submitting loan requests, from initial inquiry to document verification.
- **Secure Authentication**: Robust user management using JWT tokens and integrated Google OAuth 2.0.
- **Cloud-Native Integrations**: 
  - **Azure Blob Storage**: Secure handling and storage of application-related documents.
  - **SendGrid**: Automated email notifications for application status updates.
- **Clean Architecture**: Adherence to Domain-Driven Design (DDD) and SOLID principles across all backend services.
- **Comprehensive Testing**: Automated validation with xUnit (Backend) and Playwright (End-to-End).

## System Architecture

The platform consists of three primary components:

1.  **Frontend**: A responsive Single Page Application (SPA) built with Angular 20.
2.  **Aggregator Service**: A .NET 9 API that acts as an intermediary, consolidating offers from various banking systems into a unified format.
3.  **Bank Backend**: A .NET 9 service simulating a core banking system, responsible for offer generation, document processing, and application state management.

## Tech Stack

- **Backend**: .NET 9, ASP.NET Core Web API, Entity Framework Core
- **Frontend**: Angular 20, TypeScript, RxJS, Tailwind CSS
- **Database**: Microsoft SQL Server
- **Infrastructure**: Docker, Docker Compose
- **Cloud Services**: Azure Blob Storage, SendGrid
- **Security**: JWT, Google Identity Platform

## Getting Started

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (for local development)
- [Node.js v20+](https://nodejs.org/) (for local frontend development)

### Quick Start with Docker

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/xxxDKGxxx/dotnet-obieralny.git
    cd dotnet-obieralny
    ```

2.  **Configure environment variables**:
    Create a `.env` file in the root directory based on `.env.example`:
    ```bash
    cp .env.example .env
    ```
    Fill in the required values for Azure Blob Storage and SendGrid.

3.  **Run the application**:
    ```bash
    docker-compose up -d
    ```

The services will be available at:
- **Frontend**: [http://localhost:8080](http://localhost:8080)
- **Aggregator API**: [http://localhost:5001](http://localhost:5001)
- **Backend API**: [http://localhost:5000](http://localhost:5000)

## Authors

- [Dominik Zieliński](https://github.com/xxxDKGxxx)
- [Paula Wołkowska](https://github.com/pwolkowska)
- [Bartosz Ząbkowski](https://github.com/bzabk)
- [Jerzy Wąsiewicz](https://github.com/j-was)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
