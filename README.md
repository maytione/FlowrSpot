# FlowrSpot Project
### Assignment Info
The app is used for flower spotting while hiking, traveling, etc. Users can check out different flowers, their details, and sightings as well as add their own. 

#### Requirements
* RESTful API as a .NET web app
* Communication is JSON over HTTP
* The database is PostgreSQL
* Include Docker

0. Project structure
Clean architecture project organisation
CQRS pattern
XUnit testing

1. User model
Exposed an endpoints that enables user registration (username, password, email)
Exposed an endpoints that enables user login (email/username, password)

2. Flowers
Exposed endpoints to get and create flowers (name, image ref, description).

3. Authentication
Each request except for user registration and retrieval of flowers should only be allowed to registered i.e. authenticated users. 
Using JWT

4. Sightings
Exposed sighting endpoints (longitude, latitude, user ref, flower ref, image ref). Operations on this endpoint include getting, creating and deleting a sighting. Only users who created a sighting can also delete it.

5. Likes
Exposed endpoints for likes. A user can like a sighting and unlike (delete) it. Along with likes implementation also extend sighting endpoint with the counter of the number of likes of a sighting. Users can only delete their own likes, not from others.

6. Quote of the day
Add a random motivational quote (https://quotes.rest and get the free quote-of-the-day) on new sighting

### Installation:

Download source code or git clone it to local folder eg. /projects/FlowrSpot
```
$ cd /projects/FlowrSpot
$ dir or ls -all
	.git/
	.gitignore
	.vs/
	Dockerfile
	FlowrSpot.Application/
	FlowrSpot.Domain/
	FlowrSpot.Infrastructure/
	FlowrSpot.Test/
	FlowrSpot.WebApi/
	FlowrSpot.sln
	README.md
	docker-compose.yml
```
build docker image (make sure that docker daemon is up and running) https://docs.docker.com/config/daemon/troubleshoot/
```
$ docker build . -t flowrspot
```
export connection string
```
$ export FLOWERSPOT_CNNSTR="Host=db;Port=5432;Database=flowrspot_db;User Id=postgres;Password=postgres;"
```
application is using 3rd party service They Said So Quotes API
in order to use that service we need to provide API key (it is free)
register here to get API key: https://theysaidso.com/register

![Alt](/qod.png "They Said So Quotes API")

export API key to app
```
$ export FLOWERSPOT_QODAPI="cq....CT....ngPR..."
```

Note:
In case API is not working (unauthorized or something) we have prepared some random local Quotes :)
```
"I go hiking because I like to practice random acts of disappearing.",
"Hiking is the answer. Who cares what the question is?",
"The best view comes after the hardest climb.",
"Hiking is just walking where it’s okay to pee.",
"Life is better when you’re hiking.",
"There are no shortcuts to any place worth going.",
"Hiking is the only way I can clear my head.",
"The mountains are calling, and I must go.",
"Hiking: because therapy is expensive.",
"I'm on a seafood diet. I see food and I eat it, especially after a hike."
```

`optional`:
we can run it using only InMemoryDatabase
```
$ export UseInMemoryDatabase=true
```
run compose file
```
$ docker-compose up
```

navigate to http://localhost:5000/swagger/index.html to access API

navigate to http://localhost:5050/ to access pgAdmin
with credentials: `admin@test.com` / `Admin123!`

use api/v1/User/register endpoint to register new user

```
curl -X 'POST' \
  'http://localhost:5000/api/v1/User/register' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "userName": "TestUser",
  "email": "user@test.com",
  "password": "User123!",
  "repeatPassword": "User123!"
}'
```
with response
```
{
  "userName": "TestUser",
  "email": "user@test.com",
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi..."
}
```

Use `accessToken` to be authorized on protected endpoints

![Alt](/auth.png "Auth example")


### Tech stack included:
Name | Description
-----|------------
***AutoMapper*** | *library that automates the mapping of properties between object*
***MediatR*** | *implementation of the Mediator design pattern*
***FluentValidation*** | *interface for defining and applying validation rules to objects*
***Microsoft Entity Framework Core*** | *cross-platform Object-Relational Mapping (ORM) framework*
***Serilog with Sensitive Enrichers*** |  *library that provides a flexible and expressive logging API*
***Swashbuckle*** | *generate interactive API documentation*
***Docker*** | *containerize applications for consistency*
***PostgreSQL*** | *the world's most advanced open source relational database*
***pgAdmin*** | *PostgreSQL Tools*


have a fun :)



### TODO:
* ~~Project structure~~
* ~~JWT Tokens~~
* ~~Add registration~~
* ~~Add login~~
* ~~Flowers CRUD~~
* ~~Sightings CRUD~~
* ~~Long Lat Validation~~
* ~~Likes implementation~~
* ~~Quote of the day - calling 3rd party service (API key configuration template)~~
* ~~Add internal quotes (funny and not so funny)~~
* ~~Setup Docker and PostgreSQL (configurable InMemoryDb or real db)~~
* Test coverage
* Final README.md
