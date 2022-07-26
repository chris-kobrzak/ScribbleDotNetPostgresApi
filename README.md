# POC .NET API connecting to Postgres database

## Prerequisites

- .NET 6
- Run `src/Oss.Client.Database/Scripts/InitDatabase.sql` to initialise the database.
- Confirm/update database credentials under `src/Oss.Api/appsettings.json`

## Sample requests

### Obtain authorisation token

```sh
# With HTTPie
http --verify=no -f https://localhost:5101/api/login \
  Login=carmen@gmail.com \
  Password=foo
```

### List users

```sh
curl 'https://localhost:5101/api/users' \
  -k \
  --header 'Authorization: Bearer <your JWT token>'
```
