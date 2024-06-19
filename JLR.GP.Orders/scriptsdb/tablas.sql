
use master;

DROP DATABASE DC_VN;
create   database DC_VN;
use DC_VN;

CREATE      TABLE Tbl_SupervisorAprobacion (
Id INT PRIMARY KEY IDENTITY,
    NumeroCotizacion  VARCHAR(20),   -- Suponiendo que es una clave primaria única
    CotizacionGrupo VARCHAR(20),        -- Ajusta el tamaño del VARCHAR según sea necesario
    TurnoInicial varchar(30),                   -- Suponiendo que es un número entero
    FechProdInicial  datetime,               -- Fecha de producción inicial
    TurnoCambio varchar(30), ,                    -- Suponiendo que es un número entero
    FechaProdCambio datetime,               -- Fecha de cambio de producción
    IdUsuario INT,                      -- SUPERVISOR
    IdUsuarioSolicita INT,              -- EK QUE CREA
    FechaCreacion DATETIME,              -- Fecha de aprobación
    Estado VARCHAR(20)
);

Create Table Tbl_TipoCliente(
Id int primary key identity,
Nombre varchar(20)); 
INSERT INTO Tbl_TipoCliente (Nombre) VALUES
('Finales'),
('Proyecto'),
('Fabricante'),
('Provincia'),
('Distribucion'),
('lutron');
Create Table Tbl_Destino(
Id int primary key identity,
Nombre varchar(20)); 
INSERT INTO Tbl_Destino (Nombre) VALUES
('Entrega planta'),
('Instalación'),
('Provincia'),
('Despacho'),
('Entrega en tienda');

 Create Table Tbl_TipoOperacion(
Id int primary key identity,
Nombre varchar(20)); 
INSERT INTO Tbl_TipoOperacion (Nombre) VALUES
('Venta'),
('Garantia'),
('Consumo Interno'),
('Transf. Gratuita'),
('Reproceso'),
('Redes');

CREATE TABLE Tbl_Proyecto(
Id int primary key identity,
Nombre varchar(50));

Create table Tbl_TipoUsuario(
Id int primary key identity,
Nombre varchar(50));
INSERT INTO Tbl_TipoUsuario (Nombre) VALUES
('Analista'),
('Supervisor'), 
('Jefe');

Create   Table Tbl_Rol(
Id int primary key identity,
Nombre varchar(100),
Descripcion VARCHAR(100),
FechaCreacion datetime default getdate(),
FechaModificacion datetime,
IdUsuarioCreacion int,
IdUsuarioModifica int,
Estado int default 1   
);
INSERT INTO Tbl_Rol (Nombre) VALUES
('Operacion'),
('Ventas'),
('Producción'),
('Almacén'),
('Operario Distribución'),
('Operario Producción'),
('Administrador'),
('Supervisor'),
('Usuario'),
('Chofer'),
('Cargador'),
('Instalador');

CREATE TABLE Tbl_CargaProduccion(
Id INT IDENTITY PRIMARY KEY,
CodigoProducto VARCHAR(20),
Producto VARCHAR(150),
Equivalencia INT,
FechaCreacion datetime default getdate(),
FechaModificacion datetime,
IdUsuarioCreacion int,
IdUsuarioModifica int,
Estado int default 1   
)
SELECT * FROM Tbl_CargaProduccion


