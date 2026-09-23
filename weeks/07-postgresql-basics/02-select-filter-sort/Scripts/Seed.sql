insert into customers (id, email, display_name, created_at)
values
    (1001, 'alice@example.com', 'Alice', timestamp '2026-09-01 10:00:00'),
    (1002, 'bob@example.com', 'Bob', timestamp '2026-09-03 11:30:00'),
    (1003, 'carol@example.com', 'Carol', timestamp '2026-09-10 09:15:00'),
    (1004, 'dave@example.com', 'Dave', timestamp '2026-09-15 18:00:00');

insert into events (id, title, starts_at, capacity, price)
values
    (1001, 'PostgreSQL Deep Dive', timestamp '2026-10-10 18:00:00', 120, 49.90),
    (1002, '.NET Community Meetup', timestamp '2026-09-25 19:00:00', 200, 0.00),
    (1003, 'Cloud Architecture Day', timestamp '2026-11-05 09:30:00', 80, 129.00),
    (1004, 'C# Performance Workshop', timestamp '2026-10-02 10:00:00', 30, 89.50),
    (1005, 'Legacy Systems Evening', timestamp '2026-08-15 18:30:00', 60, 25.00);

insert into bookings (id, customer_id, created_at, status)
values
    (1001, 1001, timestamp '2026-09-18 12:00:00', 'confirmed'),
    (1002, 1002, timestamp '2026-09-19 14:30:00', 'pending'),
    (1003, 1003, timestamp '2026-09-20 09:00:00', 'cancelled'),
    (1004, 1001, timestamp '2026-09-21 17:15:00', 'confirmed');

insert into booking_items (booking_id, event_id, ticket_count, unit_price)
values
    (1001, 1001, 2, 49.90),
    (1001, 1004, 1, 89.50),
    (1002, 1003, 2, 129.00),
    (1003, 1002, 1, 0.00),
    (1004, 1005, 3, 25.00);