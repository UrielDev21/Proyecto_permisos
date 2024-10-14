-- Tabla de usuarios --
create table usuarios
(
    user varchar(100) PRIMARY KEY,
    pass varchar(255),
    nombre VARCHAR(255), 
    apellidop varchar(255),
    apellidom varchar(255), 
    fecha_nacimiento DATE,
    rfc VARCHAR(13)
);

describe usuarios; 
-- Tabla permisos -- 
create table permisos
(
    id_permiso int PRIMARY key auto_increment, 
    fk_username varchar(100),
    nombre_formulario varchar(255),
    Lectura BIT,
    Escritura BIT,
    Actualizacion BIT,
    Eliminacion BIT,
    foreign key (fk_username) references usuarios (user)
); 

describe permisos;

-- Tabla de refacciones --
create table refacciones
(
    codigo_barras VARCHAR(25) PRIMARY key, 
    nombre varchar(255), 
    descripcion varchar(255),
    marca varchar(255)
);

-- Tabla taller --
CREATE table taller
(
    Codigo_herramienta varchar(25) PRIMARY KEY,
    nombre varchar(255), 
    medida VARCHAR(100),
    marca varchar(255),
    descripcion varchar(255)
);

-- Procedimiento almacenado para insertar usuarios --
-- Otras modificaciones --
drop procedure if exists p_insertar_usuarios; 
CREATE procedure p_insertar_usuarios
(
    in _nombre varchar(255),
    in _apellidop varchar(255),
    in _apellidom varchar(255), 
    in _fecha_nacimiento DATE,
    in _rfc VARCHAR(13),
    in _user varchar(100), 
    in _pass varchar(255)
)
begin  

    insert into usuarios (nombre, apellidop, apellidom, fecha_nacimiento, rfc, user, pass) values 
    (_nombre, _apellidop, _apellidom, _fecha_nacimiento, _rfc, _user, _pass);
end;

-- Prueba de funcionamiento --
call p_insertar_usuarios('Juan', 'Perez', 'Prado', '2000-01-01', 'DAOYUIR121341', 'Juan11', sha1('1234')); 
select * from usuarios; 

SHOW PROCEDURE STATUS WHERE Name = 'p_insertar_usuarios';


-- Procedimiento almacenado para modificar usuarios --
drop procedure if exists p_modificar_usuarios; 
CREATE procedure p_modificar_usuarios
(
    in _nombre varchar(255),
    in _apellidop varchar(255),
    in _apellidom varchar(255), 
    in _fecha_nacimiento DATE,
    in _rfc VARCHAR(13),
    in _user varchar(100), 
    in _pass varchar(255)
)
begin 
    update usuarios set nombre = _nombre, apellidop = _apellidop, apellidom = _apellidom, fecha_nacimiento = _fecha_nacimiento, rfc = _rfc, user = _user, pass = _pass
    where user = _user; 
end;  

-- Prueba de funcionamiento --
call p_modificar_usuarios('Juan', 'Perez', 'Hernandez', '2000-01-01', 'DAOYUIR121341', 'Juan11', sha1('1234')); 

select * from usuarios; 

-- Procedimiento almacenado para eliminar usuarios --
DROP procedure if exists p_eliminar_usuarios; 
CREATE procedure p_eliminar_usuarios
(
    in _user varchar(100)

)
begin
    delete from usuarios
    where user = _user; 
end; 

-- Prueba de funcionamiento --
call p_eliminar_usuarios('Juan11'); 

-- Procedimiento almacenado para insertar permisos --
drop PROCEDURE if exists p_insertar_permiso;
create Procedure p_insertar_permiso
(
    in _fk_username varchar(100),
    in _nombre_formulario varchar(255),
    in _Lectura bit,
    in _Escritura bit,
    in _Actualizacion bit, 
    in _Eliminacion bit
)
BEGIN   
    insert into permisos (fk_username, nombre_formulario, Lectura, Escritura, Actualizacion, Eliminacion) values
    (_fk_username, _nombre_formulario, _Lectura, _Escritura, _Actualizacion, _Eliminacion); 
end; 

-- Prueba de funcionamiento --

call p_insertar_permiso('juan11', 'FrmAgregarRefacciones', 1, 1, 0, 0);

selecT * from permisos; 

-- Procedimiento almacenado para borrar permisos --
drop procedure if EXISTS P_eliminar_permisos; 
CREATE Procedure p_eliminar_permisos
(
    in _fk_username varchar(100)
)
BEGIN
    delete from permisos
    where fk_username = _fk_username;
end;

call p_eliminar_permisos('juan11');


-- Procedimiento almacenado para insertar en la tabla de refacciones --
create procedure p_insertar_refacciones
(
    in _codigo_barras varchar(25),
    in _nombre varchar(255), 
    in _descripcion varchar(255), 
    in _marca varchar(255)
)
begin
    insert into refacciones (codigo_barras, nombre, descripcion, marca) values 
    (_codigo_barras, _nombre, _descripcion, _marca); 
