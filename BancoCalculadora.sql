CREATE DATABASE db_calculadora
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

CREATE SCHEMA sc_calculadora
    AUTHORIZATION postgres;

CREATE TABLE sc_calculadora.tb_contas
(
    idcontas integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    valor1 bigint,
    valor2 bigint,
    operacao character varying COLLATE pg_catalog."default",
    resultado bigint,
    datetime timestamp without time zone
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE sc_calculadora.tb_contas
    OWNER to postgres;
