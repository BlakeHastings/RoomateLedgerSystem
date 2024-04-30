# RoomateLedgerSystem
This is a project for importing transactions from multiple systems to allow
for easier auditing of finances for rent calculations.

This project is written to separate concerns due to the nature of data sources
coming from unmanaged systems (websites of companies) which means you may be
able to use some of data pipelines for your own projects. 


# Utilizes entity framework core
TODO: add information about setting up dev environment for entity framework core
https://learn.microsoft.com/en-us/ef/core/get-started/overview/install#get-the-net-core-cli-tools
Install-Package Microsoft.EntityFrameworkCore.Tools

# Recommended Dev Environment Configurations
- Install .NET 8 SDK
	winget install Microsoft.DotNet.SDK.8

- disable stop on selenium exceptions that are inside WebDriverWait Until calls
- DO NOT PUT CREDENTIALS IN appsettings.json!!! Use the VS Secrets Manager

# Code Standards

## Database Table Naming Standard
- Plural table names
- Capitals
- Camelcase for composites
- No underscores in table names

## Database Field Naming Standard
- Camelcase
- underscores for prefix / suffix
 
## Web Parsers
- Variables / methods should closely relate to the page content being parsed. (If parsing a table, match variable names for values to the headers of the table)
- Data extracted from the page should have its own model relative to the page.
- Mapping between expected end model and page data model should be separate from parsing logic

## Comments
- Always try to explain with code (Method names, easy to read patterns, variable names)
- If comment is needed, it should explain what is not visually apparent to a developer

# running ETL console app
TODO: figure out how to pass config to ETL console app
