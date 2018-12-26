-- tablas

create table solicitud 
(
    COD_LINEA_NEGOCIO	CHAR(1),
    COD_MODAL_EST	CHAR(2),
    COD_PERIODO	CHAR(6),
    COD_TRAMITE	NUMBER(5),
    COD_UNICO	NUMBER(15) primary key,
    ESTADO	VARCHAR2(2),
    COD_ALUMNO	CHAR(9)
);

create table DETALLE_SOLICITUD
(
    COD_DETALLE	NUMBER(15),
    COD_CURSO	VARCHAR2(6),
    COD_TIPO_PRUEBA	CHAR(2),
    NUM_PRUEBA	NUMBER(5),
    SECCION	VARCHAR2(4),
    GRUPO	CHAR(2),
    ESTADO_CURSO	VARCHAR2(2),
    COD_UNICO	NUMBER(15),
    foreign key(COD_UNICO) references solicitud(COD_UNICO)
);

create or replace procedure SP_ReadSolicitudDetalle(COD_LINEA_NEGOCIO_ex IN CHAR, COD_MODAL_EST_ex IN CHAR, COD_PERIODO_ex IN CHAR, COD_TRAMITE_ex IN NUMBER, ESTADO_ex IN VARCHAR2, CUR_ObtieneSolicitudDetalle OUT SYS_REFCURSOR)
is
begin
    open CUR_ObtieneSolicitudDetalle for
        select *
        from solicitud so inner join DETALLE_SOLICITUD ds on so.COD_UNICO = ds.COD_UNICO
        where so.COD_LINEA_NEGOCIO = COD_LINEA_NEGOCIO_ex and so.COD_MODAL_EST = COD_MODAL_EST_ex and so.COD_PERIODO = COD_PERIODO_ex and so.COD_TRAMITE = COD_TRAMITE_ex and so.ESTADO = ESTADO_ex;
end;
/
