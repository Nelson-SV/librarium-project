#!/bin/bash

dotnet ef migrations add AddMemberEmailUniqueIndex \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output-dir Migrations