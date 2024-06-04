# Rent-Motorcycle-API

API to control customers rent.

.Net 8 API using Clean Architecture, DDD and and Vertical Slice archutecture.

Using Postgres database with Dapper ORM and Kafka message broker with MassTransit lib to produce and consume messages.

Docker and Docker Compose to build and run the application.

### Clean Architecture

Implemented by Use Cases with inputs and output ports to present data on API.

### DDD

Application projects segregated by domains:
 - **API**:
    REST Endpoints mapped without Controllers, using Minimal API approach.

 - **Application**:
    Use cases implementing all the business rules, logicals and access to repositories.

 - **Domain**:
    Models, Enums and Configurations from application domain scope.

- **Infra**:
    Database connection, Repositories and Message Broker connection.

### Vertical Slice

Approcah used to design the project, for example: use case folders has subfolfers with all that only the use case uses, excepting for shared classes like Domain Models and Repositories. It tryes to let together as maximum as possible the dependencies and avoid external dependencies.

## Use Cases

### Customers

- **GetCustomers**:
    Get Customers by Id or filtring by optional params.

- **SaveCustomer**:
    Create or update a Customer.

### Motorcycles

- **GetMotorcycles**:
    Get Motorcycles by Id or filtring by Plate.

- **SaveMotorcycle**:
    Create oa Motorcycle or update Plate.

- **RemoveMotorcycle**:
    Delete a Motorcycle if has no related Customer Rent.


### Rents

- **GetRents**:
    Get Rents by Id or filtring by Customer Id.

    When Getting By Id, is possible to query an End Date Preview and preview the End Value based on this date.

- **SaveRent**:
    Create a Rent or update End Date and calculate End Value.


## API

The API micro-service with REST Endpoints mapped without Controllers, using Minimal API approach.

### Endpoints

    GET    /api/customer          Get Customers filtering by optional params
    POST   /api/customer          Creates a Customer
    GET    /api/customer/{id}     Get Customer by Id
    PUT    /api/customer/{id}     Update a Customer
    
    PUT    /api/customer/{id}/document/upload  Upload Customer document image

    GET    /api/motorcycle        Get Motorcycles filtering by optional params
    POST   /api/motorcycle        Create a Motorcycle
    GET    /api/motorcycle/{id}   Get Motorcycle by Id
    PUT    /api/motorcycle/{id}   Update a Motorcycle Plate
    DELETE /api/motorcycle/{id}   Remove a Motorcycle if has no Rent related

    GET    /api/rent              Get Rents
    POST   /api/rent              Create a Rent for a Customer to a Motorcycle
    GET    /api/rent/{id}         Get Rent by Id and optionally End Date to preview End Value
    PUT    /api/rent/{id}         Update a Rent End Date and calculate the End Value

### Dependency Injection

The Dependence Injection is adding services segregated by Installers Extensions:

    .AddEndpoints();
    .AddUseCases();

    .AddNotifications();
    .AddConsumers();

    .AddRepositories();
    .AddDatabase();

    .AddConfigurations();

## Tests

- **Unit Tests**:
    Tests for each class and methods independently.

- **Integration Tests**:
    Tests for each endpoint call, testing the integration of all classes and methods (Requires docker compose up).

## Configuration

### Requirements

Need to install the follow:

- Git:
    https://git-scm.com/downloads

- Docker:
    https://www.docker.com/


## Getting Started

#### Clone the repository:

```bash
git clone https://github.com/fksalviano/rent-motorcycle-api.git
```

#### Go to the project directory

```bash
cd rent-motorcycle-api
```

#### Build project with Docker

```bash
docker build -t rent-motorcycle-api .
```

#### Up Docker Compose (Database with SQL Scripts and Kafka)

```bash
docker-compose up -d
```

#### Run application on container setting the network

```bash
docker run -d -p 5115:8080 \
    --network rent-motorcycle-api_default rent-motorcycle-api
```

#### Open API Swagger documentation

- http://localhost:5115/swagger


![API Swagger Doc](swagger.png?raw=true "API Swagger Doc")

# Class Diagram

Class diagram with application domain entities and his relationships.

```mermaid
classDiagram
direction LR
    Customer <-- Rent
    Rent --> Motorcycle
````

# API Architecture Sequnce Diagram

This is the general architecture used by default to implement all the endpoints and use cases showed below.

```mermaid
sequenceDiagram
    API->>Endpoint : Request
    Note left of Endpoint: API is not using Controllers.<br>Uses EndpoinsMapping classes to<br> map Endpoints classes
    Endpoint->>UseCase : Execute(input)
    UseCase->>Repository : CRUD(model)
    Repository-->>UseCase : Result Data
    UseCase-->>Endpoint : output
    Endpoint-->>API : Response(StatusCode)
````


# Customer

To exemplify the use of above architecture, those are the detailed sequence diagram for Customer´s endpoints and use cases:

## GetCustomers

UseCase executed to get Customer by Id or filtring by params.

```mermaid
sequenceDiagram
    alt is GetById
        API->>GetCustomersEndpoint : GetCustomerById(id)
    else
        API->>GetCustomersEndpoint : GetCustomers(taxId?, driverLicenseNumber?)
    end
    GetCustomersEndpoint->>GetCustomersUseCase : Execute(input)
    GetCustomersUseCase->>CustomerRepository : GetCustomers(filter)
    CustomerRepository-->>GetCustomersUseCase : Customer[]
    alt is GetById
        GetCustomersUseCase-->>GetCustomersEndpoint : Output(Customer)
    else
        GetCustomersUseCase-->>GetCustomersEndpoint : Output(Customer[])
    end
    alt is GetById
        GetCustomersEndpoint-->>API : Response.OK(Customer)
    else
        GetCustomersEndpoint-->>API : Response.OK(Customer[])
    end
````

## SaveCustomer

UseCase executed to create or update a Customer

```mermaid
sequenceDiagram
    API->>SaveCustomerEndpoint : SaveCustomers(request, id?)
    SaveCustomerEndpoint->>SaveCustomerUseCase : Execute(input)
    alt input.IsUpdate
        SaveCustomerUseCase->>CustomerRepository : UpdateCustomer(Customer)
    else
        SaveCustomerUseCase->>CustomerRepository : CreateCustomer(Customer)
    end
    CustomerRepository-->>SaveCustomerUseCase : SavedCustomers
    SaveCustomerUseCase-->>SaveCustomerEndpoint : Output(Customer)
    alt input.IsUpdate
        SaveCustomerEndpoint-->>API : Response.Accepted(Customer)
    else
        SaveCustomerEndpoint-->>API : Response.OK(Customer)
    end
````
