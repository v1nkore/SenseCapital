# SenseCapital - TicTacToe REST API
This app is a 3x3 tic-tac-toe API for two players

## Learn how to navigate the resources provided by the TicTacToe API

All data is sent and received as JSON, and accessed from:
* http://localhost:8090 - http docker container

## Run the project
You will need the following tools

* [Visual Studio 2022]()
* [.Net 6 or later]()
* [Docker Desktop]()

## Installing
Follow these steps to get your development environment set up:
1. Clone this repository

2. At the root directory which include **docker-compose.yml** file, run below command:
```csharp
docker-compose up -d
```
3. Wait for docker compose all containers. That's it (some containers need extra time to work, so please wait if not worked in first shut)

4. You can access pgadmin page as http://host.docker.internal:5050

# Endpoints

## Most endpoints send a response according to the contract:
```javascript
{
    "value": any,
    "ok": boolean,
    "metadata": []
}
```

## **value** - value of any type, including null
## **ok** - indicates operation status
## **metadata** - array of strings, describes what went wrong

# **TicTacToe API endpoints described below**:

## Player endpoints:

<details>
<summary style="font-size:20px">Get list of players</summary>

## Request

```http
GET api/players
```

## Response

```javascript
[
    {
        "id": uuid,
        "name": string,
        "games" []
    }
]
```

## OK Example
```javascript
[
    {
        "id": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
        "name": "Vlad",
        "games": [
            {
                "id": "7c92f13a-8c6a-4ca4-bc2a-70110bcc4510",
                "playerTurn": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
                "currentState": [
                    "...",
                    ".X.",
                    "..."
                ],
                "redPlayerId": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
                "bluePlayerId": "f0ad570f-a7c3-4df9-b10e-47ea47037e4d",
                "status": "Started",
                "stepCount": 1
            }
        ]
    }
]
```
</details>

<details>
<summary style=font-size:20px>
Get player by id
</summary>

## Request

```http
GET api/players/{id}
```

| Parameter | Type | Description | From |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | **Required**  Player id| Route |

## Response

```javascript
{
    "value": {
        "id": uuid,
        "name": string,
        "games": [],
    },
    "ok": boolean,
    "metadata": []
}
```

## OK Example
```javascript
{
    "value": {
        "id": "3bf05f6f-fc5d-457f-9ac5-39df92cdb5e2",
        "name": "Updated player name 777"
    },
    "ok": true,
    "metadata": null
}
```

## Bad Request Example
```javascript
{
    "value": null,
    "ok": false,
    "metadata": [
        "Player with the specified id was not found"
    ]
}
```
</details>

<details>
<summary style="font-size:20px">Create new player</summary>

## Request

```http
POST api/players
```

**Parameters**

| Parameter | Type | Description | From |
| :--- | :--- | :--- | :--- |
| `name` | `string` | **Required** Player name | Body |

## Response

```javascript
{
    "value": uuid,
    "ok": boolean,
    "metadata": []
}
```

## OK Example
```javascript
{
    "value": "3bf05f6f-fc5d-457f-9ac5-39df92cdb5e2",
    "ok": true,
    "metadata": null
}
```

## Bad Request Example
```javascript
{
    "value": "00000000-0000-0000-0000-000000000000",
    "ok": false,
    "metadata": [
        "Invalid player name"
    ]
}
```
</details>

<details>
<summary style="font-size:20px">Update player</summary>

## Request

```http
PATCH api/players
```

| Parameter | Type | Description | From |
| :--- | :--- | :--- | :--- |
| `command` | `object` | **Required** Updated player model | Body |

### `command` Schema

```javascript
{
    "id" : uuid,
    "name" string
}
```

## Response

```javascript
{
    "value": {
        "id": uuid,
        "name": string,
        "games": []
    },
    "ok": boolean,
    "metadata": []
}
```

## OK Example
```javascript
{
    "value": {
        "id": "3bf05f6f-fc5d-457f-9ac5-39df92cdb5e2",
        "name": "Updated player name 777"
    },
    "ok": true,
    "metadata": null
}
```

## Bad Request Example
```javascript
{
    "value": null,
    "ok": false,
    "metadata": [
        "Player with the specified id was not found"
    ]
}
```
</details>

<details>
<summary style="font-size:20px">Delete player</summary>

## Request

```http
DELETE api/players/{id}
```

| Parameter | Type | Description | From |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | **Required** Player id | Route |

## Response

```javascript
{
    "value": uuid,
    "ok": boolean,
    "metadata": []
}
```

## OK Example
```javascript
{
    "value": "18af5f10-5c22-41a1-8a8f-d135eca9f5ac",
    "ok": true,
    "metadata": null
}
```

## Bad Request Example
```javascript
{
    "value": null,
    "ok": false,
    "metadata": [
        "Player with the specified id was not found"
    ]
}
```
</details>

## Game endpoints:

<details>
<summary style="font-size:20px">Get list of games</summary>

## Request

```http
GET api/games
```

## Response

```javascript
[
    {
        "id": uuid,
        "playerTurn": uuid,
        "currentState": []
        "redPlayerId": uuid,
        "redPlayer": {
            "id": uuid,
            "name": string
        },
        "bluePlayerId": uuid,
        "bluePlayer": {
            "id": uuid,
            "name": string,
        },
        "status": string,
        "stepCount": number
    }
]
```