CREATE TABLE	 Tbl_ConfigCargaProduccion(
Id INT IDENTITY PRIMARY KEY,


INSERT INTO dbo.Tbl_CargaProduccion (CodigoProducto, Producto, Equivalencia, IdUsuarioCreacion, Estado)
VALUES ('', 'Roller Shade', 1, 1, 1)
GO

INSERT INTO dbo.Tbl_CargaProduccion (CodigoProducto, Producto, Equivalencia, IdUsuarioCreacion, Estado)
VALUES ('', 'Roller Motorizado', 2, 1, 1)
GO

INSERT INTO dbo.Tbl_CargaProduccion (CodigoProducto, Producto, Equivalencia, IdUsuarioCreacion, Estado)
VALUES ('', 'Roller Con Cassete', 2, 1, 1)
GO

INSERT INTO dbo.Tbl_CargaProduccion (CodigoProducto, Producto, Equivalencia, IdUsuarioCreacion, Estado)
VALUES ('', 'Zebra', 3, 1, 1)
GO

INSERT INTO dbo.Tbl_CargaProduccion (CodigoProducto, Producto, Equivalencia, IdUsuarioCreacion, Estado)
VALUES ('', 'Zebra Motorizada', 4, 1, 1)
GO

INSERT INTO dbo.Tbl_CargaProduccion (CodigoProducto, Producto, Equivalencia, IdUsuarioCreacion, Estado)
VALUES ('', 'Riel Motorizado', 2, 1, 1)
GO

INSERT INTO dbo.Tbl_CargaProduccion (CodigoProducto, Producto, Equivalencia, IdUsuarioCreacion, Estado)
VALUES ('', 'Shanghila', 8, 1, 1)
GO

INSERT INTO dbo.Tbl_CargaProduccion (CodigoProducto, Producto, Equivalencia, IdUsuarioCreacion, Estado)
VALUES ('', '3 Rieles Hoteleros', 1, 1, 1)
GO

CREATE TABLE Tbl_Usuario(
Id int identity primary key,
IdTipoUsuario int,
IdRol INT,
Nombre varchar(100),
Apellido varchar(100),
Dni varchar(20),
Correo varchar(50),
Usuario varchar(100),
Clave varchar(100),
CodigoUsuario varchar(100),

FechaCreacion datetime default getdate(),
FechaModificacion datetime,
IdUsuarioCreacion int,
IdUsuarioModifica int,
Estado int default 1    );

Create    table Tbl_Modulos(
Id int primary key identity,
Nombre varchar(50),
Descripcion varchar(50),
Ruta varchar(50),
Icono varchar(50),
Orden INT,
Estado int default 1 
);

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Ventas', 'Ventas', 'SolicitudPendiente', 'markunread_mailbox', 1, 1)
GO

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Linea Prod', 'Linea Producción', 'linea-Prod', 'poll', 1, 2)
GO

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Operacion Construccion', 'Operaciones Construcción', 'Op-Construccion', 'apps', 1, 3)
GO

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Monitoreo Produccion', 'Monitoreo Producción', 'Monitoreo-Produccion', 'donut_small', 1, 4)
GO

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Componentes', 'Mantenimeinto Componentes', 'Mantenimiento-componente', 'confirmation_number', 1, 6)
GO

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Mantenimiento OP', 'Mantenimiento de OP', 'Mantenimiento-OP', 'build', 1, 5)
GO

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Usuarios', 'Mantenimiento Usuarios', 'Usuarios', 'supervised_user_circle', 1, 7)
GO

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Perfiles', 'Mantenimiento Perfiles', 'Mantenimiento-Perfil', 'drag_indicator', 1, 8)
GO

INSERT INTO dbo.Tbl_Modulos (Nombre, Descripcion, Ruta, Icono, Estado, Orden)
VALUES ('Supervision', 'Supervision', 'Supervision', 'library_books', 1, 9)
GO

Create   Table Tbl_ModuloRol(
Id int primary key identity,
IdModulo int,
IdRol int,
FechaCreacion datetime default getdate(),
FechaModificacion datetime,
IdUsuarioCreacion int,
IdUsuarioModifica int,
Estado int default 1    
);

Create table Tbl_UsuarioRol(
Id int primary key identity,
IdUsuario int,
IdRol int,
Estado int default 1 );
 
Create  Table Tbl_Estado(
Id int primary key identity,
CodigoEstado varchar(20),
Nombre varchar(100),
Descripcion VARCHAR(500)
);
insert into Tbl_Estado values('PS', 'Pendiente Supervisor', 'Grupos de ootizacion perdientes por aprobacion del supervisor');
insert into Tbl_Estado values('PV', 'Pendiente Venta', 'Cotización registrada en el sistema');

insert into Tbl_Estado values('PO', 'Pendiente Operaciones', 'Cotización aprobada por ventas y enviada a Operaciones para la planificación y ejecución de la construcción.');

insert into Tbl_Estado values('PC', 'En Construcción', 'El proceso de construcción en las estaciones ha comenzado.');

insert into Tbl_Estado values('CT', 'Construcción Terminada', 'La construcción ha sido completada y está pendiente de exploción.');
insert into Tbl_Estado values('CT', 'Terminad', 'OP Finalizada, Todas las operaciones relacionadas han sido completadas y se encuentra lista para su entrega al cliente.');

-- insert into Tbl_Estado values('EC','Entregado Cliente','');
-- Esta línea está comentada ya que la descripción está pendiente de ser definida.

insert into Tbl_Estado values('T', 'Terminado', 'OP Finalizada, Todas las operaciones relacionadas han sido completadas y se encuentra lista para su entrega al cliente.');
insert into Tbl_Estado values('R', 'Rechazado', 'Rechazado por supervisor');

CREATE  TABLE Tbl_Componentes (
    Id INT PRIMARY KEY IDENTITY,           
    Codigo VARCHAR(50),               
    Nombre VARCHAR(255),         
    DescripcionComponente VARCHAR(255),  
    Color VARCHAR(50),                    
    Unidad VARCHAR(50),               
    SubProducto VARCHAR(50),              
    CodigoProducto VARCHAR(50)  ,
	
FechaCreacion datetime default getdate(),
FechaModificacion datetime,
IdUsuarioCreacion int,
IdUsuarioModifica int,
Estado int default 1    
);

create	  tABLE Tbl_OrdenProduccion(
Id INT PRIMARY KEY IDENTITY,
NumeroCotizacion Varchar(10),
CodigoSisgeco varchar(10),
NumdoCref varchar(20),
FechaSap datetime,
--DATOS DE SISGECO
FechaCotizacion varchar(10),
FechaVenta varchar(10),
TipoMoneda varchar(10),
TipoCambio varchar(10),
Monto numeric(10,3),
SubTotal numeric(10,3),
TotalIgv numeric(10,3),
Total numeric(10,3),

Observacion varchar(100),
Observacion2 varchar(100),

IdTipoCliente int,
RucCliente varchar(35),
Cliente varchar(190),
Departamento varchar(50),
Provincia varchar(50),
Distrito varchar(50),
Direccion varchar(100),
Telefono varchar(20),

IdDestino int,
IdTipoPeracion int,
IdProyecto int,
Nivel varchar(10),
SubNivel varchar(10),


CodigoVendedor varchar(20),
NombreVendedor varchar(100),

Archivo varchar(100),
IdEstado int,
FechaRegistro datetime,--FECHA DB
FechaModificacion Datetime,
IdUsuarioCreacion int,
IdUsuarioModifica int,

Estado int default 1 
);


CREATE TABLE Tbl_Ambiente(
Id int primary key identity,
NumeroCotizacion varchar(10),
Indice int,
Ambiente varchar(50),
CantidadProducto int);


Create Table Tbl_DetalleOpGrupo(
Id int primary key identity,
IdTbl_OrdenProduccion int,
NumeroCotizacion varchar(10),
CotizacionGrupo varchar(10),
CodigoSisgeco varchar(10),
FechaProduccion DateTime,
Turno varchar(10),
Tipo varchar(50),

IdUsuarioCrea int,
IdUsuarioModifica int,
FechaCreacion datetime default getdate(),
FechaModifica datetime,
IdEstado int default 1);


 CREATE TABLE TBL_DetalleOrdenProduccion (
    Id INT PRIMARY KEY IDENTITY,
	IdTbl_DetalleOpGrupo int,
	NumeroCotizacion varchar(10),
	CotizacionGrupo varchar(10),
	CodigoSisgeco varchar(10),
	--PRODUCTO
	CodigoProducto varchar(50),
	NombreProducto varchar(100),
	Familia varchar(10),
	SubFamilia varchar(10),
	UnidadMedida varchar(10),
	Cantidad NUMERIC (10,3),
	Alto NUMERIC (10,3),
	Ancho NUMERIC (10,3),
	Linea Varchar(10),
	Precio varchar(50),
	PrecioInc varchar(50),
    Igv  varchar(50),
	Lote varchar(10),
	---pendientes -agregados
	TuboMedida  NUMERIC (10,3),--ok
	TelaMedida NUMERIC (10,3),--ok
	AlturaMedida NUMERIC (10,3), --ok 
	Central VARCHAR(50),-- ok es el check al registrar producto
	CentralIndex VARCHAR(50), -- depende de la logixa del central
	IndexDetalle VARCHAR(50), --EL INCREMENTAL ++
	

	---
	Escuadra varchar(50), -- ESCUDRA ES EL SI O EL NO DEL POPUP DONDE GUARDA EL PRODUCTO

	--
	/*

	DESDE WEB CUARDAR DE LA SIGUIENTE MANERA: 
	tubo_medida = parseFloat(regAncho) - 0.025;
            regtubo_medida=tubo_medida;
                console.log("tubo-->",tubo_medida);

            tela_medida = parseFloat(tubo_medida) - 0.001;
            regtela_medida=tela_medida;
                console.log("tela-->",tela_medida);

            altura_medida = parseFloat(regAlto) + 0.20;
            regaltura_medida=altura_medida;
                console.log("altura_medida-->",altura_medida);




	*/
	--    
    FechaProduccion DATETIME,
    FechaEntrega DATETIME,
    Nota VARCHAR(200), 
    Color VARCHAR(100),
    Tipo varchar(20),

	--CAMPOS DEL FORMULARIO
    IndiceAgrupacion VARCHAR(100),
    IdTbl_Ambiente VARCHAR(100),-- INDICE
	Ambiente varchar(100),
    Turno VARCHAR(100),
    SoporteCentral VARCHAR(100),
    TipoSoporteCentral VARCHAR(100),
    Caida VARCHAR(100),
    Accionamiento VARCHAR(100),
    CodigoTubo varchar(100),
    NombreTubo VARCHAR(100),
    Mando VARCHAR(100),
    TipoMecanismo VARCHAR(100),
    ModeloMecanismo VARCHAR(100),
    TipoCadena VARCHAR(100),
    CodigoCadena VARCHAR(100),
    Cadena VARCHAR(100),
    TipoRiel VARCHAR(100),
    TipoInstalacion VARCHAR(100),
    CodigoRiel VARCHAR(100),
    Riel VARCHAR(100),
    TipoCassete VARCHAR(100),
    Lamina VARCHAR(100),
    Apertura VARCHAR(100),
    ViaRecogida VARCHAR(100),
    TipoSuperior VARCHAR(100),
    CodigoBaston VARCHAR(100),
    Baston VARCHAR(100),
    NumeroVias VARCHAR(100),
    TipoCadenaInferior VARCHAR(100),
    MandoCordon VARCHAR(100),
    MandoBaston VARCHAR(100),
    CodigoBastonVarrilla VARCHAR(100),
    BastonVarrilla VARCHAR(100),
    Cabezal VARCHAR(100),
    CodigoCordon VARCHAR(100),
    Cordon VARCHAR(100),
    CodigoCordonTipo2 VARCHAR(100),
    CordonTipo2 VARCHAR(100),
    Cruzeta VARCHAR(100),
    Dispositivo VARCHAR(100),
    CodigoControl VARCHAR(100),
    Control VARCHAR(100),
    CodigoSwitch VARCHAR(100),
    Switch VARCHAR(100),
    LlevaBaston VARCHAR(100),
    MandoAdaptador VARCHAR(100),
    CodigoMotor VARCHAR(100),
    Motor VARCHAR(100),
    CodigoTela VARCHAR(100),
    Tela VARCHAR(100),

    Cenefa VARCHAR(100),
    NumeroMotores INT,
    Serie VARCHAR(100),
    AlturaCadena DECIMAL(10, 2),
    AlturaCordon DECIMAL(10, 2),
    MarcaMotor VARCHAR(100),

	--END CAMPOS
	IdUsuarioCrea int,
IdUsuarioModifica int,
FechaCreacion datetime default getdate(),
FechaModifica datetime, 
IdEstado int  
);
 
CREATE TABLE dbo.Tbl_Escuadra
	(
	Id                INT IDENTITY NOT NULL,
	IdProducto INT,
	CotizacionGrupo   VARCHAR (10) NULL,
	Codigo            VARCHAR (100) NULL,
	Descripcion       VARCHAR (100) NULL,
	Cantidad          INT NULL,
	IdUsuarioCrea     INT NULL,
	IdUsuarioModifica INT NULL,
	FechaCreacion     DATETIME DEFAULT (getdate()) NULL,
	FechaModifica     DATETIME NULL,
	PRIMARY KEY (Id)
	)
GO


create table Tbl_Estacion(
Id int PRIMARY KEY IDENTITY,
Nombre  varchar(50),
CodigoEstacion varchar(24),
FechaCreacion datetime default getdate(),
FechaModificacion datetime,
IdUsuarioCreacion int,
IdUsuarioModificacion int,
Estado int default 1
);

 
 INSERT INTO dbo.Tbl_Estacion (Nombre, CodigoEstacion, FechaCreacion, Estado)
VALUES ('Inicio Produccion', '100001', '2024-05-28 19:53:50.653', 1)
GO

INSERT INTO dbo.Tbl_Estacion (Nombre, CodigoEstacion, FechaCreacion, Estado)
VALUES ('Termino Produccion', '100002', '2024-05-30 19:19:01.46', 1)
GO


CREATE   TABLE dbo.Tbl_ProduccionEstacion
	(
	Id                    INT IDENTITY NOT NULL,
	IdDetalleOp           INT NULL,
	IdEstacion INT,
	NumeroCotizacion      VARCHAR (20) NULL,
	CotizacionGrupo       VARCHAR (20) NULL,
	CodigoUsuario         VARCHAR (20) NULL,
	TipoProceso           VARCHAR (50) DEFAULT ('Normal') NULL,
	FechaInicio           DATETIME NULL,
	FechaFin              DATETIME NULL,
	FechaCreacion         DATETIME DEFAULT (getdate()) NULL,
	FechaModificacion     DATETIME NULL,
	IdUsuarioCreacion     INT NULL,
	IdUsuarioModificacion INT NULL,
	Estado                INT DEFAULT ((1)) NULL,
	PRIMARY KEY (Id)
	)
GO


    
CREATE    DROP TABLE dbo.Tbl_DetalleProductoEstacion
	(
	Id                       INT IDENTITY NOT NULL,
	IdProduccionEstacion     INT NULL,
	IdDetalleOrdenProduccion INT NULL,
	NumeroCotizacion         VARCHAR (50) NULL,
	CotizacionGrupo          VARCHAR (20) NULL,
	IdEstacion INT,
	/*CodigoProducto           VARCHAR (50) NULL,
	NombreProducto           VARCHAR (100) NULL,
	Unidad                   VARCHAR (10) NULL,
	Accionamiento            VARCHAR (20) NULL,
	CantidadReal             VARCHAR (20) NULL,
	TipoAccesorio            VARCHAR (50) NULL,
	CodigoAccesorio          VARCHAR (50) NULL,
	NombreAccesorio          VARCHAR (200) NULL,
	CantidadAccesorio        VARCHAR (20) NULL,
	MermaAccesorio           VARCHAR (20) NULL,*/
	FechaInicio DATETIME,
	FechaFin DATETIME,
	FechaCreacion            DATETIME DEFAULT (getdate()) NULL,
	FechaModificacion        DATETIME NULL,
	IdUsuarioCreacion        INT NULL,
	IdUsuarioModificacion    INT NULL,
	Estado                   INT DEFAULT ((1)) NULL,
	PRIMARY KEY (Id)
	)
GO


 


-------::::::::::::::::::::::::::::::::::::::::::END:::::::::::::::::::::::::::::-----

select NumeroCotizacion,CodigoProducto,linea,NombreProducto,UnidadMedida,Cantidad,Alto,Ancho,
'' AS indice_agrupacion,  '' AS index_detalle,  '' as Pase,'' as Existe
from TBL_DetalleOrdenProduccion 
where NumeroCotizacion= '09232'
order by linea ASC 

-----------

sp_helptext usp_get_OPsV2

SELECT top 5
                '' AS Id,
                CC.numero AS NumeroCotizacion,
                CC.numdocref AS CodigoSisgeco,
                CD.codarticulo AS CodigoProducto,
                CD.linea AS Linea,
                CD.[des] AS NombreProducto,
                A.codunidad AS UnidadMedida,
                CD.cantventa AS Cantidad,
                CD.alto AS Alto,
                CD.ancho AS Ancho,
                '' AS IndiceAgrupacion,
                '' AS IndexDetalle,
                CASE
                    WHEN SUBSTRING(cd.codarticulo, 1, 3) != 'PRT' THEN 'PASDIRECCT'
                    ELSE ''
                END AS Pase,
                '' AS Existe
            FROM
                [dbo].[CotizacionDet] AS CD
                INNER JOIN [dbo].[CotizacionCab] AS CC ON CD.numero = CC.numero
                INNER JOIN [dbo].[Articulo] AS A ON CD.codarticulo = A.codigo
            WHERE

			select top 2 * from [CotizacionDet]

----  
-- =============================================   
-- Author: Nelson Rodriguez  
-- Create date: 03/11/2021  
-- Description: Buscar Producto por OP  
-- =============================================  
CREATE  PROCEDURE [dbo].[usp_Producto_por_OP] --'OP1000-18'  
@NumOP as nvarchar(25)  
AS     
BEGIN    
  
--IF @NumOP<>''    
--BEGIN   
  
select    top 2
CC.numero as NumeroCotizacion,
CC.numdocref as'CodigoSisgeco',  
CD.codarticulo as 'CodigoProducto',  
CD.linea as 'linea',   
CD.[des] as 'NombreProducto',  
A.codunidad AS 'UnidadMedida',  
CD.cantventa as 'Cantidad',  
CD.alto as 'Alto',  
CD.ancho as 'Ancho' ,  
'' AS indice_agrupacion,  
'' AS index_detalle,  
(CASE WHEN   
 substring(cd.codarticulo,1,3) !='PRT'       
 THEN 'PASDIRECCT'  
ELSE  
''  
END  )AS Pase  ,
'' Existe
from [dbo].[CotizacionDet] as CD  
inner join [dbo].[CotizacionCab] as CC on CD.numero = CC.numero   
inner join [dbo].[Articulo] as A on CD.codarticulo = A.codigo   
where CC.numero = '000579'    
ORDER BY CD.linea ASC  
--END   
  
END
-------------

  
CREATE   PROCEDURE [dbo].[usp_get_OPsV2]    --LISTA OPS
@numero NVARCHAR(100)  
AS    
BEGIN     
SELECT   top 50   
numero,    
isnull(numdocref,'') AS numdocref ,    
--PV.numdocide as 'RUC',     
PV.codigo as 'RUC',     
PV.des AS 'Cliente',    
CC.total AS 'Total',    
    
convert(VARCHAR(10),CC.fecha,121) AS fecha_cotizacion,    
convert(VARCHAR(10),CC.fechaven,121) AS fechaVenta,    
CC.moneda AS tipomoneda,    
CC.TC AS tipocambio,    
CC.totalneto AS subtotal,    
CC.total AS monto,    
v.codigo AS codvendedor,    
v.des AS nomVendedor,    
z.des AS distrito,    
u.provincia,    
u.departamento,    
CC.observacion,    
CC.observacion2,    
CC.totalIGV,    
c.direccion AS direccion,    
c.fono AS telefono,    
(CASE WHEN     
((SELECT count(*) FROM CotizacionDet d WHERE d.numero=CC.numero)=    
(SELECT count(*) FROM CotizacionDet  d where d.numero=CC.numero AND substring(d.codarticulo,1,3) !='PRT'))      
 THEN 'PASDIRECCT'    
ELSE    
''    
END  )AS Pase    
-- total AS monto,    
    
FROM dbo.CotizacionCab CC    
LEFT JOIN Zona z ON z.codigo = CC.codzona    
INNER JOIN provClie PV ON PV.codigo = CC.codprovclie    
LEFT JOIN Ubigeo u ON u.codigo=PV.codubigeo    
LEFT JOIN Vendedor v ON v.codigo = CC.codvendedor    
LEFT JOIN ProvClie c ON c.codigo=CC.codprovclie    
where  YEAR( cc.fecha)=YEAR(GETDATE())  
AND cc.numero LIKE '%' + @numero + '%'  
-- WHERE numdocref <> ''    
END

select * from Tbl_OrdenProduccion
select * from tbl_ambiente


insert into Tbl_OrdenProduccion (id) values(1)

select * from TBL_DetalleOrdenProduccion

select  
GRD.CotizacionGrupo,
GRD.FechaProduccion,
GRD.Turno,
E.Nombre as EstadoGrupo,
GRD.Tipo as TipoGrupo,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTRS' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as  RS,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTRZ' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as ZB,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTPH' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as PH,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTCV' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as CV,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTSH' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as SH,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTCS' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as CE,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,5)='PRTPJ' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as PJ,
(select count(*) from TBL_DetalleOrdenProduccion dop where  substring(dop.CodigoProducto,1,3)<>'PRT' and dop.CotizacionGrupo=GRD.CotizacionGrupo) as OTROS,
OP.*
from Tbl_DetalleOpGrupo GRD
JOIN Tbl_OrdenProduccion OP on OP.NumeroCotizacion=GRD.NumeroCotizacion
JOIN Tbl_Estado E on E.Id=GRD.IdEstado

