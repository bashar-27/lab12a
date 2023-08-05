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
