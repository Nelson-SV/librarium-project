1:
Added the initial schema migration in V001__initial_schema.sql.
Created the entities Book, Loan and Member, and also the LibrariumDbContext.

2:
Added the initial schema migration in V002__add_loan_foreign_keys.sql.
Realized I didn't added the foreign keys relation in the Loan table.
(For example: public Member Member { get; set; })

3:
