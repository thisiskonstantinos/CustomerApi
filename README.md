# The Customer API

This API is a simple implementation of CRUD operation for a single Customer table in SQLite with Entity Framework.

## Improvements and future work
* The Solution is split into layers that are represented by Projects
* All internal IDs would not be exposed. Instead GUIDs would be used publicly that are not the Primary Keys from the database.
* Eception handling would need improvement. More exceptions needed and correct matching in the try/catch statements. Correct exceptions with the HTTP Code should be returned too.
* Validators for the validation in the spec, should ideally run in the Domain.Logic layer. Depending on the validation needed, possibly directly on the controller.
* Unit Testing would should cover the remaining projects
