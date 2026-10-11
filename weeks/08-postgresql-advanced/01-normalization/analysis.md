№1. Первая нормальная форма

1. Объясни, что неудобно и опасно в четырёх согласованных списках.
Неудобно: 
а. тип хранения - строка?!
б. уникальность записей (атомарность значений) - каким образом будет проверяться?
в. Работа с группой значений - должна отдать приоритет какому правилу (свойству) при изменении (dml)?
г. Дальнейшая обработка (возможная) данных - затруднительна. Пример: аггрегация по сумме в рамках курса (манипуляция с данными) - жесткая фиксация.

Опасно:
а. В принципе - работать с такой структурой (если для нее не заданы жесткие правила).
б. Менять что-либо местами
в. Манипулировать изменениями в рамках БД

Точная формулировка:
Списки могут иметь разную длину или разный порядок элементов. Тогда цена и оценка будут приписаны другому курсу, а обычные ограничения таблицы могут этого не обнаружить.

2. Покажи эти данные в виде таблицы, где одна строка представляет запись сотрудника на один курс.
| employee_id | course_id | agreed_price | score |
| ----------- | --------- | ------------ | ----- |
| 1           | 101       | 5000         | 85    |
| 1           | 102       | 7000         | 90    |
| 2           | 101       | 5500         | null  |

3. Укажи потенциальный ключ получившейся таблицы.
potential key: (employee_id, course_id)

№2. Зависимости и вторая нормальная форма

employee_id → employee_name, department_id
course_id → course_title, course_price
(employee_id, course_id) → agreed_price, score
department_id → department_name

№3. Состав таблиц
Employees
| employee_id (bigint, primary key) | employee_name(text) | department_id(bigint, foreign key) |

Departments
| department_id (bigint, primary key) | department_name(text) |

Courses
| course_id(bigint, primary_key) | course_title(text) | course_price(numeric(12,2)) |

CourseRecords
| course_id(bigint, primary_key, foreign key) | employee_id(bigint,primary key, foreign key) | agreed_price(numeric(12,2)) | score(int) |
