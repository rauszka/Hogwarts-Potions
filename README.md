# Hogwarts-Potions
A simple backend application in C# using Entity Framework Core. It has routes for listing potions, rooms, and other extra functions. Furthermore, it provides insight into asynchronous programming and code-first approach.

## Tasks

## Create a database

- The school seems to have run out of parchments. Create a database to persist students and rooms. The first step is to create a HogwartsContext class, which inherits from DbContext.
- There is a Student model class with a long ID with a [DatabaseGenerated(DatabaseGeneratedOption.Identity)] attribute.
- There is a DbSet<Student> property in the HogwartsContext class.
- There is a Room model class with a long ID with a [DatabaseGenerated(DatabaseGeneratedOption.Identity)] attribute.
- There is a DbSet<Room> property in the HogwartsContext class.

## Using the database

- All services must use the database. This also includes request handling. Every piece of data used by responses must originate from the database.
- Endpoints for creating, finding, deleting, updating, finding available rooms, or finding rooms for rat owners create the same response as with the in-memory database.

## Potions – Studying recipes

- The basics of the Potions class are recipes. By recipes, we can identify potions later. A Recipe has an id, a name, a Student who brew it, and, of course, a list of - - Ingredients. An Ingredient contains an id and a name.
- There is an Ingredient class with a long ID with a [DatabaseGenerated(DatabaseGeneratedOption.Identity)] attribute.
- There is a DbSet<Ingredient> property in the HogwartsContext class.
- There is an Recipe class with a long ID with a [DatabaseGenerated(DatabaseGeneratedOption.Identity)] attribute.
- There is a DbSet<Recipe> property in the HogwartsContext class.

## About potions

- A Potion has an id, name, a Student who brews it, a list of ingredients, a BrewingStatus, and a Recipe. Until a potion does not contain five ingredients, its BrewingStatus is brew. After that, if there is already a Recipe with the same ingredients (in any order), the status is replica. Otherwise, the status is discovery.  Create an endpoint at /potions. Here you must list all potions.
- Potions are persisted in the database.
- At /potions all existing potions are listed.


## Brewing potions

- If you aim to know " how to bottle fame, brew glory.." and the other things Professor Snape told you at the beginning of the class, you should take notes of your potions. Your task is to learn the ins and outs of potion brewing by handling POST requests at the /potions endpoint. The request consists of a Potion just brewed, containing the Student ID, and the list of ingredients. If a Recipe with these Ingredients (in any order) already exists, the Potion is a replica. If no such Recipe exists, the potion is a discovery, so the Recipe must also be persisted.
- There is an endpoint at /potions, where a Student can brew the Potion by sending a POST request.
- The list of Ingredients is checked if it matches any Potion.
- If the brew is a discovery, the Recipe is persisted with the list of Ingredients, the Student, and with a name generated from the Student's name (e.g. "John Doe's discovery #2").
- The response contains the persisted Potion.

## Student's cookbook
- By sending a GET request to /potions/{student-id}, all explored Potions of a Student are listed.
- At /potions/{student-id}, all known Potions of a Student are listed.

## Exploratory brewing

- By sending a POST request to /potions/brew a new Potion is generated containing the Student and the status of brewing.
- By sending a PUT request to /potions/{potion-id}/add the Potion with the potion-id gets updated with the new Ingredient. The response contains the updated Potion object.
- If the potion has less than five Ingredients, sending a GET request to /potions/{potion-id}/help, returns the Recipes that contain the same Ingredients as the Potion brewing.


## Requirements

- create a new **feature branch** and commit every change to your Github repo
- It’s even better if you make [atomic commits](https://en.wikipedia.org/wiki/Atomic_commit)


### Features
- Refresh your knowledge of asynchronously fetching data from backend.
- Serialize data to JSON.
- Call an API from the backend.
- Multi-server systems.


# Links
##### [Dependency Injection vs Dependency Inversion](https://betterprogramming.pub/straightforward-simple-dependency-inversion-vs-dependency-injection-7d8c0d0ed28e)
##### [Asynchronous programming with async and await](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/)
##### [CRUD API with EF Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud?view=aspnetcore-5.0)
##### [Get started with EF Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-5.0)
