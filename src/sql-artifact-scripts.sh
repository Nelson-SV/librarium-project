#!/bin/bash

dotnet ef migrations script 20260306204756_AddStatusFieldInLoanAsNullable \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output ../migrations/sql/V009__changed_status_field_in_loan_as_non_nullable.sql