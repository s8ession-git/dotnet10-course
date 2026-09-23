-- 1
select id, title, price from events;

-- 2
select * from events 
where price > 50;

-- 3
select * from events 
where (capacity >= 50) and (price < 100); 

-- 4
select * from events
where starts_at >= timestamp '2026-10-01 00:00:00';

-- 5
select * from events 
order by price desc, starts_at asc;

-- 6
select * from events 
where starts_at >= timestamp '2026-09-01 00:00:00'
order by events.starts_at desc
limit 3;

-- 7 
select title, price, price * 2 as price_for_two_tickets 
from events;

-- 8
select id, display_name || '<' || email || '>' as customer_label 
from customers;

-- 9
select * from bookings b 
where status = 'confirmed';

-- 10
select null = null; -- unknown? [NULL] !
select null is null; -- true? true!
select 10 > null; -- unknown? [NULL] !