ALTER PROCEDURE SP_GetOrdenProduccionDetalleGrupo 
@Vendedor VARCHAR(30),
@NumeroCotizacion VARCHAR(30),
@Cliente VARCHAR(30),
@FechaInicio VARCHAR(30),
@FechaFin VARCHAR(30),
@CodigoSisgeco VARCHAR(30),
@RucCliente VARCHAR(30),
@IdProyecto VARCHAR(30)
AS  
BEGIN   
    SET NOCOUNT ON;
    
    -- Convertir las fechas una sola vez fuera del SELECT
    DECLARE @FechaInicioConv DATE = NULL;
    DECLARE @FechaFinConv DATE = NULL;
    
    IF @FechaInicio <> '--' SET @FechaInicioConv = CONVERT(DATE, @FechaInicio);
    IF @FechaFin <> '--' SET @FechaFinConv = CONVERT(DATE, @FechaFin);

    SELECT  
    GRD.Id AS IdGrupo,
        GRD.CotizacionGrupo,
        GRD.FechaProduccion,
        GRD.Turno,
        E.Nombre AS EstadoPedido,
        GRD.Tipo AS TipoGrupo,
        TP.Nombre AS TipoOperacion,
        TC.Nombre AS TipoCliente,
        GRD.FechaProduccion, 
        TPP.Nombre AS NombreProyecto,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTRS' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS RS,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTRZ' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS ZB,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTPH' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS PH,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTCV' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS CV,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTSH' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS SH,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTCS' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS CE,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTPJ' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS PJ,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 3) <> 'PRT' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS OTROS,
        OP.*
    FROM 
        Tbl_DetalleOpGrupo GRD
    JOIN 
        Tbl_OrdenProduccion OP ON OP.NumeroCotizacion = GRD.NumeroCotizacion
    JOIN 
        Tbl_Estado E ON E.Id = GRD.IdEstado
    JOIN 
         Tbl_TipoOperacion TP ON TP.Id=OP.IdTipoPeracion 
    JOIN 
        Tbl_TipoCliente TC ON TC.Id = OP.IdTipoCliente
    JOIN 
        Tbl_Proyecto TPP ON TPP.Id = OP.IdProyecto
    WHERE 
        (CASE WHEN @Vendedor = '--' THEN 1 ELSE CASE WHEN OP.NombreVendedor = @Vendedor THEN 1 ELSE 0 END END = 1) AND  
        (CASE WHEN @NumeroCotizacion = '--' THEN 1 ELSE CASE WHEN OP.NumeroCotizacion = @NumeroCotizacion THEN 1 ELSE 0 END END = 1) AND
        (CASE WHEN @Cliente = '--' THEN 1 ELSE CASE WHEN OP.Cliente = @Cliente THEN 1 ELSE 0 END END = 1) AND 
        (CASE WHEN @FechaInicio = '--' THEN 1 ELSE CASE WHEN OP.FechaRegistro >= @FechaInicioConv THEN 1 ELSE 0 END END = 1) AND
        (CASE WHEN @FechaFin = '--' THEN 1 ELSE CASE WHEN OP.FechaRegistro <= @FechaFinConv THEN 1 ELSE 0 END END = 1) AND
        (CASE WHEN @CodigoSisgeco = '--' THEN 1 ELSE CASE WHEN OP.CodigoSisgeco = @CodigoSisgeco THEN 1 ELSE 0 END END = 1) AND 
        (CASE WHEN @RucCliente = '--' THEN 1 ELSE CASE WHEN OP.RucCliente = @RucCliente THEN 1 ELSE 0 END END = 1) AND 
        (CASE WHEN @IdProyecto = '--' THEN 1 ELSE CASE WHEN OP.IdProyecto = @IdProyecto THEN 1 ELSE 0 END END = 1);
    
    SET NOCOUNT OFF;
