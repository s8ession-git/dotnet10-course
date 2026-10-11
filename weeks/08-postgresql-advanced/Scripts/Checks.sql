-- 1. Добавдение данных из исходного примера в новую структуру

-- select * from departments;
begin;
	insert into departments(id, name) values (10, 'SQL'), (20, 'C#');
	update departments set name = 'Разработка' where id = 10;
	update departments set name = 'Аналитика' where id = 20;
commit;
-- rollback;


-- select * from employees;
begin;
	insert into employees(name, department_id) values ('Анна', 10), ('Борис', 10), ('Вера', 20);
commit;


-- select * from courses;
begin;
	insert into courses(id, title, price) values (101, 'SQL', 6000), (102, 'C#', 8000);
commit;

-- select * from course_records;
begin;
	insert into course_records(course_id, employee_id, agreed_price, score)
	values (101, 1, 5000, 85), (102, 1, 7000, 90), (101, 2, 5500, 78), (102, 3, 6500, null);
commit;

-- 2. Добавить одного сотрудника без курсов и один курс без участников
begin;
	insert into employees(name, department_id) values ('Григорий', 20);
	insert into courses(id, title, price) values (103, 'Python', 3000);
commit;

-- 3. Составить отчёт

select e.id employee_id, e.name employee_name, d.name department_name, c.title course_title, cr.agreed_price, c.price current_price, cr.score 
from employees e 
join departments d on (e.department_id = d.id)
left join course_records cr on (e.id = cr.employee_id)
left join courses c on (cr.course_id = c.id)
order by e.id, c.id;

-- 4. Транзакция на изменение цены + rollback
begin;
update courses set price = 9000 where id = 101;

-- Повторить отчет (для проверки)
select e.id employee_id, e.name employee_name, d.name department_name, c.title course_title, cr.agreed_price, c.price current_price, cr.score 
from employees e 
join departments d on (e.department_id = d.id)
left join course_records cr on (e.id = cr.employee_id)
left join courses c on (cr.course_id = c.id)
order by e.id, c.id;

rollback;
