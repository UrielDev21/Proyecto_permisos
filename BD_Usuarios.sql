-- Tabla de usuarios --
create table usuarios
(
    id_usuario int PRIMARY key auto_increment, 
    nombre VARCHAR(255), 
    apellidop varchar(255),
    apellidom varchar(255), 
    fecha_nacimiento DATE,
    rfc VARCHAR(13),
        user varchar(100), 
    pass varchar(255)
);
describe usuarios; 
-- Tabla permisos -- 
create table permisos
(
    id_permiso int PRIMARY key auto_increment, 
    fk_id_usuario int, 
    tipo_permiso VARCHAR(200),
    permiso_formulario varchar(200),
    foreign key (fk_id_usuario) references usuarios (id_usuario)
); 
DESCRIBE permisos;

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
    in _pass varchar(255),
    in _tipo_permiso varchar(200),
    in _permiso_formulario varchar(200)
)
begin 

    declare nuevo_id_usuario int; 

    insert into usuarios (nombre, apellidop, apellidom, fecha_nacimiento, rfc, user, pass) values 
    (_nombre, _apellidop, _apellidom, _fecha_nacimiento, _rfc, _user, _pass);

    set nuevo_id_usuario = last_insert_id(); 

    insert into permisos(fk_id_usuario, tipo_permiso, permiso_formulario) values 
    (nuevo_id_usuario, _tipo_permiso, _permiso_formulario); 
end;
-- Procedimiento almacenado para modificar usuarios --
drop procedure if exists p_modificar_usuarios; 
CREATE procedure p_modificar_usuarios
(
    in _id_usuario int,
    in _nombre varchar(255),
    in _apellidop varchar(255),
    in _apellidom varchar(255), 
    in _fecha_nacimiento DATE,
    in _rfc VARCHAR(13),
    in _user varchar(100), 
    in _pass varchar(255),
    in _tipo_permiso varchar(200),
    in _permiso_formulario varchar(200)
)
begin 
    update usuarios set nombre = _nombre, apellidop = _apellidop, apellidom = _apellidom, fecha_nacimiento = _fecha_nacimiento, rfc = _rfc, user = _user, pass = _pass
    where id_usuario = _id_usuario;

    update permisos set tipo_permiso = _tipo_permiso, permiso_formulario = _permiso_formulario
    where fk_id_usuario = _id_usuario; 
end;  


call p_modificar_usuarios(1, 'Carlos', 'Perez', 'Gomez', '1999-01-01', 'CDLFODOOS1123', 'carlos99', sha1('12345'), 'Lectura', 'Ver_refacciones'); 

select * from usuarios; 
select * from permisos; 

-- Procedimiento almacenado para eliminar usuarios --
DROP procedure if exists p_eliminar_usuarios; 
CREATE procedure p_eliminar_usuarios
(
    in _id_usuario int

)
begin
    delete from permisos
    where fk_id_usuario = _id_usuario;

    delete from usuarios
    where id_usuario = _id_usuario; 
end; 

call p_eliminar_usuarios(1); 

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

-- Vista para usuarios --
drop VIEW if exists v_vista_usuarios; 
create VIEW v_vista_usuarios AS
select u.nombre, u.apellidop, u.apellidom, u.fecha_nacimiento, u.rfc, u.user, u.pass, p.tipo_permiso, p.permiso_formulario from usuarios u
join permisos p on u.id_usuario = p.fk_id_usuario;
select * from v_vista_usuarios WHERE nombre like '%Jose%';

-- Vista para refacciones --
create View v_vista_refacciones AS
select * from  refacciones;

select * from v_vista_refacciones where nombre like '%Empaque%';

-- Vista para taller -- 
create VIEW v_vista_taller AS
select * from taller;

select * from v_vista_taller where nombre like '%Llave%';

-- Pruebas de funcionamiento de los procedimientos almacenados --

call p_insertar_usuarios('Juan', 'Perez', 'Gomez', '1990-01-01', 'SJD1231ASVLDF', 'juan90', sha1('1234'), 'Lectura,Escritura', 'Ver_refacciones,Agregar_refacciones');
call p_modificar_usuarios(8, 'Carlos', 'Perez', 'Gomez', '1999-01-01', 'CDLFODOOS1123', 'carlos99', sha1('12345'), 'Lectura', 'Ver_refacciones'); 
call p_modificar_usuarios(4, 'Carlos', 'Perez', 'Gomez', '1999-01-01', 'CDLFODOOS1123', 'carlos99', sha1('12345'), 'Lectura', 'Ver_refacciones'); 


call p_eliminar_usuarios_permisos(3);
call p_eliminar_usuarios_permisos(4);
call p_eliminar_usuarios_permisos(5);

select * from permisos;
select * from usuarios;

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
