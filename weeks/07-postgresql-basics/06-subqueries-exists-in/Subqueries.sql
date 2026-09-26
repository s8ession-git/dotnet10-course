-- 1
select id event_id, title, price 
from events 
where price > (select avg(price) from events)
order by price desc, id asc;

-- 2
select c.id customer_id, c.display_name, 
(
	select count(*)
	from bookings b
	where b.customer_id = c.id
) as booking_count
from customers c
order by c.id;

-- 3
select c.id customer_id, c.display_name
from customers c 
where c.id in (select b.customer_id from bookings b where b.status='confirmed')
order by 1;

-- 4
select c.id customer_id, c.display_name
from customers c
where exists (select 1 from bookings b where b.status ='confirmed' and b.customer_id = c.id)
order by 1;

-- 5
select c.id customer_id, c.display_name
from customers c 
where not exists (select 1 from bookings b where b.status = 'confirmed' and b.customer_id = c.id)
order by 1;

-- 6
select b.id booking_id, b.customer_id, b.status   
from bookings b
where not exists (select 1 from booking_items bi where bi.booking_id = b.id)
order by  1;

-- 7
select 
	total.booking_id,
	total.booking_total
from
(
	select bi.booking_id, sum(bi.ticket_count * bi.unit_price) booking_total 
	from booking_items bi
	group by bi.booking_id
) as total
where total.booking_total >= 1000
order by 2 desc, 1 asc;

-- 8 
select c.id customer_id, c.display_name
from customers c
where 
exists (select 1 from bookings b where b.customer_id = c.id) and 
not exists (select 1 from bookings b where b.customer_id = c.id and b.status = 'confirmed')
order by 1;
