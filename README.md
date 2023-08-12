# lab12a-Async-Inn-API



Author: Bashar nidal



----

## Async Inn API

* Lab 14 -  Navigation Properties & Routing
* Lab 13 -  Dependency Injection
* Lab 12 - Intro to Entity Framework Core and APIs

----

## Description

This is a RESTful API server built using ASP.NET Core to allow Async Hotel management to better manage the assets in their hotels. This application can modify and manage rooms, amenities, and new hotel locations. The data entered by the user will persist across a relational database and maintain its integrity as changes are made to the system.



----




----

## Entity Relationship Diagram


![async-inn-erd](https://github.com/bashar-27/lab12a/assets/83985765/98d63164-07ab-455d-a0c7-0f3b54118fd8)


* Hotel table has one to many relationship with HotelRoom table
* Room table has one to many relationship with HotelRoom table
* Amenities table has one to many relationship with RoomAmenities table
* HotelRoom table is a joint table with a payload
* RoomAmenities table is a pure join table
* Layout is an enum

----

## Features

* RESTful API
* Entity Framework Core
* Dependency Injection
----

## Endpoints

### Amenities

| Method | EndPoint | Description |
|:-|:-|:-|
| GET | ```/api/Amenities``` | |
| POST | ```/api/Amenities``` | |
| GET | ```/api/Amenities/{id}``` | |
| PUT | ```/api/Amenities/{id}``` | |
| DELETE | ```/api/Amenities/{id}``` | |

```json
Sample Response of GET /api/Amenities

[
    {
        "id": 0,
        "name": "string"
    }
]
```

### HotelRooms

| Method | EndPoint | Description |
|:-|:-|:-|
| GET | ```/api/Hotels/{hotelId}/Rooms``` | |
| POST | ```/api/Hotels/{hotelId}/Rooms``` | |
| GET | ```/api/Hotels/{hotelId}/Rooms/{roomNumber}``` | |
| PUT | ```/api/Hotels/{hotelId}/Rooms/{roomNumber}``` | |
| DELETE | ```/api/Hotels/{hotelId}/Rooms/{roomNumber}``` | |

```json
Sample Response of GET /api/Hotels/{hotelId}/Rooms

[
    {
        "hotelId": 0,
        "roomNumber": 0,
        "rate": 0,
        "petFriendly": true,
        "roomId": 0,
        "room": {
            "id": 0,
            "name": "string",
            "layout": "string",
            "amenities": [
                {
                    "id": 0,
                    "name": "string"
                }
            ]
        }
    }
]
```

### Hotels

| Method | EndPoint | Description |
|:-|:-|:-|
| GET | ```/api/Hotels``` | |
| POST | ```/api/Hotels``` |
| GET | ```/api/Hotels/{id}``` |
| PUT | ```/api/Hotels/{id}``` |
| DELETE | ```/api/Hotels/{id}``` |

```json
Sample Response of GET /api/Hotels

[
    {
        "id": 0,
        "name": "string",
        "streetAddress": "string",
        "city": "string",
        "state": "string",
        "phone": "string",
        "rooms": [
            {
                "hotelId": 0,
                "roomNumber": 0,
                "rate": 0,
                "petFriendly": true,
                "roomId": 0,
                "room": {
                    "id": 0,
                    "name": "string",
                    "layout": "string",
                    "amenities": [
                        {
                            "id": 0,
                            "name": "string"
                        }
                    ]
                }
            }
        ]
    }
]
```

### Rooms

| Method | EndPoint | Description |
|:-|:-|:-|
| GET | ```/api/Rooms/``` | |
| POST | ```/api/Rooms/``` | |
| GET | ```/api/Rooms/{id}``` | |
| PUT | ```/api/Rooms/{id}``` | |
| DELETE | ```/api/Rooms/{id}``` | |
| POST | ```/api/Rooms/{roomId}/Amenity/{amenityId}``` | |
| DELETE | ```/api/Rooms/{roomId}/Amenity/{amenityId}``` | |

```json
Sample Response of GET /api/Rooms

[
    {
        "id": 0,
        "name": "string",
        "layout": "string",
        "amenities": [
            {
                "id": 0,
                "name": "string"
            }
        ]
    }
]
```

----
<hr>

## Swagger:
This is a swagger add to the code to make it easyer for the development to greatly improve the development and documentation experience.
Swagger is a tool that allows you to generate interactive API documentation, making it easier for developers to understand and interact with your API.
# Packgages should add to implement the swagger:
`Swashbuckle.AspNetCore`
# You should write this code add to apply swagger inside Program.cs:

```c#
   builder.Services.AddSwaggerGen(option =>
            option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Title = "AsyncInn API",
                Version = "v1",
            })
            );
```
```c#
    app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "AsyncInn API");
                options.RoutePrefix = "docs";
            });
```
```c#
var app = builder.Build();
            app.UseSwagger(options=>
            options.RouteTemplate="api/{documentName}/swagger.json");
```
## Image for Swagger after run code

![Screenshot 2023-08-05 195906](https://github.com/bashar-27/lab12a/assets/83985765/ce281d26-880d-4226-a0d3-4e618f252d5c)

## Image for test Code in UnitX

![Screenshot 2023-08-05 195741](https://github.com/bashar-27/lab12a/assets/83985765/c0cfe7aa-2354-4cea-a3d9-cb4641c8221e)

# Claims-based authorization in ASP.NET Core
Claims-based authorization in ASP.NET Core is a powerful mechanism for controlling access to resources in web applications. 
It is based on the concept of security claims, which are statements about a user or entity that describe their identity, role, or other attributes.
These claims are typically represented as key-value pairs, where the key is the claim type and the value is the claim value.

The process of claims-based authorization involves the following steps:

Authentication: When a user logs in or presents their credentials, ASP.NET Core's authentication middleware verifies their identity. The authentication process validates the user's credentials and generates a set of claims based on the user's identity.

Claim generation: After successful authentication, the authentication middleware generates claims based on the user's identity. These claims are added to the user's principal, which is an object representing the user's identity and associated claims.

Authorization: Once the claims are generated and associated with the user's principal, the authorization process begins. This process involves determining whether the user is allowed access to a particular resource based on the claims they possess.

Policy-based authorization: ASP.NET Core provides a policy-based authorization system, which allows developers to define authorization policies that specify the required claims or roles needed to access specific resources or perform certain actions. These policies are typically defined in the application's startup code.

Checking access: When a user attempts to access a resource, the claims-based authorization system checks whether the user's principal contains the required claims specified by the policy. If the user possesses the necessary claims, they are granted access; otherwise, access is denied.

Claims-based authorization is flexible and can be used in various scenarios, such as role-based access control, fine-grained access control, and custom access control based on user attributes.

# JWT (JSON Web Token) authentication

JWT (JSON Web Token) authentication is a popular method of securely transmitting authentication information between parties as a JSON object. It is commonly used in web applications and APIs to authenticate and authorize users. JWTs are compact and self-contained, making them easy to pass as tokens in HTTP headers or URL parameters.

A JWT consists of three parts:

 Header: Contains the type of token (JWT) and the signing algorithm used, such as HMAC SHA256 or RSA.

 Payload: Contains the claims or statements about the user or entity. These claims can include information like user ID, roles, permissions, and other custom data.

Signature: The signature is generated by combining the encoded header, payload, and a secret key. This signature is used to verify the integrity of the token and to ensure it hasn't been tampered with.


### The flow of JWT authentication typically involves the following steps:

 User Authentication: When a user logs in or presents their credentials, the server verifies their identity, and if successful, creates a JWT containing the necessary claims for the user.
 
 Token Issuance: The server generates a JWT, sets the claims in its payload, signs it with a secret key, and returns the token to the client.

 Token Usage: The client stores the JWT (e.g., in local storage or as a cookie) and includes it in the headers of subsequent HTTP requests to the server.

Token Verification: On the server-side, the incoming JWT is validated by verifying the signature and checking the token's expiration time, audience, and issuer, among other things. This ensures the token is genuine and hasn't expired.

Authorization: After verifying the token, the server can access the claims within the JWT to determine the user's identity, roles, and permissions. Based on these claims, the server can then authorize or deny access to protected resources or actions.

JWT authentication is stateless, meaning the server does not need to store any session information for each user, which makes it scalable and suitable for distributed systems

<hr>

## Lab19 

This is the part that spreates between the user (Agents ) and the (property mangers) and the (District manger) and shows wat can thety access and what can they do : for example : district manger can do all the opreations on the api as he has the full accesson the controllers the property manager can add/update/read new HotelRooms to hotels, and amenities to rooms. the agent can only update/read a HotelRoom and add/delete amenities to rooms. so the agent "can't" delete a hotelRoom . as he is only an agent .Last thing AllowAnonymous can just read(Get).

## UnitX
![Screenshot 2023-08-12 204822](https://github.com/bashar-27/lab12a/assets/83985765/559dc40f-1c32-433c-946a-56635eeb1b45)

