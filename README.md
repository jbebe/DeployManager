# DeployManager


## Specification

* Date: UTC, format: `yyyy-MM-dd hh:mm:ss`
* TimeSpan: format: ISO 8601 `[-]P[{days}D][T[{hours}H][{min}M][{sec}S]]`
* The reason for using deploy and server types as individual keys is to make it easy to group reservations by deploy type. If we merge the two together, we could not distinguish reservations by deploy type.

### Schema

#### Reservation

A single reservation on a single server with all the important informations.

|      | Id          | DeployType | ServerType | BranchName    | UserId      | Start    | End      |
|------|-------------|------------|------------|---------------|-------------|----------|----------|
| Type | varchar(32) | int        | int        | nvarchar(512) | varchar(32) | datetime | datetime |
| Description | 32 byte hash of the rest of the fields | value of DeployType enum | value of ServerType enum | Name of the branch that will be checked out for deploy | Email address or other identifier of the author who committed the reservation | Starting date of the reservation | End date of the reservation

#### BatchReservation

|      | BatchId     | ReservationId |
|------|-------------|---------------|
| Type | varchar(32) | varchar(32)   |
| Description | Id of the batch | Id of the reservation |

#### ServerType

Keeps track of the globally available servers.

|      | Id   | Name          | Description   |
|------|------|---------------|---------------|
| Type | int  | nvarchar(128) | nvarchar(512) |
| Description | Value of ServerType enum | Name of ServerType enum | Explains what the server is used for.

#### DeployType

Keeps track of the globally available servers.

|      | Id   | Name          | Description   | Available |
|------|------|---------------|---------------|-----------|
| Type | int  | nvarchar(128) | nvarchar(512) | bit       |
| Description | Value of DeployType enum | Name of DeployType enum | Explains how the slot should be used. | Whether the deploy type is available for reservation or not. Availability can be set to false temporarily if needed.

#### ServerInstance

Keeps track of the actual instances. (deploy+server combinations)

|      | DeployType | ServerType | Available |
|------|------------|------------|-----------|
| Type | int        | int        | bit       |
| Description | value of DeployType enum | value of ServerType enum | Whether the server is available for reservation or not. Availability can be set to false temporarily if needed.

#### UserAuth

|      | Id          | Name          | Enabled |
|------|-------------|---------------|---------|
| Type | varchar(32) | nvarchar(512) | bit     |
| Description | Id of the user | Name of the user or email address | Whether the user is enabled to use the service or not. Useful for temporal suspension.

#### DeployTypeAuth

|      | UserId      | DeployType | Permission |
|------|-------------|------------|------------|
| Type | varchar(32) | int        | int        |
| Description | Id of the user | Available deploy type for management | States: NONE READ WRITE ADMIN <br> None cannot use the deploy type, read can only read, write can manage their own reservations while admin can delete other's reservation. |

### Endpoints

#### Server information

**Get slots and servers**
```
Form: GET /api/resource/type
Description:
    Gives back the available slots and server types.
Authorization:
    * Member of DeployManager group
    * Member of <deploy type> group
Input: -
Output:
    { 
        "deployTypes": [
            { 
                "id": 1, 
                "name": "Production",
                "description": "Production is the slot that is live for the end users."
            }
        ], 
        "serverTypes": [
            {
                 "id": 2,
                 "name": "AccountApi"
                 "description": "Account Api is used for everything in relation with account management".
            }
        ]
    }
```

**Get all available servers**
```
Form: GET /api/resource/instance
Description:
    Gives back basic informations about the available servers.
    Not all deploy-server combinations are available, 
    so you have to use this for the reservation interface.
Authorization:
    * Member of DeployManager group
    * Member of <deploy type> group
Input: -
Output:
    [
        {
            "deployType": 1,
            "serverTypes": 2
        }
    ]
```

#### Reservation

**Query reservations**
```
Format: GET /api/reservation?start=19991231123456&deploy=5&server=3
Description:
    Returns all reservations that fulfills every filter parameter at once.
    You can filter by starting date, deploy type and server type. 
    Useful for listing on a reservation GUI.
    'start' parameter is mandatory as we cannot allow to list every old reservation.
Query:
    start: yyyyMMddhhmmss date
    deploy: DeployType enum value
    server: DeployType enum value
Authorization: 
    * Member of DeployManager group
    * Member of <deploy type> group
Input: -
Output:
    [
        { 
            "id": "764352334265",
            "deployType": 5,
            "serverType": 3,
            "branchName": "fix/ServerTools/SomeFixIDid",
            "author": "balint.juhasz@tresorit.com",
            "start": "2000-01-01 12:00:00",
            "end": "2000-02-02 12:02:02"
        }
    ]
```