end; 

-- Procedimiento almacenado para modificar en la tabla de refacciones -- 
CREATE procedure p_modificar_refacciones
(
    in _codigo_barras varchar(25),
    in _nombre varchar(255), 
    in _descripcion varchar(255), 
    in _marca varchar(255)
)
begin
    update refacciones set nombre = _nombre, descripcion = _descripcion, marca = _marca
    where codigo_barras = _codigo_barras; 
end; 

-- Procedimiento almacenado para eliminar refacciones en la tabla --
drop procedure if exists p_eliminar_refacciones; 
create procedure p_eliminar_refacciones
(
    in _codigo_barras varchar(25)
)
begin 
    delete from refacciones 
    where codigo_barras = _codigo_barras; 
end; 


-- Procedimiento almacenado para ingresar herramientas en la tabla --
drop procedure if exists p_insertar_herramienta; 
CREATE procedure p_insertar_herramienta
(
    in _Codigo_herramienta varchar(25), 
    in _nombre varchar(255), 
    in _medida varchar(10), 
    in _marca varchar(255), 
    in _descripcion  varchar(255)
)
begin 
    insert into taller (Codigo_herramienta, nombre, medida, marca, descripcion) values 
    (_Codigo_herramienta, _nombre, _medida, _marca, _descripcion);
end; 

-- Procedimiento almacenado para modificar herramientas en la tabla --
drop procedure if exists p_modificar_herramienta;
CREATE procedure p_modificar_herramienta
(
    in _Codigo_herramienta varchar(25), 
    in _nombre varchar(255), 
    in _medida varchar(10), 
    in _marca varchar(255), 
    in _descripcion  varchar(255)
)
begin 
    update taller set nombre = _nombre, medida = _medida, marca = _marca, descripcion = _descripcion
    where Codigo_herramienta = _Codigo_herramienta;
end;  
-- Procedimiento almacenado para eliminar herramientas de la tabla --
CREATE procedure p_eliminar_herramienta
(
    in _Codigo_herramienta varchar(25)
)
begin 
    delete  from taller
    where Codigo_herramienta = _Codigo_herramienta;
end;

-- Vista para usuarios y permisos --
create VIEW v_vista_general AS
select u.nombre, u.apellidop, u.apellidom, u.fecha_nacimiento, u.rfc, u.user, u.pass, p.nombre_formulario, p.Lectura, p.Escritura, p.Actualizacion, p.Eliminacion from usuarios u
join permisos p on u.user = p.fk_username;
select * from v_vista_general;

-- Vista para refacciones --
create View v_vista_refacciones AS
select * from  refacciones;

select * from v_vista_refacciones where nombre like '%Empaque%';

-- Vista para taller -- 
create VIEW v_vista_taller AS
select * from taller;

select * from v_vista_taller where nombre like '%Llave%';

-- Pruebas de funcionamiento de los procedimientos almacenados --

------------------------------------------------------------------------------------------------
call p_insertar_refacciones('6546846854657', 'Empaque de culata', 'Empaque de culata para camioneta FORD', 'Ford');
call p_modificar_refacciones('6546846854657', 'Empaque de culata', 'Empaque de culata para camioneta TOYOTA', 'TOYOTA');
CALL p_eliminar_refacciones('6546846854657');
SELECT * FROM refacciones;
-------------------------------------------------------------------------------------------------
call p_insertar_herramienta('SSAHDFLJ1', 'Llave', '1/2', 'CRAFTMAN', 'Llave de buena calidad');
call p_modificar_herramienta('SSAHDFLJ1', 'Perica', '2 pulgadas', 'TRUPER', 'Perica marca truper');

call p_eliminar_herramienta('SSAHDFLJ1');
select * from taller;

-- Procedimiento almacenado para validar --

DROP PROCEDURE IF EXISTS p_validar; 
CREATE PROCEDURE p_validar
(
    IN _user VARCHAR(255),
    IN _pass VARCHAR(255)
)
BEGIN 
    DECLARE x INT;

    SELECT COUNT(*) INTO x 
    FROM usuarios 
    WHERE user = _user AND pass = _pass;

    IF x > 0 THEN
        SELECT 'Correcto' AS rs,
            p.fk_username AS Username,
            p.nombre_formulario,
            p.Lectura,
            p.Escritura,
            p.Actualizacion,
            p.Eliminacion
        FROM 
            permisos p
        WHERE 
            p.fk_username = _user;
    ELSE
        SELECT 'Error' AS rs;
    END IF;
END;

-- Prueba de funcionamiento --
call p_validar('Juan11', sha1('1234')); 

SELECT * from usuarios; 

describe permisos; 