END
GO





CREATE FUNCTION dbo.FIND_IN_SET
(
    @value NVARCHAR(MAX),
    @list NVARCHAR(MAX)
)
RETURNS INT
AS
BEGIN
    -- Declare variables
    DECLARE @pos INT = 0;
    DECLARE @index INT = 1;
    DECLARE @item NVARCHAR(MAX);
    
    -- Iterate through the list
    WHILE @index > 0
    BEGIN
        -- Get the position of the next comma
        SET @index = CHARINDEX(',', @list, @pos + 1);
        
        -- Get the item from the list
        SET @item = LTRIM(RTRIM(SUBSTRING(@list, @pos + 1, CASE WHEN @index > 0 THEN @index - @pos - 1 ELSE LEN(@list) END)));
        
        -- Check if the item matches the value
        IF @item = @value
        BEGIN
            RETURN 1; -- Return 1 if the value is found
        END
        
        -- Update the position
        SET @pos = @index;
    END
    
    -- Return 0 if the value is not found
    RETURN 0;
END
  
    ALTER Proc SP_ListarProdParaEstacionXGrupo  
@GrupoCotizacion varchar(50),
@ESTACION VARCHAR(50)
as
BEGIN

  	SELECT 
            Dop.Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            dop.CodigoProducto,
            (dop.NombreProducto) as NombreProducto,
            (dop.Lamina) AS Lamina,
             dop.CodigoCadena,
			(Dop.Baston) as Baston,
            (dop.CodigoBastonVarrilla),
            dop.CodigoCordon,
            dop.CodigoCordonTipo2,
            (dop.CodigoTela)   AS Cod_Tela,
            (dop.Tela)   AS Nombre_Tela,
            (dop.CodigoRiel) AS CodigoRiel,
            (dop.CodigoBaston) AS CodigoBaston,
            (dop.CodigoTubo)    AS Cod_Tubo,
            (NombreTubo) AS Nombre_Tubo,
            (dop.CodigoSwitch) AS CodigoSwitch,
            (SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            (dop.TipoSuperior) AS TipoSuperior,
            (dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            (dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            (dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id)) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
 /*
--DECLARE @ESTACION VARCHAR(50)
--SET @ESTACION='Ensable1'

IF @ESTACION='Ensable1' 
BEGIN
 -- INIT
 	SELECT 
         STRING_AGG(CAST(Dop.Id AS VARCHAR(50)), '-') AS Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            dop.CodigoProducto,
            Max(dop.NombreProducto) as NombreProducto,
            Max(dop.Lamina) AS Lamina,
             dop.CodigoCadena,
			Max(Dop.Baston) as Baston,
            Max(dop.CodigoBastonVarrilla),
            dop.CodigoCordon,
            dop.CodigoCordonTipo2,
            Max(dop.CodigoTela)   AS Cod_Tela,
            Max(dop.Tela)   AS Nombre_Tela,
            Max(dop.CodigoRiel) AS CodigoRiel,
            Max(dop.CodigoBaston) AS CodigoBaston,
            Max(dop.CodigoTubo)    AS Cod_Tubo,
            Max(NombreTubo) AS Nombre_Tubo,
            Max(dop.CodigoSwitch) AS CodigoSwitch,
            Max(SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            Max(dop.TipoSuperior) AS TipoSuperior,
            SUM(dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            SUM(dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            SUM(dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST(SUM((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id))) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
			-- AND SUBSTRING(dop.CodigoProducto, 1, 5) in('PRTCS')
        GROUP BY 
             dop.CodigoSisgeco,GRD.NumeroCotizacion,Grd.CotizacionGrupo,
            dop.CodigoProducto ,   dop.CodigoCadena,dop.CodigoCordon,dop.CodigoCordonTipo2,   dop.Accionamiento
			 

 -- END
 END
 
ELSE IF  @ESTACION='Corte1' 
BEGIN
-- ROLLER SHADE estacion 1
			SELECT 
         STRING_AGG(CAST(Dop.Id AS VARCHAR(50)), '-') AS Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            dop.CodigoProducto,
            Max(dop.NombreProducto) as NombreProducto,
            Max(dop.Lamina) AS Lamina,
            Max(dop.CodigoCadena) AS CodigoCadena,
			Dop.Baston,
            dop.CodigoBastonVarrilla,
            Max(dop.CodigoCordon) AS CodigoCordon,
            Max(dop.CodigoCordonTipo2) AS CodigoCordonTipo2,
            Max(dop.CodigoTela)   AS Cod_Tela,
            Max(dop.Tela)   AS Nombre_Tela,
            dop.CodigoRiel AS CodigoRiel,
            Max(dop.CodigoBaston) AS CodigoBaston,
             dop.CodigoTubo    AS Cod_Tubo,
            Max(NombreTubo) AS Nombre_Tubo,
            Max(dop.CodigoSwitch) AS CodigoSwitch,
            Max(SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            Max(dop.TipoSuperior) AS TipoSuperior,
            SUM(dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            SUM(dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            SUM(dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST(SUM((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id))) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
			-- AND SUBSTRING(dop.CodigoProducto, 1, 5) in('PRTPH','PRTPJ','PRTRF')
        GROUP BY 
             dop.CodigoSisgeco,GRD.NumeroCotizacion,Grd.CotizacionGrupo,
            dop.CodigoProducto ,   dop.CodigoTubo,   Dop.Baston,Dop.CodigoBastonVarrilla,Dop.Lamina,Dop.CodigoRiel ,
            dop.Accionamiento
END
ELSE IF  @ESTACION='Corte2' 
BEGIN
-- // ROLLER SHADE ESTACION 2 : TELA
			SELECT 
         STRING_AGG(CAST(Dop.Id AS VARCHAR(50)), '-') AS Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            dop.CodigoProducto,
            Max(dop.NombreProducto) as NombreProducto,
            Max(dop.Lamina) AS Lamina,
            Max(dop.CodigoCadena) AS CodigoCadena,
			Max(Dop.Baston) as Baston,
            Max(dop.CodigoBastonVarrilla),
            Max(dop.CodigoCordon) AS CodigoCordon,
            Max(dop.CodigoCordonTipo2) AS CodigoCordonTipo2,
            dop.CodigoTela   AS Cod_Tela,
            Max(dop.Tela)   AS Nombre_Tela,
            Max(dop.CodigoRiel) AS CodigoRiel,
            Max(dop.CodigoBaston) AS CodigoBaston,
            Max(dop.CodigoTubo)    AS Cod_Tubo,
            Max(NombreTubo) AS Nombre_Tubo,
            Max(dop.CodigoSwitch) AS CodigoSwitch,
            Max(SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            Max(dop.TipoSuperior) AS TipoSuperior,
            SUM(dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            SUM(dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            SUM(dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST(SUM((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id))) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
			-- AND SUBSTRING(dop.CodigoProducto, 1, 5) in('PRTPH','PRTPJ','PRTRF')
        GROUP BY 
             dop.CodigoSisgeco,GRD.NumeroCotizacion,Grd.CotizacionGrupo,
            dop.CodigoProducto ,   dop.CodigoTela,  dop.Accionamiento
			 
END
ELSE
BEGIN

SELECT 
         STRING_AGG(CAST(Dop.Id AS VARCHAR(50)), '-') AS Id,
            dop.CodigoSisgeco,
            GRD.NumeroCotizacion,
            Grd.CotizacionGrupo,
            Max(dop.CodigoProducto) as CodigoProducto,
            Max(dop.NombreProducto) as NombreProducto,
            Max(dop.Lamina) AS Lamina,
            Max(dop.CodigoCadena) as CodigoCadena,
			Max(Dop.Baston) as Baston,
            Max(dop.CodigoBastonVarrilla),
            Max(dop.CodigoCordon) as CodigoCordon,
            Max(dop.CodigoCordonTipo2) as CodigoCordonTipo2,
            Max(dop.CodigoTela)   AS Cod_Tela,
            Max(dop.Tela)   AS Nombre_Tela,
            Max(dop.CodigoRiel) AS CodigoRiel,
            Max(dop.CodigoBaston) AS CodigoBaston,
            Max(dop.CodigoTubo)    AS Cod_Tubo,
            Max(NombreTubo) AS Nombre_Tubo,
            Max(dop.CodigoSwitch) AS CodigoSwitch,
            Max(SUBSTRING(dop.CodigoSisgeco, 1, 5)) AS FamiliaProducto,
            1 AS est, 
            dop.Accionamiento,
            Max(dop.TipoSuperior) AS TipoSuperior,
            SUM(dbo.CalcularTubo(Dop.Id)) AS TuboCalculo,   
            SUM(dbo.CalcularTela(Dop.Id)) AS TelaCalculo,   
            SUM(dbo.CalcularAltura(Dop.Id)) AS AlturaCalculo,
            CAST(SUM((dbo.CalcularTela(Dop.Id) * dbo.CalcularAltura(Dop.Id))) AS DECIMAL(18,3)) AS Area,
            NULL AS LaminaUnidad,
            NULL AS CadenaUnidad,
            NULL AS BastonVarrillaUnidad,
            NULL AS CordonUnidad,
            NULL AS CordonTipo2Unidad,
            NULL AS rielUnidad,
            NULL AS bastonUnidad,
            NULL AS Cod_TuboUnidad,
            NULL AS Cod_TelaUnidad
        FROM  
            TBL_DetalleOrdenProduccion Dop  
        JOIN  
            Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo = Dop.CotizacionGrupo 
                                    AND Grd.NumeroCotizacion = Dop.NumeroCotizacion
        WHERE 
            SUBSTRING(dop.CodigoProducto, 1, 3) = 'PRT' 
			AND GRD.CotizacionGrupo=@GrupoCotizacion
			-- AND SUBSTRING(dop.CodigoProducto, 1, 5) in('PRTPH','PRTPJ','PRTRF')
        GROUP BY 
             dop.CodigoSisgeco,GRD.NumeroCotizacion,Grd.CotizacionGrupo,
			 dop.Accionamiento

END
*/
END
GO
Alter PROC Sp_ListarExplocion
@NumeroCotizacion VARCHAR(10),
@FechaInicio VARCHAR(10),
@FechaFin VARCHAR(10)
AS
BEGIN
SELECT  GRD.Id , GRD.NumeroCotizacion,GRD.CotizacionGrupo, OP.NumeroCotizacion AS Cotizacion,
OP.CodigoSisgeco, OP.RucCliente AS Ruc, OP.Cliente AS RazonSocial,  DP.CodigoProducto,
DP.NombreProducto,DP.Accionamiento, DP.Cantidad, 1 AS CantidadProductos,
E.Nombre AS Estado
FROM TBL_DetalleOrdenProduccion DP
LEFT JOIN Tbl_DetalleOpGrupo GRD ON DP.NumeroCotizacion=GRD.NumeroCotizacion AND DP.CotizacionGrupo=GRD.CotizacionGrupo
LEFT JOIN	Tbl_OrdenProduccion OP ON GRD.NumeroCotizacion=OP.NumeroCotizacion
LEFT JOIN Tbl_estado E ON GRD.IdEstado=E.Id
 END	

 

ALTER PROC Sp_listarComponentesProductoPorGrupo
@Grupo VARCHAR(20),
@Id VARCHAR(10)
as
BEGIN 
SELECT * FROM (
SELECT 'Cadena' AS Componente, CodigoCadena AS Codigo, Cadena AS Nombre FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo 
UNION ALL
SELECT 'Riel', CodigoRiel, Riel FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'Baston', CodigoBaston, Baston FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'BastonVarrilla', CodigoBastonVarrilla, BastonVarrilla FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'Cordon', CodigoCordon, Cordon FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'CordonTipo2', CodigoCordonTipo2, CordonTipo2 FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'Control', CodigoControl, Control FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'Switch', CodigoSwitch, Switch FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'Motor', CodigoMotor, Motor FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'Tela', CodigoTela, Tela FROM TBL_DetalleOrdenProduccion  WHERE CotizacionGrupo= @Grupo
UNION ALL
SELECT 'Tubo', CodigoTubo, NombreTubo FROM TBL_DetalleOrdenProduccion   WHERE CotizacionGrupo= @Grupo
) AS com WHERE isnull(Codigo,'--') <>'--';
END
GO

--ULTIMO:


 
ALTER PROCEDURE [dbo].[SP_GetLayoutGrupo]      
    @NumeroCotizacionGrupo NVARCHAR(50)      
 AS BEGIN  

select     
case when  
 subString(dop.CodigoProducto,1,5) = 'PRTRS' then  
-- ROLLER SHADE   
(select  top 1  
case when dop1.Accionamiento='Manual' then  
   case when a.CantidadProducto=1 then   
   --     
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.028)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
   --  
     when isnull(a.CantidadProducto,'0')='0' then  
   --  
           case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.028)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
    --   
     when a.CantidadProducto=2 then  
    --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.025)   
          end  
  --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'    then  (dop1.Ancho - 0.021)           
            when dop1.CodigoTubo='PALRZ00000026'    then  (dop1.Ancho - 0.021)  
          end  
  --  
     when a.CantidadProducto >=4 then   
      --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'    then  (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.021)   
            -- when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.038)                 
  
          end  
   
 --  
