-- Turning a reporting scan into a seek with a covering index.
-- The query rolls up bandwidth by customer and day over a date range.

-- Before: only the clustered PK exists, so this scans the whole table.
SELECT CustomerId, [Date], SUM(Bytes) AS TotalBytes
FROM   dbo.BandwidthDaily
WHERE  CustomerId = @customerId
  AND  [Date] BETWEEN @from AND @to
GROUP  BY CustomerId, [Date];

-- The fix: index the predicate columns, INCLUDE the measured column so the
-- query is answered entirely from the index (a "covering" index) — no key
-- lookups back to the base table.
CREATE NONCLUSTERED INDEX IX_BandwidthDaily_Customer_Date
    ON dbo.BandwidthDaily (CustomerId, [Date])
    INCLUDE (Bytes);

-- Read the actual plan before and after:
--   SET STATISTICS IO ON;
-- You want "Index Seek" instead of "Index Scan", and far fewer logical reads.
