#!/bin/bash

dotnet ef migrations script 20260308130545_ChangedIsbnNewFieldInBookToNotNullable \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output ../migrations/sql/V013__removed_isbn_field_and_renamed_isbnnew_in_book.sql