**Get single reservation**
```
Format: GET /api/reservation/764352334265
Description:
    Returns a single reservation. 
    Contains the same information as in the lister 
    Later we can add more to it if details are needed. (Comments, file, etc.)
Authorization: 
    * Member of DeployManager group
    * Member of <deploy type> group
Input: -
Output:
    { 
        "id": "764352334265",
        "deployType": 5,
        "serverType": 3,
        "branchName": "fix/ServerTools/SomeFixIDid",
        "author": "balint.juhasz@tresorit.com",
        "start": "2000-01-01 12:00:00",
        "end": "2000-02-02 12:02:02"
    }
```

**Update reservation**
```
Format: PUT /api/reservation/764352334265
Description:
    Updates a single reservation with new informations.
    For simplicity, the whole object will be overwritten. (no merge)
    It contains every data as the Reservation response, except for the id.
Authorization: 
    * Member of DeployManager group
    * Member of <deploy type> group
    * Reservation is yours
Input:
    { 
        "deployType": 5,
        "serverType": 3,
        "branchName": "fix/ServerTools/SomeFixIDid",
        "author": "balint.juhasz@tresorit.com",
        "start": "2000-01-01 12:00:00",
        "end": "2000-02-02 12:02:02"
    }
Output: -
```

**Create reservation**
```
Format: POST /api/reservation
Description:
    Inserts a single reservation.
    It contains every data as the Reservation response, except for the id.
Authorization: 
    * Member of DeployManager group
    * Member of <deploy type> group with 
Input:
    {
        "deployType": 5,
        "serverType": 3,
        "branchName": "fix/ServerTools/SomeFixIDid",
        "author": "balint.juhasz@tresorit.com",
        "start": "2000-01-01 12:00:00",
        "end": "2000-02-02 12:02:02"
    }
Output:
    {
        "id": "764352334265"
    }
```

**Delete reservation**
```
Format: DELETE /api/reservation/764352334265
Description:
    Deletes a single reservation.
Authorization: 
    * Exists in GlobalAuthorization table
    * Member of <deploy type> group
    * Reservation is yours OR permission is ADMIN
Input: -
Output: -
```

#### Batch reservation

**Get batch reservations**
```
Format: GET /api/batch/reservation
Description:
    Returns batch reservations ordered from oldest to newest.
Authorization: 
    * Exists in GlobalAuthorization table
    * Batch reservation is yours OR permission is ADMIN
Input: -
Output: 
    [
        {
            "id": "837352334265",
            "reservations": [
                "764352334265", 
                "264352334265", 
                "864352334265"
            ]
        }
    ]
```

**Create batch reservation**
```
Format: POST /api/batch/reservation
Description:
    Creates a new batch reservation.
Authorization: 
    * Exists in GlobalAuthorization table
    * Member of <deploy type> group
    * Batch reservation is yours OR permission is ADMIN
Input: 
    {
        "deployType": 2,
        "serverTypes": [1, 2, 3, 4, 5],
        "branchName": "fix/ServerTools/SomeFixIDid",
        "author": "balint.juhasz@tresorit.com",
        "from": "2000-01-01 12:00:00",
        "to": "2001-11-11 12:30:40"
    }
Output: 
    {
        "from": "2000-01-01 12:00:00",
        "to": "2001-11-11 12:30:40",
        "id": "837352334265",
        "reservations": [
            "764352334265", 
            "264352334265", 
            "864352334265"
        ]
    }
```

**Delete batch reservation**
```
Format: DELETE /api/batch/reservation/837352334265
Description:
    Deletes a batch reservation.
Authorization: 
    * Exists in GlobalAuthorization table
    * Batch reservation is yours OR permission is ADMIN
Input: -
Output: -
```

**Get free times for reservation**
```
Format: GET /api/batch/available
Description:
    Gets available reservation times in a given interval.
Authorization: 
    * Exists in GlobalAuthorization table
    * Member of <deploy type> group
    * Batch reservation is yours OR permission is ADMIN
Input: 
    {
        "deployType": 2,
        "serverTypes": [1, 2, 3, 4, 5],
        "length": "P1DT2H3M4S",
        "min": "2000-01-01 12:00:00",
        "max": "2001-11-11 12:30:40"
    }
Output: 
    [
        {
            "from": "2000-01-01 12:00:00",
            "to": "2001-11-11 12:30:40"
        },
        {
            "from": "2000-01-01 12:00:00",
            "to": "2001-11-11 12:30:40"
        }
    ]
```

#### User identity

Get your own identity

```
Format: GET /api/user
Description:
    Returns all available information about the user who uses the service.
Authorization: 
    * Exists in GlobalAuthorization table
Input: -
Output: 
    {
        "name": "Balint Juhasz"
    }
```