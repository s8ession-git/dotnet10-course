-- 1
select 
b.id as booking_id, 
c.display_name, c.email,
b.status, 
b.created_at as booking_creation_time, 
c.created_at as customer_creation_time
from bookings b 
join customers c on (b.customer_id = c.id)
order by 1;

-- 2
select bi.booking_id, bi.event_id, e.title, bi.ticket_count, bi.unit_price  
from booking_items bi 
join events e on (bi.event_id = e.id)
order by bi.booking_id, bi.event_id;

-- 3
select b.id booking_id, c.display_name customer_name, e.title event_title, e.starts_at event_starts_at, bi.ticket_count, bi.unit_price, bi.ticket_count * bi.unit_price line_total, b.status booking_status 
from bookings b 
join customers c on c.id = b.customer_id
join booking_items bi on bi.booking_id = b.id
join events e on e.id = bi.event_id
ORDER BY b.id, e.id;

-- 4
select c.id customer_id, c.display_name, b.id booking_id, b.status booking_status  
from customers c left join bookings b on c.id = b.customer_id
ORDER BY c.id, b.id;

-- 5
select * from customers c 
left join bookings b on c.id = b.customer_id and b.status = 'confirmed';

-- 5: alternative - более логично - вложенный запрос - оставляю только тех customers, у которых статус бронирования - confirmed. Зачем мне всех перечислять?
select * from customers where id in ( select distinct(customer_id) from bookings where status = 'confirmed');

-- 6
select c.* from customers c left join bookings b on c.id = b.customer_id where b.status is null;

-- 7
select c.* from customers c left join bookings b on c.id = b.customer_id where b.status = 'confirmed';

-- 8
SELECT
    b.id AS booking_id,
    c.display_name AS customer_name,
    e.title AS event_title,
    e.starts_at AS event_starts_at,
    bi.ticket_count,
    bi.unit_price
FROM bookings AS b
JOIN customers AS c
    ON c.id = b.customer_id
JOIN booking_items AS bi
    ON bi.booking_id = b.id
JOIN events AS e
    ON e.id = bi.event_id
WHERE b.status = 'confirmed'
ORDER BY e.starts_at, b.id;


-- 9
select bi.booking_id, c.display_name customer_name, e.title event_title, bi.ticket_count from booking_items bi
join events e on bi.event_id = e.id
join bookings b on b.id = bi.booking_id 
join customers c on c.id = b.customer_id 
where bi.ticket_count >= 2;

-- 10
select * from customers c LEFT join bookings b on c.id = b.customer_id ;
select * from bookings b LEFT JOIN customers c on c.id = b.customer_id ;