end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --    -- BATERIA  
         case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.028)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)    
             
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.038)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.038)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
          end  
 when isnull(a.CantidadProducto,'0')='0' then   
       --  
        case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.028)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.030)    
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.038)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.038)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
              
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.023)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.023)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.023)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.025)    
              
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Ancho - 0.029)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.029)   
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then (dop1.Ancho - 0.040)   
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then (dop1.Ancho - 0.040)    
          end     
         
 --  
      when a.CantidadProducto=3 then   
       --  
          case     
            when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)    
            -- RAEX                    
      when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    --- PRIMERO PRODUCTO   
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- ULTIMO PRODUCTO  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
          -- END (dop1.Ancho - 0.029)   
          when dop1.CodigoTubo='PALLU00000003' and  dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end   
                  
           when dop1.CodigoTubo='PALRS00000024' and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end  
   when dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.040)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.040)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.040)  
    end   
       end   
 --  
      when a.CantidadProducto>=4 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRS00000004'   and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)      
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'   then (dop1.Ancho - 0.021)    
              
            when dop1.CodigoTubo='PALRS00000005'   and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then  
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
          -- END (dop1.Ancho - 0.029)   
           when dop1.CodigoTubo='PALLU00000003'  and   dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0 then  
            case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
         
          when dop1.CodigoTubo='PALRS00000024' then  
            case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.030)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.030)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.020)  
    end     
    
   when dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then  
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Ancho - 0.040)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Ancho - 0.040)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Ancho - 0.040)  
    end   
       end    
         
 --  
 end   
