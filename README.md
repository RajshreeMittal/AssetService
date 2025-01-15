# AssetService .NET Application

## Overview
The AssetService is a .NET application used to manage assets and their associated data. It connects to a SQL Server database for storage and a Redis cache for quick retrieval of frequently accessed data.

## Prerequisites
- .NET SDK 9.0 or later
- Docker (for containerization)
- SQL Server and Redis (can be run using Docker Compose)

## Getting Started

- Attached docker-compose.yaml for the connectivity of assetservice,sql server instance and redis
- Pull the docker image for assetservice command - docker pull rajshreemittalnagarro/assetservice:1.1
- Run the docker-compose.yaml so that sql server and redis can be connected within same network
- Check with docker ps
- Hit localhost:8080/api/asset for all asset related apis
