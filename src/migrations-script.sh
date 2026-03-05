#!/bin/bash

dotnet ef migrations add AddBookAuthorNavigationInBook \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output-dir Migrations