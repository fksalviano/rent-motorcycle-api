# Class Diagram

Classddiagram with application domain entities and his relationships.

```mermaid
classDiagram
direction LR
    Customer <-- Rent
    Rent --> Motorcycle
````

# API Architecture Sequnce Diagram

This is the general architecture used by the default to implement all the endpoints and use cases showed below.

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

## GetCustomer

UseCase executed to get Customer by Id or filtring by params.

```mermaid
sequenceDiagram
    alt is GetById
        API->>GetEndpoint : GetCustomerById(id)
    else
        API->>GetEndpoint : GetCustomers(taxId?, driverLicenseNumber?)
    end
    GetEndpoint->>GetUseCase : Execute(input)
    GetUseCase->>GetRepository : GetCustomers(filter)
    GetRepository-->>GetUseCase : Customer[]
    alt is GetById
        GetUseCase-->>GetEndpoint : Output(Customer)
    else
        GetUseCase-->>GetEndpoint : Output(Customer[])
    end
    alt is GetById
        GetEndpoint-->>API : Response.OK(Customer)
    else
        GetEndpoint-->>API : Response.OK(Customer[])
    end
````

## SaveCustomer

UseCase executed to create or update a Customer

```mermaid
sequenceDiagram        
    API->>SaveEndpoint : SaveCustomers(request, id?)    
    SaveEndpoint->>SaveUseCase : Execute(input)
    alt input.IsUpdate
        SaveUseCase->>SaveRepository : UpdateCustomer(Customer)
    else
        SaveUseCase->>SaveRepository : CreateCustomer(Customer)
    end
    SaveRepository-->>SaveUseCase : SavedCustomers    
    SaveUseCase-->>SaveEndpoint : Output(Customer)    
    alt input.IsUpdate
        SaveEndpoint-->>API : Response.Accepted(Customer)
    else
        SaveEndpoint-->>API : Response.OK(Customer)
    end
````
