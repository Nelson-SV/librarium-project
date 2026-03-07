#!/bin/bash

dotnet ef migrations script 20260306212830_ChangedStatusFieldInLoanToNonNullable \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output ../migrations/sql/V010__add_is_deleted_field_with_default_value_false.sql