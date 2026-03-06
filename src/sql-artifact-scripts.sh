#!/bin/bash

dotnet ef migrations script 20260306141021_AddMemberPhoneNumberAsNonNullable \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output ../migrations/sql/V008__add_status_field_in_loan_as_nullable.sql