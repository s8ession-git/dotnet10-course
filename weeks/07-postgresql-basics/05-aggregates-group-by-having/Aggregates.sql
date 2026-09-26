-- 1
select count(*) booking_count from bookings;

-- 2
select sum(bi.ticket_count) total_tickets, min(bi.unit_price) min_unit_price, max(bi.unit_price) max_unit_price, avg(bi.unit_price) avg_unit_price 
from booking_items bi 
group by booking_id;

-- 3
select status, count(status) booking_count
from bookings
group by 1
order by status asc;

-- 4
select b.customer_id, count(b.customer_id) booking_count 
from customers c 
join bookings b on b.customer_id = c.id
group by 1
order by 1;

-- 5
select bi.booking_id, count(bi.booking_id) item_count, sum(bi.ticket_count) total_tickets, sum(bi.ticket_count * bi.unit_price) booking_total 
from bookings b
join booking_items bi on b.id = bi.booking_id
group by bi.booking_id
order by 1;

-- 6
select b.customer_id, count(b.customer_id) booking_count
from bookings b 
right join customers c 
on b.customer_id = c.id
group by 1
order by 1;

-- 7
select bi.event_id, sum(bi.ticket_count), sum(bi.unit_price) 
from events e 
join booking_items bi on e.id = bi.event_id
group by bi.event_id;

-- 8
select b.customer_id, count(b.status) 
from bookings b 
join customers c on b.customer_id = c.id
where b.status = 'confirmed'
group by b.customer_id
having count(b.status) >= 2
order by 1;

-- 9
select bi.event_id, sum(ticket_count) total_tickets, count(b.status) confirmed_total 
from booking_items bi  
join bookings b on b.id = bi.booking_id 
join events e on e.id = bi.event_id
where b.status = 'confirmed'
group by bi.event_id
having sum(ticket_count) >= 5;

-- 10
select c.id, count(distinct(b.status)) booking_count, sum(coalesce(bi.ticket_count, 0)) total_tickets, sum(coalesce(bi.unit_price, 0))
from bookings b 
left join booking_items bi on b.id = bi.booking_id
right join customers c on b.customer_id = c.id
group by c.id
order by 1;