## OK Example
```javascript
[
    {
        "value": {
            "id": "7c92f13a-8c6a-4ca4-bc2a-70110bcc4510",
            "playerTurn": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
            "currentState": [
                "...",
                ".X.",
                "..."
            ],
            "redPlayerId": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
            "redPlayer": {
                "id": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
                "name": "Vlad",
            },
            "bluePlayerId": "f0ad570f-a7c3-4df9-b10e-47ea47037e4d",
            "bluePlayer": {
                "id": "f0ad570f-a7c3-4df9-b10e-47ea47037e4d",
                "name": "John",
            },
            "status": "Started",
            "stepCount": 1
        },
        "ok": true,
        "metadata": null
    }
]
```
</details>

<details>
<summary style="font-size:20px">Get game by id</summary>

## Request

```http
GET api/games/{id}
```

| Parameter | Type | Description | From |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | **Required** Game id | Route |

## Response

```javascript
{
        "id": uuid,
        "playerTurn": uuid,
        "currentState": []
        "redPlayerId": uuid,
        "redPlayer": {
            "id": uuid,
            "name": string
        },
        "bluePlayerId": uuid,
        "bluePlayer": {
            "id": uuid,
            "name": string,
        },
        "status": string,
        "stepCount": number
}
```

## OK Example
```javascript
{
    "value": {
        "id": "7c92f13a-8c6a-4ca4-bc2a-70110bcc4510",
        "playerTurn": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
        "currentState": [
            "...",
            ".X.",
            "..."
        ],
        "redPlayerId": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
        "redPlayer": {
            "id": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
            "name": "Vlad",
        },
        "bluePlayerId": "f0ad570f-a7c3-4df9-b10e-47ea47037e4d",
        "bluePlayer": {
            "id": "f0ad570f-a7c3-4df9-b10e-47ea47037e4d",
            "name": "John",
        },
        "status": "Started",
        "stepCount": 1
    },
    "ok": true,
    "metadata": null
}
```

## Bad Request Example
```javascript
{
    "value": null,
    "ok": false,
    "metadata": [
        "Game with the specified id was not found"
    ]
}
```
</details>

<details>
<summary style="font-size:20px">Create game</summary>

## Request

```http
POST api/games
```

| Parameter | Type | Description | From |
| :--- | :--- | :--- | :--- |
| `command` | `object` | **Required** Create game model | Body |

### `command` Schema
```javascript
{
    "bluePlayerId": uuid,
    "redPlayerId": uuid
}
```

## Response
```javascript
{
    "value": uuid,
    "ok": boolean,
    "metadata": null
}
```

## OK Example
```javascript
{
    "value": "dc5b9f18-b298-4e03-bb66-5c5adf05a4f2",
    "ok": true,
    "metadata": null
}
```
## Bad Request Example
```javascript
{
    "value": "00000000-0000-0000-0000-000000000000",
    "ok": false,
    "metadata": [
        "Player can't plan game with himself"
    ]
}
```
</details>

<details>
<summary style="font-size:20px">Do step</summary>

## Request

```http
PATCH api/games
```

| Parameter | Type | Description | From |
| :--- | :--- | :--- | :--- |
| `command` | `object` | **Required** Do step model | Body |

### `command` Schema

```javascript
{
    "gameId" : uuid,
    "playerId" : uuid,
    "cell" : number
}
```

## Response
```javascript
{
    "value": {
        "id": uuid,
        "playerTurn": uuid,
        "currentState": []
        "redPlayerId": uuid,
        "bluePlayerId": uuid,
        "status": string,
        "stepCount": number
    },
    "ok": boolean,
    "metadata": []
}
```

## OK Example
```javascript
{
    "value": {
        "id": "980ef79d-74c1-47ce-8443-df20ec9ed7ad",
        "playerTurn": "18af5f10-5c22-41a1-8a8f-d135eca9f5ac",
        "currentState": [
            "...",
            ".X.",
            "..."
        ],
        "redPlayerId": "18af5f10-5c22-41a1-8a8f-d135eca9f5ac",
        "bluePlayerId": "b134de1e-1ad6-41fd-8434-930d8689eaa3",
        "status": "Started",
        "stepCount": 1
    },
    "ok": true,
    "metadata": null
}
```

## Bad Request Example
```javascript
{
    "value": {
        "id": "7c92f13a-8c6a-4ca4-bc2a-70110bcc4510",
        "playerTurn": "f0ad570f-a7c3-4df9-b10e-47ea47037e4d",
        "currentState": [
            "..0",
            ".X.",
            "..."
        ],
        "redPlayerId": "cfd7e708-ac5b-49af-987f-d4ee5189a5a2",
        "bluePlayerId": "f0ad570f-a7c3-4df9-b10e-47ea47037e4d",
        "status": "Started",
        "stepCount": 2
    },
    "ok": false,
    "metadata": [
        "Cell with the specified row and column already marked"
    ]
}
```

</details>

<details>
<summary style="font-size:20px">Delete game</summary>

## Request

```http
DELETE api/games/{id}
```

| Parameter | Type | Description | From |
| :--- | :--- | :--- | :--- |
| `id` | `uuid` | **Required** Game id | Route |

## Response

```javascript
{
    "value": uuid,
    "ok": boolean,
    "metadata": []
}
```

## OK Example
```javascript
{
    "value": "933a29a0-e2bf-4ec9-9b03-c27b6a4c1698",
    "ok": true,
    "metadata": null
}
```

## Bad Request Example
```javascript
{
    "value": "980ef79d-74c1-47ce-8443-df20ec9ed7ac",
    "ok": false,
    "metadata": [
        "Game with the specified id was not found"
    ]
}
```
</details>