end   
from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id   
order by dop1.Id asc   
-- limit 1  
)  
--  Roller Zebra  TUBO  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN   
(  
select  top 1  
case when dop1.Accionamiento='Manual'  then  
 -- N2  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.030)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.030)   
          end  
            
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.021)   
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.035)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --    
       case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.035)   
          end  
          --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Ancho - 0.023)   
          end  
 --  
 end   
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
  
-- ROLLER BASICO TUBO  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.030)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.030)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.025)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Ancho - 0.025)      
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.021)    
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.019)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.019)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Ancho - 0.021)    
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
  
  
  
-- END  
-- Roller Shangrilla TUBO  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(select  top 1   
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
          end  
 --  
        when isnull(a.CantidadProducto,'0')='0' then  
     --  
             case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.030)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.030)   
              end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.020)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.020)   
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
          end  
 --  
         when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.025)   
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.025)    
            end  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then (dop1.Ancho - 0.021)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Ancho - 0.021)   
          end  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
ELSE '0'  
end as TuboCalculo,  
  
  
  
-- --TELA---- CONTINUAR DESDE AQUI MARISOL===============>  
case when   
 subString(dop.CodigoProducto,1,5)   ='PRTRS' then  
-- ROLLER SHADE   
(select  top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.028) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) -0.001)   
            -- when dop1.CodigoTubo='PALLU00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
        when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.028) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) -0.001)   
            -- when dop1.CodigoTubo='PALLU00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.025) -0.001)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
     when a.CantidadProducto >=4 then   
      --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.028) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)   
              
              
            -- RAEX  
              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)   
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.028) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.030) -0.001)   
              
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.038) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)    
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.023) -0.001)   
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.025) -0.001)   
              
            -- RAEX  
            when dop1.CodigoTubo='PALRS00000005'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.029) -0.001)               
            when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then ((dop1.Ancho - 0.029) -0.001)               
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then ((dop1.Ancho - 0.040) -0.001)    
            -- LUTRON  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then ((dop1.Ancho - 0.040) -0.001)   
              
          end  
 --  
    when a.CantidadProducto=3 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)               
              
            -- RAEX   
            when dop1.CodigoTubo='PALRS00000005' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0    then   
          -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
          -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where       dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
          -- END ((dop1.Ancho - 0.029) -0.001)   
          when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
    when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.040)-0.001)  
    end    
                  
          end  
 --  
      
     when a.CantidadProducto>=4 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000005'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRZ00000026'  and dop1.CodigoMotor='MOTRZ00000002'  then ((dop1.Ancho - 0.021) -0.001)    
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when dop1.CodigoTubo='PALRS00000024'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then -- --FORMULA  
     case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
          -- END ((dop1.Ancho - 0.029) -0.001)   
            when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.030)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.020)-0.001)  
    end   
   when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0    then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    ((dop1.Ancho - 0.040)-0.001)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    ((dop1.Ancho - 0.040)-0.001)  
    end    
                  
          end  
            
 end  
 --   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
  
-- ROLLER BASICO TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) - 0.001)   
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) - 0.001)  
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) - 0.001)   
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.030) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.030) - 0.001)  
          end  
          --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.025) - 0.001)  
            when dop1.CodigoTubo='PALLU00000003'    then ((dop1.Ancho - 0.025) - 0.001)  
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) - 0.001)   
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.019) - 0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.019) - 0.001)    
            when dop1.CodigoTubo='PALRS00000005'    then ((dop1.Ancho - 0.021) - 0.001)   
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
-- END  
--  Roller Zebra  TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
 -- N  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.030) -0.001)   
    end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.035) -0.001)   
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
           case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.035) -0.001)   
          end  
          --   
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Ancho - 0.023) -0.001)   
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id -- limit 1  
)  
  
  
-- Roller Shangrilla TELA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.030) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.030) -0.001)   
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.020) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.020) -0.001)   
          end  
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.025) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.025) -0.001)   
          end  
 --   
       when isnull(a.CantidadProducto,'0')='0' then   
       --  
          case  when dop1.CodigoTubo='PALRS00000003'   then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Ancho - 0.021) -0.001)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Ancho - 0.021) -0.001)   
          end  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
-- limit 1  
)  
  
ELSE '0'  
end as TelaCalculo,  
  
-- --ALTURA----  
case when   
 subString(dop.CodigoProducto,1,5)   ='PRTRS' then  
-- ROLLER SHADE   
(  
select  top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --    
    when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
         case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)   
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
               
          end  
 --  
     when a.CantidadProducto >=4 then   
      --  
          case   
   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'    then (dop1.Alto + 0.25)  
              
          end   
            
 --  
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
            -- RAEX  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
          end  
  --  
       when isnull(a.CantidadProducto,'0')='0' then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
  end  
 --  
   when a.CantidadProducto=2 then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.30)  
            -- RAEX              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRS00000024'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then (dop1.Alto + 0.30)  
            -- LUTRON              
            when dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0  then (dop1.Alto + 0.30)  
          end  
 --  
      when a.CantidadProducto=3 then   
       --  
          case   
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
              
            when dop1.CodigoTubo='PALRS00000005'   and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0 then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
    when dop1.CodigoTubo='PALRS00000024' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
          -- END (dop1.Alto + 0.25)   
            when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0  then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
    when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
          end  
 --  
      when a.CantidadProducto>=4 then   
       --  
          case   
            
            when dop1.CodigoTubo='PALRS00000003'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'  and dop1.CodigoMotor='MOTRZ00000002'  then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000005'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)  
            when dop1.CodigoTubo='PALRZ00000026'   and dop1.CodigoMotor='MOTRZ00000002' then (dop1.Alto + 0.25)   
              
            when dop1.CodigoTubo='PALRS00000005' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
     when dop1.CodigoTubo='PALRS00000024' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
             -- --FORMULA  
    case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.25)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.25)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.25)  
    end   
          -- END (dop1.Alto + 0.25)   
           when  dop1.CodigoTubo='PALLU00000003' and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTRM00000003,MOTRM00000002,MOTRS00000001,MOTCS00000001,MOTRS00000002')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
   when  dop1.CodigoTubo='PALLU00000003'  and dbo.FIND_IN_SET(dop1.CodigoMotor,'MOTLU00000002,MOTLU00000004,MOTLU00000003,MOTLU00000001')>0   then   
           case when dop1.Id =(select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion) then  
    (dop1.Alto + 0.30)  
    -- '0.029'  
    when dop1.Id=(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion)  then  
    (dop1.Alto + 0.30)  
    -- '0.029'        
    when   (select  count(*) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion  
    and dopn1.Id not in((select min(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion),(select max(dopn1.Id) from  TBL_DetalleOrdenProduccion dopn1 where   
    dopn1.CotizacionGrupo=dop1.CotizacionGrupo and dopn1.IndiceAgrupacion=dop1.IndiceAgrupacion))   
    ) >= 1 then  
    (dop1.Alto + 0.30)  
    end   
          end  
 --  
 --  
 end   
end     
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id --limit 1  
)  
  
  
  
