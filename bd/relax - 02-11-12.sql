---------------------------------------------
-- Export file for user SISTEMA            --
-- Created by Jesus on 27/10/2012, 1:47:20 --
---------------------------------------------

spool all.log

prompt
prompt Creating user Sistema
prompt ============================
prompt
create user SISTEMA
  IDENTIFIED BY shomies2012
  default tablespace SYSTEM
  temporary tablespace TEMP
  profile DEFAULT
  quota 5000m on system;
-- Grant/Revoke system privileges 
grant unlimited tablespace to SISTEMA with admin option;
grant create session to SISTEMA with admin option;

prompt
prompt Creating table TIPODOCUMENTO
prompt ============================
prompt
create table SISTEMA.TIPODOCUMENTO
(
  id           VARCHAR2(6) not null,
  descripcion  VARCHAR2(50),
  tipolongitud VARCHAR2(1),
  longitud     NUMBER(2),
  activo       NUMBER(1)
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column SISTEMA.TIPODOCUMENTO.id
  is 'CODIGO DEL TIPO DE DOCUMENTO';
comment on column SISTEMA.TIPODOCUMENTO.descripcion
  is 'descripcion del tipo de documento';
comment on column SISTEMA.TIPODOCUMENTO.tipolongitud
  is 'Tipo de Longitud [F=Fija,I=Indefinida]';
comment on column SISTEMA.TIPODOCUMENTO.longitud
  is 'longitud del tipo de documento';
comment on column SISTEMA.TIPODOCUMENTO.activo
  is 'esta activo';
alter table SISTEMA.TIPODOCUMENTO
  add constraint TIPODOCUMENTO_PK primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.TIPODOCUMENTO
  add constraint TIPODOCUMENTO_CK_01
  check ( activo IN(1,0));
alter table SISTEMA.TIPODOCUMENTO
  add constraint TIPODOCUMENTO_CK_02
  check ( tipoLongitud IN('F','I'));

prompt
prompt Creating table PERSONA
prompt ======================
prompt
create table SISTEMA.PERSONA
(
  id              NUMBER not null,
  nombrecompleto  VARCHAR2(300) not null,
  idtipodocumento VARCHAR2(6) not null,
  numerodocumento VARCHAR2(15) not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.PERSONA
  add constraint PK_PERSONA primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.PERSONA
  add constraint FK_PERSONA_TIPODOCUMENTO foreign key (IDTIPODOCUMENTO)
  references SISTEMA.TIPODOCUMENTO (ID);

prompt
prompt Creating table PERSONANATURAL
prompt =============================
prompt
create table SISTEMA.PERSONANATURAL
(
  nombres         VARCHAR2(100) not null,
  apellidopaterno VARCHAR2(100) not null,
  apellidomaterno VARCHAR2(100) not null,
  fechanacimiento DATE not null,
  sexo            VARCHAR2(1) not null,
  id              NUMBER not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.PERSONANATURAL
  add constraint PK_PERSONANATURAL primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.PERSONANATURAL
  add constraint FK_PERSONANATURAL_PERSONA foreign key (ID)
  references SISTEMA.PERSONA (ID);
alter table SISTEMA.PERSONANATURAL
  add constraint CK_PERSONANATURAL_01
  check ( SEXO IN('M','F'));

prompt
prompt Creating table CARGO
prompt ====================
prompt
create table SISTEMA.CARGO
(
  id          NUMBER(3) not null,
  descripcion VARCHAR2(150) not null,
  estado      NUMBER(1) not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.CARGO
  add constraint PK_CARGO primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.CARGO
  add constraint CK_CARGO
  check ( ESTADO IN(1,0));

prompt
prompt Creating table TRABAJADOR
prompt =========================
prompt
create table SISTEMA.TRABAJADOR
(
  id           NUMBER not null,
  fechaingreso DATE not null,
  fechasalida  DATE,
  estado       NUMBER(1) not null,
  idcargo      NUMBER(3)
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.TRABAJADOR
  add constraint PK_TRABAJADOR primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.TRABAJADOR
  add constraint FK_TRABAJADOR_CARGO foreign key (IDCARGO)
  references SISTEMA.CARGO (ID);
alter table SISTEMA.TRABAJADOR
  add constraint FK_TRABAJADOR_PERSONANATURAL foreign key (ID)
  references SISTEMA.PERSONANATURAL (ID);
alter table SISTEMA.TRABAJADOR
  add constraint CK_TRABAJADOR_01
  check ( ESTADO IN(1,0));

prompt
prompt Creating table USUARIO
prompt ======================
prompt
create table SISTEMA.USUARIO
(
  username     VARCHAR2(15) not null,
  clave        VARCHAR2(25) not null,
  idtrabajador NUMBER not null,
  estado       NUMBER(1) not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.USUARIO
  add constraint PK_USUARIO primary key (USERNAME)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.USUARIO
  add constraint FK_USUARIO_TRABAJADOR foreign key (IDTRABAJADOR)
  references SISTEMA.TRABAJADOR (ID);
alter table SISTEMA.USUARIO
  add constraint CK_USUARIO
  check ( ESTADO IN(1,0));

prompt
prompt Creating table AUDITORIASISTEMA
prompt ===============================
prompt
create table SISTEMA.AUDITORIASISTEMA
(
  id           NUMBER not null,
  fecha        DATE not null,
  userregistro VARCHAR2(15) not null,
  fechaproceso DATE not null,
  esaperturado NUMBER(1) not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column SISTEMA.AUDITORIASISTEMA.id
  is 'Correlativo del registro';
comment on column SISTEMA.AUDITORIASISTEMA.fecha
  is 'Fecha del Sistema';
comment on column SISTEMA.AUDITORIASISTEMA.userregistro
  is 'Usuario Registrador';
comment on column SISTEMA.AUDITORIASISTEMA.fechaproceso
  is 'Fecha y Hora del Proceso';
comment on column SISTEMA.AUDITORIASISTEMA.esaperturado
  is 'Estado de Apertura [1=Aperturado,0=Cerrado]';
create unique index SISTEMA.XN_SISTEMA_01 on SISTEMA.AUDITORIASISTEMA (FECHA)
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.AUDITORIASISTEMA
  add constraint FK_SISTEMA_USUARIO_01 foreign key (USERREGISTRO)
  references SISTEMA.USUARIO (USERNAME);
alter table SISTEMA.AUDITORIASISTEMA
  add constraint SISTEMA_CK
  check ( esaperturado IN(1,0));

prompt
prompt Creating table CATEGORIA
prompt ========================
prompt
create table SISTEMA.CATEGORIA
(
  id          NUMBER not null,
  descripcion VARCHAR2(250) not null,
  imagen      CLOB,
  estado      NUMBER(1) default 0 not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on table SISTEMA.CATEGORIA
  is 'Categoria de Producto';
alter table SISTEMA.CATEGORIA
  add constraint PK_CATEGORIA primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.CATEGORIA
  add constraint CK_CATEGORIA_01
  check ( ESTADO IN(0,1,2));

prompt
prompt Creating table CONCEPTO
prompt =======================
prompt
create table SISTEMA.CONCEPTO
(
  id          NUMBER not null,
  descripcion VARCHAR2(150) not null,
  tipo        NUMBER(1) not null,
  estado      NUMBER(1)
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column SISTEMA.CONCEPTO.id
  is 'Codigo del Concepto';
comment on column SISTEMA.CONCEPTO.descripcion
  is 'Descripcion del concepto';
comment on column SISTEMA.CONCEPTO.tipo
  is 'Tipo del concepto [1=Ingreso,2=Egreso]';
comment on column SISTEMA.CONCEPTO.estado
  is 'Estado del concepto [1=Activo,0=Inactivo]';
alter table SISTEMA.CONCEPTO
  add constraint CONCEPTO_PK primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.CONCEPTO
  add constraint CONCEPTO_CK
  check ( tipo in(1,2));
alter table SISTEMA.CONCEPTO
  add constraint CONCEPTO_CK_02
  check ( estado in(1,0));

prompt
prompt Creating table CORRELATIVOS
prompt ===========================
prompt
create table SISTEMA.CORRELATIVOS
(
  nametable   VARCHAR2(50) not null,
  correlativo NUMBER not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on table SISTEMA.CORRELATIVOS
  is 'Correlativos de Tablas';
comment on column SISTEMA.CORRELATIVOS.nametable
  is 'Nombre de la Tabla';
comment on column SISTEMA.CORRELATIVOS.correlativo
  is 'Correlativo Siguiente';
alter table SISTEMA.CORRELATIVOS
  add constraint PK_CORRELATIVOS primary key (NAMETABLE)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

prompt
prompt Creating table DETALLECIERRE
prompt ============================
prompt
create table SISTEMA.DETALLECIERRE
(
  id         NUMBER not null,
  idconcepto NUMBER not null,
  monto      NUMBER not null,
  fecha      DATE not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column SISTEMA.DETALLECIERRE.id
  is 'Identificador del registro';
comment on column SISTEMA.DETALLECIERRE.idconcepto
  is 'Codigo del Concepto';
comment on column SISTEMA.DETALLECIERRE.monto
  is 'Monto a cerrar del concepto';
comment on column SISTEMA.DETALLECIERRE.fecha
  is 'fecha del sistema a cerrar';
create unique index SISTEMA.XN_DETALLECIERRE_01 on SISTEMA.DETALLECIERRE (IDCONCEPTO, FECHA)
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.DETALLECIERRE
  add constraint PK_DETALLECIERRE primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.DETALLECIERRE
  add constraint FK_DETALLECIERRE_CONCEPTO foreign key (IDCONCEPTO)
  references SISTEMA.CONCEPTO (ID);

prompt
prompt Creating table MESA
prompt ===================
prompt
create table SISTEMA.MESA
(
  id          NUMBER not null,
  descripcion VARCHAR2(3) not null,
  estado      NUMBER(1) default 1 not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.MESA
  add constraint PK_MESA primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

prompt
prompt Creating table ORDEN
prompt ====================
prompt
create table SISTEMA.ORDEN
(
  id              NUMBER not null,
  idcliente       NUMBER not null,
  idmesa          NUMBER not null,
  idmozo          NUMBER not null,
  idusuario       VARCHAR2(15) not null,
  fechaproceso    DATE not null,
  fechasistema    DATE not null,
  tipopago        NUMBER(1) not null,
  tipocomprobante NUMBER(1) not null,
  estado          VARCHAR2(1) not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column SISTEMA.ORDEN.id
  is 'Codigo de la Orden';
comment on column SISTEMA.ORDEN.idcliente
  is 'Codigo del Cliente';
comment on column SISTEMA.ORDEN.idmesa
  is 'Codigo de la mesa';
comment on column SISTEMA.ORDEN.idmozo
  is 'Codigo del Mozo';
comment on column SISTEMA.ORDEN.idusuario
  is 'codigo de usuario del sistema';
comment on column SISTEMA.ORDEN.fechaproceso
  is 'fecha proceso de la orden';
comment on column SISTEMA.ORDEN.fechasistema
  is 'Fecha del sistema';
comment on column SISTEMA.ORDEN.tipopago
  is 'Tipo de Pago [E=Efectivo,T=Tarjeta]';
comment on column SISTEMA.ORDEN.tipocomprobante
  is 'Tipo de Comprobante [B=Boleta,F=Factura]';
comment on column SISTEMA.ORDEN.estado
  is 'Estado de la Orden [V=VIGENTE,C=CANCELADO,P=PROCESADO,A=ANULADO]';
alter table SISTEMA.ORDEN
  add constraint PK_ORDEN primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.ORDEN
  add constraint FK_ORDEN_MESA foreign key (IDMESA)
  references SISTEMA.MESA (ID);
alter table SISTEMA.ORDEN
  add constraint FK_ORDEN_PERSONA_01 foreign key (IDCLIENTE)
  references SISTEMA.PERSONA (ID);
alter table SISTEMA.ORDEN
  add constraint FK_ORDEN_PERSONA_02 foreign key (IDMOZO)
  references SISTEMA.TRABAJADOR (ID);
alter table SISTEMA.ORDEN
  add constraint FK_ORDEN_USUARIO foreign key (IDUSUARIO)
  references SISTEMA.USUARIO (USERNAME);
alter table SISTEMA.ORDEN
  add constraint CK_ORDEN_01
  check ( estado in('V','C','P','A'));
alter table SISTEMA.ORDEN
  add constraint CK_ORDEN_02
  check ( TIPOPAGO IN(1,2));
alter table SISTEMA.ORDEN
  add constraint CK_ORDEN_03
  check ( TIPOCOMPROBANTE IN(1,2));

prompt
prompt Creating table DETALLEFICHAJE
prompt =============================
prompt
create table SISTEMA.DETALLEFICHAJE
(
  idorden      NUMBER not null,
  idfichador   NUMBER not null,
  fechaproceso DATE not null,
  monto        NUMBER not null,
  estado       NUMBER(1)
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column SISTEMA.DETALLEFICHAJE.idorden
  is 'Codigo de la orden';
comment on column SISTEMA.DETALLEFICHAJE.idfichador
  is 'codigo del waiter';
comment on column SISTEMA.DETALLEFICHAJE.fechaproceso
  is 'Fecha de proceso';
comment on column SISTEMA.DETALLEFICHAJE.monto
  is 'monto comisionado';
comment on column SISTEMA.DETALLEFICHAJE.estado
  is 'Estado 1=Procesado, 2=Pagado,3=Anulado';
create index SISTEMA.XN_DETALLEFICHAJE_01 on SISTEMA.DETALLEFICHAJE (FECHAPROCESO)
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.DETALLEFICHAJE
  add constraint PK_DETALLEFICHAJE primary key (IDORDEN, IDFICHADOR)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.DETALLEFICHAJE
  add constraint FK_DETALLEFICHAJE_ORDEN foreign key (IDORDEN)
  references SISTEMA.ORDEN (ID);
alter table SISTEMA.DETALLEFICHAJE
  add constraint FK_DETALLEFICHAJE_TRABAJADOR foreign key (IDFICHADOR)
  references SISTEMA.TRABAJADOR (ID);
alter table SISTEMA.DETALLEFICHAJE
  add constraint CK_DETALLEFICHAJE_01
  check (ESTADO IN(1,2,3));

prompt
prompt Creating table UNIDAD
prompt =====================
prompt
create table SISTEMA.UNIDAD
(
  id          NUMBER not null,
  descripcion VARCHAR2(15) not null,
  abreviatura VARCHAR2(10) not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.UNIDAD
  add constraint PK_UNIDAD primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

prompt
prompt Creating table PRODUCTO
prompt =======================
prompt
create table SISTEMA.PRODUCTO
(
  id            NUMBER not null,
  descripcion   VARCHAR2(250) not null,
  precioventa   NUMBER(18,2) not null,
  estado        NUMBER(1) default 0 not null,
  idcategoria   NUMBER not null,
  stock         NUMBER(18) not null,
  stocklimitado NUMBER(1) not null,
  imagen        CLOB,
  idunidad      NUMBER not null,
  comision      NUMBER(18,2) default 0
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on table SISTEMA.PRODUCTO
  is 'Productos';
alter table SISTEMA.PRODUCTO
  add constraint PK_PRODUCTO primary key (ID)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.PRODUCTO
  add constraint FK_PRODUCTO_CATEGORIA foreign key (IDCATEGORIA)
  references SISTEMA.CATEGORIA (ID);
alter table SISTEMA.PRODUCTO
  add constraint FK_PRODUCTO_UNIDAD foreign key (IDUNIDAD)
  references SISTEMA.UNIDAD (ID);
alter table SISTEMA.PRODUCTO
  add constraint CK_PRODUCTO_01
  check ( ESTADO IN(0,1,2));
alter table SISTEMA.PRODUCTO
  add constraint CK_PRODUCTO_02
  check ( STOCKLIMITADO IN(0,1));

prompt
prompt Creating table DETALLEORDEN
prompt ===========================
prompt
create table SISTEMA.DETALLEORDEN
(
  idorden      NUMBER not null,
  idproducto   NUMBER not null,
  cantidad     NUMBER not null,
  fechaproceso DATE not null,
  precioventa  NUMBER not null,
  comision     NUMBER default 0 not null,
  total        NUMBER not null,
  estado       NUMBER default 1 not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
comment on column SISTEMA.DETALLEORDEN.idorden
  is 'codigo de la orden';
comment on column SISTEMA.DETALLEORDEN.idproducto
  is 'Codigo del Producto';
comment on column SISTEMA.DETALLEORDEN.cantidad
  is 'Cantidad Pedida del Producto';
comment on column SISTEMA.DETALLEORDEN.fechaproceso
  is 'Fecha de Proceso';
comment on column SISTEMA.DETALLEORDEN.precioventa
  is 'Precito de venta';
comment on column SISTEMA.DETALLEORDEN.comision
  is 'Comision por venta de producto';
comment on column SISTEMA.DETALLEORDEN.total
  is 'Total Cantidad x PrecioVenta';
comment on column SISTEMA.DETALLEORDEN.estado
  is '1=Procesado, 2=Anulado';
create index SISTEMA.XN_DETALLEORDEN on SISTEMA.DETALLEORDEN (FECHAPROCESO)
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.DETALLEORDEN
  add constraint PK_DETALLEORDEN primary key (IDORDEN, IDPRODUCTO)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.DETALLEORDEN
  add constraint FK_DETALLEORDEN_ORDEN foreign key (IDORDEN)
  references SISTEMA.ORDEN (ID);
alter table SISTEMA.DETALLEORDEN
  add constraint FK_DETALLEORDEN_PRODUCTO foreign key (IDPRODUCTO)
  references SISTEMA.PRODUCTO (ID);
alter table SISTEMA.DETALLEORDEN
  add constraint CK_DETALLEORDEN_01
  check (ESTADO IN(1,2));

prompt
prompt Creating table MOVIMIENTO
prompt =========================
prompt
create table SISTEMA.MOVIMIENTO
(
  nrocomprobante NUMBER not null,
  monto          NUMBER not null,
  fecha          DATE not null,
  idconcepto     NUMBER not null,
  username       VARCHAR2(15) not null,
  estado         NUMBER(1) not null
)
tablespace SYSTEM
  pctfree 10
  pctused 40
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.MOVIMIENTO
  add constraint PK_MOVIMIENTO primary key (NROCOMPROBANTE)
  using index 
  tablespace SYSTEM
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
alter table SISTEMA.MOVIMIENTO
  add constraint FK_MOVIMIENTO_CONCEPTO foreign key (IDCONCEPTO)
  references SISTEMA.CONCEPTO (ID);
alter table SISTEMA.MOVIMIENTO
  add constraint CK_ESTADO
  check ( ESTADO IN(1,0));

prompt
prompt Creating package ADMINISTRA_CATEGORIAS
prompt ======================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_categorias IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE lista_categorias_x_estado(i_estado     IN NUMBER,
                                       o_categorias OUT SYS_REFCURSOR);
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar(o_categorias OUT SYS_REFCURSOR);
   -------------------------------------------------------------------------------------------------
   PROCEDURE registrar(i_descripcion IN VARCHAR2,
                       i_estado      IN NUMBER,
                       i_imagen      IN CLOB,
                       o_id          OUT NUMBER,
                       o_codigo      OUT NUMBER,
                       o_mensaje     OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE modificar(i_id          IN NUMBER,
                       i_descripcion IN VARCHAR2,
                       i_estado      IN NUMBER,
                       i_imagen      IN CLOB,
                       o_codigo      OUT NUMBER,
                       o_mensaje     OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE buscar_x_descripcion_y_estado(i_descripcion IN VARCHAR2,
                                           i_estado      IN NUMBER,
                                           o_categorias  OUT SYS_REFCURSOR);
   -------------------------------------------------------------------------------------------------
END administra_categorias;
/

prompt
prompt Creating package ADMINISTRA_CONCEPTOS
prompt =====================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_conceptos IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar_x_estado(i_estado    IN NUMBER,
                             o_concpetos OUT SYS_REFCURSOR,
                             o_codigo    OUT NUMBER,
                             o_mensaje   OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
END administra_conceptos;
/

prompt
prompt Creating package ADMINISTRA_CORRELATIVO
prompt =======================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_correlativo IS
   -------------------------------------------------------------------------------------------------
   -- Author  : Cesar A. Lama Cruz
   -- Created : 10/05/2012 16:49:34
   -- Purpose : Administra correlativos de las tablas
   -------------------------------------------------------------------------------------------------
   FUNCTION getnroorden(tabla IN VARCHAR2) RETURN NUMBER;
   -------------------------------------------------------------------------------------------------
   PROCEDURE actualizar(tabla   IN VARCHAR2,
                        codigo  OUT NUMBER,
                        mensaje OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
END administra_correlativo;
/

prompt
prompt Creating package ADMINISTRA_DETALLEFICHAJE
prompt ==========================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_detallefichaje IS

  -------------------------------------------------------------------------------------------------
  PROCEDURE registrar(i_idorden      IN NUMBER,
                      i_idfichador   IN NUMBER,
                      i_fechaproceso IN DATE,
                      i_monto        IN NUMBER,
                      o_codigo       OUT NUMBER,
                      o_mensaje      OUT VARCHAR2);

  -------------------------------------------------------------------------------------------------
  PROCEDURE getfichajeporfichadorayfecha(i_idfichador   IN NUMBER,
                                         i_fechaproceso IN DATE,
                                         o_fichaje      OUT SYS_REFCURSOR,
                                         o_codigo       OUT NUMBER,
                                         o_mensaje      OUT VARCHAR2);

  -------------------------------------------------------------------------------------------------
  PROCEDURE getfichajeporfichadorayfechas(i_idfichador  IN NUMBER,
                                          i_fechainicio IN DATE,
                                          i_fechafin    IN DATE,
                                          o_fichaje     OUT SYS_REFCURSOR,
                                          o_codigo      OUT NUMBER,
                                          o_mensaje     OUT VARCHAR2);

  -------------------------------------------------------------------------------------------------
  PROCEDURE getfichajepororden(i_orden   IN NUMBER,
                               o_fichaje OUT SYS_REFCURSOR,
                               o_codigo  OUT NUMBER,
                               o_mensaje OUT VARCHAR2);

  ------------------------------------------------------------------------------------------------- 
  PROCEDURE anular(i_idorden IN NUMBER,
                   o_codigo  OUT NUMBER,
                   o_mensaje OUT VARCHAR2);

-------------------------------------------------------------------------------------------------
END administra_detallefichaje;
/

prompt
prompt Creating package ADMINISTRA_DETALLEORDEN
prompt ========================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_detalleorden IS

  -------------------------------------------------------------------------------------------------
  PROCEDURE registrar(i_idorden      IN NUMBER,
                      i_idproducto   IN NUMBER,
                      i_cantidad     IN NUMBER,
                      i_fechaproceso IN DATE,
                      i_precioventa  IN NUMBER,
                      i_comision     IN NUMBER,
                      i_total        IN NUMBER,
                      o_codigo       OUT NUMBER,
                      o_mensaje      OUT VARCHAR2);

  -------------------------------------------------------------------------------------------------
  PROCEDURE getdetallepororden(i_orden   IN NUMBER,
                               o_detalle OUT SYS_REFCURSOR,
                               o_codigo  OUT NUMBER,
                               o_mensaje OUT VARCHAR2);

  -------------------------------------------------------------------------------------------------   
  PROCEDURE anular(i_idorden IN NUMBER,
                   o_codigo  OUT NUMBER,
                   o_mensaje OUT VARCHAR2);

  -------------------------------------------------------------------------------------------------
END administra_detalleorden;
/

prompt
prompt Creating package ADMINISTRA_MOVIMIENTO
prompt ======================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_movimiento IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE registramovimiento(i_monto          IN NUMBER,
                                i_fechasistema   IN DATE,
                                i_concepto       IN NUMBER,
                                i_username       IN VARCHAR2,
                                o_nrocomprobante OUT NUMBER,
                                o_codigo         OUT NUMBER,
                                o_mensaje        OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE pagofichadora(i_idfichador   IN NUMBER,
                           i_fechaproceso IN DATE,
                           o_codigo       OUT NUMBER,
                           o_mensaje      OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
END administra_movimiento;
/

prompt
prompt Creating package ADMINISTRA_ORDEN
prompt =================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_orden IS

  -------------------------------------------------------------------------------------------------
  PROCEDURE getnumero(o_id      OUT NUMBER,
                      o_codigo  OUT NUMBER,
                      o_mensaje OUT VARCHAR2);

  -------------------------------------------------------------------------------------------------
  PROCEDURE registrar(i_id              IN NUMBER,
                      i_idcliente       IN NUMBER,
                      i_idmesa          IN NUMBER,
                      i_idmozo          IN NUMBER,
                      i_idusuario       IN VARCHAR2,
                      i_fechaproceso    IN DATE,
                      i_fechasistema    IN DATE,
                      i_tipopago        IN NUMBER,
                      i_tipocomprobante IN NUMBER,
                      i_estado          IN VARCHAR2,
                      o_codigo          OUT NUMBER,
                      o_mensaje         OUT VARCHAR2);

  -------------------------------------------------------------------------------------------------
  PROCEDURE getdatos(i_idorden         IN NUMBER,
                     o_idcliente       OUT NUMBER,
                     o_idmesa          OUT NUMBER,
                     o_idmozo          OUT NUMBER,
                     o_idusuario       OUT VARCHAR2,
                     o_fechaproceso    OUT DATE,
                     o_fechasistema    OUT DATE,
                     o_tipopago        OUT NUMBER,
                     o_tipocomprobante OUT NUMBER,
                     o_estado          OUT VARCHAR2,
                     o_codigo          OUT NUMBER,
                     o_mensaje         OUT VARCHAR2);

  ------------------------------------------------------------------------------------------------- 
  PROCEDURE anular(i_idorden IN NUMBER,
                   o_codigo  OUT NUMBER,
                   o_mensaje OUT VARCHAR2);

-------------------------------------------------------------------------------------------------
END administra_orden;
/

prompt
prompt Creating package ADMINISTRA_PRODUCTOS
prompt =====================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_productos IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE registrar(i_descripcion   IN VARCHAR2,
                       i_categoria     IN NUMBER,
                       i_precioventa   IN NUMBER,
                       i_comision      IN NUMBER,
                       i_stock         IN NUMBER,
                       i_unidad        IN NUMBER,
                       i_stocklimitado IN NUMBER,
                       i_estado        IN NUMBER,
                       i_imagen        IN CLOB,
                       o_id            OUT NUMBER,
                       o_codigo        OUT NUMBER,
                       o_mensaje       OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE modificar(i_id            IN NUMBER,
                       i_descripcion   IN VARCHAR2,
                       i_categoria     IN NUMBER,
                       i_precioventa   IN NUMBER,
                       i_comision      IN NUMBER,
                       i_stock         IN NUMBER,
                       i_unidad        IN NUMBER,
                       i_stocklimitado IN NUMBER,
                       i_estado        IN NUMBER,
                       i_imagen        IN CLOB,
                       o_codigo        OUT NUMBER,
                       o_mensaje       OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar(o_productos OUT SYS_REFCURSOR,
                    o_codigo    OUT NUMBER,
                    o_mensaje   OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar_por_estado(i_estado    IN NUMBER,
                               o_productos OUT SYS_REFCURSOR,
                               o_codigo    OUT NUMBER,
                               o_mensaje   OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE buscar_x_descripcion(i_descripcion IN VARCHAR2,
                                  o_productos   OUT SYS_REFCURSOR,
                                  o_codigo      OUT NUMBER,
                                  o_mensaje     OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE buscar_x_descripcion_y_estado(i_estado      IN NUMBER,
                                           i_descripcion IN VARCHAR2,
                                           o_productos   OUT SYS_REFCURSOR,
                                           o_codigo      OUT NUMBER,
                                           o_mensaje     OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar_por_categoria(i_categoria IN NUMBER,
                                  o_productos OUT SYS_REFCURSOR,
                                  o_codigo    OUT NUMBER,
                                  o_mensaje   OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
END administra_productos;
/

prompt
prompt Creating package ADMINISTRA_REPORTES
prompt ====================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_reportes IS

  -------------------------------------------------------------------------------------------------
  PROCEDURE productos_vendidos_por_dia(i_fechareporte IN DATE,
                                       o_reporte      OUT SYS_REFCURSOR,
                                       o_codigo       OUT NUMBER,
                                       o_mensaje      OUT NUMBER);

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_pago_fichaje_por_dia(i_fechareporte IN DATE,
                                         o_reporte      OUT SYS_REFCURSOR,
                                         o_codigo       OUT NUMBER,
                                         o_mensaje      OUT NUMBER);

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_detallado_fichaje(i_fechareporte IN DATE,
                                      o_reporte      OUT SYS_REFCURSOR,
                                      o_codigo       OUT NUMBER,
                                      o_mensaje      OUT NUMBER);

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_detallado_cierre(i_fechareporte IN DATE,
                                     o_reporte      OUT SYS_REFCURSOR,
                                     o_codigo       OUT NUMBER,
                                     o_mensaje      OUT NUMBER);

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_fichaje_entre_fechas(i_fechainicio IN DATE,
                                         i_fechafinal  IN DATE,
                                         o_reporte     OUT SYS_REFCURSOR,
                                         o_codigo      OUT NUMBER,
                                         o_mensaje     OUT NUMBER);

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_chica_entre_fechas(i_fechainicio IN DATE,
                                       i_fechafinal  IN DATE,
                                       i_chica       IN NUMBER,
                                       o_reporte     OUT SYS_REFCURSOR,
                                       o_codigo      OUT NUMBER,
                                       o_mensaje     OUT NUMBER);

-------------------------------------------------------------------------------------------------
END administra_reportes;
/

prompt
prompt Creating package ADMINISTRA_SISTEMA
prompt ===================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_sistema IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE getultimafechasistema(o_ultimafechasistema OUT DATE,
                                   o_estado             OUT NUMBER,
                                   o_codigo             OUT NUMBER,
                                   o_mensaje            OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE aperturar(i_fechaapertura IN DATE,
                       i_usuario       IN VARCHAR2,
                       o_codigo        OUT NUMBER,
                       o_mensaje       OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE cerrar(i_fechaapertura IN DATE,
                    i_usuario       IN VARCHAR2,
                    o_codigo        OUT NUMBER,
                    o_mensaje       OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE getmontoconcepto(i_concepto     IN NUMBER,
                              i_fechasistema IN DATE,
                              o_monto        OUT NUMBER,
                              o_codigo       OUT NUMBER,
                              o_mensaje      OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE gettotalventa(i_fechasistema IN DATE,
                           o_monto        OUT NUMBER,
                           o_codigo       OUT NUMBER,
                           o_mensaje      OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE gettotalpagofixadoras(i_fechasistema IN DATE,
                                   o_monto        OUT NUMBER,
                                   o_codigo       OUT NUMBER,
                                   o_mensaje      OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE actualizamontoconcepto(i_id      IN NUMBER,
                                    i_monto   IN NUMBER,
                                    o_codigo  OUT NUMBER,
                                    o_mensaje OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE registramontoconcepto(i_fechasistema IN DATE,
                                   i_monto        IN NUMBER,
                                   i_concepto     IN NUMBER,
                                   o_id           OUT NUMBER,
                                   o_codigo       OUT NUMBER,
                                   o_mensaje      OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
   PROCEDURE getiddetalleporconcepto(i_fechasistema IN DATE,
                                     i_concepto     IN NUMBER,
                                     o_id           OUT NUMBER,
                                     o_codigo       OUT NUMBER,
                                     o_mensaje      OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
END administra_sistema;
/

prompt
prompt Creating package ADMINISTRA_TRABAJADOR
prompt ======================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_trabajador IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE lista_por_cargo(i_cargo        IN NUMBER,
                             o_trabajadores OUT SYS_REFCURSOR,
                             o_codigo       OUT NUMBER,
                             o_mensaje      OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
END administra_trabajador;
/

prompt
prompt Creating package ADMINISTRA_UNIDADES
prompt ====================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_unidades IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar(o_unidades OUT SYS_REFCURSOR);
   -------------------------------------------------------------------------------------------------
END administra_unidades;
/

prompt
prompt Creating package ADMINISTRA_USUARIO
prompt ===================================
prompt
CREATE OR REPLACE PACKAGE SISTEMA.administra_usuario IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE getdatos(i_username     IN VARCHAR2,
                      o_clave        OUT VARCHAR2,
                      o_idtrabajador OUT NUMBER,
                      o_estado       OUT NUMBER,
                      o_codigo       OUT NUMBER,
                      o_mensaje      OUT VARCHAR2);
   -------------------------------------------------------------------------------------------------
END administra_usuario;
/

prompt
prompt Creating package body ADMINISTRA_CATEGORIAS
prompt ===========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_categorias IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE lista_categorias_x_estado(i_estado     IN NUMBER,
                                       o_categorias OUT SYS_REFCURSOR) IS
   BEGIN
      OPEN o_categorias FOR
         SELECT id,
                descripcion,
                estado,
                imagen
         FROM   sistema.categoria
         WHERE  estado = i_estado
         ORDER  BY descripcion;
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar(o_categorias OUT SYS_REFCURSOR) IS
   BEGIN
      OPEN o_categorias FOR
         SELECT id,
                descripcion,
                estado,
                imagen
         FROM   sistema.categoria
         ORDER  BY descripcion;
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE registrar(i_descripcion IN VARCHAR2,
                       i_estado      IN NUMBER,
                       i_imagen      IN CLOB,
                       o_id          OUT NUMBER,
                       o_codigo      OUT NUMBER,
                       o_mensaje     OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      o_id     := sistema.administra_correlativo.getnroorden('CATEGORIA');
      INSERT INTO sistema.categoria
         (id,
          descripcion,
          estado,
          imagen)
      VALUES
         (o_id,
          i_descripcion,
          i_estado,
          i_imagen);
      sistema.administra_correlativo.actualizar('CATEGORIA', o_codigo, o_mensaje);
   EXCEPTION
      WHEN dup_val_on_index THEN
         o_codigo  := 1;
         o_mensaje := 'Categoria ya registrada';
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error registrando categoria [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE modificar(i_id          IN NUMBER,
                       i_descripcion IN VARCHAR2,
                       i_estado      IN NUMBER,
                       i_imagen      IN CLOB,
                       o_codigo      OUT NUMBER,
                       o_mensaje     OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      UPDATE sistema.categoria
      SET    descripcion = i_descripcion,
             estado      = i_estado,
             imagen      = i_imagen
      WHERE  id = i_id;
      IF SQL%NOTFOUND
      THEN
         o_codigo  := 1;
         o_mensaje := 'No se modifico categoria';
      END IF;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error modificando categoria [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE buscar_x_descripcion_y_estado(i_descripcion IN VARCHAR2,
                                           i_estado      IN NUMBER,
                                           o_categorias  OUT SYS_REFCURSOR) IS
   BEGIN
      OPEN o_categorias FOR
         SELECT id,
                descripcion,
                estado,
                imagen
         FROM   sistema.categoria
         WHERE  upper(descripcion) LIKE upper(i_descripcion) || '%'
                AND estado = i_estado
         ORDER  BY descripcion;
   END;
   -------------------------------------------------------------------------------------------------
END administra_categorias;
/

prompt
prompt Creating package body ADMINISTRA_CONCEPTOS
prompt ==========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_conceptos IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar_x_estado(i_estado    IN NUMBER,
                             o_concpetos OUT SYS_REFCURSOR,
                             o_codigo    OUT NUMBER,
                             o_mensaje   OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      OPEN o_concpetos FOR
         SELECT id,
                descripcion,
                tipo,
                estado
         FROM   sistema.concepto c
         WHERE  c.estado = i_estado;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error al obtener concpetos [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
END administra_conceptos;
/

prompt
prompt Creating package body ADMINISTRA_CORRELATIVO
prompt ============================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_correlativo IS

   -------------------------------------------------------------------------------------------------
   FUNCTION getnroorden(tabla IN VARCHAR2) RETURN NUMBER IS
      RESULT NUMBER;
   BEGIN
      BEGIN
         SELECT correlativo INTO RESULT FROM correlativos WHERE nametable = tabla;

      EXCEPTION
         WHEN OTHERS THEN
            RESULT := NULL;
      END;
      RETURN(RESULT);
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE actualizar(tabla   IN VARCHAR2,
                        codigo  OUT NUMBER,
                        mensaje OUT VARCHAR2) IS
   BEGIN
      BEGIN
         codigo := 0;
         UPDATE correlativos SET correlativo = correlativo + 1 WHERE nametable = tabla;
         IF SQL%NOTFOUND
         THEN
            codigo  := 1;
            mensaje := 'No se actualizo correlativo';
         END IF;
      EXCEPTION
         WHEN OTHERS THEN
            codigo  := 999;
            mensaje := 'Error actualizando correlativo [' || SQLERRM || ']';
      END;
   END;
   -------------------------------------------------------------------------------------------------
END administra_correlativo;
/

prompt
prompt Creating package body ADMINISTRA_DETALLEFICHAJE
prompt ===============================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_detallefichaje IS

  -------------------------------------------------------------------------------------------------
  PROCEDURE registrar(i_idorden      IN NUMBER,
                      i_idfichador   IN NUMBER,
                      i_fechaproceso IN DATE,
                      i_monto        IN NUMBER,
                      o_codigo       OUT NUMBER,
                      o_mensaje      OUT VARCHAR2) IS
    procesado sistema.detallefichaje.estado%TYPE DEFAULT 1;
  BEGIN
    o_codigo := 0;
    INSERT INTO sistema.detallefichaje
      (idorden,
       idfichador,
       fechaproceso,
       monto,
       estado)
    VALUES
      (i_idorden,
       i_idfichador,
       i_fechaproceso,
       i_monto,
       procesado);
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al registrar fichaje [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE getfichajeporfichadorayfecha(i_idfichador   IN NUMBER,
                                         i_fechaproceso IN DATE,
                                         o_fichaje      OUT SYS_REFCURSOR,
                                         o_codigo       OUT NUMBER,
                                         o_mensaje      OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    OPEN o_fichaje FOR
      SELECT idorden, idfichador, fechaproceso, monto, estado
        FROM sistema.detallefichaje det
       WHERE det.idfichador = i_idfichador
         AND trunc(det.fechaproceso) = trunc(i_fechaproceso);
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener fichaje [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE getfichajeporfichadorayfechas(i_idfichador  IN NUMBER,
                                          i_fechainicio IN DATE,
                                          i_fechafin    IN DATE,
                                          o_fichaje     OUT SYS_REFCURSOR,
                                          o_codigo      OUT NUMBER,
                                          o_mensaje     OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    OPEN o_fichaje FOR
      SELECT det.idorden, det.idfichador, det.fechaproceso, monto, det.estado
        FROM sistema.detallefichaje det, sistema.orden ord
       WHERE ord.id=det.idorden
         AND ord.estado = 'V'
         AND  det.idfichador = i_idfichador
         AND trunc(det.fechaproceso) BETWEEN trunc(i_fechainicio) AND
             trunc(i_fechafin)
       ORDER BY det.idorden, det.fechaproceso;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener fichaje [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE getfichajepororden(i_orden   IN NUMBER,
                               o_fichaje OUT SYS_REFCURSOR,
                               o_codigo  OUT NUMBER,
                               o_mensaje OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    OPEN o_fichaje FOR
      SELECT idorden,
             idfichador,
             (SELECT per.nombrecompleto
                FROM sistema.persona per
               WHERE per.id = det.idfichador) AS nombretrabajador,
             fechaproceso,
             monto,
             estado
        FROM sistema.detallefichaje det
       WHERE det.idorden = i_orden;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener fichaje [' || SQLERRM || ']';
  END;

  ------------------------------------------------------------------------------------------------- 
  PROCEDURE anular(i_idorden IN NUMBER,
                   o_codigo  OUT NUMBER,
                   o_mensaje OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    UPDATE sistema.detallefichaje SET estado = 3 WHERE idOrden = i_idorden;
    IF SQL%NOTFOUND
    THEN
      o_codigo  := 1;
      o_mensaje := 'Número de orden no existe';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener datos de la orden [' || SQLERRM || ']';
  END;

-------------------------------------------------------------------------------------------------
END administra_detallefichaje;
/

prompt
prompt Creating package body ADMINISTRA_DETALLEORDEN
prompt =============================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_detalleorden IS

  -------------------------------------------------------------------------------------------------
  PROCEDURE registrar(i_idorden      IN NUMBER,
                      i_idproducto   IN NUMBER,
                      i_cantidad     IN NUMBER,
                      i_fechaproceso IN DATE,
                      i_precioventa  IN NUMBER,
                      i_comision     IN NUMBER,
                      i_total        IN NUMBER,
                      o_codigo       OUT NUMBER,
                      o_mensaje      OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    INSERT INTO sistema.detalleorden
      (idorden,
       idproducto,
       cantidad,
       fechaproceso,
       precioventa,
       comision,
       total)
    VALUES
      (i_idorden,
       i_idproducto,
       i_cantidad,
       i_fechaproceso,
       i_precioventa,
       i_comision,
       i_total);
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al registrar orden [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE getdetallepororden(i_orden   IN NUMBER,
                               o_detalle OUT SYS_REFCURSOR,
                               o_codigo  OUT NUMBER,
                               o_mensaje OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    OPEN o_detalle FOR
      SELECT idorden,
             idproducto,
             (SELECT pro.descripcion
                FROM sistema.producto pro
               WHERE pro.id = det.idproducto) nombreproducto,
             (SELECT pro.imagen
                FROM sistema.producto pro
               WHERE pro.id = det.idproducto) imagenproducto,
             cantidad,
             fechaproceso,
             precioventa,
             comision,
             total
        FROM sistema.detalleorden det
       WHERE det.idorden = i_orden;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener fichaje [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------   
  PROCEDURE anular(i_idorden IN NUMBER,
                   o_codigo  OUT NUMBER,
                   o_mensaje OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    UPDATE sistema.detallefichaje SET estado = 2 WHERE idOrden = i_idorden;
    IF SQL%NOTFOUND
    THEN
      o_codigo  := 1;
      o_mensaje := 'Número de orden no existe';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener datos de la orden [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
END administra_detalleorden;
/

prompt
prompt Creating package body ADMINISTRA_MOVIMIENTO
prompt ===========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_movimiento IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE registramovimiento(i_monto          IN NUMBER,
                                i_fechasistema   IN DATE,
                                i_concepto       IN NUMBER,
                                i_username       IN VARCHAR2,
                                o_nrocomprobante OUT NUMBER,
                                o_codigo         OUT NUMBER,
                                o_mensaje        OUT VARCHAR2) IS
      procesado sistema.movimiento.estado%TYPE DEFAULT 1;
   BEGIN
      o_codigo         := 0;
      o_nrocomprobante := sistema.administra_correlativo.getnroorden('MOVIMIENTO');
      IF o_nrocomprobante IS NOT NULL
      THEN
         INSERT INTO sistema.movimiento
            (nrocomprobante,
             monto,
             fecha,
             idconcepto,
             username,
             estado)
         VALUES
            (o_nrocomprobante,
             i_monto,
             i_fechasistema,
             i_concepto,
             i_username,
             procesado);
      ELSE
         o_codigo  := 999;
         o_mensaje := 'Error obteniendo correlativo del movimiento';
      END IF;
      sistema.administra_correlativo.actualizar('MOVIMIENTO', o_codigo, o_mensaje);
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error registrando movimiento [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE pagofichadora(i_idfichador   IN NUMBER,
                           i_fechaproceso IN DATE,
                           o_codigo       OUT NUMBER,
                           o_mensaje      OUT VARCHAR2) IS
      pagado sistema.detallefichaje.estado%TYPE DEFAULT 2;
   BEGIN
      o_codigo := 0;
      UPDATE sistema.detallefichaje
      SET    estado = pagado
      WHERE  idfichador = i_idfichador
             AND trunc(fechaproceso) = trunc(i_fechaproceso);
      IF SQL%NOTFOUND
      THEN
         o_codigo  := 999;
         o_mensaje := 'Error actualizando detalle del fichaje';
      END IF;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error actualizando detalle del fichaje [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
END administra_movimiento;
/

prompt
prompt Creating package body ADMINISTRA_ORDEN
prompt ======================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_orden IS

  -------------------------------------------------------------------------------------------------
  PROCEDURE getnumero(o_id      OUT NUMBER,
                      o_codigo  OUT NUMBER,
                      o_mensaje OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    o_id     := administra_correlativo.getnroorden('ORDEN');
    IF o_id IS NULL OR
       o_id = 0
    THEN
      o_codigo  := 1;
      o_mensaje := 'No se puede obtener siguiente orden';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener correlativo de la orden [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE registrar(i_id              IN NUMBER,
                      i_idcliente       IN NUMBER,
                      i_idmesa          IN NUMBER,
                      i_idmozo          IN NUMBER,
                      i_idusuario       IN VARCHAR2,
                      i_fechaproceso    IN DATE,
                      i_fechasistema    IN DATE,
                      i_tipopago        IN NUMBER,
                      i_tipocomprobante IN NUMBER,
                      i_estado          IN VARCHAR2,
                      o_codigo          OUT NUMBER,
                      o_mensaje         OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    INSERT INTO sistema.orden
      (id,
       idcliente,
       idmesa,
       idmozo,
       idusuario,
       fechaproceso,
       fechasistema,
       tipopago,
       tipocomprobante,
       estado)
    VALUES
      (i_id,
       i_idcliente,
       i_idmesa,
       i_idmozo,
       i_idusuario,
       i_fechaproceso,
       i_fechasistema,
       i_tipopago,
       i_tipocomprobante,
       i_estado);
    administra_correlativo.actualizar('ORDEN', o_codigo, o_mensaje);
  EXCEPTION
    WHEN dup_val_on_index THEN
      o_codigo  := 1;
      o_mensaje := 'Número de orden ya registrada';
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al registrar orden [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE getdatos(i_idorden         IN NUMBER,
                     o_idcliente       OUT NUMBER,
                     o_idmesa          OUT NUMBER,
                     o_idmozo          OUT NUMBER,
                     o_idusuario       OUT VARCHAR2,
                     o_fechaproceso    OUT DATE,
                     o_fechasistema    OUT DATE,
                     o_tipopago        OUT NUMBER,
                     o_tipocomprobante OUT NUMBER,
                     o_estado          OUT VARCHAR2,
                     o_codigo          OUT NUMBER,
                     o_mensaje         OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    SELECT idcliente,
           idmesa,
           idmozo,
           idusuario,
           fechaproceso,
           fechasistema,
           tipopago,
           tipocomprobante,
           estado
      INTO o_idcliente,
           o_idmesa,
           o_idmozo,
           o_idusuario,
           o_fechaproceso,
           o_fechasistema,
           o_tipopago,
           o_tipocomprobante,
           o_estado
      FROM sistema.orden o
     WHERE o.id = i_idorden;
  EXCEPTION
    WHEN no_data_found THEN
      o_codigo  := 1;
      o_mensaje := 'Número de orden no existe';
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener datos de la orden [' || SQLERRM || ']';
  END;

  ------------------------------------------------------------------------------------------------- 
  PROCEDURE anular(i_idorden IN NUMBER,
                   o_codigo  OUT NUMBER,
                   o_mensaje OUT VARCHAR2) IS
  BEGIN
    o_codigo := 0;
    UPDATE sistema.orden SET estado = 'A' WHERE id = i_idorden;
    IF SQL%NOTFOUND
    THEN
      o_codigo  := 1;
      o_mensaje := 'Número de orden no existe';
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error al obtener datos de la orden [' || SQLERRM || ']';
  END;

-------------------------------------------------------------------------------------------------
END administra_orden;
/

prompt
prompt Creating package body ADMINISTRA_PRODUCTOS
prompt ==========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_productos IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE registrar(i_descripcion   IN VARCHAR2,
                       i_categoria     IN NUMBER,
                       i_precioventa   IN NUMBER,
                       i_comision      IN NUMBER,
                       i_stock         IN NUMBER,
                       i_unidad        IN NUMBER,
                       i_stocklimitado IN NUMBER,
                       i_estado        IN NUMBER,
                       i_imagen        IN CLOB,
                       o_id            OUT NUMBER,
                       o_codigo        OUT NUMBER,
                       o_mensaje       OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      o_id     := sistema.administra_correlativo.getnroorden('PRODUCTO');
      INSERT INTO sistema.producto
         (id,
          descripcion,
          precioventa,
          comision,
          estado,
          idcategoria,
          stock,
          stocklimitado,
          imagen,
          idunidad)
      VALUES
         (o_id,
          i_descripcion,
          i_precioventa,
          i_comision,
          i_estado,
          i_categoria,
          i_stock,
          i_stocklimitado,
          i_imagen,
          i_unidad);
      sistema.administra_correlativo.actualizar('PRODUCTO', o_codigo, o_mensaje);
   EXCEPTION
      WHEN dup_val_on_index THEN
         o_codigo  := 1;
         o_mensaje := 'Producto ya registrada';
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error registrando producto [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE modificar(i_id            IN NUMBER,
                       i_descripcion   IN VARCHAR2,
                       i_categoria     IN NUMBER,
                       i_precioventa   IN NUMBER,
                       i_comision      IN NUMBER,
                       i_stock         IN NUMBER,
                       i_unidad        IN NUMBER,
                       i_stocklimitado IN NUMBER,
                       i_estado        IN NUMBER,
                       i_imagen        IN CLOB,
                       o_codigo        OUT NUMBER,
                       o_mensaje       OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      UPDATE sistema.producto
      SET    descripcion   = i_descripcion,
             precioventa   = i_precioventa,
             comision      = i_comision,
             estado        = i_estado,
             idcategoria   = i_categoria,
             stock         = i_stock,
             stocklimitado = i_stocklimitado,
             imagen        = i_imagen,
             idunidad      = i_unidad
      WHERE  id = i_id;

      IF SQL%NOTFOUND
      THEN
         o_codigo  := 1;
         o_mensaje := 'No se modifico producto';
      END IF;
   EXCEPTION
      WHEN dup_val_on_index THEN
         o_codigo  := 1;
         o_mensaje := 'Producto ya registrada';
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error registrando producto [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar(o_productos OUT SYS_REFCURSOR,
                    o_codigo    OUT NUMBER,
                    o_mensaje   OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      OPEN o_productos FOR
         SELECT id,
                descripcion,
                precioventa,
                comision,
                estado,
                idcategoria,
                (SELECT cat.descripcion FROM sistema.categoria cat WHERE cat.id = pro.idcategoria) AS descripcioncategoria,
                stock,
                stocklimitado,
                imagen,
                idunidad,
                (SELECT uni.descripcion FROM sistema.unidad uni WHERE uni.id = pro.idunidad) descripcionunidad
         FROM   sistema.producto pro
         ORDER  BY descripcioncategoria,
                   descripcion;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error listando productos [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar_por_estado(i_estado    IN NUMBER,
                               o_productos OUT SYS_REFCURSOR,
                               o_codigo    OUT NUMBER,
                               o_mensaje   OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      OPEN o_productos FOR
         SELECT id,
                descripcion,
                precioventa,
                comision,
                estado,
                idcategoria,
                (SELECT cat.descripcion FROM sistema.categoria cat WHERE cat.id = pro.idcategoria) AS descripcioncategoria,
                stock,
                stocklimitado,
                imagen,
                idunidad,
                (SELECT uni.descripcion FROM sistema.unidad uni WHERE uni.id = pro.idunidad) descripcionunidad
         FROM   sistema.producto pro
         WHERE  pro.estado = i_estado
         ORDER  BY descripcioncategoria,
                   descripcion;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error listando productos [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE buscar_x_descripcion(i_descripcion IN VARCHAR2,
                                  o_productos   OUT SYS_REFCURSOR,
                                  o_codigo      OUT NUMBER,
                                  o_mensaje     OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      OPEN o_productos FOR
         SELECT id,
                descripcion,
                precioventa,
                comision,
                estado,
                idcategoria,
                (SELECT cat.descripcion FROM sistema.categoria cat WHERE cat.id = pro.idcategoria) AS descripcioncategoria,
                stock,
                stocklimitado,
                imagen,
                idunidad,
                (SELECT uni.descripcion FROM sistema.unidad uni WHERE uni.id = pro.idunidad) descripcionunidad
         FROM   sistema.producto pro
         WHERE  upper(pro.descripcion) LIKE upper(i_descripcion) || '%'
         ORDER  BY descripcioncategoria,
                   descripcion;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error listando productos [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE buscar_x_descripcion_y_estado(i_estado      IN NUMBER,
                                           i_descripcion IN VARCHAR2,
                                           o_productos   OUT SYS_REFCURSOR,
                                           o_codigo      OUT NUMBER,
                                           o_mensaje     OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      OPEN o_productos FOR
         SELECT id,
                descripcion,
                precioventa,
                comision,
                estado,
                idcategoria,
                (SELECT cat.descripcion FROM sistema.categoria cat WHERE cat.id = pro.idcategoria) AS descripcioncategoria,
                stock,
                stocklimitado,
                imagen,
                idunidad,
                (SELECT uni.descripcion FROM sistema.unidad uni WHERE uni.id = pro.idunidad) descripcionunidad
         FROM   sistema.producto pro
         WHERE  upper(pro.descripcion) LIKE upper(i_descripcion) || '%'
                AND pro.estado = i_estado
         ORDER  BY descripcioncategoria,
                   descripcion;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error listando productos [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar_por_categoria(i_categoria IN NUMBER,
                                  o_productos OUT SYS_REFCURSOR,
                                  o_codigo    OUT NUMBER,
                                  o_mensaje   OUT VARCHAR2) IS
      estadoactivo sistema.producto.estado%TYPE DEFAULT 1;
   BEGIN
      o_codigo := 0;
      OPEN o_productos FOR
         SELECT id,
                descripcion,
                precioventa,
                comision,
                estado,
                idcategoria,
                (SELECT cat.descripcion FROM sistema.categoria cat WHERE cat.id = pro.idcategoria) AS descripcioncategoria,
                stock,
                stocklimitado,
                imagen,
                idunidad,
                (SELECT uni.descripcion FROM sistema.unidad uni WHERE uni.id = pro.idunidad) descripcionunidad
         FROM   sistema.producto pro
         WHERE  pro.estado = estadoactivo
                AND pro.idcategoria = i_categoria
         ORDER  BY descripcioncategoria,
                   descripcion;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error listando productos [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
END administra_productos;
/

prompt
prompt Creating package body ADMINISTRA_REPORTES
prompt =========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_reportes IS

  -------------------------------------------------------------------------------------------------
  PROCEDURE productos_vendidos_por_dia(i_fechareporte IN DATE,
                                       o_reporte      OUT SYS_REFCURSOR,
                                       o_codigo       OUT NUMBER,
                                       o_mensaje      OUT NUMBER) IS
  BEGIN
    o_codigo := 0;
    OPEN o_reporte FOR
      SELECT (SELECT pro.descripcion
                FROM sistema.producto pro
               WHERE pro.id = det.idproducto) producto,
             SUM(cantidad) cantidad,
             SUM(total) total
        FROM sistema.detalleorden det
       WHERE trunc(det.fechaproceso) = trunc(i_fechareporte)
       GROUP BY det.idproducto
       ORDER BY producto;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error inesperado[' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_pago_fichaje_por_dia(i_fechareporte IN DATE,
                                         o_reporte      OUT SYS_REFCURSOR,
                                         o_codigo       OUT NUMBER,
                                         o_mensaje      OUT NUMBER) IS
  BEGIN
    o_codigo := 0;
    OPEN o_reporte FOR
      SELECT (SELECT per.nombres
                FROM sistema.personanatural per
               WHERE per.id = det.idfichador) fichadora,
             SUM(det.monto) total
        FROM sistema.detallefichaje det
       WHERE trunc(det.fechaproceso) = trunc(i_fechareporte)
       GROUP BY det.idfichador
       ORDER BY fichadora;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error inesperado [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_detallado_fichaje(i_fechareporte IN DATE,
                                      o_reporte      OUT SYS_REFCURSOR,
                                      o_codigo       OUT NUMBER,
                                      o_mensaje      OUT NUMBER) IS
  BEGIN
    o_codigo := 0;
    OPEN o_reporte FOR
      SELECT (SELECT per.nombres
                FROM sistema.personanatural per
               WHERE per.id = detf.idfichador) fichadora,
             detf.monto,
             deto.idorden orden,
             (SELECT pro.descripcion
                FROM sistema.producto pro
               WHERE pro.id = deto.idproducto) nombreproducto,
             deto.precioventa,
             deto.comision
        FROM sistema.detallefichaje detf, sistema.detalleorden deto
       WHERE deto.idorden = detf.idorden
         AND trunc(deto.fechaproceso) = trunc(i_fechareporte)
         AND trunc(detf.fechaproceso) = trunc(i_fechareporte)
       ORDER BY fichadora, orden;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error inesperado [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_detallado_cierre(i_fechareporte IN DATE,
                                     o_reporte      OUT SYS_REFCURSOR,
                                     o_codigo       OUT NUMBER,
                                     o_mensaje      OUT NUMBER) IS
  BEGIN
    o_codigo := 0;
    OPEN o_reporte FOR
      SELECT (SELECT con.descripcion
                FROM sistema.concepto con
               WHERE con.id = det.idconcepto) concepto,
             decode((SELECT con.tipo
                      FROM sistema.concepto con
                     WHERE con.id = det.idconcepto),
                    2,
                    -1 * det.monto,
                    det.monto) monto
        FROM sistema.detallecierre det
       WHERE trunc(det.fecha) = trunc(i_fechareporte)
       ORDER BY concepto;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error inesperado [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_fichaje_entre_fechas(i_fechainicio IN DATE,
                                         i_fechafinal  IN DATE,
                                         o_reporte     OUT SYS_REFCURSOR,
                                         o_codigo      OUT NUMBER,
                                         o_mensaje     OUT NUMBER) IS
  BEGIN
    o_codigo := 0;
    OPEN o_reporte FOR
      SELECT (SELECT pe.nombrecompleto
                FROM sistema.persona pe
               WHERE pe.id = d.idfichador) nombrefichadora,
             d.monto AS montofichaje,
             o.idorden AS orden,
             (SELECT p.descripcion
                FROM sistema.producto p
               WHERE p.id = o.idproducto) producto,
             o.cantidad,
             o.precioventa,
             o.total,
             o.comision,
             o.cantidad * o.comision totalComision,
             o.fechaproceso
        FROM sistema.detalleorden o, sistema.detallefichaje d
       WHERE trunc(o.fechaproceso) BETWEEN i_fechainicio AND i_fechafinal
         AND d.idorden = o.idorden
       ORDER BY d.idfichador, o.idorden;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error inesperado [' || SQLERRM || ']';
  END;

  -------------------------------------------------------------------------------------------------
  PROCEDURE reporte_chica_entre_fechas(i_fechainicio IN DATE,
                                       i_fechafinal  IN DATE,
                                       i_chica       IN NUMBER,
                                       o_reporte     OUT SYS_REFCURSOR,
                                       o_codigo      OUT NUMBER,
                                       o_mensaje     OUT NUMBER) IS
  BEGIN
    o_codigo := 0;
    OPEN o_reporte FOR
      SELECT (SELECT pe.nombrecompleto
                FROM sistema.persona pe
               WHERE pe.id = d.idfichador) nombrefichadora,
             d.monto AS montofichaje,
             o.idorden AS orden,
             (SELECT p.descripcion
                FROM sistema.producto p
               WHERE p.id = o.idproducto) producto,
             o.cantidad,
             o.precioventa,
             o.total,
             o.comision,
             o.cantidad * o.comision totalComision,
             o.fechaproceso
        FROM sistema.detalleorden o, sistema.detallefichaje d
       WHERE trunc(o.fechaproceso) BETWEEN i_fechainicio AND i_fechafinal
         AND d.idorden = o.idorden
         AND d.idfichador = i_chica
       ORDER BY o.idorden;
  EXCEPTION
    WHEN OTHERS THEN
      o_codigo  := 999;
      o_mensaje := 'Error inesperado [' || SQLERRM || ']';
  END;

-------------------------------------------------------------------------------------------------
END administra_reportes;
/

prompt
prompt Creating package body ADMINISTRA_SISTEMA
prompt ========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_sistema IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE getultimafechasistema(o_ultimafechasistema OUT DATE,
                                   o_estado             OUT NUMBER,
                                   o_codigo             OUT NUMBER,
                                   o_mensaje            OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      SELECT sis.fecha,
             sis.esaperturado
      INTO   o_ultimafechasistema,
             o_estado
      FROM   sistema.auditoriasistema sis
      WHERE  sis.fecha = (SELECT MAX(sys.fecha) FROM sistema.auditoriasistema sys);
   EXCEPTION
      WHEN no_data_found THEN
         o_codigo  := 1;
         o_mensaje := 'Sistema no aperturado';
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error obteniendo fecha del sistema [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE aperturar(i_fechaapertura IN DATE,
                       i_usuario       IN VARCHAR2,
                       o_codigo        OUT NUMBER,
                       o_mensaje       OUT VARCHAR2) IS
      correlativo sistema.auditoriasistema.id%TYPE;
   BEGIN
      o_codigo    := 0;
      correlativo := administra_correlativo.getnroorden('SISTEMA');
      IF correlativo IS NOT NULL
      THEN
         INSERT INTO sistema.auditoriasistema
            (id,
             fecha,
             userregistro,
             fechaproceso,
             esaperturado)
         VALUES
            (correlativo,
             i_fechaapertura,
             i_usuario,
             SYSDATE,
             1);
      ELSE
         o_codigo  := 999;
         o_mensaje := 'Error al obtener correlativo del sistema';
      END IF;
      administra_correlativo.actualizar('SISTEMA', o_codigo, o_mensaje);
   EXCEPTION
      WHEN dup_val_on_index THEN
         o_codigo  := 1;
         o_mensaje := 'Sistema ya aperturado a la fecha [' ||
                      to_char(i_fechaapertura, 'dd/MM/yyyy') || ']';
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error aperturando fecha del sistema [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE cerrar(i_fechaapertura IN DATE,
                    i_usuario       IN VARCHAR2,
                    o_codigo        OUT NUMBER,
                    o_mensaje       OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;

      UPDATE sistema.auditoriasistema
      SET    esaperturado = 0,
             userregistro = i_usuario
      WHERE  trunc(fecha) = trunc(i_fechaapertura);
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error aperturando fecha del sistema [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE getmontoconcepto(i_concepto     IN NUMBER,
                              i_fechasistema IN DATE,
                              o_monto        OUT NUMBER,
                              o_codigo       OUT NUMBER,
                              o_mensaje      OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      o_monto  := 0;
      SELECT det.monto
      INTO   o_monto
      FROM   sistema.detallecierre det
      WHERE  det.idconcepto = i_concepto
             AND trunc(det.fecha) = trunc(i_fechasistema);
   EXCEPTION
      WHEN no_data_found THEN
         o_monto := 0;
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error al obtener monto del concepto [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE gettotalventa(i_fechasistema IN DATE,
                           o_monto        OUT NUMBER,
                           o_codigo       OUT NUMBER,
                           o_mensaje      OUT VARCHAR2) IS
      estadoprocesado sistema.orden.estado%TYPE DEFAULT 'P';
   BEGIN
      o_codigo := 0;
      o_monto  := 0;
      SELECT nvl(SUM(det.total), 0)
      INTO   o_monto
      FROM   sistema.detalleorden det
      WHERE  trunc(det.fechaproceso) = trunc(i_fechasistema)
             AND EXISTS (SELECT *
              FROM   sistema.orden ord
              WHERE  ord.estado = estadoprocesado
                     AND ord.id = det.idorden);

   EXCEPTION
      WHEN no_data_found THEN
         o_monto := 0;
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error al obtener monto total de la venta [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE gettotalpagofixadoras(i_fechasistema IN DATE,
                                   o_monto        OUT NUMBER,
                                   o_codigo       OUT NUMBER,
                                   o_mensaje      OUT VARCHAR2) IS
      estadoprocesado sistema.orden.estado%TYPE DEFAULT 'P';
   BEGIN
      o_codigo := 0;
      o_monto  := 0;

      SELECT nvl(SUM(det.monto), 0)
      INTO   o_monto
      FROM   sistema.detallefichaje det
      WHERE  trunc(det.fechaproceso) = trunc(i_fechasistema)
             AND EXISTS (SELECT 1
              FROM   sistema.orden ord
              WHERE  ord.estado = estadoprocesado
                     AND ord.id = det.idorden);

   EXCEPTION
      WHEN no_data_found THEN
         o_monto := 0;
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error al obtener monto total de la venta [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE actualizamontoconcepto(i_id      IN NUMBER,
                                    i_monto   IN NUMBER,
                                    o_codigo  OUT NUMBER,
                                    o_mensaje OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      UPDATE sistema.detallecierre SET monto = i_monto WHERE id = i_id;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error al obtener monto total de la venta [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE registramontoconcepto(i_fechasistema IN DATE,
                                   i_monto        IN NUMBER,
                                   i_concepto     IN NUMBER,
                                   o_id           OUT NUMBER,
                                   o_codigo       OUT NUMBER,
                                   o_mensaje      OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      o_id     := sistema.administra_correlativo.getnroorden('DETALLECIERRE');
      IF o_id IS NOT NULL
      THEN
         INSERT INTO sistema.detallecierre
            (id,
             idconcepto,
             monto,
             fecha)
         VALUES
            (o_id,
             i_concepto,
             i_monto,
             i_fechasistema);
      ELSE
         o_codigo  := 999;
         o_mensaje := 'Error al obtener correlativo de detalle del cierre';
      END IF;
      sistema.administra_correlativo.actualizar('DETALLECIERRE', o_codigo, o_mensaje);
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error registrando concepto [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
   PROCEDURE getiddetalleporconcepto(i_fechasistema IN DATE,
                                     i_concepto     IN NUMBER,
                                     o_id           OUT NUMBER,
                                     o_codigo       OUT NUMBER,
                                     o_mensaje      OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      SELECT det.id
      INTO   o_id
      FROM   sistema.detallecierre det
      WHERE  det.idconcepto = i_concepto
             AND trunc(det.fecha) = trunc(i_fechasistema);
   EXCEPTION
      WHEN no_data_found THEN
         o_id := 0;
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error al obtener id del detalle [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
END administra_sistema;
/

prompt
prompt Creating package body ADMINISTRA_TRABAJADOR
prompt ===========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_trabajador IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE lista_por_cargo(i_cargo        IN NUMBER,
                             o_trabajadores OUT SYS_REFCURSOR,
                             o_codigo       OUT NUMBER,
                             o_mensaje      OUT VARCHAR2) IS
      activo sistema.trabajador.estado%TYPE DEFAULT 1;
   BEGIN
      o_codigo := 0;
      OPEN o_trabajadores FOR
         SELECT tra.id,
                per.nombres,
                per.apellidopaterno,
                per.apellidomaterno,
                per.fechanacimiento,
                per.sexo,
                fechaingreso,
                decode(fechasalida, NULL, fechaingreso) fechasalida,
                estado,
                idcargo,
                (SELECT car.descripcion FROM sistema.cargo car WHERE car.id = tra.idcargo) AS descripcioncargo
         FROM   sistema.trabajador     tra,
                sistema.personanatural per
         WHERE  per.id = tra.id
                AND tra.idcargo = i_cargo
                AND tra.estado = activo;
   EXCEPTION
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error obteniendo trabajadores [' || SQLERRM || ']';
   END;
   -------------------------------------------------------------------------------------------------
END administra_trabajador;
/

prompt
prompt Creating package body ADMINISTRA_UNIDADES
prompt =========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_unidades IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE listar(o_unidades OUT SYS_REFCURSOR) IS
   BEGIN
      OPEN o_unidades FOR
         SELECT id,
                descripcion,
                abreviatura
         FROM   sistema.unidad;
   END;
   -------------------------------------------------------------------------------------------------
END administra_unidades;
/

prompt
prompt Creating package body ADMINISTRA_USUARIO
prompt ========================================
prompt
CREATE OR REPLACE PACKAGE BODY SISTEMA.administra_usuario IS
   -------------------------------------------------------------------------------------------------
   PROCEDURE getdatos(i_username     IN VARCHAR2,
                      o_clave        OUT VARCHAR2,
                      o_idtrabajador OUT NUMBER,
                      o_estado       OUT NUMBER,
                      o_codigo       OUT NUMBER,
                      o_mensaje      OUT VARCHAR2) IS
   BEGIN
      o_codigo := 0;
      SELECT clave,
             idtrabajador,
             estado
      INTO   o_clave,
             o_idtrabajador,
             o_estado
      FROM   sistema.usuario usu
      WHERE  usu.username = i_username;
   EXCEPTION
      WHEN no_data_found THEN
         o_codigo  := 1;
         o_mensaje := 'Codigo de usuario no existe';
      WHEN OTHERS THEN
         o_codigo  := 999;
         o_mensaje := 'Error obteniendo datos de usuario';
   END;
   -------------------------------------------------------------------------------------------------
END administra_usuario;
/
prompt
prompt  Insert Data Inicial
prompt ============================
prompt

--CORRELATIVOS
insert into sistema.correlativos (NAMETABLE, CORRELATIVO)
values ('CATEGORIA', 1);
insert into sistema.correlativos (NAMETABLE, CORRELATIVO)
values ('PRODUCTO', 1);
insert into sistema.correlativos (NAMETABLE, CORRELATIVO)
values ('ORDEN', 1);
insert into sistema.correlativos (NAMETABLE, CORRELATIVO)
values ('SISTEMA', 1);
insert into sistema.correlativos (NAMETABLE, CORRELATIVO)
values ('CONCEPTO', 8);
insert into sistema.correlativos (NAMETABLE, CORRELATIVO)
values ('DETALLECIERRE', 1);
insert into sistema.correlativos (NAMETABLE, CORRELATIVO)
values ('MOVIMIENTO', 1);
  
--CARGOS
insert into sistema.cargo (ID, DESCRIPCION, ESTADO)
values (1, 'DEVELOPER', 1);
insert into sistema.cargo (ID, DESCRIPCION, ESTADO)
values (2, 'WAITER', 1);
insert into sistema.cargo (ID, DESCRIPCION, ESTADO)
values (3, 'DAMA', 1);
insert into sistema.cargo (ID, DESCRIPCION, ESTADO)
values (4, 'CAJERO', 1);  

--UNIDADES
insert into sistema.unidad (ID, DESCRIPCION, ABREVIATURA)
values (1, 'Unidad', 'Unidad');
insert into sistema.unidad (ID, DESCRIPCION, ABREVIATURA)
values (2, 'Kilogramo', 'Kg.');
insert into sistema.unidad (ID, DESCRIPCION, ABREVIATURA)
values (3, 'Paquete', 'Paquete');

--CONCEPTOS
insert into sistema.concepto (ID, DESCRIPCION, TIPO, ESTADO)
values (5, 'APERTURA DE CAJA', 1, 1);
insert into sistema.concepto (ID, DESCRIPCION, TIPO, ESTADO)
values (6, 'GASTOS ADMINISTRATIVOS', 2, 1);
insert into sistema.concepto (ID, DESCRIPCION, TIPO, ESTADO)
values (7, 'PAGO FIXADORAS', 2, 1);
insert into sistema.concepto (ID, DESCRIPCION, TIPO, ESTADO)
values (1, 'TOTAL VENTAS', 1, 1);
insert into sistema.concepto (ID, DESCRIPCION, TIPO, ESTADO)
values (2, 'PAGO CON TARJETA', 2, 1);
insert into sistema.concepto (ID, DESCRIPCION, TIPO, ESTADO)
values (3, 'PAGO EFECTIVO', 1, 0);
insert into sistema.concepto (ID, DESCRIPCION, TIPO, ESTADO)
values (4, 'ENTRADAS', 1, 1);

--MESA
insert into sistema.mesa (ID, DESCRIPCION, ESTADO)
values (999, 'XXX', 1);

--TIPO DOCUMENTO
insert into sistema.tipodocumento (ID, DESCRIPCION, TIPOLONGITUD, LONGITUD, ACTIVO)
values ('SN', 'SIN DOCUMENTO', 'F', 0, 1);
insert into sistema.tipodocumento (ID, DESCRIPCION, TIPOLONGITUD, LONGITUD, ACTIVO)
values ('DNI', 'DOCUMENTO NACIONAL DE IDENTIDAD', 'F', 8, 1);

--PERSONA
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (1, 'LAMA CRUZ CESAR', 'DNI', '43839339');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (2, 'CAJERO', 'DNI', '00000001');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (3, 'MOZO', 'DNI', '00000002');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (4, 'XIMENA', 'DNI', '00000004');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (5, 'CLIENTE', 'DNI', '99999999');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (6, 'VALENTINA', 'DNI', '00000006');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (7, 'ESTRELLA', 'DNI', '00000007');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (8, 'KAROLINE', 'DNI', '00000008');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (9, 'DAYANA', 'DNI', '00000009');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (10, 'NATALY', 'DNI', '00000010');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (11, 'YAQUI', 'DNI', '00000011');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (12, 'SILVANA', 'DNI', '00000012');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (13, 'BRENDA', 'DNI', '000000013');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (14, 'LUCIANA', 'DNI', '00000014');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (15, 'MICHAEL', 'DNI', '00000015');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (16, 'KARINA', 'DNI', '00000016');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (17, 'ANALY', 'DNI', '00000017');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (18, 'NICOL', 'DNI', '00000018');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (19, 'BARBARA', 'DNI', '00000019');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (20, 'KAREN', 'DNI', '00000020');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (21, 'JEYMI', 'DNI', '00000021');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (22, 'RENATA', 'DNI', '00000022');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (23, 'PIERINA', 'DNI', '00000023');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (24, 'TAMARA', 'DNI', '00000024');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (25, 'SOLOME', 'DNI', '00000025');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (26, 'FIORELA', 'DNI', '00000026');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (27, 'YUVITZA', 'DNI', '00000027');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (28, 'DEBORA', 'DNI', '00000028');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (29, 'AMBAR', 'DNI', '00000029');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (30, 'SHARON', 'DNI', '00000030');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (31, 'CAMILA', 'DNI', '00000031');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (32, 'CATTY', 'DNI', '00000032');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (33, 'XIOMARA', 'DNI', '00000033');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (34, 'CATTY', 'DNI', '00000034');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (35, 'CATTY', 'DNI', '00000035');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (36, 'CATTY', 'DNI', '00000036');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (37, 'CATTY', 'DNI', '00000037');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (38, 'CATTY', 'DNI', '00000038');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (39, 'CATTY', 'DNI', '00000039');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (40, 'CATTY', 'DNI', '00000040');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (41, 'CATTY', 'DNI', '00000041');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (42, 'CATTY', 'DNI', '00000042');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (43, 'DIANA', 'DNI', '00000043');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (44, 'PAOLA', 'DNI', '00000044');
insert into SISTEMA.PERSONA (ID, NOMBRECOMPLETO, IDTIPODOCUMENTO, NUMERODOCUMENTO)
values (45, 'ZARELY', 'DNI', '00000045');

--PERSONA NATURAL
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('CESAR', 'LAMA', 'CRUZ', to_date('08-11-1986', 'dd-mm-yyyy'), 'M', 1);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('CAJERO', 'CAJERO', 'CAJERO', to_date('01-01-2012', 'dd-mm-yyyy'), 'F', 2);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('MOZO', 'MOZO', 'MOZO', to_date('01-01-2012', 'dd-mm-yyyy'), 'M', 3);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('XIMENA', 'XIMENA', 'XIMENA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 4);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('VALENTINA', 'VALENTINA', 'VALENTINA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 6);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('ESTRELLA', 'ESTRELLA', 'ESTRELLA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 7);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('KAROLINE', 'KAROLINE', 'KAROLINE', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 8);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('DAYANA', 'DAYANA', 'DAYANA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 9);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('NATALY', 'NATALY', 'NATALY', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 10);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('YAQUI', 'YAQUI', 'YAQUI', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 11);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('SILVANA', 'SILVANA', 'SILVANA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 12);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('BRENDA', 'BRENDA', 'BRENDA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 13);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('LUCIANA', 'LUCIANA', 'LUCIANA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 14);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('MICHAEL', 'MICHAEL', 'MICHAEL', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 15);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('KARINA', 'KARINA', 'KARINA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 16);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('ANALY', 'ANALY', 'ANALY', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 17);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('NICOL', 'NICOL', 'NICOL', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 18);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('BARBARA', 'BARBARA', 'BARBARA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 19);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('KAREN', 'KAREN', 'KAREN', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 20);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('JEYMI', 'JEYMI', 'JEYMI', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 21);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('RENATA', 'RENATA', 'RENATA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 22);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('PIERINA', 'PIERINA', 'PIERINA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 23);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('TAMARA', 'TAMARA', 'TAMARA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 24);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('SOLOME', 'SOLOME', 'SOLOME', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 25);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('FIORELA', 'FIORELA', 'FIORELA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 26);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('YUVITZA', 'YUVITZA', 'YUVITZA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 27);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('DEBORA', 'DEBORA', 'DEBORA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 28);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('AMBAR', 'AMBAR', 'AMBAR', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 29);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('SHARON', 'SHARON', 'SHARON', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 30);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('CAMILA', 'CAMILA', 'CAMILA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 31);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('CATTY', 'CATTY', 'CATTY', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 32);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('XIOMARA', 'XIOMARA', 'XIOMARA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 33);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('VALERIA', 'VALERIA', 'VALERIA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 34);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('ALEXANDRA', 'ALEXANDRA', 'ALEXANDRA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 35);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('CIELO', 'CIELO', 'CIELO', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 36);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('JESSICKA', 'JESSICKA', 'JESSICKA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 37);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('MARIEL', 'MARIEL', 'MARIEL', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 38);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('ANGIE', 'ANGIE', 'ANGIE', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 39);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('MIA', 'MIA', 'MIA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 40);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('YULIANA', 'YULIANA', 'YULIANA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 41);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('PERLA', 'PERLA', 'PERLA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 42);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('DIANA', 'DIANA', 'DIANA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 43);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('PAOLA', 'PAOLA', 'PAOLA', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 44);
insert into SISTEMA.PERSONANATURAL (NOMBRES, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, ID)
values ('ZARELY', 'ZARELY', 'ZARELY', to_date('01-11-2012', 'dd-mm-yyyy'), 'F', 45);

--TRABAJADOR
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (1, to_date('01-09-2012', 'dd-mm-yyyy'), null, 1, 1);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (2, to_date('01-09-2012', 'dd-mm-yyyy'), null, 1, 4);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (3, to_date('01-09-2012', 'dd-mm-yyyy'), null, 1, 2);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (4, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (6, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (7, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (8, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (9, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (10, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (11, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (12, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (13, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (14, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (15, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (16, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (17, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (18, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (19, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (20, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (21, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (22, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (23, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (24, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (25, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (26, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (27, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (28, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (29, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (30, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (31, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (32, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (33, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (34, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (35, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (36, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (37, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (38, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (39, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (40, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (41, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (42, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (43, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (44, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);
insert into SISTEMA.TRABAJADOR (ID, FECHAINGRESO, FECHASALIDA, ESTADO, IDCARGO)
values (45, to_date('01-11-2012', 'dd-mm-yyyy'), null, 1, 3);

--USUARIO
insert into sistema.usuario (USERNAME, CLAVE, IDTRABAJADOR, ESTADO)
values ('CAJERO', '123456', 2, 1);

--CATEGORIAS
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (1, 'BOTELLAS', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (2, 'JARRAS', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (3, 'VASOS', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (4, 'COPAS', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (5, 'MISCELANEOS', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (6, 'SALIDAS', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (7, 'ZONA VIP', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (8, 'BAILES', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (9, 'AVERIAS', 1);
insert into sistema.categoria (ID, DESCRIPCION, ESTADO)
values (10, 'ENTRADAS', 1);

--PRODUCTOS
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (1, 'AGUA', 10.00, 1, 1, 0, 0, 1, 5.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (2, 'CERVEZA CHICA', 10.00, 1, 1, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (3, 'MALTA CUSQUEÑA', 20.00, 1, 1, 0, 0, 1, 10.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (4, 'ETIQUETA NEGRA', 350.00, 1, 1, 0, 0, 1, 120.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (5, 'ETIQUETA ROJA', 300.00, 1, 1, 0, 0, 1, 120.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (6, 'TEQUILA ', 300.00, 1, 1, 0, 0, 1, 120.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (7, 'RON', 300.00, 1, 1, 0, 0, 1, 120.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (8, 'GASEOSA CHICA', 10.00, 1, 1, 0, 0, 1, 5.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (9, 'GASEOSA GRANDE', 20.00, 1, 1, 0, 0, 1, 10.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (10, 'VODKA', 300.00, 1, 1, 0, 0, 1, 120.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (11, 'BEILY', 25.00, 1, 4, 0, 0, 1, 12.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (12, 'RELAX', 30.00, 1, 4, 0, 0, 1, 20.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (13, 'TRAGO', 20.00, 1, 4, 0, 0, 1, 10.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (14, 'PIÑA COLADA', 25.00, 1, 4, 0, 0, 1, 12.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (15, 'CERVEZA', 40.00, 1, 2, 0, 0, 1, 20.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (16, 'SANGRIA', 50.00, 1, 2, 0, 0, 1, 30.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (17, 'VODKA', 50.00, 1, 2, 0, 0, 1, 25.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (18, 'CUBA LIBRE', 50.00, 1, 2, 0, 0, 1, 25.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (19, 'RED BULL', 25.00, 1, 5, 0, 0, 1, 12.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (20, 'CIGARROS', 10.00, 1, 5, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (21, 'DERECHO A CORCHO', 80.00, 1, 6, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (22, 'COVER 80', 80.00, 1, 6, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (23, 'COVER 100', 100.00, 1, 6, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (24, 'VODKA', 25.00, 1, 3, 0, 0, 1, 12.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (25, 'CUBA LIBRE', 20.00, 1, 3, 0, 0, 1, 10.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (26, 'WISKY', 25.00, 1, 3, 0, 0, 1, 12.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (27, 'TEQUILA', 25.00, 1, 3, 0, 0, 1, 12.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (28, 'CHILI', 10.00, 1, 3, 0, 0, 1, 5.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (29, 'VIP 60', 60.00, 1, 7, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (30, 'VIP 80', 80.00, 1, 7, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (31, 'VIP 100', 100.00, 1, 7, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (32, 'DERECHO BAILE', 20.00, 1, 8, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (33, 'VASO ROTO', 5.00, 1, 9, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (34, 'JARRA ROTA', 10.00, 1, 9, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (35, 'PORTERO', 10.00, 1, 10, 0, 0, 1, 0.00);
insert into sistema.producto (ID, DESCRIPCION, PRECIOVENTA, ESTADO, IDCATEGORIA, STOCK, STOCKLIMITADO, IDUNIDAD, COMISION)
values (36, 'VISA', 10.00, 1, 10, 0, 0, 1, 0.00);

spool off
