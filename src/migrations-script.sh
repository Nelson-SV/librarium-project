#!/bin/bash

dotnet ef migrations add AddMemberPhoneNumberAsNonNullable \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output-dir Migrations