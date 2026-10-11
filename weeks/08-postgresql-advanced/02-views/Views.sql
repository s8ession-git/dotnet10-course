-- #1. Создать представление отчёта
create or replace view training.employee_course_report as
select 
	e.id as employee_id, 
	e.name as employee_name, 
	d.name as department_name,
	c.id as course_id,
	c.title as course_title, 
	cr.agreed_price, 
	c.price as current_price, 
	cr.score 
from training.employees e 
join training.departments d on (e.department_id = d.id)
left join training.course_records cr on (e.id = cr.employee_id)
left join training.courses c on (cr.course_id = c.id);

-- Проверка.
select *
from training.employee_course_report
order by employee_id, course_id;


-- #2. Проверить связь с исходными данными
begin;
	update training.courses
	set price = 9000
	where id = 101;
	
	select employee_name, agreed_price, current_price
	from training.employee_course_report
	where course_id = 101
	order by employee_id;
rollback;

-- #3. Изменить определение представления
create or replace view training.employee_course_report as
select 
	e.id as employee_id, 
	e.name as employee_name, 
	d.name as department_name,
	c.id as course_id,
	c.title as course_title, 
	cr.agreed_price, 
	c.price as current_price, 
	cr.score,
	c.price - cr.agreed_price as price_difference
from training.employees e 
join training.departments d on (e.department_id = d.id)
left join training.course_records cr on (e.id = cr.employee_id)
left join training.courses c on (cr.course_id = c.id);

-- #4. Обновляемое представление и CHECK OPTION
create view training.affordable_courses as 
select id, title, price from courses 
where price <= 6000;

begin;
	update training.affordable_courses
	set price = 6500
	where id = 101;
	
	select *
	from training.courses
	where id = 101;
rollback;

create or replace view training.affordable_courses as 
select id, title, price from courses 
where price <= 6000
with check option;

-- Теперь: есть проверка - выйдет в ошибку - но только потому, что исходное условие проверки нарушено (будь там price = 5500 - ошибки бы не возникло).
begin;
	update training.affordable_courses
	set price = 6500
	where id = 101;
	
	select *
	from training.courses
	where id = 101;
rollback;

drop view training.affordable_courses;