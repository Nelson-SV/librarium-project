Note from Nelson:
I made the migrations log file in "my way", while I was solving the requirements.
I have not seen the suggested structure before.
So I asked an AI to adjust my migrations log for that structure.
I think my file contains everything that is required, but not in the "right" structure.
Hope this is not a problem.

The following is the Migrations log considering the suggested structure:

1 — V001: Initial Schema
- Description: Created the initial database schema with Books, Loans, and Members tables.
- Type of change: Additive (non-breaking)
- API impact: No existing endpoints affected. This is the baseline schema.
- Deployment notes: Must be applied before the application starts. No existing data to consider.
- Decisions and tradeoffs: Created the three core entities — Book, Loan, and Member — with their required fields and registered them in LibrariumDbContext. No relationships were defined at this stage; that was addressed in the following migration.

  ---
2 — V002: Add Loan Foreign Keys
- Description: Added missing foreign key relationships between Loans, Books, and Members.
- Type of change: Additive (non-breaking)
- API impact: No endpoint behaviour changed.
- Deployment notes: Safe to apply at any time. No data loss risk.
- Decisions and tradeoffs: The initial migration omitted foreign key constraints on the Loan table. This migration alters the MemberId and BookId columns to the correct type and adds the FK constraints with cascade delete. Addressed immediately after discovery to ensure referential integrity from the start.

  ---
3 — V003: Add Authors and BookAuthors
- Description: Introduced Author entity and BookAuthor join table to support many-to-many book-author relationships.
- Type of change: Additive (non-breaking)
- API impact: A new v2 book controller was created to return author information alongside books. The v1 endpoint was left unchanged. Books without authors are still returned normally.
- Deployment notes: Safe to apply at any time. No existing data is affected — books can exist without authors.
- Decisions and tradeoffs: A join table was used to model the many-to-many relationship since a book can have multiple authors and an author can have multiple books. A new API version was introduced rather than modifying v1, to avoid breaking existing consumers that don't need author data.

  ---
4 — V004: Add BookAuthor Navigation in Book
- Description: Added navigation property on the Book entity to support loading authors via Include/ThenInclude.
- Type of change: Additive (non-breaking)
- API impact: No schema change. DTOs were introduced to control the response shape for v1 and v2 independently, preventing the authors collection from leaking into the v1 response.
- Deployment notes: No database update required — this only updates the EF Core model snapshot.
- Decisions and tradeoffs: Without the navigation property on Book, the Include/ThenInclude query could not be built. DTOs were introduced at this point because without them, the new BookAuthors collection would appear in the v1 response (empty), which was not the intended behaviour. Each controller version now has its own response shape.

  ---
5 — V005: Add Member Email Unique Index
- Description: Enforced uniqueness on the Member email column.
- Type of change: Additive (potentially breaking)
- API impact: No endpoint response changed. A new update member endpoint was added to allow resolving duplicate emails before applying the migration.
- Deployment notes: The migration must not be applied while duplicate emails exist in the database — SQL Server will refuse to create a unique index on a column with duplicate values. Duplicates must be resolved first via the update endpoint.
- Decisions and tradeoffs: Email is used as the login identifier, so duplicates are an operational problem. Rather than cleaning data manually, an update endpoint was added to provide a legitimate mechanism for correcting member details. The unique constraint was only applied once the data was confirmed clean.

  ---
6 — V006 + V007: Add Member Phone Number
- Description: Added a mandatory phone number field to Members, introduced in two migrations.
- Type of change: Requires coordination
- API impact: No existing endpoint response changed. The update member endpoint was used to populate phone numbers for existing members before enforcing the constraint.
- Deployment notes: V006 must be applied first (nullable column), then existing members must be updated with phone numbers, then V007 can be applied (NOT NULL constraint). Applying V007 before all rows have a value will cause a constraint failure.
- Decisions and tradeoffs: Adding a NOT NULL column directly to a table with existing data would fail. The expand/contract pattern was used: add as nullable first, populate existing rows through the update endpoint, then tighten the constraint. This approach avoids fabricating data — users provide their own phone number through a legitimate application flow.

  ---
7 — V008 + V009: Add Loan Status
- Description: Added a Status field to Loans supporting Active, Returned, Overdue, and Lost values.
- Type of change: Requires coordination
- API impact: The v1 loan endpoint was left unchanged — a DTO was introduced to ensure Status does not appear in the v1 response, preserving the contract the frontend team depends on. A new v2 loan controller was created to expose Status.
- Deployment notes: V008 adds the column as nullable and immediately populates existing rows using SQL statements in the migration's Up method. During the window between V008 being applied and the new code being deployed, the old code creates new loans without setting Status, resulting in NULL values. The same UPDATE statements must be run again before applying V009 to catch any loans created in that window. V009 can only be applied safely once all rows have a value.
- Decisions and tradeoffs: Existing loans were populated using ReturnDate as a proxy: if ReturnDate is not null the loan is Returned, otherwise Active. This was done inside the migration itself using migrationBuilder.Sql() rather than a separate script, so the data population is atomic with the schema change. The v1 endpoint was preserved without modification for at least two sprints, as the frontend team cannot update their client in the short term.

  ---
8 — V010: Add IsDeleted to Books
- Description: Added a soft-delete flag to Books to support retiring books from the catalogue.
- Type of change: Additive (non-breaking)
- API impact: The IsDeleted filter was added to the book catalogue queries (v1 and v2) so retired books no longer appear in search results. A new retire endpoint was added. The filter was intentionally not applied to loan history queries — retired books must still appear on historical loan records for auditing purposes.
- Deployment notes: Safe to apply at any time. All existing books default to IsDeleted = false. No existing behaviour changes until a book is explicitly retired.
- Decisions and tradeoffs: The developer's proposed WHERE clause was partially accepted. Applying it globally would have caused book details to appear as null on loan history responses, breaking the auditing requirement. The filter was scoped only to catalogue endpoints. A guard was also added to loan creation to reject loans against retired books, enforcing the business rule at the application layer rather than through a database trigger, which would have been harder to test and debug.

  ---
9 — V011 + V012 + V013: Replace ISBN Column Type
- Description: Replaced the bigint ISBN column with a string column to support correctly formatted ISBNs including hyphens.
- Type of change: Destructive (requires coordination)
- API impact: This is a breaking change for API consumers. During the transition, v1 and v2 continued returning the old numeric isbn value. Once a book was updated via the update endpoint, the old numeric field became 0 since there was no mapping between the two columns. V3 was introduced as the correct endpoint going forward, returning isbn as a properly formatted string. After V013 was applied and DTOs updated, v1 and v2 isbn fields reflect the string value.
- Deployment notes: Three migrations must be applied in order. V011 adds IsbnNew as nullable — safe to apply immediately. Books must then be updated with correct ISBN values via the update endpoint before V012 is applied. V013 drops the old Isbn column and renames IsbnNew to Isbn. V013 was manually adjusted in the migration file — EF Core would have generated a drop and add instead of a rename, which would have caused data loss. The SQL artifacts reflect each step individually, providing a full audit trail.
- Decisions and tradeoffs: SQL Server does not allow altering a column type in place when data exists, so a two-column approach was required. The existing values were corrupted and unrecoverable so no data migration was attempted. EF Core does not detect renames automatically, so the Up and Down methods in V013 were written manually using migrationBuilder.DropColumn and migrationBuilder.RenameColumn. A new API version (v3) was introduced to expose the corrected string ISBN without forcing an immediate breaking change on existing consumers.