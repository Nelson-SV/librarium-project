#!/bin/bash

dotnet ef migrations script 20260223210149_InitialSchema \
  --project Librarium.Data \
  --startup-project Librarium.Api \
  --output ../migrations/sql/V002__add_loan_foreign_keys.sql