-- ============================================================================
-- СКРИПТ ИНИЦИАЛИЗАЦИИ БАЗЫ ДАННЫХ holding_db
-- Вариант 19: Анализ динамики показателей финансовой отчетности
-- ============================================================================

-- 1. Создание базы данных
DROP DATABASE IF EXISTS holding_db;
CREATE DATABASE holding_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE holding_db;

-- ============================================================================
-- 2. Создание таблиц (пункты 4-6 задания)
-- ============================================================================

-- Таблица Показатели
CREATE TABLE indicators (
    indicator_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    importance DECIMAL(5, 2) NOT NULL DEFAULT 0.00,
    unit VARCHAR(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица Предприятия
CREATE TABLE enterprises (
    enterprise_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(150) NOT NULL,
    bank_details TEXT,
    phone VARCHAR(20),
    contact_person VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица Динамика показателей
CREATE TABLE dynamics (
    record_id INT AUTO_INCREMENT PRIMARY KEY,
    enterprise_id INT NOT NULL,
    indicator_id INT NOT NULL,
    report_date DATE NOT NULL,
    value DECIMAL(15, 2) NOT NULL,
    
    CONSTRAINT fk_dyn_enterprise FOREIGN KEY (enterprise_id) 
        REFERENCES enterprises(enterprise_id) ON DELETE CASCADE,
    CONSTRAINT fk_dyn_indicator FOREIGN KEY (indicator_id) 
        REFERENCES indicators(indicator_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Таблица Пользователи (для приложения)
CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    role ENUM('Администратор', 'Пользователь') NOT NULL DEFAULT 'Пользователь',
    is_blocked BOOLEAN NOT NULL DEFAULT FALSE,
    login_attempts INT NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================================
-- 3. Заполнение данными (пункт 9)
-- ============================================================================

INSERT INTO indicators (name, importance, unit) VALUES
('Выручка', 10.00, 'руб.'),
('Чистая прибыль', 15.50, 'руб.'),
('Рентабельность', 8.20, '%'),
('Объем производства', 5.00, 'шт.'),
('Количество сотрудников', 3.00, 'чел.');

INSERT INTO enterprises (name, bank_details, phone, contact_person) VALUES
('ООО "Заря"', 'БИК 044525225, р/с 40702810400000001111', '+7 (495) 111-22-33', 'Иванов И.И.'),
('АО "Вектор"', 'БИК 044525555, р/с 40702810400000002222', '+7 (495) 333-44-55', 'Петров П.П.'),
('ЗАО "ТехноСтрой"', 'БИК 044525999, р/с 40702810400000003333', '+7 (495) 777-88-99', 'Сидоров С.С.');

INSERT INTO dynamics (enterprise_id, indicator_id, report_date, value) VALUES
(1, 1, '2023-01-01', 1000000.00),
(1, 1, '2023-04-01', 1200000.00),
(1, 2, '2023-01-01', 150000.00),
(1, 2, '2023-04-01', 180000.00),
(2, 1, '2023-01-01', 5000000.00),
(2, 1, '2023-04-01', 4800000.00),
(2, 3, '2023-01-01', 12.50),
(2, 3, '2023-04-01', 11.00),
(3, 4, '2023-01-01', 5000.00),
(3, 4, '2023-04-01', 6200.00),
(3, 5, '2023-01-01', 120.00),
(3, 5, '2023-04-01', 135.00);

-- Пользователи для приложения
INSERT INTO users (username, password, role) VALUES
('admin', 'admin123', 'Администратор'),
('user', 'user123', 'Пользователь');

-- ============================================================================
-- 4. Создание пользовательской функции (пункт 14)
-- Функция "Вычисление значения Динамики показателя"
-- ============================================================================

DELIMITER $$
CREATE FUNCTION CalculateDynamicsValue(
    p_enterprise_id INT,
    p_indicator_id INT,
    p_date_start DATE,
    p_date_end DATE
) 
RETURNS DECIMAL(10, 2)
DETERMINISTIC
READS SQL DATA
BEGIN
    DECLARE v_val_start DECIMAL(15, 2);
    DECLARE v_val_end DECIMAL(15, 2);
    DECLARE v_result DECIMAL(10, 2);

    SELECT value INTO v_val_start
    FROM dynamics
    WHERE enterprise_id = p_enterprise_id 
      AND indicator_id = p_indicator_id 
      AND report_date = p_date_start
    LIMIT 1;
      
    SELECT value INTO v_val_end
    FROM dynamics
    WHERE enterprise_id = p_enterprise_id 
      AND indicator_id = p_indicator_id 
      AND report_date = p_date_end
    LIMIT 1;

    IF v_val_start IS NULL OR v_val_end IS NULL OR v_val_start = 0 THEN
        SET v_result = 0.00;
    ELSE
        SET v_result = ((v_val_end - v_val_start) / v_val_start) * 100;
    END IF;
    
    RETURN v_result;
END$$
DELIMITER ;

-- Проверка функции
SELECT CalculateDynamicsValue(1, 1, '2023-01-01', '2023-04-01') AS DynamicsPercent;

-- ============================================================================
-- 5. Создание представлений (пункт 13 - два представления)
-- ============================================================================

-- Представление 1: Исходные данные (запрос a)
CREATE OR REPLACE VIEW vw_InitialData AS
SELECT 
    e.enterprise_id,
    e.name AS enterprise_name,
    e.bank_details,
    e.phone,
    e.contact_person,
    i.indicator_id,
    i.name AS indicator_name,
    i.importance,
    i.unit,
    d.report_date,
    d.value
FROM enterprises e
JOIN dynamics d ON e.enterprise_id = d.enterprise_id
JOIN indicators i ON d.indicator_id = i.indicator_id
ORDER BY e.name, i.name, d.report_date;

-- Представление 2: Динамика показателей по предприятиям (запрос f)
CREATE OR REPLACE VIEW vw_EnterpriseDynamics AS
SELECT 
    e.name AS enterprise_name,
    i.name AS indicator_name,
    d.report_date,
    d.value,
    i.importance,
    i.unit
FROM dynamics d
JOIN enterprises e ON d.enterprise_id = e.enterprise_id
JOIN indicators i ON d.indicator_id = i.indicator_id
ORDER BY e.name, d.report_date;

-- ============================================================================
-- 6. SQL запросы по варианту (пункт 12)
-- ============================================================================

-- Запрос a: Исходные данные
SELECT 
    e.name AS Предприятие,
    e.contact_person AS Контактное_лицо,
    e.phone AS Телефон,
    i.name AS Показатель,
    i.importance AS Важность,
    i.unit AS Единица_измерения,
    d.report_date AS Дата_отчета,
    d.value AS Значение
FROM enterprises e
JOIN dynamics d ON e.enterprise_id = d.enterprise_id
JOIN indicators i ON d.indicator_id = i.indicator_id
ORDER BY e.name, i.name, d.report_date;

-- Запрос b: Предприятия и показатели
SELECT 
    e.name AS Предприятие,
    i.name AS Показатель
FROM enterprises e
CROSS JOIN indicators i
ORDER BY e.name, i.name;

-- Запрос c: Алфавитный список (диапазон А-Т)
SELECT name AS Название
FROM enterprises
WHERE LEFT(name, 1) BETWEEN 'А' AND 'Т'
UNION ALL
SELECT name AS Название
FROM indicators
WHERE LEFT(name, 1) BETWEEN 'А' AND 'Т'
ORDER BY Название;

-- Запрос d: Список с условием (показатели с важностью > 8)
SELECT * FROM indicators
WHERE importance > 8.00
ORDER BY importance DESC;

-- Запрос e: Названия в верхнем регистре
SELECT UPPER(name) AS Название_Верхний_Регистр
FROM enterprises
UNION ALL
SELECT UPPER(name) AS Название_Верхний_Регистр
FROM indicators;

-- Запрос f: Динамика показателей для предприятия к дате
SELECT 
    e.name AS Предприятие,
    i.name AS Показатель,
    d.report_date AS Дата,
    d.value AS Значение
FROM dynamics d
JOIN enterprises e ON d.enterprise_id = e.enterprise_id
JOIN indicators i ON d.indicator_id = i.indicator_id
WHERE e.enterprise_id = 1 AND d.report_date = '2023-04-01';

-- Запрос g: Самый важный показатель
SELECT * FROM indicators
WHERE importance = (SELECT MAX(importance) FROM indicators);

-- Запрос h: Среднее значение динамики показателей
SELECT 
    i.name AS Показатель,
    AVG(d.value) AS Среднее_значение
FROM dynamics d
JOIN indicators i ON d.indicator_id = i.indicator_id
GROUP BY i.name;

-- ============================================================================
-- 7. Проверочные запросы (пункт 11)
-- ============================================================================

-- Вывод содержимого таблиц (пункт 10)
SELECT '=== ТАБЛИЦА: indicators ===' AS '';
SELECT * FROM indicators;

SELECT '=== ТАБЛИЦА: enterprises ===' AS '';
SELECT * FROM enterprises;

SELECT '=== ТАБЛИЦА: dynamics ===' AS '';
SELECT * FROM dynamics;

-- Удаление одной записи с условием
DELETE FROM dynamics WHERE record_id = 1;

-- Изменение одного поля в двух записях из разных таблиц
UPDATE enterprises SET phone = '+7 (495) 000-00-00' WHERE enterprise_id = 1;
UPDATE indicators SET importance = 12.00 WHERE indicator_id = 1;

-- Вывод нового содержимого
SELECT '=== НОВОЕ СОДЕРЖИМОЕ ПОСЛЕ ИЗМЕНЕНИЙ ===' AS '';
SELECT * FROM enterprises WHERE enterprise_id = 1;
SELECT * FROM indicators WHERE indicator_id = 1;
SELECT * FROM dynamics;

-- ============================================================================
-- Конец скрипта
-- ============================================================================
