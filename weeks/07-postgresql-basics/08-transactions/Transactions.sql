-- 1
begin;
select * from events; -- 1002: price = 0
update events set price = price + 100 where id = 1002;
select * from events where id = 1002;
rollback;
select * from events where id = 1002;
-- before: 0
-- after update: 100
-- after rollback: 0

-- 2
begin;
update events set price = price + 50 where id = 1002;
commit;
select * from events where id = 1002;
begin;
update events set price = price - 50 where id = 1002;
commit;
select * from events where id = 1002;

-- 3
begin;
update events set price = price + 100 where id = 1002;
update events set capacity = 0 where id = 1002;
select * from events;
rollback;
select price, capacity from events where id = 1002;
-- SQL Error [23514]: ОШИБКА: новая строка в отношении "events" нарушает ограничение-проверку "events_capacity_check". Подробности: Ошибочная строка содержит (1002, .NET Community Meetup, 2026-09-25 19:00:00, 0, 100.00).
-- SQL Error [25P02]: ОШИБКА: текущая транзакция прервана, команды до конца блока транзакции игнорируются

-- 4
begin;
update events set price = price + 100 where id = 1002;
savepoint after_price_change;
update events set capacity = 0 where id = 1002;
rollback to savepoint after_price_change;
select * from events where id = 1002;
rollback;
select * from events where id = 1002;

-- 5
select * from bookings;
begin;
insert into bookings(customer_id, status, created_at) values (1004, 'pending', now())
returning id; -- 3
insert into booking_items (booking_id, event_id, ticket_count, unit_price) values (3, 1002, 2, 79.99);
update bookings set status='confirmed' where id = 3;
select * from bookings where id = 3;
select * from booking_items bi where bi.booking_id = 3;
commit;
select * from bookings where id = 3;
select * from booking_items bi where bi.booking_id = 3;

-- 6
begin;
insert into bookings(customer_id, status, created_at) values (1001, 'pending', now()) returning id; -- 4
insert into booking_items (booking_id, event_id, ticket_count, unit_price) values (4, 1002, 0, 22.22);
/*
SQL Error [23514]: ОШИБКА: новая строка в отношении "booking_items" нарушает ограничение-проверку "booking_items_ticket_count_check"
  Подробности: Ошибочная строка содержит (4, 1002, 0, 22.22).
*/
rollback;
select * from bookings where id = 4; -- пусто

-- 7
begin;
update bookings set status = 'cancelled' where id = 3 and status = 'pending'
returning id, status;
-- 0 строк
select * from bookings where id = 3; -- confirmed
rollback;