-- ROLLER BASICO ALTURA  
  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRB' THEN  
(  
select top 1  
case when dop1.Accionamiento='Manual' then  
  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
         when isnull(a.CantidadProducto,'0')='0' then  
     --  
       case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)    
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case  when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'   then (dop1.Alto + 0.20)      
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)    
            when dop1.CodigoTubo='PALLU00000003'    then (dop1.Alto + 0.30)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)   
          end  
 --   
    when a.CantidadProducto >= 4 then  
      
          case   when dop1.CodigoTubo='PALRS00000003'    then (dop1.Alto + 0.20)  
            when dop1.CodigoTubo='PALRS00000004'    then (dop1.Alto + 0.20)     
            when dop1.CodigoTubo='PALRS00000005'    then (dop1.Alto + 0.25)   
          end  
   end      
    
end   
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
)  
  
  
-- END  
--  Roller Zebra ALTURA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTRZ' THEN  
(select top 1  
case when dop1.Accionamiento='Manual' then  
 -- N1  
   case when a.CantidadProducto=1 then   
   --  
          case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case  when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
     case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
   when a.CantidadProducto=2 then   
       --  
          case   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)   
            when dop1.CodigoTubo='PALRZ00000026'    then ((dop1.Alto*2) + 0.45)    
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id  
)  
  
-- Roller Shangrilla ALTURA  
WHEN   subString(dop.CodigoProducto,1,5)   ='PRTSH' THEN  
(  
select  top 1  
case when dop1.Accionamiento='Manual' then    
   case when a.CantidadProducto=1 then   
   --  
          case    
   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when isnull(a.CantidadProducto,'0')='0' then  
     --  
         case    
   when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when a.CantidadProducto=2 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
     when a.CantidadProducto=3 then  
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --   
   end   
     
  when dop1.Accionamiento='Motorizado' then   
 case when a.CantidadProducto=1 then   
     --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
      when isnull(a.CantidadProducto,'0')='0' then   
       --  
             case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
   when a.CantidadProducto=2 then   
       --  
          case when dop1.CodigoTubo='PALRS00000003'    then ((dop1.Alto*2) + 0.40)  
            when dop1.CodigoTubo='PALRS00000004'    then ((dop1.Alto*2) + 0.40)  
          end  
 --  
 end   
end    
  from  TBL_DetalleOrdenProduccion dop1   
LEFT OUTER join Tbl_DetalleOpGrupo lop1 on dop1.CotizacionGrupo = lop1.CotizacionGrupo  
left outer join Tbl_Ambiente a on dop1.IndiceAgrupacion=a.indice and a.NumeroCotizacion=lop1.NumeroCotizacion  
where dop1.Id=dop.Id)  
ELSE '0'  
end as AlturaCalculo,  
--  
Dop.Id,  
OP.Cliente,  
Tc.Nombre as TipoCliente,   
 Dop.CodigoSisgeco ,  
    Dop.CodigoProducto,  
    Dop.NombreProducto,  
    Dop.UnidadMedida,  
 Dop.Familia,  
    Dop.SubFamilia,  
    Dop.Cantidad,  
    Dop.Alto,  
    Dop.Ancho,    
    Dop.AlturaCadena,    
    Dop.Accionamiento,   
 Dop.NumeroMotores,  
    Dop.Motor As NombreMotor,  
    Dop.CodigoMotor,  
    Dop.MarcaMotor,  
    Isnull(Dop.NombreTubo,'') as 'NombreTubo',    
 Dop.Mando,  
    Dop.IndiceAgrupacion,      
    Dop.IdTbl_Ambiente as 'Ambiente',  
    Dop.Turno,  
    /**new atr**/  
 Grd.FechaProduccion,  
 Dop.FechaEntrega ,  
 -- nota,    
(SELECT STUFF(  
               (SELECT ' ' + sb.nota  
                FROM TBL_DetalleOrdenProduccion sb  
                WHERE sb.CotizacionGrupo = dop.CotizacionGrupo  
                FOR XML PATH(''), TYPE  
               ).value('.', 'NVARCHAR(MAX)'),   
               1, 1, ''))        
      as Nota,   
    (SELECT STRING_AGG(sb.nota, ' ')   
     FROM TBL_DetalleOrdenProduccion sb  
     WHERE sb.CotizacionGrupo  = dop.CotizacionGrupo  
     GROUP BY sb.CotizacionGrupo) AS nota ,  
 Dop.CodigoTela,  
    Dop.Tela,  
 Dop.Color,  
 case when Dop.MandoAdaptador=0 then 'No' else 'Si'end as MandoAdaptador ,  
 case when Dop.LlevaBaston   =0 then 'No' else 'Si'end as MandoAdaptador  ,  
    Dop.Cenefa,  
    Dop.SoporteCentral,  
 case when Dop.SoporteCentral   =0 then 'No' else 'Si'end as SoporteCentral  ,   
 Dop.TipoMecanismo  as 'Tipo_Mec',  
 Dop.ModeloMecanismo  as 'Modelo_Mec',  
 Dop.TipoRiel as 'Riel',  
 Dop.TipoInstalacion  as 'Tipo_Instal',  
    Dop.TipoCadena as 'Tipo_Cadena',  
 Dop.TipoCassete as 'Cassete',  
 Dop.TipoSoporteCentral  as 'Tipo_Sop_Central',  
    Dop.Caida  as'Tipo_Caida',  
 Dop.TipoSuperior  as 'Tipo_Superior',  
 Dop.Lamina,  
 Dop.Apertura,  
 Dop.TipoCadena as 'Cadena_Inf',  
 Dop.MandoCordon  as 'Mando_Cordon',  
 Dop.MandoBaston  'Mando_Baston',  
 Dop.Cruzeta  as 'Cruzeta',  
 Dop.LlevaBaston  as 'L_Baston',  
    Dop.Apertura as 'Apertura',  
 '' as 'Tubo_medida',--PENDIENTE AGREGAR Y VALIDAR  
   '' as 'Tela_medida',--PENDIENTE AGREGAR Y VALIDAR  
    '' as 'Altura_medida',--PENDIENTE AGREGAR Y VALIDAR  
    Dop.NumeroCotizacion,  
 Dop.CotizacionGrupo ,  
      
    -- PERSIANA HORIZONTAL  
    Dop.BastonVarrilla,  
    '' as cabezales, --PENDIENTE AGREGAR Y VALIDAR  
    Dop.AlturaCordon,  
    Dop.Cordon,  
    Dop.CordonTipo2,  
    Dop.ViaRecogida,  
    Dop.Baston  
 ,  
    Dop.NumeroVias,  
    Dop.Riel as 'riel_dat',  
 (select  count(*) from TBL_DetalleOrdenProduccion dtpp where dtpp.Id=dop.Id  
 and dtpp.NombreProducto LIKE  '%M1%') as tipoToldo,  
 Dop.Dispositivo,  
 isnull(OP.Direccion,'') as Direccion,  
 isnull(OP.Telefono,'') as Telefono,  
 isnull(OP.NombreVendedor,'') as Vendedor,  
 -- select * from Tbl_OrdenProduccion  
 OP.FechaRegistro as Fecha_Fabricacion,  
 --isnull(Dop.central,'NO')  -- --AGREGAR CAMPO  
 '' AS Central,   
 -- isnull(dop.central_index,'NO') --AGREGAR CAMPO  
 '' As Central_index,  
 -- isnull(index_detalle,'') --AGREGAR CAMPO  
 '' As Index_detalle,  
 ROUND((Dop.Alto * Dop.Ancho),3) as area,  
 isnull(Ds.Nombre,'') as Destino,   
 isnull(Tp.Nombre,'') as Tipo_OP,  
