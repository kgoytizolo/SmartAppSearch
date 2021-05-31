# SmartAppSearch
This application allows a customized search of properties and / or management companies for a commercial real state business.

# Technical features
- AWS ElasticSearch
- .NET 5 - (Which contains .NET Core 3.1 and .NET Framework 4.8.x for legacy purposes - Backend)
- Data format (JSON)

# Project requirements
- AWS ElasticSearch services (cluster and node) active (use Free-Tier for this purpose).
- Elasticsearch localhost app for a faster development (https://www.elastic.co/)
- Install cURL utility for full text search and index mapping / parsing, configuring analyzers, tokens, etc through REST APIs. 

# Software architecture
- Main applications: 
    - (.NET 5) REST API to call search queries (SERVICE HOST).
        - Service has been configured to be executed asynchronously in order to handle several service transactions efficiently. 
        - NEST library is called in order to access to the Elasticsearch DB easily.
    - (.NET 5) Unit Tests to call the existing (.NET 5) REST API search service (SERVICE CLIENT).
- Uses N-Layers in order to apply SOLID principles:
    - Using Repository Pattern to apply Single-responsibility principle and Open–closed principle.
    - Using dependency injection to get required resources (DB connections, Logs, configs, repositories, Mocks for testing or simulation, etc.)



