-- 1
insert into customers (id, email, display_name, created_at)
values (2001, 'erin@example.com', 'Erin', DEFAULT)
returning *;
-- 2
insert into events (id, title, starts_at, capacity, price)
values 
(2001, 'Docker for .NET Developers', timestamp '2026-12-05 11:00:00', 90, 75.00),
(2002, 'Free SQL Practice', timestamp '2026-10-20 18:30:00', 150, 0.00)
returning id, title, price;

-- 3
-- select * from bookings;
insert into bookings (id, customer_id, created_at, status)
values (2001, 2001, timestamp '2026-09-23 20:00:00', 'pending')
returning id, customer_id, created_at, status;

-- 4
-- select * from booking_items bi ;
insert into booking_items (booking_id, event_id, ticket_count, unit_price)
values (2001, 2001, 2, 75.00), (2001, 2002, 1, 0.00)
returning booking_id, event_id, ticket_count, unit_price;

-- 5
select * from events where id = 2001;
update events 
set price = price * 1.1 where id = 2001
returning id, title, price;

-- 6
select * from bookings;
update bookings set status = 'confirmed' where id = 2001 and status = 'pending'
returning id, customer_id, status;
update bookings set status = 'confirmed' where id = 2001 and status = 'pending'; -- не существует записи с такими 2 условиями

-- 7
select * from customers;
update customers set display_name = 'Erin Parker' where id = 2001
returning id, email, display_name;

-- 8
-- delete from customers where id = 2001;
-- Существует ссылка в таблице bookings на запись => частично удалить не получится. Ограничение: наличие внешнего ключа
-- Таблица: bookings
-- Защита нужна для поддержания НФ между таблицами. Чтобы не было неприятностей и непредсказуемого поведения

-- 9
select * from booking_items;
delete from booking_items where booking_id = 2001
returning booking_id, event_id, ticket_count; 

-- 10
select * from bookings;
delete from bookings where id = 2001 
returning id, customer_id, status;

-- 11
select * from customers;
delete from customers where id = 2001
returning id, email, display_name;

-- 12
delete from events where id in (2001, 2002)
returning id, title;

-- 13
select * from customers where id = 2001;
select * from events where id = 2001;
select * from bookings where id = 2001;
select * from booking_items where booking_id = 2001;

-- Контрольные вопросы:
/*
Чем отличается пропущенный при INSERT столбец от явно переданного NULL?
Если у пропущенного столбца имеется значение по умолчанию, то в полученной через INSERT строке будет значение этого значения по умолчанию - не обязательно NULL

Что произойдёт, если выполнить UPDATE без WHERE?
Обновятся все существующие строки таблицы.

Что произойдёт, если выполнить DELETE без WHERE?
Произойдет удаление всех строк в таблице. Таблица станет пустой.

Чем RETURNING отличается от отдельного последующего SELECT?
Returning - возвращает результат изменения запроса. Зачем-то... То есть вместо подсчета кол-ва строк, мы можем получить результаты выборки, которые меняются.
Следующий select уже может не показать тех изменений, которые проводились в рамках операции DML.

Почему покупателя нельзя было удалить раньше его бронирования?
Потому что у покупателя имеется связь с бронированием по ключу в таблице бронирований. 
В принципе - можно было - через: ON DELETE CASCADE, например.

Почему позиции бронирования пришлось удалить раньше самого бронирования?
booking_items - имеет связь с bookings (через booking_id).
Идем от общего к частному (от таблицы с внешним ключом к таблице, на которую ранее существовала связь).

Что возвращает UPDATE ... RETURNING, если условию WHERE не соответствует ни одна строка?
Пустой результат, вероятно.
*/