Dop.FechaEntrega,  
Convert(varchar,getdate(),103) as FechaImpresion,  
-- Dop.Escuadra,   
'' EscruadraDet  ,
 
 ISNULL(
    (
      SELECT 
        STUFF(
          (
            SELECT ' <b>|</b> ' + '<b>(</b> ' + CAST(ees.Codigo AS VARCHAR(100)) + '<b> - </b>' + CAST(ees.Cantidad AS VARCHAR(100)) + ' <b>)</b> '
            FROM Tbl_Escuadra ees 
            WHERE ees.CotizacionGrupo = dop.CotizacionGrupo
            FOR XML PATH(''), TYPE
          ).value('.', 'NVARCHAR(MAX)'), 
          1, 10, ''
        )
    ),
    ''
  ) AS EscruadraDet
   
  
from  TBL_DetalleOrdenProduccion Dop  
Join  Tbl_DetalleOpGrupo Grd ON Grd.CotizacionGrupo= Dop.CotizacionGrupo And Grd.NumeroCotizacion=Dop.NumeroCotizacion  
JOIN Tbl_OrdenProduccion OP on OP.NumeroCotizacion=Dop.NumeroCotizacion  
JOIN Tbl_Destino Ds on Ds.Id=OP.IdDestino   
JOIN Tbl_TipoOperacion Tp on Tp.Id=OP.IdTipoPeracion   
JOIN Tbl_TipoCliente Tc on Tc.Id=OP.IdTipoCliente   
WHERE  
substring(Dop.CodigoProducto,1,3)='PRT'   
-- Dop.Id <=27
--select Ancho - 0.025 from TBL_DetalleOrdenProduccion   
  
END  
GO

-----------




ALTER PROC Sp_ListarOperacionesConstruccion  
  @Fecha VARCHAR(10)
  AS
  BEGIN	
select 
GRD.Id,
  GRD.NumeroCotizacion,GRD.CodigoSisgeco ,
  p.nombre as NombreProyecto,
  GRD.CotizacionGrupo,
  E.Nombre   as EstadoOp,
  E.Descripcion AS EstadoDescripcion,
  TP.Nombre as TipoOperacion,
  op.RucCliente as RucCliente,
  op.cliente as NombreCliente,
  GRD.FechaCreacion AS FechaCreacion,
  GRD.FechaProduccion as FechaProduccion

  
from Tbl_DetalleOpGrupo GRD
JOIN Tbl_OrdenProduccion OP on OP.NumeroCotizacion=GRD.NumeroCotizacion
JOIN Tbl_Estado E on E.Id=GRD.IdEstado
join Tbl_TipoOperacion TP on tp.Id=OP.IdTipoPeracion 
join tbl_tipocliente tc on  tc.Id=IdTipoCliente
JOIN  tbl_proyecto p ON p.Id= op.IdProyecto

END
GO




Create   PROCEDURE SP_GetOrdenProduccionDetalleGrupoVenta 
@Vendedor VARCHAR(30),
@NumeroCotizacion VARCHAR(30),
@Cliente VARCHAR(30),
@FechaInicio VARCHAR(30),
@FechaFin VARCHAR(30),
@CodigoSisgeco VARCHAR(30),
@RucCliente VARCHAR(30),
@IdProyecto VARCHAR(30),
@IdTipoCliente VARCHAR(30)
AS  
BEGIN   
    SET NOCOUNT ON;
    
    -- Convertir las fechas una sola vez fuera del SELECT
    DECLARE @FechaInicioConv DATE = NULL;
    DECLARE @FechaFinConv DATE = NULL;
    
    IF @FechaInicio <> '--' SET @FechaInicioConv = CONVERT(DATE, @FechaInicio);
    IF @FechaFin <> '--' SET @FechaFinConv = CONVERT(DATE, @FechaFin);

    SELECT  
    GRD.Id AS IdGrupo,
        GRD.CotizacionGrupo,
        GRD.FechaProduccion,
        GRD.Turno,
        E.Nombre AS EstadoPedido,
        E.Descripcion AS EstadoDescripcion,
        GRD.Tipo AS TipoGrupo,
        TP.Nombre AS TipoOperacion,
        TC.Nombre AS TipoCliente,
        GRD.FechaProduccion, 
        TPP.Nombre AS NombreProyecto,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTRS' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS RS,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTRZ' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS ZB,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTPH' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS PH,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTCV' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS CV,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTSH' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS SH,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTCS' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS CE,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 5) = 'PRTPJ' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS PJ,
        (SELECT COUNT(*) FROM TBL_DetalleOrdenProduccion DOP WHERE SUBSTRING(DOP.CodigoProducto, 1, 3) <> 'PRT' AND DOP.CotizacionGrupo = GRD.CotizacionGrupo) AS OTROS,
        OP.*
    FROM 
        Tbl_DetalleOpGrupo GRD
    JOIN 
        Tbl_OrdenProduccion OP ON OP.NumeroCotizacion = GRD.NumeroCotizacion
    JOIN 
        Tbl_Estado E ON E.Id = GRD.IdEstado
    JOIN 
         Tbl_TipoOperacion TP ON TP.Id=OP.IdTipoPeracion 
    JOIN 
        Tbl_TipoCliente TC ON TC.Id = OP.IdTipoCliente
    JOIN 
        Tbl_Proyecto TPP ON TPP.Id = OP.IdProyecto
    WHERE 
        (CASE WHEN @Vendedor = '--' THEN 1 ELSE CASE WHEN OP.NombreVendedor = @Vendedor THEN 1 ELSE 0 END END = 1) AND  
        (CASE WHEN @NumeroCotizacion = '--' THEN 1 ELSE CASE WHEN OP.NumeroCotizacion = @NumeroCotizacion THEN 1 ELSE 0 END END = 1) AND
        (CASE WHEN @Cliente = '--' THEN 1 ELSE CASE WHEN OP.Cliente = @Cliente THEN 1 ELSE 0 END END = 1) AND 
        (CASE WHEN @FechaInicio = '--' THEN 1 ELSE CASE WHEN OP.FechaRegistro >= @FechaInicioConv THEN 1 ELSE 0 END END = 1) AND
        (CASE WHEN @FechaFin = '--' THEN 1 ELSE CASE WHEN OP.FechaRegistro <= @FechaFinConv THEN 1 ELSE 0 END END = 1) AND
        (CASE WHEN @CodigoSisgeco = '--' THEN 1 ELSE CASE WHEN GRD.CotizacionGrupo= @CodigoSisgeco THEN 1 ELSE 0 END END = 1) AND 
        (CASE WHEN @RucCliente = '--' THEN 1 ELSE CASE WHEN OP.RucCliente = @RucCliente THEN 1 ELSE 0 END END = 1) AND 
        (CASE WHEN @IdProyecto = '--' THEN 1 ELSE CASE WHEN OP.IdProyecto = @IdProyecto THEN 1 ELSE 0 END END = 1) AND
        (CASE WHEN @IdTipoCliente = '--' THEN 1 ELSE CASE WHEN OP.IdTipoCliente = @IdTipoCliente THEN 1 ELSE 0 END END = 1); 
    
    SET NOCOUNT OFF;
END
GO



CREATE PROC SP_ListarLineaProduccion
@Fecha VARCHAR(10)
AS
BEGIN
SELECT  
GRD.Turno,
FORMAT(GRD.FechaProduccion, 'dd MMMM', 'es-ES') AS Fechas,
count(*) AS Total
FROM TBL_DetalleOrdenProduccion DOP
JOIN Tbl_DetalleOpGrupo GRD ON DOP.CotizacionGrupo=GRD.CotizacionGrupo
WHERE GRD.Tipo='Producto'
GROUP BY GRD.Turno,FORMAT(GRD.FechaProduccion, 'dd MMMM', 'es-ES')

/*
SELECT  
GRD.Turno,
FORMAT(GRD.FechaProduccion, 'dd MMMM', 'es-ES') AS Fechas
FROM TBL_DetalleOrdenProduccion DOP
JOIN Tbl_DetalleOpGrupo GRD ON DOP.CotizacionGrupo=GRD.CotizacionGrupo
WHERE GRD.Tipo='Producto'
AND  FORMAT(GRD.FechaProduccion, 'dd MMMM', 'es-ES')='04 mayo'


SELECT  
GRD.Turno,
FORMAT(GRD.FechaProduccion, 'dd MMMM', 'es-ES') AS Fechas,
DOP.CodigoProducto,DOP.NombreProducto
FROM TBL_DetalleOrdenProduccion DOP
JOIN Tbl_DetalleOpGrupo GRD ON DOP.CotizacionGrupo=GRD.CotizacionGrupo
WHERE GRD.Tipo='Producto'
AND  FORMAT(GRD.FechaProduccion, 'dd MMMM', 'es-ES')='04 mayo' AND GRD.Turno='Tarde'


*/
END