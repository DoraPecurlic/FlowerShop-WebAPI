SELECT o."Id", o."FlowerType", o."Quantity", o."OrderTypeId", o."IsActive", o."DateCreated", o."DateUpdated", u."FirstName" || ' ' || u."LastName" AS "UserFullName", u."Role"
FROM public."Order" o
JOIN public."User" u ON o."CreatedByUserId" = u."Id";
